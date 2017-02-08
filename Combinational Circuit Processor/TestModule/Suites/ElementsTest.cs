
/***************************************************************************/

namespace TestModule.Suites
{
    /***************************************************************************/

    using LogicalModel.Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LogicalModel.API;

    /***************************************************************************/

    [ TestClass ]
    public class ElementsTest
    {
        /***************************************************************************/

        [ TestMethod ]
        public void simpleInputPortElement()
        {
            Port a = new Port();

            a.LogicValue = LogicValue.Enum.High;

            PortElement a_element = new PortElement( a, PortKind.Enum.Input, 10 );

            Assert.AreEqual( a_element.ID, 10 );

            Assert.AreEqual( a_element.Kind, PortKind.Enum.Input );

            Assert.AreEqual( a_element.evaluate(), LogicValue.Enum.High );

            a.LogicValue = LogicValue.Enum.Low;

            Assert.AreEqual( a_element.evaluate(), LogicValue.Enum.Low );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void simpleOutputPort()
        {
            Port a = new Port();

            a.LogicValue = LogicValue.Enum.High;

            PortElement a_element = new PortElement( a, PortKind.Enum.Output, 7 );

            Assert.AreEqual( a_element.ID, 7 );

            Assert.AreEqual( a_element.Kind, PortKind.Enum.Output );

            Assert.AreEqual( a_element.evaluate(), LogicValue.Enum.High );

            a.LogicValue = LogicValue.Enum.Low;

            Assert.AreEqual( a_element.evaluate(), LogicValue.Enum.Low );
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
