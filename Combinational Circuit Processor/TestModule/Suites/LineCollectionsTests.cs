
/***************************************************************************/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicalModel.API;
using LogicalModel.Implementation;
using TestModule.Fakes;

/***************************************************************************/

namespace TestModule.Suites
{
    [ TestClass ]
    public class LineCollectionsTests
    {
        /***************************************************************************/

        [ TestMethod ]
        public void createEmptyLineCollection()
        {
            ILineCollection noLineCollection = new EmptyLineCollection();

            Assert.AreEqual( noLineCollection.Size, 0 );
        }

        /***************************************************************************/

        [ TestMethod ]
        [
            ExpectedException( typeof ( MemberAccessException )
          , Resoursers.Exceptions.Messages.addingLineToEmptyCollection )
        ]
        public void addToEmptyLineCollection()
        {
            ILineCollection noLineCollection = new EmptyLineCollection();
            ILine line = new FakeLine();

            noLineCollection[ 0 ] = line;
        }

        /***************************************************************************/

        [ TestMethod ]
        [
            ExpectedException( typeof( MemberAccessException )
          , Resoursers.Exceptions.Messages.gettingLineFromEmptyCollection )
        ]
        public void getNoLineCollection()
        {
            ILineCollection noLineCollection = new EmptyLineCollection();
            ILine line;

            line = noLineCollection[ 1 ];
        }

        /***************************************************************************/

        [ TestMethod ]
        public void createOneLineCollection()
        {
            ILine line = new FakeLine();
            ILineCollection oneLineCollection = new OneLineCollection( line );

            Assert.AreEqual( oneLineCollection.Size, 1 ); 
            Assert.AreEqual( oneLineCollection[ 0 ], line);
        }

        /***************************************************************************/

        [ TestMethod ]
        public void setOneLineCollection()
        {
            ILine line = new FakeLine();
            ILineCollection oneLineCollection = new OneLineCollection( line );

            ILine line2 = new FakeLine();
            oneLineCollection[ 0 ] = line2;

            Assert.AreEqual( oneLineCollection.Size, 1 );
            Assert.AreEqual( oneLineCollection[ 0 ], line2 );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void invalidSetOneLineCollection()
        {
            ILine line = new FakeLine();
            ILineCollection oneLineCollection = new OneLineCollection( line );

            ILine line2 = new FakeLine();

            oneLineCollection[ 1 ] = line2;
        }

        /***************************************************************************/

        [ TestMethod ]
        [
            ExpectedException( typeof( IndexOutOfRangeException )
          , Resoursers.Exceptions.Messages.wrongLineIndex )
        ]
        public void invalidGetOneLineCollection()
        {
            ILine line = new FakeLine();
            ILineCollection oneLineCollection = new OneLineCollection( line );

            ILine line2 = oneLineCollection[ -1 ];
        }

        /***************************************************************************/

        [ TestMethod ]
        public void createLineCollection()
        {
            ILineCollection lineCollection = new LineCollection( 3 );

            Assert.AreEqual( lineCollection.Size,  3) ;
            foreach( ILine line in lineCollection )
                Assert.IsNull( line );
            
            ILineCollection lineCollection2 = new LineCollection( 5 );

            Assert.AreEqual( lineCollection2.Size, 5 );
            for ( int i = 0; i < 5; ++i )
                Assert.IsNull( lineCollection2[ i ] );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void setLineCollection()
        {
            ILineCollection lineCollection = new LineCollection( 3 );

            ILine line1 = new FakeLine();
            ILine line2 = new FakeLine();
            ILine line3 = new FakeLine();

            lineCollection[ 0 ] = line1;

            Assert.AreEqual( lineCollection.Size, 3 );
            Assert.AreEqual( lineCollection[ 0 ], line1 );
            Assert.IsNull( lineCollection[ 1 ] );
            Assert.IsNull( lineCollection[ 2 ] );

            lineCollection[ 1 ] = line2;
            lineCollection[ 2 ] = line3;

            Assert.AreEqual( lineCollection.Size, 3 );
            Assert.AreEqual( lineCollection[ 0 ], line1 );
            Assert.AreEqual( lineCollection[ 1 ], line2 );
            Assert.AreEqual( lineCollection[ 2 ], line3 );
        }

        /***************************************************************************/

        [ TestMethod ]
        [
            ExpectedException( typeof( IndexOutOfRangeException )
          , Resoursers.Exceptions.Messages.wrongLineIndex )
        ]
        public void invalidSetLineCollection()
        {
            ILineCollection lineCollection = new LineCollection( 3 );

            ILine line = new FakeLine();

            lineCollection[ 3 ]  = line;
        }

        /***************************************************************************/

        [ TestMethod ]
        [
            ExpectedException( typeof( IndexOutOfRangeException )
          , Resoursers.Exceptions.Messages.wrongLineIndex )
        ]
        public void invalidLineCollection()
        {
            ILineCollection lineCollection = new LineCollection( 3 );

            ILine line = lineCollection[ -1 ];
        }

        /***************************************************************************/

        [ TestMethod ]
        [
            ExpectedException( typeof( ArgumentException )
          , Resoursers.Exceptions.Messages.wrongLineCollectionSize  )
        ]
        public void invalidLineCollectionSize()
        { 
            ILineCollection lineCollection = new LineCollection( -3 );         
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
