using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    /***************************************************************************/

    public class BinaryElement : Element
    {
        /***************************************************************************/

        public BinaryElement( OperationType.Enum _type, Element _sourceA, Element _sourceB ) :
            base( _type )
        {
            m_sourceA = _sourceA;
            m_sourceB = _sourceB;
        }

        /***************************************************************************/

        public override bool evaluate()
        {
           switch(elementKind)
            {
                case OperationType.Enum.AND:
                    return m_sourceA.evaluate() && m_sourceB.evaluate();
                
                case OperationType.Enum.OR:
                    return m_sourceA.evaluate() || m_sourceB.evaluate();

                case OperationType.Enum.XOR:
                    return m_sourceA.evaluate() ^ m_sourceB.evaluate();

                default:
                    return false;
            }
        }

        /***************************************************************************/

        public Element this [int index]
        {
            get
            {
                return index == 0 ? m_sourceA : m_sourceB;
            }
        }

        /***************************************************************************/

        private readonly Element m_sourceA;

        private readonly Element m_sourceB;

        /***************************************************************************/

    }
}
