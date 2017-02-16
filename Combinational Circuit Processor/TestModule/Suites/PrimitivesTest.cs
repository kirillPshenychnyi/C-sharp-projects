
/***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

/***************************************************************************/

namespace TestModule.Suites
{
    /***************************************************************************/

    using LogicalModel.API;
    using LogicalModel.Implementation;
   
    /***************************************************************************/

    [ TestClass ]
    public class PrimitivesTest
    {
        /***************************************************************************/

        [ TestMethod ]
        public void simple2AND()
        {
            ElementsFactory factory = ElementsFactory.getInstance();
            factory.reset();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );

            PortElement c = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement and2 = factory.createPrimitive( LibraryElementKind.Enum.AND, 2 );

            and2.makeConnection( a, 0, 0 );
            and2.makeConnection( b, 1, 0 );

            c.makeConnection( and2, 0, 0 );

            a.Value = LogicValue.Enum.Low;
            Assert.AreEqual( c.Value, LogicValue.Enum.Low );

            b.Value = LogicValue.Enum.High;
            Assert.AreEqual( c.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.High;
            Assert.AreEqual( c.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.Unknown;
            Assert.AreEqual( c.Value, LogicValue.Enum.Unknown );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void simple3OR()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );
            PortElement c = factory.createPortElement( PortKind.Enum.Input );

            PortElement d = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement or3 = factory.createPrimitive( LibraryElementKind.Enum.OR, 3 );

            or3.makeConnection( a, 0, 0 );
            or3.makeConnection( b, 1, 0 );
            or3.makeConnection( c, 2, 0 );

            d.makeConnection( or3, 0, 0 );

            a.Value = LogicValue.Enum.High;
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.Low;
            b.Value = LogicValue.Enum.Low;
            c.Value = LogicValue.Enum.Low;

            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.Unknown;
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void simple3NAND()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );

            PortElement c = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement and2 = factory.createPrimitive( LibraryElementKind.Enum.NAND, 2 );

            and2.makeConnection( a, 0, 0 );
            and2.makeConnection( b, 1, 0 );

            c.makeConnection( and2, 0, 0 );

            a.Value = LogicValue.Enum.Low;
            Assert.AreEqual( c.Value, LogicValue.Enum.High );

            b.Value = LogicValue.Enum.High;
            Assert.AreEqual( c.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.High;
            Assert.AreEqual( c.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.Unknown;
            Assert.AreEqual( c.Value, LogicValue.Enum.Unknown );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void simple3NOR()
        {

            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );
            PortElement c = factory.createPortElement( PortKind.Enum.Input );

            PortElement d = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement or3 = factory.createPrimitive( LibraryElementKind.Enum.NOR, 3 );

            or3.makeConnection( a, 0, 0 );
            or3.makeConnection( b, 1, 0 );
            or3.makeConnection( c, 2, 0 );

            d.makeConnection( or3, 0, 0 );

            a.Value = LogicValue.Enum.High;
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.Low;
            b.Value = LogicValue.Enum.Low;
            c.Value = LogicValue.Enum.Low;

            Assert.AreEqual( d.Value, LogicValue.Enum.High );
            
            a.Value = LogicValue.Enum.Unknown;
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void inverterTest()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement inverter = factory.createInverter();

            inverter.makeConnection( a, 0, 0 );

            b.makeConnection( inverter, 0, 0 );

            a.Value = LogicValue.Enum.High;
            Assert.AreEqual( b.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.Low;
            Assert.AreEqual( b.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.Unknown;
            Assert.AreEqual( b.Value, LogicValue.Enum.Unknown );
        }

        /***************************************************************************/

        [ TestMethod ]
        [
           ExpectedException( typeof( ArgumentException )
         , Resoursers.Exceptions.Messages.wrongInputsCount )
        ]
        public void wrongInputsCount()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            ILogicalElement nand = factory.createPrimitive( LibraryElementKind.Enum.AND, 1 );
        }

        /***************************************************************************/

        [ TestMethod ]
        [
           ExpectedException( typeof( ArgumentException )
         , Resoursers.Exceptions.Messages.nonPrimitiveElement )
        ]
        public void nonPrimitive()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            ILogicalElement nand = factory.createPrimitive( LibraryElementKind.Enum.DC, 0 );
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
