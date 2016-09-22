using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    /***************************************************************************/

    public class UnaryElement : Element
    {
        /***************************************************************************/

        public UnaryElement(Element _argumentElement, OperationType.Enum _code  ): 
            base( _code )
        {
            m_argument = _argumentElement;
        }

        /***************************************************************************/

        public override bool evaluate()
        {
            switch (elementKind)
            {
                case OperationType.Enum.Buffer:
                    return m_argument.evaluate();
                case OperationType.Enum.NOT:
                    return !m_argument.evaluate();
                default:
                    throw new ArgumentException( Messages.Exceptions.UnknownOperation );
            }
        }

        /***************************************************************************/

        public Element source
        {
            get { return m_argument; }
        }

        /***************************************************************************/

        private readonly Element m_argument;

        /***************************************************************************/

    } // class UnaryElement

} // namespace CombinationalLogic
