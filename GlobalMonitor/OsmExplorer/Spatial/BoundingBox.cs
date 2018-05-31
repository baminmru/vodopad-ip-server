using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using OsmExplorer.Rendering;
using Volante;
using System.Drawing;

namespace OsmExplorer.Spatial
{
    /// <summary>
    /// A Rectangular box that can be defined around a set of points
    /// or a single point with provided dimensions
    /// </summary>
    [Serializable]
    public class BoundingBox : Persistent, ISerializable, IRenderable
    {
        #region Private
        private double m_ymax;
        private double m_ymin;
        private double m_xmax;
        private double m_xmin;
        [NonSerialized()]
        private RectangleR2 m_Rectangle;
        #endregion
        #region Internal
        internal BoundingBox(SerializationInfo info, StreamingContext context)
        {
            this.m_ymax = info.GetDouble("y_max");
            this.m_ymin = info.GetDouble("y_min");
            this.m_xmax = info.GetDouble("x_max");
            this.m_xmin = info.GetDouble("x_min");
            this.m_Rectangle = new RectangleR2(this.m_ymax, this.m_xmin, this.m_ymin, this.m_xmax);
        }
        internal BoundingBox() { }
        #endregion

        /// <summary>
        /// Constructs a BoundingBox that contains all the specified LatLons
        /// </summary>
        /// <param name="points">One or more LatLon points</param>
        public BoundingBox(params LatLon[] points)
        {
            var SortedLats = new SortedSet<double>();
            var SortedLons = new SortedSet<double>();

            foreach (LatLon ll in points)
            {
                SortedLats.Add(ll.Lat);
                SortedLons.Add(ll.Lon);
            }

            var MaxLat = SortedLats.Max;
            var MinLat = SortedLats.Min;
            var MaxLon = SortedLons.Max;
            var MinLon = SortedLons.Min;

            if (MaxLat == MinLat)
            {
                MinLat -= 0.000005;
                MaxLat += 0.000005;
            }

            if (MaxLon == MinLon)
            {
                MinLon -= 0.000005;
                MaxLon += 0.000005;
            }

            this.m_ymax = MaxLat;
            this.m_ymin = MinLat;
            this.m_xmax = MaxLon;
            this.m_xmin = MinLon;
            this.m_Rectangle = new RectangleR2(MaxLat, MinLon, MinLat, MaxLon);
        }
        /// <summary>
        /// Constructs a square BoundingBox centered on the specified point 
        /// with dimensions (in degrees) specified by the size parameter
        /// </summary>
        /// <param name="CenterPoint">LatLon that the BoundingBox is centered over</param>
        /// <param name="size">Size in degrees of the edges of the BoundingBox</param>
        public BoundingBox(LatLon CenterPoint, double size)
        {
            this.m_ymax = CenterPoint.Lat + 0.5 * size;
            this.m_ymin = CenterPoint.Lat - 0.5 * size;
            this.m_xmax = CenterPoint.Lon + 0.5 * size;
            this.m_xmin = CenterPoint.Lon - 0.5 * size;
            this.m_Rectangle = new RectangleR2(this.m_ymax, this.m_xmin, this.m_ymin, this.m_xmax);
        }
        /// <summary>
        /// Constructs a square BoundingBox centered on the specified point 
        /// with dimensions (in degrees) specified by the height and width parameters.
        /// </summary>
        /// <param name="CenterPoint">LatLon that the BoundingBox is centered over.</param>
        /// <param name="height">Height in degrees of latitude of the BoundingBox.</param>
        /// <param name="width">Width in degress of longitude of the BoundingBox.</param>
        public BoundingBox(LatLon CenterPoint, double height, double width)
        {
            this.m_ymax = CenterPoint.Lat + 0.5 * height;
            this.m_ymin = CenterPoint.Lat - 0.5 * height;
            this.m_xmax = CenterPoint.Lon + 0.5 * width;
            this.m_xmin = CenterPoint.Lon - 0.5 * width;
            this.m_Rectangle = new RectangleR2(this.m_ymax, this.m_xmin, this.m_ymin, this.m_xmax);
        }
        /// <summary>
        /// Creates a BoundingBox from a Volante.RectangleR2
        /// </summary>
        /// <param name="rect">Volante RectangleR2</param>
        public BoundingBox(RectangleR2 rect)
        {
            m_ymax = rect.Top;
            m_ymin = rect.Bottom;
            m_xmax = rect.Right;
            m_xmin = rect.Left;
            m_Rectangle = rect;
        }

