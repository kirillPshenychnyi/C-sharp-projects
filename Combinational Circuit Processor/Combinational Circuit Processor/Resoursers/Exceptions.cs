
/***************************************************************************/

namespace Resoursers.Exceptions
{
    public class Messages
    {
        /***************************************************************************/

        public const string duplicateConnection            = "The line is already connectet to {0} element";
        public const string noElementConnection            = "The line is not connected to {0} element";
        public const string noPinConnection                = "The line is not connected to {0} pin of the {1} element";    
        public const string addingLineToEmptyCollection    = "Can't add line to the Empty Line Collectionn";
        public const string gettingLineFromEmptyCollection = "Can't get line from the Empty Line Collection";
        public const string noLinesToCheck                 = "Can't make such action on empty line collection";
        public const string wrongLineIndex                 = "Line {0} is out of range";
        public const string combinationalFeedback          = "Cannot connect this line to the input it's source element";
        public const string wrongLineCollectionSize        = "Cannot create Line collection with {0} size";
        public const string wrongCorrespondOfIOPorts       = "Cannot create {0} element with {1} inputs and {2} outputs";
        public const string unknownLogicalValue            = "Unknown logic value";
        public const string nonPrimitiveElement            = "Element {0} is not primitive. Use proper method for creating it";
        public const string wrongInputsCount               = "Cannot create {0} element with {1} inputs. At least {2} are required";

        /***************************************************************************/
    }
}

/***************************************************************************/
