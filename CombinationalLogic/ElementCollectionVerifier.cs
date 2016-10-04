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

    public sealed class ElementCollectionVerifier
    {
        /***************************************************************************/

        public delegate bool ArgumentPredicate();

        /***************************************************************************/

        public ElementCollectionVerifier(Elements _elements,  PortList _portList)
        {
            m_elements = _elements;
            m_portList = _portList;
        }

        /***************************************************************************/

        public void checkPredicate(ArgumentPredicate _predicate, string _violateMessage)
        {
            if (!_predicate())
                throw new ArgumentException(_violateMessage);
        }

        /***************************************************************************/

        public void validateName(string _name)
        {
            checkPredicate( () => { return _name != null && _name.Length > 0; }, Messages.Exceptions.EmptyElementName);
        }

        /***************************************************************************/

        public void validateElementExistance(string _newElement)
        {
            checkPredicate(() => { return hasElement(_newElement); }, Messages.Exceptions.UnknownElement);
        }

        /***************************************************************************/

        public void validateElementUnique(string _element)
        {
            checkPredicate(() => { return !hasElement(_element); }, Messages.Exceptions.DuplicatedElementName);
        }

        public void validatePortExistance(string _newElement)
        {
            checkPredicate(() => { return hasPort(_newElement); }, Messages.Exceptions.UnknownPort);
        }

        /***************************************************************************/

        public void validatePortUnique(string _element)
        {
            checkPredicate(() => { return !hasPort(_element); }, Messages.Exceptions.DuplicatedPort);
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

        private readonly Elements m_elements;

        private readonly PortList m_portList;

        /***************************************************************************/

    } // class ElementCollectionVerifier

} // namespace CombinationalLogic
