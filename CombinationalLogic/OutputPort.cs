using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    public class OutputPort : Port
    {
        public OutputPort( string _name, Element _elementOpt ) : 
            base(_name)
        {
            m_elementOpt = _elementOpt;
        }

        public override bool Value
        {
            get { return m_elementOpt == null ? false : m_elementOpt.evaluate(); }
            set { }
        }

        Element m_elementOpt;
    }
}
