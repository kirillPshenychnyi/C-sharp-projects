/***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;

/***************************************************************************/

namespace TestModule.Suites
{
	/***************************************************************************/

    using LogicalModel.API;
    using LogicalModel.Implementation;

    /***************************************************************************/
	
	public class DCTests
	{
        /***************************************************************************/

        [ TestMethod ]
        public void simple_2_4_DC()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            factory.reset();

            ILogicalElement dc = factory.createLogicalElement( LibraryElementKind.Enum.DC, 2 );

            PortElement addressLow  = factory.createPortElement( PortDirection.Input, "a0" );
            PortElement addressHigh = factory.createPortElement( PortDirection.Input, "a1" );

		    PortElement enable = factory.createPortElement( PortDirection.Input, "en" );

            PortElement b_port = factory.createPortElement( PortDirection.Output, "b" );
            PortElement c_port = factory.createPortElement( PortDirection.Output, "c" );
            PortElement d_port = factory.createPortElement( PortDirection.Output, "d" );
            PortElement e_port = factory.createPortElement( PortDirection.Output, "e" );

            dc.makeConnection( addressLow, 0, 0 );
	        dc.makeConnection( addressHigh, 1, 0 );

	        dc.makeConnection( enable, 2, 0 );

            b_port.makeConnection( dc, 0, 0 );
            c_port.makeConnection( dc, 0, 1 );
            d_port.makeConnection( dc, 0, 2 );
            e_port.makeConnection( dc, 0, 3 );
                     
	        Assert.AreEqual( b_port.Value, LogicValue.Enum.Unknown ); 
 	        Assert.AreEqual( c_port.Value, LogicValue.Enum.Unknown );
	        Assert.AreEqual( d_port.Value, LogicValue.Enum.Unknown );
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Unknown );
            
	        // enable = X

	        enable.Value = LogicValue.Enum.DontCare;
	        addressHigh.Value = LogicValue.Enum.Low;
            addressLow.Value = LogicValue.Enum.Low;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.DontCare );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

		    addressHigh.Value = LogicValue.Enum.High;
           	addressLow.Value = LogicValue.Enum.High;
            
		    Assert.AreEqual( b_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.DontCare );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

		    // enable low

		    enable.Value = LogicValue.Enum.Low;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    addressHigh.Value = LogicValue.Enum.DontCare;
           	addressLow.Value = LogicValue.Enum.DontCare;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    //enable high
		    // 00

		    enable.Value = LogicValue.Enum.High;

		    addressHigh.Value = LogicValue.Enum.Low;
           	addressLow.Value = LogicValue.Enum.Low;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.High ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    addressHigh.Value = LogicValue.Enum.Low;
   	        addressLow.Value = LogicValue.Enum.High;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.High );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    // 10

		    addressHigh.Value = LogicValue.Enum.High;
           	addressLow.Value = LogicValue.Enum.Low;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.High );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

		    // 11

		    addressHigh.Value = LogicValue.Enum.High;
           	addressLow.Value = LogicValue.Enum.High;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

		    // X1

		    addressHigh.Value = LogicValue.Enum.DontCare;
           	addressLow.Value = LogicValue.Enum.High;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.Low );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

		    // XX

		    addressHigh.Value = LogicValue.Enum.DontCare;
           	addressLow.Value = LogicValue.Enum.DontCare;

		    Assert.AreEqual( b_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( c_port.Value, LogicValue.Enum.DontCare );
		    Assert.AreEqual( d_port.Value, LogicValue.Enum.DontCare );
 		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );
		}

        /***************************************************************************/
    }
}
