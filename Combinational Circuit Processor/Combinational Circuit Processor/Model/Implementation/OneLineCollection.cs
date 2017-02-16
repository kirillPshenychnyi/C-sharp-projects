
/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;

    /***************************************************************************/

    public class OneLineCollection : BaseLineCollection
    {
        /***************************************************************************/

        public OneLineCollection( ILine _line )
        {
            m_line = _line;
        }

        /***************************************************************************/

        public override int Size
        {
            get
            {
                return 1;
            }
        }

        /***************************************************************************/

        public override ILine this[ int i ]
        {
            get
            {
                checkIndex( i );
                return m_line;
            }

            set
            {
                checkIndex( i );
                m_line = value;
            }   
        }

        /***************************************************************************/

        public override bool hasUnknownStates()
        {
            return m_line.Value != LogicValue.Enum.Unknown;
        }

        /***************************************************************************/

        public override bool hasValue( LogicValue.Enum _value )
        {
            return m_line.Value == _value;
        }

        /***************************************************************************/

        public override System.Collections.IEnumerator GetEnumerator()
        {
            yield return m_line;
        }

        /***************************************************************************/

        private ILine m_line;

        /***************************************************************************/
    }
}

/***************************************************************************/
