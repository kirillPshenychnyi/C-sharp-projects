
/*****************************************************************************/

namespace LogicalModel.API
{
    /***************************************************************************/

    public interface ICombinationalCircuit
    {
        void evaluate();

        ILogicalElement addLogicalElement(
            LibraryElementKind.Enum _kind
        ,   int _inputsCount 
        );

       //void addPort( PortDirection _kind );
    }

    /***************************************************************************/
}

/***************************************************************************/
