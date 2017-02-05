using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Implementation
{
    using API;

    public class EmptyLineCollection : ILineCollection
    {
        public int Size
        {
            get
            {
                return 0;
            }
        }

        public ILine this[ int i ]
        {
            get
            {
                throw new MemberAccessException( 
                    Resoursers.Exceptions.Messages.gettingLineFromEmptyCollection
                );
            }

            set
            {
                throw new MemberAccessException(
                    Resoursers.Exceptions.Messages.addingLineToEmptyCollection
                );
            }
        }
    }
}
