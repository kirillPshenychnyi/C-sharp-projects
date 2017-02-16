
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
              , ILineCollection _inputs
              , ILineCollection _outputs
        )
            : base( 
                  _id
                , LibraryElementKind.Enum.Port
                , LogicalFunctionsLibrary.portElement
                , LogicalFunctionsLibrary.primitivesModifier 
                , _inputs
                , _outputs
            )
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
                return Outputs[ 0 ].Value;
            }

            set
            {
                Outputs[ 0 ].Value = value;
            }
        }
 
        /***************************************************************************/

        private readonly IPort m_port;

        private readonly PortKind.Enum m_kind;

        /***************************************************************************/
    }
}

/***************************************************************************/
