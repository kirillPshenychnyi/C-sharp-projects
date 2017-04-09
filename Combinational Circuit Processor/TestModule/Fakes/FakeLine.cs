
/***************************************************************************/

using System;
using System.Collections.Generic;

/***************************************************************************/

namespace TestModule.Fakes
{
    /***************************************************************************/

    using LogicalModel.API;

    /***************************************************************************/

    using ElementNumberPair = Tuple<LogicalModel.API.ILogicalElement, int>;
    using PinsSet = HashSet<int>;

    /***************************************************************************/

    public class FakeLine : ILine
    {
        /***************************************************************************/

        public ElementNumberPair SourceElement
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /***************************************************************************/

        public int ElementsConnected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /***************************************************************************/

        ElementNumberPair ILine.SourceElement
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /***************************************************************************/

        public LogicValue.Enum Value
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /***************************************************************************/

        public void addConnection( ILogicalElement _element, int _pin )
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/

        public void removeConnection( ILogicalElement _element, int _pin )
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/

        public PinsSet getConnections( ILogicalElement _element )
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/

        public void removeConnection(ILogicalElement _element)
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/

        public bool hasConnection( ILogicalElement _element )
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
