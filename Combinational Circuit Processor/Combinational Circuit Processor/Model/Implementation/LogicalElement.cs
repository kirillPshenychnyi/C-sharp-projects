
/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;

    /***************************************************************************/

    abstract public class LogicalElement : ILogicalElement
    {

        /***************************************************************************/

        public LogicalElement( int _id )
        {
            m_id = _id;
        }

        /***************************************************************************/

        public int ID
        {
            get
            {
                return m_id;
            }
        }

        /***************************************************************************/

        void connectInput( ILine _line, int _pin )
        {
            m_inputPorts[ _pin ] = _line;
        }

        void connectOutput( ILine _line, int _pin )
        {
            m_outputPorts[ _pin ] = _line;
        }

        /***************************************************************************/

        public abstract LogicValue.Enum evaluate();

        /***************************************************************************/

        private readonly int m_id;

        private readonly ILineCollection m_inputPorts;

        private readonly ILineCollection m_outputPorts;

        /***************************************************************************/
    }
}

/***************************************************************************/
