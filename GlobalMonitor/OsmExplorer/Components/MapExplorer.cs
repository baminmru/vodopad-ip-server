//
// OsmExplorer - a C# application and class 
// library for exploring OpenStreetMap data
// Copyright (C) 2012, Ryan Conrad
//
// Rendering based on a project by Ciaran Gultnieks: http://projects.ciarang.com/p/csmapcontrol
// Powered by the Volante embedded database engine by Krzysztof Kowalczyk: http://blog.kowalczyk.info/software/volante/database.html
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>. 

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using OsmExplorer.Collections;
using OsmExplorer.Data;
using OsmExplorer.Exceptions;
using OsmExplorer.Rendering;
using OsmExplorer.Routing;
using OsmExplorer.Spatial;
using OsmExplorer.Units;

namespace OsmExplorer.Components
{
    /// <summary>
    /// Map Explorer
    /// </summary>
    public partial class MapExplorer : UserControl
    {
        #region Private members
        private const int pixels = 256;

        private int m_ZoomLevel = 25;
        private RouteStop m_HighlightedLatLon;
        private LatLon m_CursorPosition;
        private bool m_UpdateView = false;
        private bool m_MouseMoving = false;
        private bool m_MouseLeft = false;
        private int m_MousePositionX;
        private int m_MousePositionY;
        private double m_LonScale;
        #region Zoom Increments
        private static readonly double[] m_ZoomScaleIncrements = {
			0.00005/pixels,
			0.0005/pixels,
			0.0008/pixels,			
			0.002/pixels,
			0.002/pixels,
			0.003/pixels,
			0.004/pixels,
			0.005/pixels,
			0.006/pixels,
			0.007/pixels,
			0.008/pixels,		
			0.009/pixels,
			0.01/pixels,
			0.015/pixels,
			0.02/pixels,
			0.025/pixels,
			0.03/pixels,
			0.035/pixels,
			0.04/pixels,
			0.045/pixels,
			0.05/pixels,
			0.055/pixels,
			0.06/pixels,
			0.065/pixels,
			0.07/pixels,
			0.075/pixels,
			0.08/pixels,
			0.085/pixels,
			0.09/pixels,
			0.095/pixels,
			0.1/pixels,
			0.2/pixels,
			0.3/pixels,
			0.4/pixels,
			0.5/pixels,
			0.5/pixels,
			0.6/pixels,
			0.7/pixels,
			0.8/pixels,
			0.9/pixels,
			1.0/pixels,
			2.0/pixels,
			3.0/pixels,
			4.0/pixels,
			5.0/pixels,
			6.0/pixels,
			7.0/pixels,
			8.0/pixels,
			9.0/pixels,
			10.0/pixels,
			15.0/pixels,
			20.0/pixels,
			30.0/pixels,
			40.0/pixels,
			50.0/pixels,
			60.0/pixels,
			70.0/pixels,
			80.0/pixels,
			90.0/pixels,
			100.0/pixels,
			110.0/pixels,
			130.0/pixels,
			150.0/pixels,
			170.0/pixels,
			190.0/pixels,
			210.0/pixels,
			240.0/pixels,
			1};
        #endregion
        private MapGeometry m_Geometry;
        private TileManager m_Manager;

