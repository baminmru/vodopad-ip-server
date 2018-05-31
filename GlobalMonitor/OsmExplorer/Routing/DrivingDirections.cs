using System.Collections;
using System.Collections.Generic;
using OsmExplorer.Routing.Internal;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Class representing a set of natural-language driving directions for a route.
    /// </summary>
    public class DrivingDirections : IEnumerable<Direction>
    {
        private List<Direction> m_Directions;
        internal DrivingDirections(params P2PRouterDirections[] directions) 
        {
            m_Directions = new List<Direction>();
            foreach (var dir in directions) 
            {
                m_Directions.AddRange(dir.Directions);
            }
        }

        /// <summary>
        /// Gets an enumerator that enumerates through the list of Directions in this DrivingDirections.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the list of Directions.</returns>
        public IEnumerator<Direction> GetEnumerator()
        {
            return m_Directions.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Directions.GetEnumerator();
        }
    }
}
