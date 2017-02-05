using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Implementation
{
    using API;

    public class Port : IPort
    {
        /***************************************************************************/

        public Port()
        {
            m_logicValue = LogicValue.Value.Unknown;
        }

        /***************************************************************************/

        public LogicValue.Value logicValue
        {
            get
            {
                return m_logicValue;
            }
            set
            {
                m_logicValue = value;
            }
        }

        /***************************************************************************/

        private LogicValue.Value m_logicValue;

        /***************************************************************************/
    }
}
