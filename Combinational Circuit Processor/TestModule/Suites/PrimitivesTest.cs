
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

            ILogicalElement and2 = factory.createLogicalElement( LibraryElementKind.Enum.AND, 2 );

            and2.makeConnection( a, 0, 0 );
            and2.makeConnection( b, 1, 0 );

            c.makeConnection( and2, 0, 0 );

            a.Value = LogicValue.Enum.Low;
            Assert.AreEqual( c.Value, LogicValue.Enum.Unknown );

            b.Value = LogicValue.Enum.High;
            Assert.AreEqual( c.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.High;
            Assert.AreEqual( c.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.Unknown;
            Assert.AreEqual( c.Value, LogicValue.Enum.Unknown );
        }

 	/***************************************************************************/

        [ TestMethod ]
        public void simple3AND()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );
            PortElement c = factory.createPortElement( PortKind.Enum.Input );

            PortElement d = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement and3 = factory.createLogicalElement( LibraryElementKind.Enum.AND, 3 );

            and3.makeConnection( a, 0, 0 );
            and3.makeConnection( b, 1, 0 );
            and3.makeConnection( c, 2, 0 );

            d.makeConnection( and3, 0, 0 );

		    a.Value = LogicValue.Enum.Unknown;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
		    // U00
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Unknown;
		    c.Value = LogicValue.Enum.High;
		    // 0U1
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Unknown;
		    // X1U
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
		    // X00
		    Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.DontCare;
		    // X0X
		    Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.DontCare;
		    // 0XX
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.DontCare;
		    // X0X
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.Low;
		    // XX0
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
		    // 111
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
		    // 001
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Low;
		    // 110
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
		    // 101
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );
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

            ILogicalElement or3 = factory.createLogicalElement( LibraryElementKind.Enum.OR, 3 );

            or3.makeConnection( a, 0, 0 );
            or3.makeConnection( b, 1, 0 );
            or3.makeConnection( c, 2, 0 );

            d.makeConnection( or3, 0, 0 );

		    a.Value = LogicValue.Enum.Unknown;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
		    // U00
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Unknown;
		    c.Value = LogicValue.Enum.High;
		    // 0U1
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Unknown;
		    // X1U
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
		    // X00
		    Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.DontCare;
		    // X0X
		    Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.DontCare;
		    // 1XX
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.DontCare;
		    // X1X
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.High;
		    // XX1
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
		    // 000
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
		    // 001
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Low;
		    // 110
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
		    // 101
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );
        }

 	/***************************************************************************/

        [ TestMethod ]
        public void simple3NAND()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );
            PortElement c = factory.createPortElement( PortKind.Enum.Input );

            PortElement d = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement nand3 = factory.createLogicalElement( LibraryElementKind.Enum.NAND, 3 );

            nand3.makeConnection( a, 0, 0 );
            nand3.makeConnection( b, 1, 0 );
            nand3.makeConnection( c, 2, 0 );

            d.makeConnection( nand3, 0, 0 );

		    a.Value = LogicValue.Enum.Unknown;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
		    // U00
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Unknown;
		    c.Value = LogicValue.Enum.High;
		    // 0U1
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Unknown;
		    // X1U
		    Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
		    // X00
		    Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.DontCare;
		    // X0X
		    Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.DontCare;
		    // 0XX
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.DontCare;
		    // X0X
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.Low;
		    // XX0
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
		    // 111
		    Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
		    // 001
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Low;
		    // 110
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
		    // 101
		    Assert.AreEqual( d.Value, LogicValue.Enum.High );
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

            ILogicalElement nor3 = factory.createLogicalElement( LibraryElementKind.Enum.NOR, 3 );

            nor3.makeConnection( a, 0, 0 );
            nor3.makeConnection( b, 1, 0 );
            nor3.makeConnection( c, 2, 0 );

            d.makeConnection( nor3, 0, 0 );

	        a.Value = LogicValue.Enum.Unknown;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // U00
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Unknown;
	        c.Value = LogicValue.Enum.High;
	        // 0U1
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Unknown;
	        // X1U
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // X00
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.DontCare;
	        // X0X
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.DontCare;
	        // 1XX
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.DontCare;
	        // X1X
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.High;
	        // XX1
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // 000
	        Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
	        // 001
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Low;
	        // 110
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
	        // 101
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );
        }

 	    /***************************************************************************/

        [ TestMethod ]
        public void inverterTest()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement inverter = factory.createLogicalElement( LibraryElementKind.Enum.Inverter, 1 );

            inverter.makeConnection( a, 0, 0 );

            b.makeConnection( inverter, 0, 0 );

            a.Value = LogicValue.Enum.High;
            Assert.AreEqual( b.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.Low;
            Assert.AreEqual( b.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.Unknown;
            Assert.AreEqual( b.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
        	Assert.AreEqual( b.Value, LogicValue.Enum.DontCare );
        }

	    /***************************************************************************/

        [ TestMethod ]
        public void simple3XOR()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );
            PortElement c = factory.createPortElement( PortKind.Enum.Input );

            PortElement d = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement xor3 = factory.createLogicalElement( LibraryElementKind.Enum.XOR, 3 );

            xor3.makeConnection( a, 0, 0 );
            xor3.makeConnection( b, 1, 0 );
            xor3.makeConnection( c, 2, 0 );

            d.makeConnection( xor3, 0, 0 );
          
	        a.Value = LogicValue.Enum.Unknown;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // U00
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Unknown;
	        c.Value = LogicValue.Enum.High;
	        // 0U1
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Unknown;
	        // X1U
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // X00
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.DontCare;
	        // X0X
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.DontCare;
	        // 11X
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // 000
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
	        // 001
	        Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
	        // 011
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
	        // 111
	        Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Low;
	        // 110
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );
        }


	    /***************************************************************************/

        [ TestMethod ]
        public void simple3NXOR()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortKind.Enum.Input );
            PortElement b = factory.createPortElement( PortKind.Enum.Input );
            PortElement c = factory.createPortElement( PortKind.Enum.Input );

            PortElement d = factory.createPortElement( PortKind.Enum.Output );

            ILogicalElement nxor3 = factory.createLogicalElement( LibraryElementKind.Enum.NXOR, 3 );

            nxor3.makeConnection( a, 0, 0 );
            nxor3.makeConnection( b, 1, 0 );
            nxor3.makeConnection( c, 2, 0 );

            d.makeConnection( nxor3, 0, 0 );

	        a.Value = LogicValue.Enum.Unknown;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // U00
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Unknown;
	        c.Value = LogicValue.Enum.High;
	        // 0U1
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Unknown;
	        // X1U
	        Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // X00
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.DontCare;
	        // X0X
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.DontCare;
	        // 11X
	        Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
	        // 000
	        Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
	        // 001
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
	        // 011
	        Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
	        // 111
	        Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Low;
	        // 110
	        Assert.AreEqual( d.Value, LogicValue.Enum.High );
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

            ILogicalElement nand = factory.createLogicalElement( LibraryElementKind.Enum.AND, 1 );
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

            ILogicalElement nand = factory.createLogicalElement( LibraryElementKind.Enum.DC, 0 );
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