        /// <summary>
        /// Returns whether the BoundingBox contains the specified point
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <returns>Returns true if the point lies on an edge or within the BoundingBox</returns>
        public bool Contains(LatLon point)
        {
            if (point.Lat > y_max)
                return false;
            if (point.Lat < y_min)
                return false;
            if (point.Lon > x_max)
                return false;
            if (point.Lon < x_min)
                return false;
            return true;
        }

        /// <summary>
        /// Returns the top edge of the BoundingBox
        /// </summary>
        public double y_max
        {
            get
            {
                return m_ymax;
            }
        }
        /// <summary>
        /// Returns the bottom edge of the BoundingBox
        /// </summary>
        public double y_min
        {
            get
            {
                return m_ymin;
            }
        }
        /// <summary>
        /// Returns the right edge of the BoundingBox
        /// </summary>
        public double x_max
        {
            get
            {
                return m_xmax;
            }
        }
        /// <summary>
        /// Returns the left edge of the BoundingBox
        /// </summary>
        public double x_min
        {
            get
            {
                return m_xmin;
            }
        }
        /// <summary>
        /// Returns a representative Volante.RectangleR2 of this BoundingBox
        /// </summary>
        public RectangleR2 Rectangle
        {
            get
            {
                return m_Rectangle;
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Pen for rendering this
        /// BoundingBox.
        /// </summary>
        public Pen RenderPen { get; set; }
        /// <summary>
        /// Renders the BoundingBox using the specified graphics instance and
        /// an associated RenderCollection.
        /// </summary>
        /// <param name="graphics">A System.Drawing.Graphics object.</param>
        /// <param name="collection">An associated RenderCollection.</param>
        public virtual void Render(Graphics graphics, RenderCollection collection)
        {
            if (this.RenderPen == null)
                this.RenderPen = new Pen(Color.Black, 5F);

            LatLon P1 = new LatLon(m_ymin, m_xmin);
            LatLon P2 = new LatLon(m_ymax, m_xmin);
            LatLon P3 = new LatLon(m_ymax, m_xmax);
            LatLon P4 = new LatLon(m_ymin, m_xmax);
            LatLon[] Points = new LatLon[] { P1, P2, P3, P4, P1 };

            for (int i = 1; i < Points.Count(); i++)
                graphics.DrawLine(this.RenderPen, collection.LatLonToPoint(Points[i - 1]), collection.LatLonToPoint(Points[i]));
        }
        
        /// <summary>
        /// Populates a System.Runtime.Serialization.SerializationInfo with the data
        /// needed to serialize a BoundingBox object
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to 
        /// populate with data</param>
        /// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) 
        /// for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("y_max", this.y_max);
            info.AddValue("y_min", this.y_min);
            info.AddValue("x_max", this.x_max);
            info.AddValue("x_min", this.x_min);
        }
        /// <summary>
        /// Indicates if this BoundingBox is equal to the specified object instance.
        /// </summary>
        /// <param name="obj">Specified object instance.</param>
        /// <returns>Returns false if the specified object is not a BoundingBox or
        /// and/or has dimensions different from the current BoundingBox instance.
        /// Otherwise, returns true.</returns>
        public override bool Equals(object obj)
        {
            BoundingBox other = obj as BoundingBox;
            if (other != null)
                return this.y_max == other.y_max
                    && this.y_min == other.y_min
                    && this.x_max == other.x_max
                    && this.x_min == other.x_min;
            return false;
        }
        /// <summary>
        /// Returns a hashcode for a BoundingBox instance.
        /// </summary>
        /// <returns>Hashcode for this instance.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
