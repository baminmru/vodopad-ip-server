using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volante;

namespace OsmExplorer.Routing.Internal
{
    internal sealed class PersistentTrafficControl : Persistent
    {
        private char m_StopSign;
        private char m_TrafficSignal;

        public PersistentTrafficControl() { }
        public PersistentTrafficControl(char[] Controls)
        {
            m_StopSign = Controls[0];
            m_TrafficSignal = Controls[1];
        }

        public int Id 
        {
            get 
            {
                return m_StopSign ^ (m_TrafficSignal << 4);
            }
        }
        public char StopSign 
        {
            get 
            {
                return m_StopSign;
            }
        }
        public char TrafficSignal 
        {
            get 
            {
                return m_TrafficSignal;
            }
        }
    }
}
