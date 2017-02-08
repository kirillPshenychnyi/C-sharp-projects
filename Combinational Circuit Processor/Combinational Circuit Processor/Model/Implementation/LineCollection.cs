
/***************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;

    /***************************************************************************/

    public class LineCollection : BaseLineCollection
    {
        /***************************************************************************/

        public LineCollection( int _size )
        {
            if (_size <= 0)
                throw new ArgumentException( 
                    string.Format( Resoursers.Exceptions.Messages.wrongLineCollectionSize, _size ) );

            m_lines = new ILine[ _size ];
        }

        /***************************************************************************/

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

        /***************************************************************************/

        public override int Size
        {
            get
            {
                return m_lines.Length;
            }
        }

        /***************************************************************************/

        public override System.Collections.IEnumerator GetEnumerator()
        {
            foreach ( ILine line in m_lines )
                yield return line;
        }

        /***************************************************************************/

        private ILine[] m_lines;

        /***************************************************************************/
    }
}

/***************************************************************************/
