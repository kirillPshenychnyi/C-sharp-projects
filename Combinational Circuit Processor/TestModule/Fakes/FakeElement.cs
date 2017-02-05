using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModule.Fakes
{
    using LogicalModel.API;

    public class FakeElement : ILogicalElement
    {
        public string Name
        {
            get { return "dummy";  }
        }
    }
}
