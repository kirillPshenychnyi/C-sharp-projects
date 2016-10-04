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

    public class CombinationalScheme
    {
        /***************************************************************************/

        public CombinationalScheme(string _name)
        {
           m_name = _name;

           m_elements = new Elements();

           m_portList = new PortList();

           m_collectionVerifier = new ElementCollectionVerifier(m_elements, m_portList);

           m_factory = ElementsFactory.Instance;
        }

        /***************************************************************************/

        public void addOutputPort(string _portName, string _sourceElementOpt)
        {
            m_collectionVerifier.validateName(_portName);

            m_collectionVerifier.validatePortUnique(_portName);

            Element elementOpt;
            m_elements.TryGetValue(_sourceElementOpt, out elementOpt);

            m_portList.Add(_portName,m_factory.createOutputPort(_portName, elementOpt) );

        }

        /***************************************************************************/

        public void addUnaryElement(
            Element.OperationType.Enum _operationType
          , string _elementName
          , string _connectedElement)
        {
            m_collectionVerifier.validateName(_elementName);

            m_collectionVerifier.validateName(_connectedElement);

            m_collectionVerifier.validateElementExistance(_connectedElement);

            m_collectionVerifier.validateElementUnique(_elementName);

            m_collectionVerifier.checkPredicate(
                    () => { return Element.OperationType.isUnary(_operationType); }
                , Messages.Exceptions.UnknownElement
                );

            Element sourceElement = m_elements[_connectedElement];
           
            m_elements.Add(_elementName, m_factory.createUnaryElement(_operationType,sourceElement) );
        }

        /***************************************************************************/

        public void addBinaryElement(
            Element.OperationType.Enum _operationType
          , string _elementName
          , string _firstSource
          , string _secondSource)
        {
            m_collectionVerifier.validateName(_elementName);

            m_collectionVerifier.validateName(_firstSource);

            m_collectionVerifier.validateName(_secondSource);

            m_collectionVerifier.validateElementUnique(_elementName);

            m_collectionVerifier.validateElementExistance(_firstSource);

            m_collectionVerifier.validateElementExistance(_secondSource);
            
            m_collectionVerifier.checkPredicate(
                    () => { return Element.OperationType.isBinaryElement(_operationType); }
                  , Messages.Exceptions.UnknownElement
                );

            Element sourceElementA = m_elements[_firstSource];
            Element sourceElementB = m_elements[_secondSource];

            m_elements.Add(
                _elementName
              , m_factory.createBinaryElement(_operationType, sourceElementA, sourceElementB));
        }

        /***************************************************************************/

        public void addInputPort(string _portName)
        {
            m_collectionVerifier.validateName(_portName);

            m_collectionVerifier.validateElementUnique(_portName);
            m_collectionVerifier.validatePortUnique(_portName);

            InputPort inputPort = m_factory.createInputPort(_portName);

            m_portList.Add(_portName, inputPort);

            m_elements.Add(_portName, m_factory.createInputPortElement(inputPort) );
        }

        /***************************************************************************/

        public bool getValueFromPort(string _portName)
        {
            m_collectionVerifier.validatePortExistance(_portName);

            return m_portList[_portName].Value;
        }

        public void setInputPortValue(string _portName, bool _value)
        {
            m_collectionVerifier.validatePortExistance(_portName);

            Port inputPort = m_portList[_portName];

            m_collectionVerifier.checkPredicate(
                () => { return inputPort is InputPort; }
              , Messages.Exceptions.PortIsNotInput);

            (inputPort as InputPort).Value = _value; 
 
        }
        
        /***************************************************************************/

        private readonly string m_name;

        private readonly PortList m_portList;

        private readonly Elements m_elements;

        private readonly ElementsFactory m_factory;

        private readonly ElementCollectionVerifier m_collectionVerifier;

        /***************************************************************************/

    } // class CombinationalScheme 

    /***************************************************************************/

} // namespace CombinationalLogic
