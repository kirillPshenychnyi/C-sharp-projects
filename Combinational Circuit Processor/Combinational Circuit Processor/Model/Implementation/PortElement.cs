
/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using LogicalModel.API;

    /***************************************************************************/

    public class PortElement : LogicalElement
    {
        /***************************************************************************/

        public PortElement(
                PortKind.Enum _portKind
              , int _id
			  , ILibraryElementType _type
        )
            : base( _id, _type, 0 )
        {
            m_port = new Port();
            m_kind = _portKind;
        }

        /***************************************************************************/

        public PortKind.Enum PortKind
        {
            get
            {
                return m_kind;
            }
        }

        public LogicValue.Enum Value
        {
            get
            {
                return m_port.LogicValue;
            }

            set
            {
              	m_port.LogicValue = value;
                if ( m_kind == API.PortKind.Enum.Input )
                    evaluate();
                else
                    m_port.LogicValue = Inputs[ 0 ].Value;
            }
        }
 
        /***************************************************************************/

        private readonly IPort m_port;

        private readonly PortKind.Enum m_kind;

        /***************************************************************************/
    }
}

/***************************************************************************/
