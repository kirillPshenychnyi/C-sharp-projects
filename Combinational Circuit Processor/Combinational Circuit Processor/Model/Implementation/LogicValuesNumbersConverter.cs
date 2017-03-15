
/***************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
	/***************************************************************************/

	using API;
	using LogicSet = System.Collections.Generic.List< API.LogicValue.Enum >;
	using LogicSets =
		System.Collections.Generic.List< 
				System.Collections.Generic.List< 
						API.LogicValue.Enum 
				> 
		>;
	using Numbers = System.Collections.Generic.List< int >;

	/***************************************************************************/

	public class LogicValuesNumbersConverter
	{
		/***************************************************************************/

		public Numbers toNumbers(
				ILineCollection _lines
			,	int _firstIndex
			,	int _lastIndex
		)
		{
			reset();
			LogicSet logicSet = toLogicSet( _lines, _firstIndex, _lastIndex );
			internalExecute( logicSet );

			Numbers result = new Numbers();

			int simpleLogicSetsCount = m_simpleLogicSets.Count;
			for ( int i = 0; i < simpleLogicSetsCount; ++i )
			{
				result.Add( executeOnSimpleLogicSet( m_simpleLogicSets[i] ) );
			}
			return result;
		}

		/***************************************************************************/

		public int simpleLogicSetToNumber( 
				ILineCollection _lines
			,	int _firstIndex
			,	int _lastIndex
		)
		{
			LogicSet logicSet = toLogicSet( _lines, _firstIndex, _lastIndex );

			return executeOnSimpleLogicSet( logicSet );
		}

		/***************************************************************************/

		public int executeOnSimpleLogicSet( LogicSet _set )
		{
		    int dontCareIndex = findValue( _set, LogicValue.Enum.DontCare );
		    if ( dontCareIndex != -1 )
			    throw new Exception();

		    int currentBinaryPow = 1;
		    int resultNumber = 0;
      
            for ( int i = 0; i < _set.Count; ++i )
            {
                int logicValueAsNumber = LogicValue.asNumber( _set[ i ] );

                resultNumber += currentBinaryPow * logicValueAsNumber;

                currentBinaryPow *= 2;
            }
		
			return resultNumber;
		}

		/***************************************************************************/

		public LogicSet toLogicSet( int _number, int _size )
		{
            LogicSet result = new LogicSet();
            for ( int i = 0; i < _size; ++i )
            {
                result.Add( LogicValue.Enum.Unknown );
            }
      
			for ( int i = 0; i < _size; ++i  )
			{
				int remainder = _number % 2;
        
				if ( remainder == 1 )
					result[ i ] = LogicValue.Enum.High;
				else
					result[ i ] = LogicValue.Enum.Low;
				
				_number /= 2;
			}

			return result;
		}

		/***************************************************************************/

		private void internalExecute( LogicSet _set )
		{
			int dontCareIndex = findValue( _set, LogicValue.Enum.DontCare );
			if ( dontCareIndex == -1 )
			{
				m_simpleLogicSets.Add( _set );
				return;
			}

			LogicSet lowLogicSet = logicSetCopy( _set );
			lowLogicSet[ dontCareIndex ] = LogicValue.Enum.Low;
			internalExecute( lowLogicSet );
            	
			LogicSet highLogicSet = logicSetCopy( _set );
			highLogicSet[ dontCareIndex ] = LogicValue.Enum.High;
			internalExecute( highLogicSet );
		}

		/***************************************************************************/

		private LogicSet toLogicSet( 
				ILineCollection _lines
			,	int _firstIndex
			,	int _lastIndex
		)
		{
			LogicSet newSet = new LogicSet();

			for( int i = _firstIndex; i <= _lastIndex; ++i )
			{
				newSet.Add( _lines[ i ].Value );
			}
			
			return newSet;
		}

		/***************************************************************************/

		private LogicSet logicSetCopy( LogicSet _set )
		{
			LogicSet newSet = new LogicSet();

			int count = _set.Count;
			for( int i = 0; i < count; ++i )
			{
				newSet.Add( _set[ i ] );
			}
			
			return newSet;
		}

		/***************************************************************************/

		private void reset()
		{
			m_simpleLogicSets = new LogicSets();
		}

		/***************************************************************************/

		public int findValue( LogicSet _set, LogicValue.Enum _targetValue )
		{
			return _set.FindIndex( 
					( LogicValue.Enum _value ) => 
					{
						return _value == _targetValue;
					}
			);
		}

		/***************************************************************************/

		private LogicSets m_simpleLogicSets;

		/***************************************************************************/
	}
}

/***************************************************************************/