
/***************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;
    using ElementPin = Tuple< API.ILogicalElement, int >;

    /***************************************************************************/

    public class LogicalElement : ILogicalElement
    {
        /***************************************************************************/

	    public LogicalElement(
	        	int _id
	        ,	ILibraryElementType _elementType
			,	int _modifier
            )
        {
            	m_elementType = _elementType;
				m_modifier = _modifier;
            	m_id = _id;
				int inputsCount = m_elementType.calculateInputs( m_modifier );
				int outputsCount = m_elementType.calculateOutputs( m_modifier );

				m_inputLines = BaseLineCollection.createLineCollection( inputsCount );
				m_outputLines = BaseLineCollection.createLineCollection( outputsCount );

	            for (int i = 0; i < inputsCount; i++)
	            {
	                m_inputLines[ i ] = new Line();
	                m_inputLines[ i ].addConnection(this, i);
            	}

                for ( int i = 0; i < outputsCount; i++ )
                {
              	    m_outputLines[ i ] = new Line();
                    m_outputLines[ i ].SourceElement = new ElementPin( this, i );
                }
        }

        /***************************************************************************/

        public int ID
        {
            get
            {
                return m_id;
            }
        }

        public LibraryElementKind.Enum Kind
        {
            get
            {
                return m_elementType.elementKind;
            }
        }

        public ILineCollection Inputs
        {
            get
            {
                return m_inputLines;
            }
        }

        public ILineCollection Outputs
        {
            get
            {
                return m_outputLines;
            }
        }
		
		public int modifier
		{
			get
			{
				return m_modifier;
			}
		}

        /***************************************************************************/

        public void makeConnection(
            ILogicalElement _source
          , int _inputPin
          , int _outputPin
        )
        {
            m_inputLines[ _inputPin ] = _source.Outputs[ _outputPin ];
            
            _source.Outputs[ _outputPin ].addConnection( this, _inputPin );
        }

        /***************************************************************************/

        public void evaluate()
        {
            m_elementType.evaluate( this );
        }

        /***************************************************************************/

        public void updateValue( int _pin, LogicValue.Enum _value )
        {
            m_inputLines[ _pin ].Value = _value;
        }

        /***************************************************************************/

        private ILineCollection m_inputLines;

        private ILineCollection m_outputLines;

        private readonly ILibraryElementType m_elementType;

		private int m_modifier;

        private readonly int m_id;
  
        /***************************************************************************/
    }
}

/***************************************************************************/
