using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationalLogic
{
    namespace Messages
    {
        public class Exceptions
        {
            public static readonly string UnknownOperation = "Unknown operation code";
            public static readonly string UnknownElement = "Unknown connected element";
            public static readonly string DuplicatedElementName = "Duplicated element name";
            public static readonly string EmptyElementName = "Empty name element is not allowed";
            public static readonly string UnknownPort = "Unknown port";
            public static readonly string EmptyPortName = "Empty port name is not allowed";
            public static readonly string DuplicatedPort = "Duplicated port";
            public static readonly string PortIsNotInput = "Port is not input";

        } // class Exceptions

    } // Messages 
} // namespace CombinationalLogic