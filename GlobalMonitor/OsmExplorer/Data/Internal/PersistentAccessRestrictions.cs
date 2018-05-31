using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volante;

namespace OsmExplorer.Routing.Internal
{
    internal sealed class PersistentAccessRestriction : Persistent
    {
        private int m_Id;
        private bool m_Restriction;

        public PersistentAccessRestriction() { }
        public PersistentAccessRestriction(bool restriction, RestrictionType rType) 
        {
            m_Id = Convert.ToInt32(restriction) ^ (int)rType;
            m_Restriction = restriction;
        }

        public int Id 
        {
            get 
            {
                return m_Id;
            }
        }
        public static implicit operator bool(PersistentAccessRestriction pRestriction) 
        {
            return pRestriction.m_Restriction;
        }
    }
}
