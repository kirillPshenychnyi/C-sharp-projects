
/*****************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;

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
                ms_instance = new ElementsFactory();

            return ms_instance;
        }

        /***************************************************************************/

        /* for internal testing only */
        public void reset()
        {
            ms_currentID = 0;
        }

        /***************************************************************************/

        public ILogicalElement createMUX( int _inputsCount )
        {
           checkPortCounts( _inputsCount, 2, LibraryElementKind.Enum.MUX );

           double addressCount = Math.Log( _inputsCount, 2.0 );

           if ( Math.Floor( addressCount ) - addressCount != 0 )
                throw new ArgumentException( 
                    string.Format( 
                            Resoursers.Exceptions.Messages.wrongCorrespondOfIOPorts
                        ,   LibraryElementKind.toString( LibraryElementKind.Enum.MUX )
                        ,   _inputsCount
                        ,   1
                     )
                );

            int inputsSize = _inputsCount + ( int )addressCount;

            ILine outputLine = new Line();
            LineCollection inputs = new LineCollection( inputsSize );
            OneLineCollection outputs = new OneLineCollection( outputLine );

            return 
                new LogicalElement(
                      ms_currentID++
                    , LibraryElementKind.Enum.MUX
                    , LogicalFunctionsLibrary.mux
                    , LogicalFunctionsLibrary.adressElementModify
                    , inputs
                    , outputs
                );

        }

        /***************************************************************************/

        public ILogicalElement createPrimitive( LibraryElementKind.Enum _kind, int _inputs )
        {
            if ( !LibraryElementKind.isSimpleElement( _kind ) )
                throw new ArgumentException(
                        string.Format(
                            Resoursers.Exceptions.Messages.nonPrimitiveElement
                        ,   LibraryElementKind.toString( _kind )
                        )
                 );
       
            checkPortCounts( _inputs, 2, _kind );

            ILine outputLine = new Line();
            LineCollection inputs = new LineCollection( _inputs );
            OneLineCollection outputs = new OneLineCollection( outputLine );

            return 
                new LogicalElement(
                      ms_currentID++
                    , _kind
                    , LogicalFunctionsLibrary.primitiveEvaluator
                    , LogicalFunctionsLibrary.primitivesModifier
                    , inputs
                    , outputs
                );
        }

        /***************************************************************************/

        public ILogicalElement createInverter()
        {
            return
                new LogicalElement(
                      ms_currentID++
                    , LibraryElementKind.Enum.Inverter
                    , LogicalFunctionsLibrary.not
                    , LogicalFunctionsLibrary.primitivesModifier
                    , new OneLineCollection( new Line() )
                    , new OneLineCollection( new Line() )
                );
        }

        /***************************************************************************/

        public PortElement createPortElement( PortKind.Enum _kind )
        {
            ILineCollection inputLines;
            if ( _kind == PortKind.Enum.Output )
                inputLines = new OneLineCollection( new Line() );
            else
                inputLines = new EmptyLineCollection();

           return
                new PortElement(
                       _kind
                    ,   ms_currentID++
                    ,   inputLines
                    ,   new OneLineCollection( new Line() )
                );
        }

        /***************************************************************************/

        void checkPortCounts( int _inputs, int _minCount, LibraryElementKind.Enum _kind )
        {             
            if ( _inputs < _minCount )
                throw new ArgumentException(
                    string.Format(
                            Resoursers.Exceptions.Messages.wrongInputsCount
                        ,   LibraryElementKind.toString( _kind )
                        ,   _inputs
                        ,   _minCount
                     )
                );
        }

        /***************************************************************************/

        private static ElementsFactory ms_instance;

        private int ms_currentID;

        /***************************************************************************/
    }
}

/*****************************************************************************/

