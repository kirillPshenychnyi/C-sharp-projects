
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LogicalModel.API;
using LogicalModel.Implementation;

namespace TestModule.Suites
{
    [ TestClass ]
    public class PortLinesTests
    {
        /***************************************************************************/

        [ TestMethod ]
        public void createPort()
        {
            IPort port = new Port();

            Assert.AreEqual( port.logicValue, LogicValue.Value.Unknown );
        }

        /***************************************************************************/

        [ TestMethod ]
        public void portSetValue()
        {
            IPort port = new Port();

            port.logicValue = LogicValue.Value.High;

            Assert.AreEqual( port.logicValue, LogicValue.Value.High );

            port.logicValue = LogicValue.Value.Low;

            Assert.AreEqual( port.logicValue, LogicValue.Value.Low );

            port.logicValue = LogicValue.Value.Unknown;

            Assert.AreEqual( port.logicValue, LogicValue.Value.Unknown );
        }

        /***************************************************************************/
    }
}
