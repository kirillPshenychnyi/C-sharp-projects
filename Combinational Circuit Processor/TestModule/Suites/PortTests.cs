
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
    public class PortLinesTests
    {
        /***************************************************************************/

        [ TestMethod ]
        public void createPort()
        {
            IPort port = new Port();

            Assert.AreEqual( port.LogicValue, LogicValue.Enum.Unknown );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void portSetValue()
        {
            IPort port = new Port();

            port.LogicValue = LogicValue.Enum.High;

            Assert.AreEqual( port.LogicValue, LogicValue.Enum.High );

            port.LogicValue = LogicValue.Enum.Low;

            Assert.AreEqual( port.LogicValue, LogicValue.Enum.Low );

            port.LogicValue = LogicValue.Enum.Unknown;

            Assert.AreEqual( port.LogicValue, LogicValue.Enum.Unknown );
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
