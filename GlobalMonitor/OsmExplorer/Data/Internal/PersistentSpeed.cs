using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volante;
using OsmExplorer.Units;

namespace OsmExplorer.Data.Internal
{
    internal sealed class PersistentSpeed : Persistent
    {
        private byte m_SpeedValue;

        public PersistentSpeed() { }
        public PersistentSpeed(byte value) 
        {
            m_SpeedValue = value;
        }

        public byte SpeedValue 
        {
            get 
            {
                return m_SpeedValue;
            }
        }
        public static implicit operator Speed(PersistentSpeed speed)
        {
            return new Speed(speed.SpeedValue, SpeedUnits.KPH);
        }
    }
}
