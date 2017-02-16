
/*****************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;

    /***************************************************************************/

    public class LogicalFunctionsLibrary
    {
        /***************************************************************************/

        public static void elementWithCoverage( 
            ILogicalElement _element
         ,  LogicValue.Enum _significantValue 
         ,  LogicValue.Enum _valueToSet
        )
        {
            ILineCollection inputs = _element.Inputs;
            ILineCollection outputs = _element.Outputs;

            if ( inputs.hasValue( _significantValue ) )
            {
                outputs[ 0 ].Value = _valueToSet;
                return;
            }

            if ( !inputs.hasUnknownStates() )
                outputs[ 0 ].Value = LogicValue.invert( _valueToSet );
            else
                outputs[ 0 ].Value = LogicValue.Enum.Unknown;
        }

        public static void xor( ILogicalElement _element )
        {
            // TODO by nikitam
        }

        public static void nxor( ILogicalElement _element )
        {
            // TODO by nikitam
        }

        public static void not( ILogicalElement _element )
        {
            _element.Outputs[ 0 ].Value = LogicValue.invert( _element.Inputs[0].Value );
        }

        /***************************************************************************/
   
        public static void decoder( ILogicalElement _element )
        {
            // TODO by nikitam
        }

        /***************************************************************************/

        public static void encoder( ILogicalElement _element )
        {
            // TODO by nikitam
        }

        /***************************************************************************/

        public static void mux( ILogicalElement _element )
        {
            ILineCollection inputs = _element.Inputs;
           
            int totalInputsCount = inputs.Size;

            int informInputs = totalInputsCount;
            
            while( true )
            {
                informInputs--;
                double log2 = Math.Log( informInputs, 2.0 );

                if ( log2 - ( int )log2 == 0.0 )
                    break;
            }

            int currentBinaryPow = 1;
            int inputLineNumber = 0;

            for( int i = totalInputsCount; i > informInputs; --i  )
            {
                int logicValueAsNumber = LogicValue.asNumber( inputs[i - 1].Value );

                if ( logicValueAsNumber == -1 )
                    return;

                inputLineNumber += currentBinaryPow * LogicValue.asNumber( inputs[ i - 1 ].Value );

                currentBinaryPow *= 2;
            }

            _element.Outputs[ 0 ].Value = inputs[ inputLineNumber ].Value;  
        }

        /***************************************************************************/

        public static void portElement( ILogicalElement _portElement )
        {
            PortElement portElement = _portElement as PortElement;

            switch ( portElement.PortKind )
            {
                case PortKind.Enum.Input:
                    // do not set values to input ports during internal simulation
                    break;
                case PortKind.Enum.Output:
                    portElement.Outputs[ 0 ].Value = portElement.Inputs[ 0 ].Value;                
                    break;       
            }
        }

        /***************************************************************************/

        public static void dmx( LineCollection _inputs, LineCollection _outputs )
        {
            // TODO by nikitam
        }

        /***************************************************************************/

        public static void primitiveEvaluator( ILogicalElement _primitiveElement )
        {
            switch ( _primitiveElement.Kind )
            {
                case LibraryElementKind.Enum.AND:
                    elementWithCoverage(
                        _primitiveElement
                     ,  LogicValue.Enum.Low
                     ,  LogicValue.Enum.Low 
                    );
                    break;

                case LibraryElementKind.Enum.OR:
                    elementWithCoverage(
                        _primitiveElement
                    ,   LogicValue.Enum.High
                    ,   LogicValue.Enum.High
                   );
                    break;           

                case LibraryElementKind.Enum.NAND:
                    elementWithCoverage(
                        _primitiveElement
                    ,   LogicValue.Enum.Low
                    ,   LogicValue.Enum.High
                   );
                    break;

                case LibraryElementKind.Enum.NOR:
                    elementWithCoverage(
                       _primitiveElement
                    , LogicValue.Enum.High
                    , LogicValue.Enum.Low
                   );
                    break;

                case LibraryElementKind.Enum.XOR:
                    xor( _primitiveElement );
                    break;

                case LibraryElementKind.Enum.NXOR:
                    nxor( _primitiveElement );
                    break;
            }

        }

        /***************************************************************************/

        public static int adressElementModify( int _modifier )
        {
            return ( int )Math.Pow( 2, _modifier );
        }

        /***************************************************************************/

        public static int primitivesModifier( int _modifier )
        {   
            /* OR, XOR, NOT, AND, NAND, NOR, NXOR, PortElement may have only one output line */
            return 1;
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
