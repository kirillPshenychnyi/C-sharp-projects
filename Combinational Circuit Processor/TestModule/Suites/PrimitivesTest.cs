
/***************************************************************************/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/***************************************************************************/

using PortList = System.Collections.Generic.List< LogicalModel.Implementation.PortElement >;

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
            
            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Input, "b" );

            PortElement c = factory.createPortElement( PortDirection.Output, "c" );

            ILogicalElement and2 = factory.createLogicalElement( LibraryElementKind.Enum.AND, 2 );

            Utils.Evaluator evaluator = new Utils.Evaluator( and2, new PortList { c } );

            and2.makeConnection( a, 0, 0 );
            and2.makeConnection( b, 1, 0 );

            c.makeConnection( and2, 0, 0 );

            a.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( c.Value, LogicValue.Enum.Unknown );

            b.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( c.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( c.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            Assert.AreEqual( c.Value, LogicValue.Enum.Unknown );
        }

 	/***************************************************************************/

        [ TestMethod ]
        public void simple3AND()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Input, "b" );
            PortElement c = factory.createPortElement( PortDirection.Input, "c" );

            PortElement d = factory.createPortElement( PortDirection.Output, "d" );

            ILogicalElement and3 = factory.createLogicalElement( LibraryElementKind.Enum.AND, 3 );

            Utils.Evaluator evaluator = new Utils.Evaluator( and3, new PortList { d } );

            and3.makeConnection( a, 0, 0 );
            and3.makeConnection( b, 1, 0 );
            and3.makeConnection( c, 2, 0 );

            d.makeConnection( and3, 0, 0 );

		    a.Value = LogicValue.Enum.Unknown;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // U00
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Unknown;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 0U1
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            // X1U
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // X00
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X0X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
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
            evaluator.evaluate();
            // XX0
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 111
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 001
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 110
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 101
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );
        }


	/***************************************************************************/

        [ TestMethod ]
        public void simple3OR()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Input, "b" );
            PortElement c = factory.createPortElement( PortDirection.Input, "c" );

            PortElement d = factory.createPortElement( PortDirection.Output, "d" );

            ILogicalElement or3 = factory.createLogicalElement( LibraryElementKind.Enum.OR, 3 );

            Utils.Evaluator evaluator = new Utils.Evaluator( or3, new PortList { d } );

            or3.makeConnection( a, 0, 0 );
            or3.makeConnection( b, 1, 0 );
            or3.makeConnection( c, 2, 0 );

            d.makeConnection( or3, 0, 0 );

		    a.Value = LogicValue.Enum.Unknown;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // U00
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Unknown;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 0U1
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            // X1U
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // X00
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X0X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // 1XX
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X1X
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // XX1
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 000
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 001
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 110
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 101
            Assert.AreEqual( d.Value, LogicValue.Enum.High );
        }

 	/***************************************************************************/

        [ TestMethod ]
        public void simple3NAND()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Input, "b" );
            PortElement c = factory.createPortElement( PortDirection.Input, "c" );

            PortElement d = factory.createPortElement( PortDirection.Output, "d" );

            ILogicalElement nand3 = factory.createLogicalElement( LibraryElementKind.Enum.NAND, 3 );

            Utils.Evaluator evaluator = new Utils.Evaluator( nand3, new PortList { d } );

            nand3.makeConnection( a, 0, 0 );
            nand3.makeConnection( b, 1, 0 );
            nand3.makeConnection( c, 2, 0 );

            d.makeConnection( nand3, 0, 0 );

		    a.Value = LogicValue.Enum.Unknown;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // U00
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Unknown;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 0U1
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            // X1U
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // X00
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X0X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // 0XX
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X0X
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.DontCare;
		    b.Value = LogicValue.Enum.DontCare;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // XX0
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 111
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

		    a.Value = LogicValue.Enum.Low;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 001
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.High;
		    c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 110
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

		    a.Value = LogicValue.Enum.High;
		    b.Value = LogicValue.Enum.Low;
		    c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 101
            Assert.AreEqual( d.Value, LogicValue.Enum.High );
        }

	    /***************************************************************************/

        [ TestMethod ]
        public void simple3NOR()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Input, "b" );
            PortElement c = factory.createPortElement( PortDirection.Input, "c" );

            PortElement d = factory.createPortElement( PortDirection.Output, "d" );

            ILogicalElement nor3 = factory.createLogicalElement( LibraryElementKind.Enum.NOR, 3 );

            Utils.Evaluator evaluator = new Utils.Evaluator( nor3, new PortList { d } );

            nor3.makeConnection( a, 0, 0 );
            nor3.makeConnection( b, 1, 0 );
            nor3.makeConnection( c, 2, 0 );

            d.makeConnection( nor3, 0, 0 );

	        a.Value = LogicValue.Enum.Unknown;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // U00
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Unknown;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 0U1
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            // X1U
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // X00
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X0X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // 1XX
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X1X
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // XX1
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 000
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 001
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 110
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 101
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );
        }

 	    /***************************************************************************/

        [ TestMethod ]
        public void inverterTest()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Output, "b" );

            ILogicalElement inverter = factory.createLogicalElement( LibraryElementKind.Enum.Inverter, 1 );

            Utils.Evaluator evaluator = new Utils.Evaluator( inverter, new PortList { b } );

            inverter.makeConnection( a, 0, 0 );

            b.makeConnection( inverter, 0, 0 );

            a.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b.Value, LogicValue.Enum.Low );

            a.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b.Value, LogicValue.Enum.High );

            a.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            Assert.AreEqual( b.Value, LogicValue.Enum.Unknown );

		    a.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b.Value, LogicValue.Enum.DontCare );
        }

	    /***************************************************************************/

        [ TestMethod ]
        public void simple3XOR()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Input, "b" );
            PortElement c = factory.createPortElement( PortDirection.Input, "c" );

            PortElement d = factory.createPortElement( PortDirection.Output, "d" );

            ILogicalElement xor3 = factory.createLogicalElement( LibraryElementKind.Enum.XOR, 3 );

            Utils.Evaluator evaluator = new Utils.Evaluator( xor3, new PortList { d } ); 

            xor3.makeConnection( a, 0, 0 );
            xor3.makeConnection( b, 1, 0 );
            xor3.makeConnection( c, 2, 0 );

            d.makeConnection( xor3, 0, 0 );
          
	        a.Value = LogicValue.Enum.Unknown;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // U00
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Unknown;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 0U1
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            // X1U
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // X00
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X0X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // 11X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 000
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 001
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 011
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 111
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 110
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );
        }


	    /***************************************************************************/

        [ TestMethod ]
        public void simple3NXOR()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            PortElement a = factory.createPortElement( PortDirection.Input, "a" );
            PortElement b = factory.createPortElement( PortDirection.Input, "b" );
            PortElement c = factory.createPortElement( PortDirection.Input, "c" );

            PortElement d = factory.createPortElement( PortDirection.Output, "d" );

            ILogicalElement nxor3 = factory.createLogicalElement( LibraryElementKind.Enum.NXOR, 3 );

            Utils.Evaluator evaluator = new Utils.Evaluator( nxor3, new PortList { d } );

            nxor3.makeConnection( a, 0, 0 );
            nxor3.makeConnection( b, 1, 0 );
            nxor3.makeConnection( c, 2, 0 );

            d.makeConnection( nxor3, 0, 0 );

	        a.Value = LogicValue.Enum.Unknown;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // U00
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Unknown;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 0U1
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Unknown;
            evaluator.evaluate();
            // X1U
            Assert.AreEqual( d.Value, LogicValue.Enum.Unknown );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // X00
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.DontCare;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // X0X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.DontCare;
	        c.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            // 11X
            Assert.AreEqual( d.Value, LogicValue.Enum.DontCare );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            // 000
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.Low;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 001
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.Low;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 011
            Assert.AreEqual( d.Value, LogicValue.Enum.High );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            // 111
            Assert.AreEqual( d.Value, LogicValue.Enum.Low );

	        a.Value = LogicValue.Enum.High;
	        b.Value = LogicValue.Enum.High;
	        c.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
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
