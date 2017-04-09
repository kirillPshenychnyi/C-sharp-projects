
/*****************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;
	using EvalFunction = Action< API.ILogicalElement >;
	using CalculateLinesFunction = Func< int, int >;

    /***************************************************************************/

    public class ElementsFactory
    {
        /***************************************************************************/

        private ElementsFactory()
        {
            ms_currentID = 0;
        }

        /***************************************************************************/

        public static ElementsFactory getInstance()
        {
            if ( ms_instance == null )
			{
				ms_instance = new ElementsFactory();
				ms_instance.initialize();
			}
                
            return ms_instance;
        }

        /***************************************************************************/

        /* for internal testing only */
        public void reset()
        {
            ms_currentID = 0;
        }

        /***************************************************************************/

        public PortElement createPortElement( PortKind.Enum _kind )
        {
           return
                new PortElement(
                       _kind
                    ,   ms_currentID++
					,   ms_portElementTypes[ _kind ]
                );
            }

		/***************************************************************************/

        public ILogicalElement createLogicalElement(
				LibraryElementKind.Enum _kind
			,	int _modifier
		)
        {
			return new LogicalElement(
					ms_currentID++
				,	ms_elementTypes[ _kind ]
				,	_modifier
			);
        }

        /***************************************************************************/

        private void initialize()
        {
            ms_elementTypes = new System.Collections.Generic.Dictionary< LibraryElementKind.Enum, LibraryElementType >();
		    ms_portElementTypes = new System.Collections.Generic.Dictionary< PortKind.Enum, LibraryElementType >();

            /*think about lazy initialization*/
		    createPrimitiveElementType(
				    LibraryElementKind.Enum.AND
			    ,	LogicalFunctionsLibrary.primitiveEvaluator
		    );

		    createPrimitiveElementType(
				    LibraryElementKind.Enum.OR
			    ,	LogicalFunctionsLibrary.primitiveEvaluator
		    );

		    createPrimitiveElementType(
				    LibraryElementKind.Enum.XOR
			    ,	LogicalFunctionsLibrary.xor
		    );

		    createPrimitiveElementType(
				    LibraryElementKind.Enum.NAND
			    ,	LogicalFunctionsLibrary.primitiveEvaluator
		    );

		    createPrimitiveElementType(
				    LibraryElementKind.Enum.NOR
			    ,	LogicalFunctionsLibrary.primitiveEvaluator
		    );

		    createPrimitiveElementType(
				    LibraryElementKind.Enum.NXOR
			    ,	LogicalFunctionsLibrary.nxor
		    );

		    ms_elementTypes[ LibraryElementKind.Enum.Inverter ] = new LibraryElementType(
				    LibraryElementKind.Enum.Inverter
			    ,	LogicalFunctionsLibrary.not
			    ,	LogicalFunctionsLibrary.primitivesOutputsCalculator
			    ,	LogicalFunctionsLibrary.primitivesOutputsCalculator
			    ,	1
			    ,	1
		    );

		    createNotPrimitiveElementType(
				    LibraryElementKind.Enum.MUX
			    ,	LogicalFunctionsLibrary.mux
			    ,	LogicalFunctionsLibrary.muxInputsCalculator
			    ,	LogicalFunctionsLibrary.primitivesOutputsCalculator
		    );

		    createNotPrimitiveElementType(
				    LibraryElementKind.Enum.DMX
			    ,	LogicalFunctionsLibrary.dmx
			    ,	LogicalFunctionsLibrary.dmxInputsCalculator
			    ,	LogicalFunctionsLibrary.dmxOutputsCalculator
		    );

		    createNotPrimitiveElementType(
				    LibraryElementKind.Enum.ENC
			    ,	LogicalFunctionsLibrary.encoder
			    ,	LogicalFunctionsLibrary.encInputsCalculator
			    ,	LogicalFunctionsLibrary.primitivesInputsCalculator
		    );

		    createNotPrimitiveElementType(
				    LibraryElementKind.Enum.DC
			    ,	LogicalFunctionsLibrary.decoder
			    ,	LogicalFunctionsLibrary.dcInputsCalculator
			    ,	LogicalFunctionsLibrary.dcOutputsCalculator
		    );

		    createPortElementType(
				    PortKind.Enum.Input
			    ,	LogicalFunctionsLibrary.portElement
			    ,	LogicalFunctionsLibrary.return0Calculator
			    ,	LogicalFunctionsLibrary.primitivesOutputsCalculator
		    );

		    createPortElementType(
				    PortKind.Enum.Output
			    ,	LogicalFunctionsLibrary.portElement
			    ,	LogicalFunctionsLibrary.primitivesOutputsCalculator
			    ,	LogicalFunctionsLibrary.return0Calculator
		    );
	}

	/***************************************************************************/

     private void createPrimitiveElementType(
			LibraryElementKind.Enum _kind
		,	EvalFunction _evalFunction
	)
	{
		if ( !LibraryElementKind.isSimpleElement( _kind ) )
			throw new ArgumentException( 
                string.Format( Resoursers.Exceptions.Messages.nonPrimitiveElement, LibraryElementKind.toString( _kind ) ) 
            );

		ms_elementTypes[ _kind ] = new LibraryElementType(
				_kind
			,	_evalFunction
			,	LogicalFunctionsLibrary.primitivesInputsCalculator
			,	LogicalFunctionsLibrary.primitivesOutputsCalculator
			,	ms_minPrimitivesModifier
			,	ms_maxPrimitivesModifier
		);
	}

	/***************************************************************************/

	private void createNotPrimitiveElementType(
			LibraryElementKind.Enum _kind
		,	EvalFunction _evalFunction
		,	CalculateLinesFunction _inputsLinesCalculator
		,	CalculateLinesFunction _outputsLinesCalculator
	)
	{
		if ( LibraryElementKind.isSimpleElement( _kind ) )
                throw new ArgumentException(
                    string.Format( Resoursers.Exceptions.Messages.primitiveElement, LibraryElementKind.toString( _kind ) )
                );

            ms_elementTypes[ _kind ] = new LibraryElementType(
				_kind
			,	_evalFunction
			,	_inputsLinesCalculator
			,	_outputsLinesCalculator
			,	ms_minNotPrimitivesModifier
			,	ms_maxNotPrimitivesModifier
		);
	}

	/***************************************************************************/

	private void createPortElementType(
			PortKind.Enum _kind 
		,	EvalFunction _evalFunction
		,	CalculateLinesFunction _inputsLinesCalculator
		,	CalculateLinesFunction _outputsLinesCalculator
	    )
	    {
		    ms_portElementTypes[ _kind ] = new LibraryElementType(
				    LibraryElementKind.Enum.Port
			    ,	_evalFunction
			    ,	_inputsLinesCalculator
			    ,	_outputsLinesCalculator
			    ,	0
			    ,	0
		    );
	    }

        /***************************************************************************/

        private static ElementsFactory ms_instance;

        private int ms_currentID;

        private System.Collections.Generic.Dictionary< LibraryElementKind.Enum, LibraryElementType > ms_elementTypes;

        private System.Collections.Generic.Dictionary< PortKind.Enum, LibraryElementType > ms_portElementTypes;

        private const int ms_minPrimitivesModifier = 2;

        private const int ms_maxPrimitivesModifier = 8;

        private const int ms_minNotPrimitivesModifier = 1;

        private const int ms_maxNotPrimitivesModifier = 5;

        /***************************************************************************/
    }
}

/*****************************************************************************/

