using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OsmExplorer.Rendering;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Class representing results from a many-to-many route calculation
    /// for a set of route stops.
    /// </summary>
    public class RouteResultMatrix : IRenderable, IEnumerable<RouteResult>
    {
        private RouteResult[][] m_ResultArray;
        private List<RouteResult> m_ResultList;
        private TimeSpan m_Runtime;

        internal RouteResultMatrix(RouteResult[][] resultArray) 
        {
            m_ResultArray = resultArray;
            m_ResultList = new List<RouteResult>();

            for (int i = 0; i < resultArray.Count(); i++) 
            {
                for (int j = 0; j < resultArray.Count(); j++)
                {
                    if (resultArray[i][j] != null)
                        m_ResultList.Add(resultArray[i][j]);
                }
            }
            m_Runtime = m_ResultList.Min(result => result.AlgorithmRuntime);
        }

        public RouteResult Result(int Row, int Column) 
        {
            try
            {
                return m_ResultArray[Row][Column];
            }
            catch (IndexOutOfRangeException ex) 
            {
                throw new IndexOutOfRangeException("Index outside of result array bounds", ex);
            }
        }
        public TimeSpan Runtime 
        {
            get 
            {
                return m_Runtime;
            }
        }

        public IEnumerator<RouteResult> GetEnumerator()
        {
            return m_ResultList.GetEnumerator();
        }
        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public System.Drawing.Pen RenderPen { get; set; }
        public void Render(System.Drawing.Graphics graphics, RenderCollection collection)
        {
            foreach (var result in m_ResultList) 
            {
                result.Render(graphics, collection);
            }
        }
    }
}
