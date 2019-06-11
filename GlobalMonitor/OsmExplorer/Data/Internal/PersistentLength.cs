using Volante;
using OsmExplorer.Units;

namespace OsmExplorer.Data.Internal
{
    internal sealed class PersistentLength : Persistent
    {
        private uint m_Distance;

        public PersistentLength() { }
        public PersistentLength(uint value)
        {
            m_Distance = value;
        }

        public uint Distance
        {
            get
            {
                return m_Distance;
            }
        }
        public static implicit operator Length(PersistentLength distance)
        {
            return new Length(distance.Distance, LengthUnits.Meters);
        }
    }
}
