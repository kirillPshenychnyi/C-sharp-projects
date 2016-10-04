using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    public class InputPort : Port
    {
        /***************************************************************************/

        public InputPort( string _name ) : 
            base ( _name )
        {
            m_value = false;
        }

        /***************************************************************************/

        public override bool Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /***************************************************************************/

        private bool m_value;

        /***************************************************************************/

    } // class InputPort

} // namespace CombinationalLogic 
