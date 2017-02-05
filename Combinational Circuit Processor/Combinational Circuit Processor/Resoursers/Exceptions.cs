using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resoursers.Exceptions
{
    public class Messages
    {
        public const string duplicateConnection            = "The line is already connectet to {0} element";
        public const string noElementConnection            = "The line is not connected to {0} element";
        public const string noPinConnection                = "The line is not connected to {0} pin of the {1} element";    
        public const string addingLineToEmptyCollection    = "Can't add line to the Empty Line Collectionn";
        public const string gettingLineFromEmptyCollection = "Can't get line from the Empty Line Collection";
        public const string wrongLineIndex                 = "Line {0} is out of range";
        public const string combinationalFeedback          = "Cannot connect this line to the input it's source element";
        public const string wrongLineCollectionSize        = "Cannot create Line collection with {0} size";
    }
}