        private static double Clip(double value, double minimum, double maximum)
        {
            if (value < minimum)
                return minimum;
            if (value > maximum)
                return maximum;
            return value;
        }
        private void CalcGeometry()
        {
            if (this.Width == m_Geometry.Width &&
                this.Height == m_Geometry.Height &&
                this.CenterPoint.Equals(m_Geometry.CenterPoint) &&
                this.LonPerPixel == m_Geometry.LonPerPixel)
                return;

            m_Geometry.Width = this.Width;
            m_Geometry.Height = this.Height;
            m_Geometry.CenterPoint = this.CenterPoint;
            m_Geometry.LonPerPixel = this.LonPerPixel;

            int tilesnumwanted = (int)(360.0 / (256 * m_LonScale));
            m_Geometry.Zoom = (int)Clip(Math.Floor((Math.Log(tilesnumwanted) / Math.Log(2))), 0, 18);

            GetTileXYFromLatLon(m_Geometry.CenterPoint, out m_Geometry.CentreTileNumX, out m_Geometry.CentreTileNumY);
            LatLon Point1 = GetLatLonFromTileXY(m_Geometry.CentreTileNumX, m_Geometry.CentreTileNumY);
            m_Geometry.CentreTileLonT = Point1.Lon;

            LatLon Point2 = GetLatLonFromTileXY(m_Geometry.CentreTileNumX + 1, m_Geometry.CentreTileNumY + 1);
            m_Geometry.CentreTileLonB = Point2.Lon;

            double LonPerTile = 360.0 / (Math.Pow(2, m_Geometry.Zoom));
            double LonPerTilePixel = LonPerTile / 256D;
            double LatPerTile = Math.Atan(Math.Sinh(Math.PI * (1 - (double)m_Geometry.CentreTileNumY / Math.Pow(2, m_Geometry.Zoom - 1)))) * 180 / Math.PI;
            double LatPerTilePixel = LatPerTile / 256D;

            m_Geometry.ViewTileSizePixels = (int)(256D * LonPerTilePixel / m_LonScale);
            double m_ViewTileSizePixels_Y = 16D * LatPerTilePixel;
            double m_ViewTileSizePixels_X = 16D * LonPerTilePixel / m_LonScale;

            int size = (int)(m_ViewTileSizePixels_X * m_ViewTileSizePixels_Y);

            int ytile = (int)(m_Geometry.Height / 2 - ((1 - (Math.Log(Math.Tan((m_Geometry.CenterPoint.Lat - Point1.Lat) / (Point2.Lat - Point1.Lat)) + 1 / Math.Cos((m_Geometry.CenterPoint.Lat - Point1.Lat) / (Point2.Lat - Point1.Lat))) / Math.PI)) / 2 * Math.Pow(2, m_Geometry.Zoom)));
            double y1 = ((1 - (Math.Log(Math.Tan(m_Geometry.CenterPoint.Lat - Point1.Lat) + 1 / Math.Cos(m_Geometry.CenterPoint.Lat - Point1.Lat)) / Math.PI)) / 2 * Math.Pow(2, m_Geometry.Zoom));
            double y2 = ((1 - (Math.Log(Math.Tan(Point2.Lat - Point1.Lat) + 1 / Math.Cos(Point2.Lat - Point1.Lat))) / Math.PI) / 2 * Math.Pow(2, m_Geometry.Zoom));
            int blah = (int)(m_Geometry.Height / 2 - ((double)m_Geometry.ViewTileSizePixels * y1 / y2));

            m_Geometry.ViewYTileYPos = (int)(m_Geometry.Height / 2 - ((double)m_Geometry.ViewTileSizePixels * ((m_Geometry.CenterPoint.Lat - Point1.Lat) / (Point2.Lat - Point1.Lat))));
        }
        private void GetTileXYFromLatLon(LatLon location, out int x, out int y)
        {
            x = (int)Math.Floor(((location.Lon + 180.0) / 360.0 * Math.Pow(2.0, m_Geometry.Zoom)));
            y = (int)Math.Floor(((1.0 - Math.Log(Math.Tan(location.Lat * Math.PI / 180.0) +
               1.0 / Math.Cos(location.Lat * Math.PI / 180.0)) / Math.PI) / 2.0 * Math.Pow(2.0, m_Geometry.Zoom)));
        }
        private LatLon GetLatLonFromTileXY(int x, int y)
        {
            double lon = ((x / Math.Pow(2.0, m_Geometry.Zoom) * 360.0) - 180.0);

            double n = Math.PI - ((2.0 * Math.PI * y) / Math.Pow(2.0, m_Geometry.Zoom));
            double lat = (180.0 / Math.PI * Math.Atan(Math.Sinh(n)));

            return new LatLon(lat, lon);
        }
        private LatLon PointToLatLon(Point location)
        {
            CalcGeometry();

            if (m_Geometry.ViewTileSizePixels == 0)
                return new LatLon();

            double lon = m_Geometry.CenterPoint.Lon + m_LonScale * (location.X - m_Geometry.Width / 2);
            //pixelY = (0.5 – log((1 + sinLatitude) / (1 – sinLatitude)) / (4 * pi)) * 256 * 2 level
            //double offset = (0.5 - Math.Log((1 + Math.Sin(0)) / (1 - Math.Sin(0))) / (4 * Math.PI)) * 256D * Math.Pow(2, m_Geometry.Zoom);
            
            double offset = (double)(location.Y - m_Geometry.ViewYTileYPos) / m_Geometry.ViewTileSizePixels;
            double ty = m_Geometry.CentreTileNumY + offset;

            double n = Math.PI - ((2.0 * Math.PI * ty) / Math.Pow(2.0, m_Geometry.Zoom));
            double lat = (180.0 / Math.PI * Math.Atan(Math.Sinh(n)));
            return new LatLon(lat, lon);
        }
        
