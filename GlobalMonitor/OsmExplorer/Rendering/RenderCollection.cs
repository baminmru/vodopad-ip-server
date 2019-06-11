using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using OsmExplorer.Components;
using OsmExplorer.Spatial;
using System;

namespace OsmExplorer.Rendering
{
    /// <summary>
    /// Represents a collection of objects that implement the OsmExplorer.Rendering.IRenderable interface 
    /// to be rendered within a MapExplorer control.
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    public class RenderCollection : ICollection<IRenderable>
    {
        #region Private
        private HashSet<IRenderable> m_Items;
        private List<IRenderable> m_List;
        private MapExplorer m_Control;
        private SharpMap.Map m_Map;
        private RenderContext m_Context;

        private void RenderItems(Graphics g)
        {
            lock (m_Items)
            {
                foreach (var item in m_List)
                {
                    if (item != null)
                        item.Render(g, this);
                }
            }
        }
        private void OnControlPaint(object sender, System.Windows.Forms.PaintEventArgs e) 
        {
            RenderItems(e.Graphics);
        }

        private enum RenderContext
        {
            MapExplorer,
            SharpMap
        }

        #endregion
        #region Internal

        internal PointF LatLonToPoint(LatLon point)
        {
            switch (m_Context) 
            {
                case RenderContext.MapExplorer:
                    return m_Control.LatLonToPoint(point);
                case RenderContext.SharpMap:
                    return m_Map.WorldToImage(new SharpMap.Geometries.Point(point.Lon, point.Lat));
                default:
                    throw new ArgumentException("Unsupported rendering context");
            }
            
        }

        #endregion

        /// <summary>
        /// Creates a new RenderCollection for rendering OsmExplorer.Spatial objects within
        /// a MapExplorer control.
        /// </summary>
        /// <param name="explorer">An OsmExplorer.MapExplorer control.</param>
        public RenderCollection(MapExplorer explorer)
        {
            this.m_Control = explorer;
            this.m_Items = new HashSet<IRenderable>();
            this.m_List = new List<IRenderable>();
            this.m_Context = RenderContext.MapExplorer;
            this.m_Control.Paint += new System.Windows.Forms.PaintEventHandler(OnControlPaint);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        public RenderCollection(SharpMap.Map map) 
        {
            this.m_Map = map;
            this.m_Items = new HashSet<IRenderable>();
            this.m_List = new List<IRenderable>();
            this.m_Context = RenderContext.SharpMap;
            this.m_Map.MapRendering += new SharpMap.Map.MapRenderedEventHandler(RenderItems);
        }

        /// <summary>
        /// Adds an IRenderable item to the RenderCollection.
        /// </summary>
        /// <param name="item">Item implementing IRenderable interface.</param>
        public void Add(IRenderable item)
        {
            lock (m_Items) 
            {
                if (m_Items.Add(item))
                    m_List.Add(item);
            }
        }
        /// <summary>
        /// Removes all items from the RenderCollection.
        /// </summary>
        public void Clear()
        {
            lock (m_Items) 
            {
                m_Items.Clear();
                m_List.Clear();
            }
        }
        /// <summary>
        /// Determines if the RenderCollection contains the specified item.
        /// </summary>
        /// <param name="item">Object implementing IRenderable interface.</param>
        /// <returns>True if the RenderCollection contains the specified item.</returns>
        public bool Contains(IRenderable item)
        {
            return m_Items.Contains(item);
        }
        /// <summary>
        /// Copies the elements of the RenderCollection to a
        /// System.Array, starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">A System.Array object.</param>
        /// <param name="arrayIndex">Start index of the System.Array.</param>
        public void CopyTo(IRenderable[] array, int arrayIndex)
        {
            lock (m_Items) 
            {
                IRenderable[] rArray = System.Linq.Enumerable.ToArray(m_List);
                rArray.CopyTo(array, arrayIndex);
            }
        }
        /// <summary>
        /// Returns the number of IRenderable items in the RenderCollection.
        /// </summary>
        public int Count
        {
            get 
            {
                return m_List.Count;
            }
        }
        /// <summary>
        /// Returns whether the RenderCollection is read-only. Always false.
        /// </summary>
        public bool IsReadOnly
        {
            get 
            {
                return false;
            }
        }
        /// <summary>
        /// Removes an item from the RenderCollection.
        /// </summary>
        /// <param name="item">An item to remove that implements the IRenderable interface.</param>
        /// <returns>True if the item was successfully removed.</returns>
        public bool Remove(IRenderable item)
        {
            lock (m_Items) 
            {
                bool removed = m_Items.Remove(item);
                if (removed)
                    m_List.Remove(item);
                return removed;
            }
        }
        /// <summary>
        /// Gets an enumerator that iterates through the RenderCollection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the items in the RenderCollection.</returns>
        public IEnumerator<IRenderable> GetEnumerator()
        {
            return m_List.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
