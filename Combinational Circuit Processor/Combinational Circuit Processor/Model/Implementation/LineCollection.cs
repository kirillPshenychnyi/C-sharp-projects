using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel.Implementation
{
    using API;

    public class LineCollection : BaseLineCollection
    {
        public LineCollection( int _size )
        {
            if (_size <= 0)
                throw new ArgumentException( 
                    string.Format( Resoursers.Exceptions.Messages.wrongLineCollectionSize, _size ) );

            m_lines = new ILine[ _size ];
        }

        public override ILine this[ int i ]
        {
            get
            {
                checkIndex( i );
                return m_lines[ i ];
            }

            set
            {
                checkIndex( i );
                m_lines[ i ] = value;
            }
        }

        public override int Size
        {
            get
            {
                return m_lines.Length;
            }
        }

        /***************************************************************************/
    
        private ILine[] m_lines;

        /***************************************************************************/
    }
}