        private void DrawTiles(PaintEventArgs e)
        {
            int drawx, drawy;
            drawx = (int)(m_Geometry.Width / 2 - ((double)m_Geometry.ViewTileSizePixels * ((m_Geometry.CenterPoint.Lon - m_Geometry.CentreTileLonT) / (m_Geometry.CentreTileLonB - m_Geometry.CentreTileLonT))));
            drawy = m_Geometry.ViewYTileYPos;

            double LonPerTile = 360.0 / (Math.Pow(2, m_Geometry.Zoom));
            double LonPerTilePixel = LonPerTile / 256D;
            
            int X_Increment = (int)(360.0 / (Math.Pow(2, m_Geometry.Zoom) * m_LonScale));
            int Y_Increment = (int)(360.0 / (Math.Pow(2, m_Geometry.Zoom) * m_LonScale));

            int x = m_Geometry.CentreTileNumX;
            int y = m_Geometry.CentreTileNumY;
            double tileLat = m_Geometry.CenterPoint.Lat;
            double tileLon = m_Geometry.CenterPoint.Lon;

            while (drawx >= 0)
            {
                drawx -= X_Increment;
                x--;
            }
            while (drawy >= 0)
            {
                drawy -= Y_Increment;
                y--;
            }
            int curdrawx;
            int curx;
            while (drawy < m_Geometry.Height)
            {
                curdrawx = drawx;
                curx = x;
                while (curdrawx < m_Geometry.Width)
                {
                    LatLon pt1 = GetLatLonFromTileXY(curx, y);
                    LatLon pt2 = GetLatLonFromTileXY(curx + 1, y + 1);
                    try
                    {
                        System.Drawing.Image img = m_Manager.GetTile(curx, y, m_Geometry.Zoom);
                        if (img != null)
                            e.Graphics.DrawImage(img, curdrawx, drawy,
                                X_Increment + 1, Y_Increment + 1);
                    }
                    catch
                    {
                    }
                    curdrawx += X_Increment;
                    curx++;
                }
                LatLon ll = PointToLatLon(new Point(X_Increment + 1, Y_Increment));
                PointF p = LatLonToPoint(ll);
                drawy += (int)p.Y;
                y++;
            }
        }
        private void RaiseViewChanged(object sender, MouseEventArgs e)
        {
            if (ViewChanged != null)
            {
                ViewChanged(this, new ViewChangedEventArgs(CenterPoint, LonPerPixel));
            }
        }
        private void RaiseMouseChanged(object sender, MouseEventArgs e)
        {
            if (MouseMoved != null)
            {
                MouseMoved(this, new MouseMoveEventArgs(m_CursorPosition, e));
            }
        }
        private void UpdateView()
        {
            m_UpdateView = true;
        }
        private void NewTileDataAvailable()
        {
            if (!this.IsHandleCreated)
                return;
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate { UpdateView(); }));
                return;
            }
            UpdateView();
        }
        private void Timer1Tick(object sender, EventArgs e)
        {
            if (m_UpdateView)
            {
                Invalidate();
                m_UpdateView = false;
            }
        }

        private void MapExplorer_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            try
            {
                //    DataRepository.LoadRepository();
                //    BoundingBox bb = DataRepository.WrappingBox;

                //    double Lat = (bb.y_max + bb.y_min) / 2;
                //    double Lon = (bb.x_max + bb.x_min) / 2;
                //    this.CenterPoint = new LatLon(Lat, Lon);
                //}
                //catch (DatabaseException ex)
                //{
                //MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.CenterPoint = new LatLon(59.9300, 30.2900);
            }
            catch (DatabaseException ex)
            {
            }
            finally 
            {
                m_Manager = new TileManager(TileType.Osm );
                m_Manager.NewDataAvailable += NewTileDataAvailable;
                if (DataRepository.DatabaseOpen)
                    renderCollection.Add(DataRepository.WrappingBox);
            }
        }
        private void MapExplorer_MouseDown(object sender, MouseEventArgs e)
        {
            if (!m_MouseLeft) 
            {
                m_MouseMoving = true;
                m_MousePositionX = e.X;
                m_MousePositionY = e.Y;
            }
        }
        private void MapExplorer_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseMoving = false;
        }
        private void MapExplorer_MouseLeave(object sender, EventArgs e)
        {
            m_MouseMoving = false;
            m_MouseLeft = true;
            Invalidate();
        }
        private void MapExplorer_MouseEnter(object sender, EventArgs e) 
        {
            m_MouseLeft = false;
        }
        private void MapExplorer_MouseMove(object sender, MouseEventArgs e)
        {
            m_CursorPosition = PointToLatLon(e.Location);
            RaiseMouseChanged(this, e);
            if (m_MouseMoving && !m_MouseLeft)
            {
                int dy = e.Y - m_MousePositionY;
                int dx = e.X - m_MousePositionX;
                if (dx != 0 || dy != 0)
                {
                    
                    //double dLat1 = (2 * Math.Atan(Math.Exp((double)e.Y)) - Math.PI / 2) -
                    //    (2 * Math.Atan(Math.Exp((double)m_MousePositionY)) - Math.PI / 2);
                    //Clip(1 - (((double)e.Y) / Math.Pow(2, m_ZoomLevel)), -1, 1); // Limit value we pass to sinh

                    int dZoom = (int)((double)m_ZoomLevel / (double)m_ZoomScaleIncrements.Count() * 18D);
                    double n1 = 1 - ((2D * (double)e.Y) / Math.Pow(2, m_ZoomLevel)); 
                    double dLat1 = Math.Atan(Math.Sinh(Math.PI * n1)) * 180.0 / Math.PI;
                    double n2 = 1 - ((2D * ((double)e.Y - (double)m_MousePositionY)) / Math.Pow(2, m_ZoomLevel));
                    double dLat2 = Math.Atan(Math.Sinh(Math.PI * n2)) * 180.0 / Math.PI;
                    double dl = dLat2 - dLat1;

                    double dlat = ((2 * Math.Atan(Math.Exp((double)e.Y)) - Math.PI / 2) -
                        (2 * Math.Atan(Math.Exp((double)m_MousePositionY)) - Math.PI / 2)) * 180 / Math.PI;

                    double dLat = LonPerPixel* (double)dy;
                    double dLon = LonPerPixel * (double)dx;
                    double dLon1 = dx / Math.Pow(2, dZoom) * 360.0;

                    double Latitude = CenterPoint.Lat + dLat;
                    double Longitude = CenterPoint.Lon - dLon;

                    CenterPoint = new LatLon(Latitude, Longitude);
                    RaiseViewChanged(this, e);
                }
                m_MousePositionX = e.X;
                m_MousePositionY = e.Y;
            }
            Invalidate();
        }
        private void MapExplorer_MouseWheel(object sender, MouseEventArgs e) 
        {
            int i = -e.Delta / 120;
            this.m_ZoomLevel = i > 0 ? Math.Min(this.m_ZoomLevel + i, 68) : Math.Max(this.m_ZoomLevel + i, 0);
            if (this.m_ZoomLevel < 68 && this.m_ZoomLevel > 0) 
            {
                LonPerPixel = ZoomScales[this.m_ZoomLevel];
            }
                
            this.Invalidate();
        }

        private class MapGeometry 
        {
            public LatLon CenterPoint;
            public int Width;
            public int Height;
            public double TopLat;
            public double BottomLat;

            public double LonPerPixel;

            public int Zoom;
            public int CentreTileNumX;
            public int CentreTileNumY;
            public int ViewYTileYPos;
            public int ViewTileSizePixels;
            public double CentreTileLonT;
            public double CentreTileLonB;
        }
        #endregion
        #region Internal
        internal LatLon CenterPoint;
        public  LatLon MapCenterPoint
         {
            get 
            {
                return CenterPoint;
            }
            set 
            {
                CenterPoint = value;
                CalcGeometry();
            }
        }


        internal double LonPerPixel 
        {
            get 
            {
                return m_LonScale;
            }
            set 
            {
                m_LonScale = value;
                Invalidate();
            }
        }
        internal PointF LatLonToPoint(LatLon point)
        {
            CalcGeometry();
            int x = m_Geometry.Width / 2 + (int)((point.Lon - m_Geometry.CenterPoint.Lon) / m_LonScale);
            double ty = ((1.0 - Math.Log(Math.Tan(point.Lat * Math.PI / 180.0) +
                1.0 / Math.Cos(point.Lat * Math.PI / 180.0)) / Math.PI) / 2.0 * Math.Pow(2.0, m_Geometry.Zoom));
            int y = m_Geometry.ViewYTileYPos + (int)((ty - m_Geometry.CentreTileNumY) * m_Geometry.ViewTileSizePixels);
            return new Point(x, y);
        }
        #endregion

        /// <summary>
        /// Creates a new MapExplorer instance for tile and spatial rendering.
        /// </summary>
        public MapExplorer()
        {
            if (!DesignMode)
            {
                InitializeComponent();
                renderCollection = new RenderCollection(this);
                m_Geometry = new MapGeometry();
                LonPerPixel = m_ZoomScaleIncrements[25];
            }
        }

        /// <summary>
        /// Gets or sets the RenderCollection associated with the MapExplorer
        /// component.
        /// </summary>
        public RenderCollection renderCollection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TileManager Manager 
        {
            get 
            {
                return m_Manager;
            }
            set 
            {
                m_Manager = value;
            }
        }
        /// <summary>
        /// A predetermined list of discrete scales that can be
        /// used to provide a linear zoom range for a scrollbar.
        /// </summary>
        public double[] ZoomScales 
        {
            get 
            {
                return m_ZoomScaleIncrements;
            }
        }
        /// <summary>
        /// Set the zoom level of the MapExplorer window.
        /// </summary>
        public int ZoomLevel 
        {
            get 
            {
                return m_ZoomLevel;
            }
            set 
            {
                if (m_ZoomLevel == m_ZoomScaleIncrements.Count() - 1)
                    return;
                if (m_ZoomLevel == 0)
                    return;
                m_ZoomLevel = value;
                LonPerPixel = m_ZoomScaleIncrements[value];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public RouteStop HighlightedCoordinate 
        {
            get 
            {
                return m_HighlightedLatLon;
            }
            set 
            {
                m_HighlightedLatLon = value;
            }
        }

        /// <summary>
        /// Event raised when the view parameters are changed such as 
        /// when the map window is dragged by the mouse.
        /// </summary>
        public event ViewChangedEventHandler ViewChanged;
        /// <summary>
        /// Event raised when the mouse is moved in the map window.
        /// </summary>
        public event MouseMoveEventHandler MouseMoved;

        /// <summary>
        /// Raises the System.Windows.Forms.Control.Paint event.
        /// </summary>
        /// <param name="e">A System.Windows.Forms.PaintEventArgs that contains the event data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode) 
            {
                try
                {

                    base.OnPaint(e);
                    DrawTiles(e);
                    base.OnPaint(e);
                }catch{
                }
                var heap = new PriorityHeap<Length, RouteStop>();

                foreach (var ll in this.renderCollection) 
                {
                    if (ll is RouteStop) 
                    {
                        heap.Push(m_CursorPosition.PDistanceTo((ll as RouteStop).Location), ll as RouteStop);
                    }
                }
                if (!heap.IsEmpty)
                {
                    var closest = heap.Pop();
                    var pt = LatLonToPoint(closest.Location);
                    var cursorPt = LatLonToPoint(m_CursorPosition);
                    RectangleF rect1 ;
                    if (closest.Image == null)
                    {
                        rect1 = new RectangleF(pt, new Size(5, 5));
                    }
                    else
                    {
                        rect1 = new RectangleF(pt, closest.Image.Size);
                    }
                    var rect2 = new RectangleF(cursorPt, new Size(5, 5));

                    if (rect1.IntersectsWith(rect2))
                    {
                        Pen pen;
                        pen = new Pen(Color.Blue, 3);
                        if (closest.HiliteImage != null)
                        {
                            e.Graphics.DrawImage(closest.HiliteImage, pt.X - closest.HiliteImage.Size.Width / 2, pt.Y - closest.HiliteImage.Size.Height / 2);
                        }
                        else
                        {
                            e.Graphics.DrawEllipse(pen, pt.X - 6, pt.Y - 6, 13, 13);
                        }
                        if (closest.Text != "")
                        {
                            Brush brush = new SolidBrush(Color.Blue);

                            if (closest.HiliteImage == null)
                            {
                                e.Graphics.DrawString(closest.Text, this.Font, brush, pt.X + 9, pt.Y - 15);
                            }
                            else
                            {
                                e.Graphics.DrawString(closest.Text, this.Font, brush, pt.X + closest.HiliteImage.Size.Width / 2 + 2, pt.Y +2); 
                            }
                           
                            brush.Dispose(); 
                        }
                        pen.Dispose();
                       
                        m_HighlightedLatLon = closest;
                    }
                    else
                        m_HighlightedLatLon = null;
                }
            }
        }
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                if (m_Manager != null)
                    m_Manager.NewDataAvailable += NewTileDataAvailable;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Class that manages map image tile downloading and caching.
        /// Maintains an internal limit on tile cache size to conserve memory.
        /// </summary>
        public class TileManager
        {
            #region Private
            private const int m_DefaultMaxTileCache = 75;
            private readonly string m_UrlString;
            private readonly int m_MaxTileCache;

            private ConcurrentDictionary<TileKey, Bitmap>[] m_Tiles;
            private PriorityHeap<double, TileKey>[] m_SortedTiles;
            private TileKey m_DownloadTile = null;
            private TileKeyComparer m_KeyComparer;
            private int m_X;
            private int m_Y;
            private int m_RequestedZoom;
            private WebClient m_Client;

            private void Download(object obj)
            {
                try
                {
                    
                    string[] vals = m_DownloadTile.TileString.Split(',');
                    string url = m_UrlString + vals[0] + "/" + vals[1] + "/" + vals[2] + ".png";
                   
                    Bitmap tile=null;
                    try
                    {

                        tile = (Bitmap)Image.FromFile("c:\\mapcash\\" + CurrentTileType.ToString()+"_"  + vals[0] + "_" + vals[1] + "_" + vals[2] + ".png", true);
                    }
                    catch
                    {
                        Stream stream = m_Client.OpenRead(url);
                        tile = new Bitmap(stream);
                        stream.Flush();
                        stream.Close();
                        DirectoryInfo di = new DirectoryInfo("c:\\mapcash\\");
                        if (!di.Exists)
                        {
                            di.Create(); 
                        }
                            
                        tile.Save("c:\\mapcash\\" + CurrentTileType.ToString() + "_" + vals[0] + "_" + vals[1] + "_" + vals[2] + ".png");
                    }
                    

                    Bitmap ExistingTile;
                    if (m_Tiles[m_RequestedZoom].TryGetValue(m_DownloadTile, out ExistingTile))
                    {
                        ExistingTile = tile;
                    }
                    else
                    {

                        m_Tiles[m_RequestedZoom].TryAdd(m_DownloadTile, tile);
                        m_SortedTiles[m_RequestedZoom].Push(m_Distance(m_DownloadTile), m_DownloadTile);
                        if (m_SortedTiles[m_RequestedZoom].Count > m_MaxTileCache)
                        {
                            //If reached the tile cache limit, remove the furthest tile
                            TileKey key;
                            lock (m_SortedTiles) { key = m_SortedTiles[m_RequestedZoom].Pop(); }
                            Bitmap disposed;
                            m_Tiles[m_RequestedZoom].TryRemove(key, out disposed);
                        }
                    }
                    if (NewDataAvailable != null)
                        NewDataAvailable();
                }
                catch (Exception)
                {
                    m_Tiles[m_RequestedZoom][m_DownloadTile] = null;
                    if (NewDataAvailable != null)
                        NewDataAvailable();
                }
                finally
                {
                    m_DownloadTile = null;
                }
            }
            private double m_Distance(TileKey key)
            {
                double i = Math.Sqrt(Math.Pow(key.X - m_X, 2) + Math.Pow(key.Y - m_Y, 20));
                return i;
            }

            private class TileKey
            {
                private int m_X;
                private int m_Y;
                private int m_Zoom;

                public TileKey(int x, int y, int zoom)
                {
                    m_X = x;
                    m_Y = y;
                    m_Zoom = zoom;
                }

                public int X
                {
                    get
                    {
                        return m_X;
                    }
                }
                public int Y
                {
                    get
                    {
                        return m_Y;
                    }
                }
                public int Zoom
                {
                    get
                    {
                        return m_Zoom;
                    }
                }
                public string TileString
                {
                    get
                    {
                        return m_Zoom.ToString() + ',' + m_X.ToString() + ',' + m_Y.ToString();
                    }
                }

                public override bool Equals(object obj)
                {
                    TileKey other = obj as TileKey;
                    if (other != null)
                    {
                        if (this.X != other.X)
                            return false;
                        if (this.Y != other.Y)
                            return false;
                        if (this.Zoom != other.Zoom)
                            return false;
                        return true;
                    }
                    return false;

                }
                public override int GetHashCode()
                {
                    return m_X.GetHashCode() ^ m_Y.GetHashCode() ^ m_Zoom.GetHashCode();
                }
            }
            private struct TileKeyComparer : IComparer<double>
            {
                public int Compare(double x, double y)
                {
                    if (x > y)
                        return -1;
                    if (x < y)
                        return 1;
                    return 0;
                }
            }
            #endregion
            #region Internal
            internal delegate void NewDataAvailableHandler();
            internal event NewDataAvailableHandler NewDataAvailable;
            #endregion

            /// <summary>
            /// Creates a new TileManager instance with the specified TileType
            /// (OSM or MapQuest) and the default cache size (150 tiles). See
            /// remarks.
            /// </summary>
            /// <param name="type">Type of tiles to download.</param>
            /// <remarks>TileManager maintains a cache of tiles and automatically
            /// removes the tiles furthest from the current map view when the 
            /// cache exceeds the cache size limit. </remarks>
            public TileManager(TileType type)
            {
                switch (type)
                {
                    case TileType.Osm:
                        m_UrlString = "http://tile.openstreetmap.org/";
                        break;
                    case TileType.MapQuest:
                        m_UrlString = "http://otile1.mqcdn.com/tiles/1.0.0/osm/";
                        break;
                }
                CurrentTileType = type;
                this.m_MaxTileCache = m_DefaultMaxTileCache;
                this.m_KeyComparer = new TileKeyComparer();
                this.m_Tiles = new ConcurrentDictionary<TileKey, Bitmap>[19];
                this.m_SortedTiles = new PriorityHeap<double, TileKey>[19];
                m_Client = new WebClient();
                for (int i = 0; i < 19; i++)
                {
                    m_Tiles[i] = new ConcurrentDictionary<TileKey, Bitmap>(10, 150);
                    m_SortedTiles[i] = new PriorityHeap<double, TileKey>(m_KeyComparer);
                }
            }


            private TileType CurrentTileType;
            /// <summary>
            /// Creates a new TileManager instance with the specified TileType
            /// (OSM or MapQuest) and the specified tile cache size (recommended is at least 50).
            /// See remarks.
            /// </summary>
            /// <param name="type">Type of tiles to download.</param>
            /// <param name="MaxTileCache">Maximum number of cached tiles to store.</param>
            /// <exception cref="System.ArgumentOutOfRangeException">MaxTileCache is set to 0.</exception>
            /// <remarks>TileManager maintains a cache of tiles and automatically
            /// removes the tiles furthest from the current map view when the 
            /// cache exceeds the cache size limit. </remarks>
            public TileManager(TileType type, int MaxTileCache)
            {
                if (MaxTileCache <= 0)
                    throw new ArgumentOutOfRangeException("Tile cache size must be greater than 0.");
                CurrentTileType = type;
                switch (type)
                {
                    case TileType.Osm:
                        m_UrlString = "http://tile.openstreetmap.org/";
                        break;
                    case TileType.MapQuest:
                        m_UrlString = "http://otile1.mqcdn.com/tiles/1.0.0/osm/";
                        break;
                }
                this.m_MaxTileCache = MaxTileCache;
                this.m_KeyComparer = new TileKeyComparer();
                this.m_Tiles = new ConcurrentDictionary<TileKey, Bitmap>[19];
                this.m_SortedTiles = new PriorityHeap<double, TileKey>[19];
                m_Client = new WebClient();
                for (int i = 0; i < 19; i++)
                {
                    m_Tiles[i] = new ConcurrentDictionary<TileKey, Bitmap>(10, MaxTileCache);
                    m_SortedTiles[i] = new PriorityHeap<double, TileKey>(m_KeyComparer);
                }
            }

            /// <summary>
            /// Gets an Image tile for the specified x-y coordinates (transformed from
            /// LatLon values) and zoom level.
            /// </summary>
            /// <param name="x">x-coordinate</param>
            /// <param name="y">y-coordinate</param>
            /// <param name="zoom">Zoom level</param>
            /// <returns>Bitmap image</returns>
            public Image GetTile(int x, int y, int zoom)
            {
                m_Y = y;
                m_X = x;
                m_RequestedZoom = zoom;
                TileKey key = new TileKey(x, y, zoom);
                Bitmap CurrentTile;
                m_SortedTiles[zoom] = new PriorityHeap<double, TileKey>(m_KeyComparer);
                lock (m_SortedTiles)
                {
                    foreach (var kvp in m_Tiles[zoom])
                    {
                        m_SortedTiles[zoom].Push(m_Distance(kvp.Key), kvp.Key);
                    }
                }
                if (m_Tiles[zoom].TryGetValue(key, out CurrentTile))
                    return CurrentTile;

                if (m_DownloadTile == null)
                {
                    m_DownloadTile = key;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Download));
                }
                return null;
            }
            /// <summary>
            /// Empties the tile cache of all downloaded map tiles.
            /// </summary>
            public void EmptyCache()
            {
                for (int i = 0; i < 19; i++)
                {
                    foreach (var key in m_Tiles[i].Keys)
                    {
                        if (m_Tiles[i][key] != null)
                            m_Tiles[i][key].Dispose();
                    }
                    m_Tiles[i] = new ConcurrentDictionary<TileKey, Bitmap>();
                }

            }
        }
    }
}
