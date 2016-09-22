using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic 
{
    public class InputPortElement : Element
    {
        public InputPortElement(InputPort _inputPort):
            base(Element.OperationType.Enum.Buffer)
        {
            m_inputPort = _inputPort;
        }

        public override bool evaluate()
        {
            return true;
        }

        InputPort m_inputPort;

    } // class InputPortElement

} // namespace CombinationalLogic
