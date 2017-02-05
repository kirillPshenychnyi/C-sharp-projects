using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.API
{
    using ElementPin = Tuple< ILogicalElement, int >;
    using PinsSet = HashSet< int >;

    public interface ILine
    {
        ElementPin SourceElement
        {
            get;
        }

        int ElementsConnected
        {
            get;
        }

        PinsSet getConnections( ILogicalElement _element );

        void addConnection( ILogicalElement _element, int _pin );
        void removeConnection( ILogicalElement _element, int _pin );
        void removeConnection( ILogicalElement _element );
    }
}
