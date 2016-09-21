using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    public abstract class Element
    {
        public Element( OperationType _type)
        {
            m_operatorKind = _type;
        }

        public enum OperationType { AND, OR, XOR, NOT, Buffer };

        public abstract bool evaluate();

        public OperationType elementKind
        {
            get { return m_operatorKind; }
        }

        private readonly OperationType m_operatorKind;

    } // interface Element
} // namespace CombinationalLogic
