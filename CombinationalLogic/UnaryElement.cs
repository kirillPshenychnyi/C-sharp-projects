using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    public class UnaryElement : Element
    {
      
        public UnaryElement(Element _argumentElement, OperationType _code  ): 
            base( _code )
        {
            m_type = _code;
            m_argument = _argumentElement;
        }

        public override bool evaluate()
        {
            switch (m_type)
            {
                case OperationType.Buffer:
                    return m_argument.evaluate();
                case OperationType.NOT:
                    return !m_argument.evaluate();
                default:
                    throw new ArgumentException( Messages.Exceptions.UnknownOperation );
            }
        }

        public Element source
        {
            get { return m_argument; }
        }

        private readonly OperationType m_type;

        private readonly Element m_argument;

    } // class UnaryElement

} // CombinationalLogic
