using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    public sealed class ElementsFactory
    {
        /***************************************************************************/

        private ElementsFactory()
        {
            m_elements = new Dictionary<string, Element>();
        }

        /***************************************************************************/

        private delegate bool ArgumentPredicate ();

        /***************************************************************************/

        private void check(ArgumentPredicate _predicate, string _violateMessage)
        {
            if (!_predicate() )
                throw new ArgumentException(_violateMessage);
        }

        /***************************************************************************/

        public UnaryElement createUnaryElement( 
            Element.OperationType _operationType
        ,   string _elementName
        ,   string _connectedElement 
        )
        {
            validateElementsExistance(_elementName, _connectedElement);
            
            check(
                    () => {
                            return
                                _operationType != Element.OperationType.Buffer
                            ||  _operationType != Element.OperationType.NOT;
                          }
                ,   Messages.Exceptions.UnknownElement
                );

            Element sourceElement = m_elements[_connectedElement];

            UnaryElement element = new UnaryElement(sourceElement, _operationType);

            m_elements.Add(_elementName, element);

            return element;
 
        }

        /***************************************************************************/

        public BinaryElement createBinaryElement(
          Element.OperationType _operationType
        , string _elementName
        , string _connectedElementA
        , string _connectedElementB
        )
        {
            validateName(_connectedElementB);

            validateElementsExistance(_elementName, _connectedElementA);
            
            check(() => { return hasElement(_connectedElementB); }, Messages.Exceptions.UnknownElement);

            check(
                    () => {
                        return
                            _operationType != Element.OperationType.OR
                        || _operationType != Element.OperationType.AND
                        ||  _operationType != Element.OperationType.XOR;
                    }
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
            validateName(_portElementName);
            validateName(_inputPortName);

            check(() => { return m_portList.ContainsKey(_portElementName); }, Messages.Exceptions.DuplicatedPort );
            check(() => { return m_portList.ContainsKey(_portElementName); }, Messages.Exceptions.UnknownPort );

            Port port = m_portList[_inputPortName];

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

            m_portList.Add(_portName, inputPort);

            return inputPort;
        }

        /***************************************************************************/

        public OutputPort createOutputPort(string _portName, string _sourceElementOpt)
        {
            validateName(_portName);

            validateName(_sourceElementOpt);

            Element elementOpt;
            m_elements.TryGetValue(_sourceElementOpt, out elementOpt);

            OutputPort outputPort = new OutputPort(_portName, elementOpt);

            m_portList.Add(_portName, outputPort);

            return outputPort;
        }

        /***************************************************************************/

        private void validateName( string _name )
        {
            check(() => { return _name != null && _name.Length > 0; }, Messages.Exceptions.EmptyElementName);
        }

        /***************************************************************************/

        private void validateElementsExistance( string _newElement, string _sourceElement)
        {
            validateName(_newElement);

            validateName(_sourceElement);

            check(() => { return !hasElement(_newElement); }, Messages.Exceptions.DuplicatedElementName);

            check(() => { return hasElement(_sourceElement); }, Messages.Exceptions.UnknownElement);
        }

        /***************************************************************************/

        public bool hasElement(string _elementName)
        {
            return m_elements.ContainsKey(_elementName);
        }

        /***************************************************************************/

        public bool hasPort(string _portName)
        {
            return m_portList.ContainsKey(_portName);
        }
    
        /***************************************************************************/

        ElementsFactory Instance
        {
            get { return m_instance; }
        }

        /***************************************************************************/

        private static readonly ElementsFactory m_instance = new ElementsFactory();

        Dictionary<string, Element> m_elements;

        Dictionary<string, Port> m_portList;

        /***************************************************************************/

    } // ElementsFactory

} // namespace CombinationalLogic
