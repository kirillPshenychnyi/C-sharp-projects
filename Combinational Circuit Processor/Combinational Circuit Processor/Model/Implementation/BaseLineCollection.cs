
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
