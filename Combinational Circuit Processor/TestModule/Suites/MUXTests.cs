
/***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;

/***************************************************************************/

namespace TestModule.Suites
{
    /***************************************************************************/

    using LogicalModel.API;
    using LogicalModel.Implementation;

    /***************************************************************************/

    [ TestClass ]
    public class MUXTests
    {
        [ TestMethod ]
        public void simple_2_1_Mux()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            factory.reset();

        	ILogicalElement mux = factory.createLogicalElement( LibraryElementKind.Enum.MUX, 1 );

            PortElement a_port = factory.createPortElement( PortKind.Enum.Input );
            PortElement b_port = factory.createPortElement( PortKind.Enum.Input );  
            PortElement address = factory.createPortElement( PortKind.Enum.Input );

            PortElement c_port = factory.createPortElement( PortKind.Enum.Output );

            mux.makeConnection( a_port, 0, 0 );
            mux.makeConnection( b_port, 1, 0 );
            mux.makeConnection( address, 2, 0 );

            c_port.makeConnection( mux, 1, 0 );

            address.Value = LogicValue.Enum.Low;
            a_port.Value = LogicValue.Enum.High;

            Assert.AreEqual( c_port.Value, LogicValue.Enum.Unknown );

            a_port.Value = LogicValue.Enum.Low;

            Assert.AreEqual( c_port.Value, LogicValue.Enum.Unknown );

            address.Value = LogicValue.Enum.High;

            Assert.AreEqual( c_port.Value, LogicValue.Enum.Unknown );

            b_port.Value = LogicValue.Enum.Low;
            Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );

            b_port.Value = LogicValue.Enum.High;
            Assert.AreEqual( c_port.Value, LogicValue.Enum.High );            
        }

        /***************************************************************************/

        [ TestMethod ]
        public void simple_4_1_Mux()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            factory.reset();

            ILogicalElement mux = factory.createLogicalElement( LibraryElementKind.Enum.MUX, 2 );

            PortElement a_port = factory.createPortElement( PortKind.Enum.Input );
            PortElement b_port = factory.createPortElement( PortKind.Enum.Input );
            PortElement c_port = factory.createPortElement( PortKind.Enum.Input );
            PortElement d_port = factory.createPortElement( PortKind.Enum.Input );

            PortElement addressLow  = factory.createPortElement( PortKind.Enum.Input );
            PortElement addressHigh = factory.createPortElement( PortKind.Enum.Input );

            PortElement e_port = factory.createPortElement( PortKind.Enum.Output );

            mux.makeConnection( a_port, 0, 0 );
            mux.makeConnection( b_port, 1, 0 );
            mux.makeConnection( c_port, 2, 0 );
            mux.makeConnection( d_port, 3, 0 );

            mux.makeConnection( addressLow, 4, 0 );
            mux.makeConnection( addressHigh, 5, 0 );

            e_port.makeConnection( mux, 1, 0 );
                      
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Unknown );
            
		    // 000U
		    // 00
            addressHigh.Value = LogicValue.Enum.Low;
            addressLow.Value = LogicValue.Enum.Low;
            a_port.Value = LogicValue.Enum.Unknown;
	        b_port.Value = LogicValue.Enum.Low;
	        c_port.Value = LogicValue.Enum.Low;
	        d_port.Value = LogicValue.Enum.Low;
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Unknown );

	        //dcba
	        //0000
	        //1X

	        addressHigh.Value = LogicValue.Enum.High;
            addressLow.Value = LogicValue.Enum.DontCare;
	        a_port.Value = LogicValue.Enum.Low;

            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

            // 01
            addressHigh.Value = LogicValue.Enum.Low;
            addressLow.Value = LogicValue.Enum.High;
            b_port.Value = LogicValue.Enum.High;
            Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

            //1000
            //1X

            addressHigh.Value = LogicValue.Enum.High;
            addressLow.Value = LogicValue.Enum.DontCare;
            d_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

	        //1100
	        // 1X
	        c_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        //11X0
	        // 1X

	        b_port.Value = LogicValue.Enum.DontCare;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        //11XX
	        // 1X

	        a_port.Value = LogicValue.Enum.DontCare;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        //11XX
	        //X0

	        addressHigh.Value = LogicValue.Enum.DontCare;
            addressLow.Value = LogicValue.Enum.Low;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

	        //11X0
	        //X0

	        a_port.Value = LogicValue.Enum.Low;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

	        // 11X1
	        // X0

	        a_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        // 11X1
	        // XX

	        addressLow.Value = LogicValue.Enum.DontCare;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

	        // 1101
	        // XX

	        b_port.Value = LogicValue.Enum.Low;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare );

	        // 1111
	        // XX

	        b_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        // 0000
	        // XX

	        a_port.Value = LogicValue.Enum.Low;
	        b_port.Value = LogicValue.Enum.Low;
	        c_port.Value = LogicValue.Enum.Low;
	        d_port.Value = LogicValue.Enum.Low;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

	        // 0000
	        // 00

	        addressLow.Value = LogicValue.Enum.Low;
	        addressHigh.Value = LogicValue.Enum.Low;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

	        //0001
	        //00

	        a_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        //0001
	        //01

	        addressLow.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

	        //0011
	        //01

	        b_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        //0011
	        //11

	        addressHigh.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );
	
	        //1011
	        //11

	        d_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

	        //1011
	        //10

	        addressLow.Value = LogicValue.Enum.Low;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

	        //1111
	        //10

	        c_port.Value = LogicValue.Enum.High;
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.High );
        }

        /***************************************************************************/
    }
}


/***************************************************************************/
