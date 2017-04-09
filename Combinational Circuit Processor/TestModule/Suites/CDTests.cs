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
	public class CDTests
	{
        /***************************************************************************/

        [ TestMethod ]
        public void simple_4_2_CD()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            factory.reset();

            ILogicalElement cd = factory.createLogicalElement( LibraryElementKind.Enum.ENC, 2 );

		    PortElement a_port = factory.createPortElement( PortKind.Enum.Input );
	     	PortElement b_port = factory.createPortElement( PortKind.Enum.Input );
	  	    PortElement c_port = factory.createPortElement( PortKind.Enum.Input );
         	PortElement d_port = factory.createPortElement( PortKind.Enum.Input );

            PortElement enable  = factory.createPortElement( PortKind.Enum.Input );

            PortElement e_port = factory.createPortElement( PortKind.Enum.Output );
            PortElement f_port = factory.createPortElement( PortKind.Enum.Output );

            Utils.Evaluator evaluator = new Utils.Evaluator( cd, new PortList { e_port, f_port } );

            cd.makeConnection( a_port, 0, 0 );
	        cd.makeConnection( b_port, 1, 0 );
	        cd.makeConnection( c_port, 2, 0 );
	        cd.makeConnection( d_port, 3, 0 );

	        cd.makeConnection( enable, 4, 0 );

            e_port.makeConnection( cd, 0, 0 );
            f_port.makeConnection( cd, 0, 1 );         
                
	        Assert.AreEqual( e_port.Value, LogicValue.Enum.Unknown ); 
 	        Assert.AreEqual( f_port.Value, LogicValue.Enum.Unknown );
                
		    // enable = X

		    enable.Value = LogicValue.Enum.DontCare;
		    a_port.Value = LogicValue.Enum.Low;
           	b_port.Value = LogicValue.Enum.Low;
		    c_port.Value = LogicValue.Enum.Low;
           	d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
		    Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.DontCare );

		    a_port.Value = LogicValue.Enum.High;
           	b_port.Value = LogicValue.Enum.High;
		    c_port.Value = LogicValue.Enum.High;
            d_port.Value = LogicValue.Enum.High;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.DontCare );

		    // enable low

		    enable.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.Low );

		    a_port.Value = LogicValue.Enum.High;
           	b_port.Value = LogicValue.Enum.High;
		    c_port.Value = LogicValue.Enum.High;
           	d_port.Value = LogicValue.Enum.High;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.Low );

		    //enable high
		    // 1XXX

		    enable.Value = LogicValue.Enum.High;

		    a_port.Value = LogicValue.Enum.DontCare;
            b_port.Value = LogicValue.Enum.DontCare;
		    c_port.Value = LogicValue.Enum.DontCare;
            d_port.Value = LogicValue.Enum.High;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.High ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.High );

		    // 1010
          
		    a_port.Value = LogicValue.Enum.Low;
		    b_port.Value = LogicValue.Enum.High;
		    c_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.High ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.High );

		    // 01XX

		    a_port.Value = LogicValue.Enum.DontCare;
           	b_port.Value = LogicValue.Enum.DontCare;
		    c_port.Value = LogicValue.Enum.High;
           	d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.High );

		    // 0111

		    a_port.Value = LogicValue.Enum.High;
           	b_port.Value = LogicValue.Enum.High;
		    c_port.Value = LogicValue.Enum.High;
            d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.High );

		    // 001X

		    a_port.Value = LogicValue.Enum.DontCare;
           	b_port.Value = LogicValue.Enum.High;
		    c_port.Value = LogicValue.Enum.Low;
            d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.High ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.Low );

		    // 0011

		    a_port.Value = LogicValue.Enum.High;
           	b_port.Value = LogicValue.Enum.High;
		    c_port.Value = LogicValue.Enum.Low;
           	d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.High ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.Low );

		    // 0001

		    a_port.Value = LogicValue.Enum.High;
           	b_port.Value = LogicValue.Enum.Low;
		    c_port.Value = LogicValue.Enum.Low;
           	d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.Low ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.Low );

		    // 0X01

		    a_port.Value = LogicValue.Enum.High;
           	b_port.Value = LogicValue.Enum.Low;
		    c_port.Value = LogicValue.Enum.DontCare;
           	d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.DontCare );
          
            // 0000

		    a_port.Value = LogicValue.Enum.Low;
           	b_port.Value = LogicValue.Enum.Low;
		    c_port.Value = LogicValue.Enum.Low;
           	d_port.Value = LogicValue.Enum.Low;

            evaluator.evaluate();
            Assert.AreEqual( e_port.Value, LogicValue.Enum.DontCare ); 
		    Assert.AreEqual( f_port.Value, LogicValue.Enum.DontCare );

        /***************************************************************************/

		}
    }
}

/***************************************************************************/