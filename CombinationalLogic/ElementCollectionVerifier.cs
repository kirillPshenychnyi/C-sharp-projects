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

        public ElementCollectionVerifier(ref Elements _elements, ref PortList _portList)
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

        public void validateElementsExistance(string _newElement, string _sourceElement)
        {
            validateName(_newElement);

            validateName(_sourceElement);

            checkPredicate(() => { return !hasElement(_newElement); }, Messages.Exceptions.DuplicatedElementName);

            checkPredicate(() => { return hasElement(_sourceElement); }, Messages.Exceptions.UnknownElement);
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
