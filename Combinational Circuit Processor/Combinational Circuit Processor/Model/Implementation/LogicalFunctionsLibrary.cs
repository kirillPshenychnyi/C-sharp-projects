
/*****************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;
	using Numbers = System.Collections.Generic.List< int >;
	using LogicSet = System.Collections.Generic.List< API.LogicValue.Enum >;

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

			if( inputs.hasUnknownStates() )
			{
				outputs[ 0 ].Value = LogicValue.Enum.Unknown;
				return;
			}
		

            if ( inputs.hasValue( _significantValue ) )
            {
                outputs[ 0 ].Value = _valueToSet;
                return;
            }

            if ( !inputs.hasValue( LogicValue.Enum.DontCare ) )
                outputs[ 0 ].Value = LogicValue.invert( _valueToSet );
            else
                outputs[ 0 ].Value = LogicValue.Enum.DontCare;
        }

        /***************************************************************************/

        public static void xor( ILogicalElement _element )
        {
		    ILineCollection inputs = _element.Inputs;
		    ILineCollection outputs = _element.Outputs;

		    if ( inputs.hasValue( LogicValue.Enum.Unknown ) )
		    {
			     outputs[ 0 ].Value = LogicValue.Enum.Unknown;
			    return;
		    }
			
		    if ( inputs.hasValue( LogicValue.Enum.DontCare ) )
		    {
			     outputs[ 0 ].Value = LogicValue.Enum.DontCare;
			    return;
		    }

		    int numberOfHighValues = 0;

		    int inputsCount = inputs.Size;

		    for( int i = 0; i < inputs.Size; ++i )
		    {
			    if ( inputs[ i ].Value == LogicValue.Enum.High )
				    ++numberOfHighValues;
		    }

		    if ( numberOfHighValues %2 == 0 )
			     outputs[ 0 ].Value = LogicValue.Enum.Low;
		    else
			    outputs[ 0 ].Value = LogicValue.Enum.High;
		
        }

        /***************************************************************************/

        public static void nxor( ILogicalElement _element )
        {
			xor( _element );
			_element.Outputs[ 0 ].Value = LogicValue.invert( _element.Outputs[0].Value );
   
        }

        /***************************************************************************/

        public static void not( ILogicalElement _element )
        {
            _element.Outputs[ 0 ].Value = LogicValue.invert( _element.Inputs[0].Value );
        }

        /***************************************************************************/
   
        public static void decoder( ILogicalElement _element )
        {
		    ILineCollection inputs = _element.Inputs;
		    ILineCollection outputs = _element.Outputs;

		    if ( inputs.hasValue( LogicValue.Enum.Unknown ) )
		    {
                outputs.setToAllLines( LogicValue.Enum.Unknown );
			    return;
		    }
          
		    int enableIndex = inputs.Size - 1;
          
            if ( inputs[ enableIndex ].Value == LogicValue.Enum.DontCare )
		    {
                 outputs.setToAllLines( LogicValue.Enum.DontCare );
			    return;
		    }     
      
			outputs.setToAllLines( LogicValue.Enum.Low );
          
		    if ( inputs[ enableIndex ].Value == LogicValue.Enum.Low )
			    return;

		    LogicValuesNumbersConverter converter = new LogicValuesNumbersConverter();
		    Numbers outputLineNumbers = converter.toNumbers( inputs, 0,enableIndex - 1 );

		    int outputLineNumbersCount = outputLineNumbers.Count;

		    if ( outputLineNumbersCount == 1 )
			    _element.Outputs[ outputLineNumbers[ 0 ] ].Value = LogicValue.Enum.High;
		    else
		    {
			    for ( int i = 0; i < outputLineNumbersCount; ++i )
			    {
				    _element.Outputs[ outputLineNumbers[ i ] ].Value = LogicValue.Enum.DontCare;
			    }
		    }
        }

        /***************************************************************************/

        public static void encoder( ILogicalElement _element )
        {
            ILineCollection inputs = _element.Inputs;
		    ILineCollection outputs = _element.Outputs;

		    if ( inputs.hasValue( LogicValue.Enum.Unknown ) )
		    {
                outputs.setToAllLines( LogicValue.Enum.Unknown );
			    return;
		    }

		    int enableIndex = inputs.Size - 1;
		    if ( inputs[ enableIndex ].Value == LogicValue.Enum.Low )
		    {
                outputs.setToAllLines( LogicValue.Enum.Low );
			    return;
		    }

		    if ( inputs[ enableIndex ].Value == LogicValue.Enum.DontCare )
		    {
                outputs.setToAllLines( LogicValue.Enum.DontCare );
			    return;
		    }

            int highValueIndex = -1;

            for( int i = enableIndex - 1; i >= 0; --i  )
	   		{
	            if ( inputs[ i ].Value == LogicValue.Enum.High )
                {
                    highValueIndex = i;
                    break;
                }

                if (  inputs[ i ].Value == LogicValue.Enum.DontCare )
                {
                    outputs.setToAllLines( LogicValue.Enum.DontCare );
                    return;
                }
            }

            if ( highValueIndex == -1 )
            {
                outputs.setToAllLines( LogicValue.Enum.DontCare );
                return;
            }

		    int outputsCount = outputs.Size;
		    LogicValuesNumbersConverter converter = new LogicValuesNumbersConverter();
		    LogicSet outputSet = converter.toLogicSet( highValueIndex, outputsCount );

		    for ( int i = 0; i < outputsCount; ++i  )
		    {
			    outputs[ i ].Value = outputSet[i];
		    }
          
        }

        /***************************************************************************/

        public static void mux( ILogicalElement _element )
        {
            ILineCollection inputs = _element.Inputs;
           
            int totalInputsCount = inputs.Size;
	
		    int firstAddressPortIndex = totalInputsCount - _element.modifier;

		    if ( inputs.hasValue( LogicValue.Enum.Unknown ) )
		    {
			    _element.Outputs[ 0 ].Value = LogicValue.Enum.Unknown;
			    return;
		    }
		
		    LogicValuesNumbersConverter converter = new LogicValuesNumbersConverter();
		    Numbers inputLineNumbers = converter.toNumbers( inputs, firstAddressPortIndex, totalInputsCount - 1 );

		    int numbersCount = inputLineNumbers.Count;
		    if ( numbersCount < 1 )
		    {
			    _element.Outputs[ 0 ].Value = LogicValue.Enum.Unknown;
			    return;
		    }

		    LogicValue.Enum inputValue = inputs[ inputLineNumbers[ 0 ] ].Value;
          
		    for ( int i = 1; i < numbersCount; ++i )
		    {
			    if ( inputValue != inputs[ inputLineNumbers[ i ] ].Value )
			    {
				    _element.Outputs[ 0 ].Value = LogicValue.Enum.DontCare;
				    return;
			    }
		    }

		    _element.Outputs[ 0 ].Value = inputValue;
        }

        /***************************************************************************/

        public static void portElement( ILogicalElement _portElement )
        {
            PortElement portElement = _portElement as PortElement;

            switch ( portElement.PortKind )
            {
                case PortKind.Enum.Input:
					// do not set values to input ports during internal simulation
              		portElement.Outputs[ 0 ].Value = portElement.Value;
                    break;
                case PortKind.Enum.Output:
              		portElement.Value = portElement.Inputs[ 0 ].Value;
                    break;       
            }
        }

        /***************************************************************************/

        public static void dmx( ILogicalElement _element )
        {
		    ILineCollection inputs = _element.Inputs;
		    ILineCollection outputs = _element.Outputs;

		    if ( inputs.hasValue( LogicValue.Enum.Unknown ) )
		    {
                outputs.setToAllLines( LogicValue.Enum.Unknown );
			    return;
		    }

            outputs.setToAllLines( LogicValue.Enum.Low );

		    int enableIndex = inputs.Size - 1;
		    int informationInputIndex = enableIndex - 1;

		    if ( inputs[ enableIndex ].Value == LogicValue.Enum.Low )
			    return;

		    if ( inputs[ informationInputIndex ].Value == LogicValue.Enum.Low )
			    return;

		    LogicValuesNumbersConverter converter = new LogicValuesNumbersConverter();
		    Numbers outputLineNumbers = converter.toNumbers( inputs, 0, informationInputIndex - 1 );

		    int outputLineNumbersCount = outputLineNumbers.Count;

		    if ( outputLineNumbersCount == 1 )
			    outputs[ outputLineNumbers[ 0 ] ].Value = inputs[ informationInputIndex ].Value;
		    else
		    {
			    for ( int i = 0; i < outputLineNumbersCount; ++i )
			    {
				    outputs[ outputLineNumbers[ i ] ].Value = LogicValue.Enum.DontCare;
			    }
		    }
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

        public static int primitivesOutputsCalculator( int _modifier )
        {   
            /* OR, XOR, NOT, AND, NAND, NOR, NXOR, PortElement may have only one output line */
            return 1;
        }

        /***************************************************************************/

        public static int primitivesInputsCalculator( int _modifier )
        {
            return _modifier;
        }
		
		/***************************************************************************/

        public static int return0Calculator( int _modifier )
        {
            return 0;
        }
		
		/***************************************************************************/

        public static int muxInputsCalculator( int _modifier )
        {		
            return ( int )Math.Pow( 2, _modifier ) + _modifier;
        }
		
		/***************************************************************************/

        public static int dmxInputsCalculator( int _modifier )
        {			
            return 1 + 1 + _modifier;
        }
		
		/***************************************************************************/

        public static int dmxOutputsCalculator( int _modifier )
        {		
            return ( int )Math.Pow( 2, _modifier );
        }
		/***************************************************************************/

        public static int dcInputsCalculator( int _modifier )
        {		
            return 1 + _modifier;
        }
		
		/***************************************************************************/

        public static int dcOutputsCalculator( int _modifier )
        {		
            return ( int )Math.Pow( 2, _modifier );
        }
		
		/***************************************************************************/

        public static int encInputsCalculator( int _modifier )
        {		
            return ( int )Math.Pow( 2, _modifier ) + 1;
        }
		
		/***************************************************************************/

        public static int encOutputsCalculator( int _modifier )
        {			
            return ( int )Math.Pow( 2, _modifier ) + 1;
        }

        /***************************************************************************/		
    }
}

/***************************************************************************/
