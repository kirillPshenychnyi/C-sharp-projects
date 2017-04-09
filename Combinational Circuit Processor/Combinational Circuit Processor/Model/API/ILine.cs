
/***************************************************************************/

using System;
using System.Collections.Generic;

/***************************************************************************/

namespace LogicalModel.API
{
    /***************************************************************************/

    using ElementPin = Tuple< ILogicalElement, int >;
    using PinsSet = HashSet< int >;

    /***************************************************************************/

    public interface ILine
    {
        ElementPin SourceElement
        {
            get;
            set;
        }

        int ElementsConnected
        {
            get;
        }

        PinsSet getConnections( ILogicalElement _element );

        LogicValue.Enum Value
        {
            get;
            set;
        }

        bool hasConnection( ILogicalElement _element );
        void addConnection( ILogicalElement _element, int _pin );
        void removeConnection( ILogicalElement _element, int _pin );
        void removeConnection( ILogicalElement _element );
    }
}

/***************************************************************************/
