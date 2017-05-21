
/***************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;

/***************************************************************************/

namespace TestModule.Suites
{
    /***************************************************************************/

    using LogicalModel.Implementation;
    using LogicalModel.API;

    /***************************************************************************/

    [ TestClass ]
    public class PortElementsTest
    {
        /***************************************************************************/

        [ TestMethod ]
        public void simpleInputPortElement()
        {
            ElementsFactory factory = ElementsFactory.getInstance();

            factory.reset();

            PortElement portElement = factory.createPortElement( PortDirection.Input, "port" );

            Assert.AreEqual( portElement.Name, "port" );
            Assert.AreEqual( portElement.ID, 0 );

            Assert.AreEqual( portElement.PortKind, PortDirection.Input );

            portElement.Value = LogicValue.Enum.High;

            Assert.AreEqual( portElement.Outputs[ 0 ].Value, LogicValue.Enum.High );

            portElement.Value = LogicValue.Enum.Low;

            Assert.AreEqual( portElement.Outputs[ 0 ].Value, LogicValue.Enum.Low );

        }

        /***************************************************************************/

        [ TestMethod ]
        public void simpleOutputPortElement()
        {
            ElementsFactory factory = ElementsFactory.getInstance();
             
            PortElement portElement = factory.createPortElement( PortDirection.Output, "out" );

            Assert.AreEqual( portElement.Name, "out" );
            Assert.AreEqual( portElement.ID, 1 );

            Assert.AreEqual( portElement.PortKind, PortDirection.Output );

            Assert.AreEqual( portElement.Value, LogicValue.Enum.Unknown );

            Assert.AreEqual( portElement.Inputs.Size, 1 );
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
