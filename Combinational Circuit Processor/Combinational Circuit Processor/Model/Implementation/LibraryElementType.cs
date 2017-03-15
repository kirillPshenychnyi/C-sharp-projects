
/***************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;
    using EvalFunction = Action< API.ILogicalElement >;
    using CalculateLinesFunction = Func< int, int >;

    /***************************************************************************/

    public class LibraryElementType : ILibraryElementType
    {
        /***************************************************************************/

		public LibraryElementType(
				LibraryElementKind.Enum _elementKind
			,	EvalFunction _evalFunction
			,	CalculateLinesFunction _calculateInputsFunction
			,	CalculateLinesFunction _calculateOutputsFuction
			,	int _minModifier
			,	int _maxModifier
		)
		{
			m_elementKind = _elementKind;
			m_evalFunction = _evalFunction;
			m_calculateInputsFunction = _calculateInputsFunction;
			m_calculateOutputsFuction = _calculateOutputsFuction;
			m_minModifier = _minModifier;
			m_maxModifier = _maxModifier;
		}

   		/***************************************************************************/

		public LibraryElementKind.Enum elementKind
		{
			get
			{
				return m_elementKind;
			}
		}

		/***************************************************************************/

		public void evaluate( ILogicalElement _element )
		{
			m_evalFunction( _element );
		}

        /***************************************************************************/

		public int calculateInputs( int _modifier )
		{
			checkModifier( _modifier );
			return m_calculateInputsFunction( _modifier );
		}

		/***************************************************************************/

		public int calculateOutputs( int _modifier )
		{
			checkModifier( _modifier );
			return m_calculateOutputsFuction( _modifier );
		}

        /***************************************************************************/

        public void checkModifier( int _modifier )
        {
            if( ( _modifier < m_minModifier ) || ( _modifier > m_maxModifier ) )
                throw new ArgumentException(
                   string.Format(
                          Resoursers.Exceptions.Messages.wrongInputsCount
                        , LibraryElementKind.toString( m_elementKind )
                        , _modifier
                        , m_minModifier
                    )
               );
        }

        /***************************************************************************/

		private LibraryElementKind.Enum m_elementKind;

		private EvalFunction m_evalFunction;

		private CalculateLinesFunction m_calculateInputsFunction;

		private CalculateLinesFunction m_calculateOutputsFuction;

		private int m_minModifier;

		private int m_maxModifier;

	/***************************************************************************/

    }
}

/***************************************************************************/