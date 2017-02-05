using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Implementation
{
    using API;

    public abstract class BaseLineCollection : ILineCollection
    {
        abstract public ILine this [ int i ]
        {
            get;

            set;
        }
       
        abstract public int Size
        {
            get;
        }

        protected void checkIndex( int _index )
        {
            if( ( _index < 0 ) || ( _index > Size ) )
                throw new IndexOutOfRangeException(
                    string.Format( Resoursers.Exceptions.Messages.wrongLineIndex, _index ) );
        }
    }
}
