using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    public abstract class Element
    {
        public Element( OperationType.Enum _type)
        {
            m_operatorKind = _type;
        }

        public class OperationType
        {
            /***************************************************************************/

            public enum Enum
            {
                AND
              , OR
              , XOR
              , NOT
              , Buffer
            };

            /***************************************************************************/

            public static bool isBinaryElement( Enum _elemet )
            {
                switch (_elemet)
                {
                    case Enum.AND:
                    case Enum.OR:
                    case Enum.XOR:
                        return true;
                    case Enum.NOT:
                    case Enum.Buffer:
                        return false;
                    default:
                        return false;
                }
            }

            /***************************************************************************/

            public static bool isUnary( Enum _element )
            {
                return !isBinaryElement(_element);
            }

            /***************************************************************************/

        }; // class OperationType

        /***************************************************************************/

        public abstract bool evaluate();

        /***************************************************************************/

        public OperationType.Enum elementKind
        {
            get { return m_operatorKind; }
        }

        /***************************************************************************/

        private readonly OperationType.Enum m_operatorKind;

        /***************************************************************************/

    } // interface Element
} // namespace CombinationalLogic
