using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using OsmExplorer.Data;
using OsmExplorer.Data.Internal;
using OsmExplorer.Rendering;
using OsmExplorer.Routing;
using OsmExplorer.Routing.Internal;
using Volante;

namespace OsmExplorer.Spatial
{
    /// <summary>
    /// Represents a portion of an OpenStreetMap Way between two decision 
    /// points (e.g. intersections, junctions, etc.). RoadLinks are thus (generally) 
    /// suitable for all routing applications.
    /// </summary>
    public class RoadLink : Persistent, IRenderable, IEquatable<RoadLink>
    {
        #region Private Members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ulong m_LinkId;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<CellId> m_FirstPointCellIds;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<CellId> m_LastPointCellIds;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<IPersistent> m_Items;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<PersistentString> m_Names;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<PersistentLatLon> m_Points;
        private RectangleR2 GetRectangle(LatLon[] points) 
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
            return new RectangleR2(MaxLat, MinLon, MinLat, MaxLon);
        }

        #endregion
        #region Internal
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IPArray<CellId> FirstPointCellIds
        {
            get
            {
                return m_FirstPointCellIds;
            }
            set
            {
                if (m_FirstPointCellIds == value)
                    return;
                m_FirstPointCellIds = value;
                Modify();
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IPArray<CellId> LastPointCellIds
        {
            get
            {
                return m_LastPointCellIds;
            }
            set
            {
                if (m_LastPointCellIds == value)
                    return;
                m_LastPointCellIds = value;
                Modify();
            }
        }

        internal RoadLink() { }
        internal RoadLink(ulong linkId, 
            IDatabase Db,
            IPArray<PersistentLatLon> points,
            IPArray<IPersistent> items,
            IPArray<PersistentString> names) 
        {
            FirstPointCellIds = Db.CreateArray<CellId>();
            LastPointCellIds = Db.CreateArray<CellId>();
            m_LinkId = linkId;
            m_Points = points;
            m_Items = items;
            m_Names = names;
        }

        #endregion

        /// <summary>
        /// A unique identifier for a given RoadLink. 
        /// RoadLinks can be tested for equality by comparing their
        /// LinkIds.
        /// </summary>
        public ulong LinkId
        {
            get
            {
                return m_LinkId;
            }
        }
        /// <summary>
        /// A non-unique value that associates a RoadLink with
        /// an OpenStreetMap way.
        /// </summary>
        public long WayId
        {
            get
            {
                return m_Items[0] as PersistentWayId;
            }
        }
        /// <summary>
        /// Road category (0-6).
        /// </summary>
        public RoadCategory Category
        {
            get
            {
                return m_Items[1] as PersistentRoadCategory;
            }
        }
        /// <summary>
        /// Gets the street name for this RoadLink, if one is available.
        /// </summary>
        public string[] Names
        {
            get
            {
                return Array.ConvertAll(m_Names.ToArray(), x => x.Get());
            }
        }

        /// <summary>
        /// The first point on the RoadLink equal to Points[0].
        /// </summary>
        public LatLon FirstPoint
        {
            get
            {
                return m_Points[0];
            }
        }
        /// <summary>
        /// The last point on the RoadLink equal to Points[Points.Count()-1];
        /// </summary>
        public LatLon LastPoint
        {
            get
            {
                return m_Points[m_Points.Count() - 1];
            }
        }
        /// <summary>
        /// Get the set of RoadFlags that describes routing 
        /// constraints and other features of this RoadLink
        /// (see RoadLink documentation).
        /// </summary>
        public RoadFlags Flags
        {
            get
            {
                return m_Items[2] as RoadFlags;
            }
            internal set 
            {
                if (m_Items[2] == value)
                    return;
                m_Items[2] = value;
                Modify();
            }
        }
        /// <summary>
        /// Gets an array of ordered points that make up this
        /// RoadLink.
        /// </summary>
        public LatLon[] Points
        {
            get
            {
                return Array.ConvertAll(m_Points.ToArray(), pt => (LatLon)pt);
            }
        }
        /// <summary>
        /// Indicates the direction of travel on the RoadLink.
        /// </summary>
        public TravelDirection DirectionOfTravel
        {
            get
            {
                return m_Items[3] as PersistentTravelDirection;
            }
        }
        /// <summary>
        /// Gets a BoundingBox that contains all of the points
        /// in the Points array of this RoadLink.
        /// </summary>
        public BoundingBox GetBoundingBox
        {
            get
            {
                return new BoundingBox(this.Points);
            }
        }
        /// <summary>
        /// Returns a Volante.RectangleR2 object containing
        /// all points in the Points array.
        /// </summary>
        public RectangleR2 Rectangle
        {
            get
            {
                return GetRectangle(this.Points);
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Pen that renders this RoadLink.
        /// </summary>
        public Pen RenderPen { get; set; }
        /// <summary>
        /// Renders the RoadLink within the context of a given RenderCollection.
        /// </summary>
        /// <param name="graphics">System.Drawing.Graphics</param>
        /// <param name="collection">An associated RenderCollection object.</param>
        public virtual void Render(Graphics graphics, RenderCollection collection)
        {
            if (this.RenderPen == null)
                this.RenderPen = new Pen(Color.FromArgb(128, Color.Red), 5F);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            List<LatLon> renderPoints = this.Points.ToList();
            if (this.DirectionOfTravel == TravelDirection.To)
                renderPoints.Reverse();

            for (int i = 1; i < renderPoints.Count; i++)
            {
                if (i == renderPoints.Count - 1)
                    this.RenderPen.EndCap = LineCap.ArrowAnchor;

                graphics.DrawLine(this.RenderPen,
                            collection.LatLonToPoint(renderPoints[i - 1]),
                            collection.LatLonToPoint(renderPoints[i]));
                
                this.RenderPen.EndCap = LineCap.NoAnchor;
                this.RenderPen.StartCap = LineCap.NoAnchor;
            }
        }

        /// <summary>
        /// Compares two RoadLinks for equality by comparing the LinkIds.
        /// </summary>
        /// <param name="other">Another RoadLink</param>
        /// <returns>True if the LinkIds are equal</returns>
        public bool Equals(RoadLink other)
        {
            if (other != null)
                return this.LinkId == other.LinkId;
            return false;

        }
        /// <summary>
        /// Determines whether the specified System.Object is equal to the current System.Object.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object.</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            RoadLink other = obj as RoadLink;
            return this.Equals(other);
        }
        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current System.Object.</returns>
        public override int GetHashCode()
        {
            return this.m_LinkId.GetHashCode();
        }
    }
}
