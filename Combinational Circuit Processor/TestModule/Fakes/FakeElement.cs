
/***************************************************************************/

namespace TestModule.Fakes
{
    using System;
    /***************************************************************************/

    using LogicalModel.API;

    /***************************************************************************/

    public class FakeElement : ILogicalElement
    {
        /***************************************************************************/

        public ILineCollection Inputs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /***************************************************************************/

        public ILineCollection Outputs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /***************************************************************************/

        int ILogicalElement.ID
        {
            get
            {
                return 0;
            }
        }

        /***************************************************************************/

        LibraryElementKind.Enum ILogicalElement.Kind
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /***************************************************************************/

        public void makeConnection(ILogicalElement _elem, int _inputPin, int _outputPin)
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/

        public void addOutputConnection(ILogicalElement _elem, int _inputPin, int _outputPin)
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/

        public void updateValue(int _pin, LogicValue.Enum _value)
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/

        void ILogicalElement.evaluate()
        {
            throw new NotImplementedException();
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
