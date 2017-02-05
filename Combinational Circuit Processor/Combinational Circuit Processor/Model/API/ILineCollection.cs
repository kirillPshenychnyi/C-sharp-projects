using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.API
{
    public interface ILineCollection
    {
        int Size
        {
            get;
        }

        ILine this [ int i ]
        {
            get;
            set;
        }
    }
}
