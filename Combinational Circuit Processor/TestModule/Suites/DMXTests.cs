
/***************************************************************************/

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
	public class DMXTests
    {
        /***************************************************************************/

        [ TestMethod ]
        public void simple_1_4_DMX()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            factory.reset();

            ILogicalElement dmx = factory.createLogicalElement( LibraryElementKind.Enum.DMX, 2 );

            PortElement addressLow  = factory.createPortElement( PortKind.Enum.Input );
            PortElement addressHigh = factory.createPortElement( PortKind.Enum.Input );

            PortElement data = factory.createPortElement( PortKind.Enum.Input );

		    PortElement enable = factory.createPortElement( PortKind.Enum.Input );

            PortElement b_port = factory.createPortElement( PortKind.Enum.Output );
            PortElement c_port = factory.createPortElement( PortKind.Enum.Output );
            PortElement d_port = factory.createPortElement( PortKind.Enum.Output );
            PortElement e_port = factory.createPortElement( PortKind.Enum.Output );

            Utils.Evaluator evaluator = new Utils.Evaluator( dmx, new PortList { b_port,c_port, d_port, e_port } );

            dmx.makeConnection( addressLow, 0, 0 );
	        dmx.makeConnection( addressHigh, 1, 0 );

	        dmx.makeConnection( data, 2, 0 );

	        dmx.makeConnection( enable, 3, 0 );

            b_port.makeConnection( dmx, 0, 0 );
            c_port.makeConnection( dmx, 0, 1 );
            d_port.makeConnection( dmx, 0, 2 );
            e_port.makeConnection( dmx, 0, 3 );
                     
	        Assert.AreEqual( b_port.Value, LogicValue.Enum.Unknown ); 
 	        Assert.AreEqual( c_port.Value, LogicValue.Enum.Unknown );
	        Assert.AreEqual( d_port.Value, LogicValue.Enum.Unknown );
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Unknown );
            
		    // enable = X

		    enable.Value = LogicValue.Enum.DontCare;
		    addressHigh.Value = LogicValue.Enum.Low;
           	addressLow.Value = LogicValue.Enum.Low;
            data.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    addressHigh.Value = LogicValue.Enum.High;
           	addressLow.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );
            
		    // enable low

		    enable.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    addressHigh.Value = LogicValue.Enum.DontCare;
           	addressLow.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    //enable high
		    // 00

		    enable.Value = LogicValue.Enum.High;

		    addressHigh.Value = LogicValue.Enum.Low;
           	addressLow.Value = LogicValue.Enum.Low;

		    data.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );


		    data.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.High );
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    // 01

		    addressHigh.Value = LogicValue.Enum.Low;
            addressLow.Value = LogicValue.Enum.High;

		    data.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.High );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    // 10

		    addressHigh.Value = LogicValue.Enum.High;
           	addressLow.Value = LogicValue.Enum.Low;

		    data.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.High );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.DontCare );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    // 11

		    addressHigh.Value = LogicValue.Enum.High;
           	addressLow.Value = LogicValue.Enum.High;

		    data.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

		    data.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

		    // X1

		    addressHigh.Value = LogicValue.Enum.DontCare;
           	addressLow.Value = LogicValue.Enum.High;

		    data.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

		    data.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

		    // XX

		    addressHigh.Value = LogicValue.Enum.DontCare;
           	addressLow.Value = LogicValue.Enum.DontCare;

		    data.Value = LogicValue.Enum.Low;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    data.Value = LogicValue.Enum.High;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.DontCare );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

		    data.Value = LogicValue.Enum.DontCare;
            evaluator.evaluate();
            Assert.AreEqual( b_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.DontCare );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

        /***************************************************************************/

		}
    }
}

/***************************************************************************/
