using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    /***************************************************************************/

    using PortList = Dictionary<string, Port>;

    using Elements = Dictionary<string, Element>;

    /***************************************************************************/

    public sealed class ElementsFactory
    {
        /***************************************************************************/

        private ElementsFactory()
        {
            m_elements = new Elements();
            m_ports = new PortList();

            m_verifier = new ElementCollectionVerifier(ref m_elements, ref m_ports);

        }

        /***************************************************************************/

        public UnaryElement createUnaryElement(
            Element.OperationType.Enum _operationType
        , string _elementName
        , string _connectedElement
        )
        {
            m_verifier.validateElementsExistance(_elementName, _connectedElement);

            m_verifier.checkPredicate(
                    () => { return Element.OperationType.isBinaryElement(_operationType); }
                ,   Messages.Exceptions.UnknownElement
                );

            Element sourceElement = m_elements[_connectedElement];

            UnaryElement element = new UnaryElement(sourceElement, _operationType);

            m_elements.Add(_elementName, element);

            return element;
 
        }

        /***************************************************************************/

        public BinaryElement createBinaryElement(
          Element.OperationType.Enum _operationType
        , string _elementName
        , string _connectedElementA
        , string _connectedElementB
        )
        {
            m_verifier.validateName(_connectedElementB);

            m_verifier.validateElementsExistance(_elementName, _connectedElementA);

            m_verifier.checkPredicate(() => { return m_verifier.hasElement(_connectedElementB); }, Messages.Exceptions.UnknownElement);

            m_verifier.checkPredicate(
                    () => { return Element.OperationType.isBinaryElement( _operationType ); }
                  , Messages.Exceptions.UnknownElement
                );

            Element sourceElementA = m_elements[_connectedElementA];
            Element sourceElementB = m_elements[_connectedElementB];

            BinaryElement element = new BinaryElement(_operationType, sourceElementA, sourceElementB);

            m_elements.Add(_elementName, element);

            return element;
        }

        /***************************************************************************/

        public InputPortElement createInputPortElement(string _portElementName, string _inputPortName)
        {
            m_verifier.validateName(_portElementName);
            m_verifier.validateName(_inputPortName);

            m_verifier.checkPredicate(() => { return ! m_verifier.hasPort(_portElementName); }, Messages.Exceptions.DuplicatedPort );
            m_verifier.checkPredicate(() => { return m_verifier.hasPort(_portElementName); }, Messages.Exceptions.UnknownPort );

            Port port = m_ports[_inputPortName];

            if (!(port is InputPort))
                throw new ArgumentException(Messages.Exceptions.PortIsNotInput);

            InputPortElement portElement = new InputPortElement( port as InputPort );

            m_elements.Add(_portElementName, portElement);

            return portElement;

        }

        /***************************************************************************/

        public InputPort createInputPort(string _portName)
        {
            InputPort inputPort = new InputPort(_portName);

            m_ports.Add(_portName, inputPort);

            return inputPort;
        }

        /***************************************************************************/

        public OutputPort createOutputPort(string _portName, string _sourceElementOpt)
        {
            m_verifier.validateName(_portName);

            m_verifier.validateName(_sourceElementOpt);

            Element elementOpt;
            m_elements.TryGetValue(_sourceElementOpt, out elementOpt);

            OutputPort outputPort = new OutputPort(_portName, elementOpt);

            m_ports.Add(_portName, outputPort);

            return outputPort;
        }

        /***************************************************************************/

        ElementsFactory Instance
        {
            get { return m_instance; }
        }

        /***************************************************************************/

        private static readonly ElementsFactory m_instance = new ElementsFactory();

        private readonly ElementCollectionVerifier m_verifier;

        Elements m_elements;

        PortList m_ports;

        /***************************************************************************/

    } // ElementsFactory

} // namespace CombinationalLogic
