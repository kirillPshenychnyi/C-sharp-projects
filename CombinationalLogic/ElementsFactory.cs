using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    /***************************************************************************/

    public sealed class ElementsFactory
    {
        /***************************************************************************/

        private ElementsFactory()
        {
        }

        /***************************************************************************/

        public UnaryElement createUnaryElement(
            Element.OperationType.Enum _operationType
        ,   Element _connectedElement
        )
        {
            return new UnaryElement(_connectedElement, _operationType);
        }

        /***************************************************************************/

        public BinaryElement createBinaryElement(
          Element.OperationType.Enum _operationType
        , Element _sourceA
        , Element _sourceB
        )
        {  
           return new BinaryElement(_operationType, _sourceA, _sourceB);
        }

        /***************************************************************************/

        public InputPortElement createInputPortElement(InputPort _inputPort)
        {
            return new InputPortElement(_inputPort);
        }

        /***************************************************************************/

        public InputPort createInputPort(string _portName)
        {
            InputPort inputPort = new InputPort(_portName);

            return inputPort;
        }

        /***************************************************************************/

        public OutputPort createOutputPort(string _portName, Element _elementOpt)
        {
            return new OutputPort(_portName, _elementOpt);
        }

        /***************************************************************************/

        public static ElementsFactory Instance
        {
            get { return m_instance; }
        }

        /***************************************************************************/

        private static readonly ElementsFactory m_instance = new ElementsFactory();

        /***************************************************************************/

    } // ElementsFactory

} // namespace CombinationalLogic
