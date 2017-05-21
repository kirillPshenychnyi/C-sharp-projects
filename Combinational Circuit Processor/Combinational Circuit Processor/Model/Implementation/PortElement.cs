
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
                PortDirection _portKind
              , string _name
              , int _id
			  , ILibraryElementType _type
        )
            : base( _id, _type, 0 )
        {
            m_port = new Port( _name );
            PortKind = _portKind;
        }

        /***************************************************************************/

        public PortDirection PortKind
        {
            get;
        }

        /***************************************************************************/

        public string Name
        {
            get
            {
                return m_port.Name;
            }
        }

        /***************************************************************************/

        public LogicValue.Enum Value
        {
            get
            {
                return m_port.LogicValue;
            }

            set
            {
              	m_port.LogicValue = value;
                if ( PortKind == PortDirection.Input )
                    evaluate();
                else
                    m_port.LogicValue = Inputs[ 0 ].Value;
            }
        }
 
        /***************************************************************************/

        private readonly IPort m_port;

        /***************************************************************************/
    }
}

/***************************************************************************/
