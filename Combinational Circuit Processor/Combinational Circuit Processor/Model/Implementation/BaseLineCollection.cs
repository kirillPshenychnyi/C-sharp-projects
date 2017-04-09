
/***************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;
    
    /***************************************************************************/

    public abstract class BaseLineCollection : ILineCollection
    {
        /***************************************************************************/

        abstract public ILine this [ int i ]
        {
            get;

            set;
        }

        /***************************************************************************/

        abstract public int Size
        {
            get;
        }

        /***************************************************************************/

        public abstract bool hasUnknownStates();

        /***************************************************************************/

        public abstract bool hasValue( LogicValue.Enum _value );

        /***************************************************************************/

        abstract public System.Collections.IEnumerator GetEnumerator();

        /***************************************************************************/

        public static ILineCollection createLineCollection( int _size )
		{
			switch( _size )
			{
				case 0:  return new EmptyLineCollection();
				case 1:	 return new OneLineCollection( null );
				default: return new LineCollection( _size );
			}
		}
      
      /***************************************************************************/

        public void setToAllLines( LogicValue.Enum _value )
        {
            for ( int i = 0; i < Size; ++i )
            {
                this[ i ].Value = _value;
            }
        }

        /***************************************************************************/

        protected void checkIndex( int _index )
        {
            if( ( _index < 0 ) || ( _index > Size ) )
                throw new IndexOutOfRangeException(
                    string.Format( Resoursers.Exceptions.Messages.wrongLineIndex, _index ) );
        }

        /***************************************************************************/
    }
}

/***************************************************************************/
