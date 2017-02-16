
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

            ILogicalElement mux = factory.createMUX( 2 );

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

            Assert.AreEqual( c_port.Value, LogicValue.Enum.High );

            a_port.Value = LogicValue.Enum.Low;

            Assert.AreEqual( c_port.Value, LogicValue.Enum.Low );

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

            ILogicalElement mux = factory.createMUX(4);

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

            mux.makeConnection( addressHigh, 4, 0 );
            mux.makeConnection( addressLow, 5, 0 );

            e_port.makeConnection( mux, 1, 0 );
                      
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Unknown );
            
            // 00
            addressHigh.Value = LogicValue.Enum.Low;
            addressLow.Value = LogicValue.Enum.Low;
            a_port.Value = LogicValue.Enum.High;
            Assert.AreEqual( e_port.Value, LogicValue.Enum.High );
            a_port.Value = LogicValue.Enum.Low;
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );
    
            // 01
            addressLow.Value = LogicValue.Enum.High;
            b_port.Value = LogicValue.Enum.High;
            Assert.AreEqual( e_port.Value, LogicValue.Enum.High );

            // 10
            addressHigh.Value = LogicValue.Enum.High;
            addressLow.Value = LogicValue.Enum.Low;
            c_port.Value = LogicValue.Enum.Low;
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low );

            // 11
            addressLow.Value = LogicValue.Enum.High;
            d_port.Value = LogicValue.Enum.High;
            Assert.AreEqual( d_port.Value, LogicValue.Enum.High );
        }

        /***************************************************************************/
    }
}


/***************************************************************************/
