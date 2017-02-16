
/***************************************************************************/

namespace LogicalModel.API
{
    public interface ILogicalElement
    {
        /***************************************************************************/

        int ID { get; }

        void evaluate();

        void updateValue( int _pin, LogicValue.Enum _value );

        void makeConnection( 
            ILogicalElement _elem
          , int _inputPin 
          , int _outputPin
        );

        LibraryElementKind.Enum Kind { get; }

        ILineCollection Inputs { get; }

        ILineCollection Outputs { get; }

        /***************************************************************************/
    }
}

/***************************************************************************/
