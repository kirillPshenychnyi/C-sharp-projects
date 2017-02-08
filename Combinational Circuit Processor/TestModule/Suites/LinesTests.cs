
/***************************************************************************/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicalModel.API;
using LogicalModel.Implementation;
using TestModule.Fakes;
using System.Collections.Generic;

/***************************************************************************/

namespace TestModule.Suites
{
    /***************************************************************************/

    using PinsSet = HashSet< int >;

    /***************************************************************************/

    [TestClass ]
    public class LinesTests
    {
        /***************************************************************************/

        [ TestMethod ]
        public void createLine()
        {
            FakeElement sourceElement = new FakeElement();
  
            ILine line = new Line( sourceElement, 0 );

            Assert.AreEqual( line.SourceElement.Item1, sourceElement );

            Assert.AreEqual( line.SourceElement.Item2, 0 );

            Assert.AreEqual( line.ElementsConnected, 0 );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void addElement()
        {
            ILogicalElement sourceElement = new Fakes.FakeElement();

            ILine line = new Line( sourceElement, 0 );

            Fakes.FakeElement element = new Fakes.FakeElement();
            
            line.addConnection( element, 0 );
            line.addConnection( element, 1 );

            Assert.AreEqual( line.ElementsConnected, 1 );

            PinsSet set = line.getConnections( element );

            Assert.AreEqual( set.Count, 2 );
            Assert.IsTrue( set.Contains( 0 ) );
            Assert.IsTrue(set.Contains( 1 ) );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void removeConnections()
        {
            ILogicalElement sourceElement = new Fakes.FakeElement();

            ILine line = new Line( sourceElement, 0 );

            ILine line2 = new Line( sourceElement, 0 );

            Fakes.FakeElement element = new Fakes.FakeElement();

            line.addConnection( element, 0 );
            line.addConnection( element, 1 );

            line.removeConnection( element );

            Assert.AreEqual( line.ElementsConnected, 0 );

            line2.addConnection( element,  1 );
            line2.addConnection( element,  3 );

            line2.removeConnection( element, 1 );
            line2.removeConnection( element,  3 );

            Assert.AreEqual( line2.ElementsConnected, 0 );
        }

        /***************************************************************************/

        [ TestMethod ]
        [ 
            ExpectedException( typeof( ArgumentException )
          , Resoursers.Exceptions.Messages.duplicateConnection ) 
        ]
        public void duplicateConnection()
        {
            ILogicalElement sourceElement = new Fakes.FakeElement();

            ILine line = new Line( sourceElement, 0 );
            
            Fakes.FakeElement element = new Fakes.FakeElement();

            line.addConnection( element, 0 );
            line.addConnection( element, 0 );
        }

        /***************************************************************************/

        [ TestMethod ]
        [
           ExpectedException( typeof( ArgumentException )
         , Resoursers.Exceptions.Messages.combinationalFeedback )
        ]
        public void combinationalFeedback()
        {
            ILogicalElement sourceElement = new Fakes.FakeElement();

            ILine line = new Line( sourceElement, 0 );

            line.addConnection( sourceElement, 1 );
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
