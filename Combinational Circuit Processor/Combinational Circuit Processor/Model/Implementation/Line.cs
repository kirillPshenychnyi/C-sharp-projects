
/***************************************************************************/

using System;
using System.Collections.Generic;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;
    using ElementPin    = Tuple< API.ILogicalElement, int > ;
    using PinsSet       = HashSet< int >;
    using Elements2Pins = Dictionary< API.ILogicalElement, HashSet< int > >;

    /***************************************************************************/

    public class Line : ILine
    {
        /***************************************************************************/

        private void baseInit()
        {
            m_elements2Pins = new Elements2Pins();
            m_value = LogicValue.Enum.Unknown;
        }

        /***************************************************************************/

        public Line( ILogicalElement _element, int _pin )
        {
            m_sourceElement = new ElementPin( _element, _pin );
            baseInit();
        }

        public Line()
        {
            m_sourceElement = null;
            baseInit();
        }

        /***************************************************************************/

        public ElementPin SourceElement
        {
            get
            {
                return m_sourceElement;
            }

            set
            {
                m_sourceElement = value;
            }
        }

        public LogicValue.Enum Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;

                foreach( var connection in m_elements2Pins )
                {
                    foreach( int connectedPin in connection.Value )
                    {
                        connection.Key.evaluate();
                    }
                }
            }
        }

        /***************************************************************************/

        public int ElementsConnected
        {
            get
            {
                return m_elements2Pins.Count;
            }

        }

        /***************************************************************************/

        public PinsSet getConnections( ILogicalElement _element )
        {
            if( !hasConnectionsWithElement( _element ) )
                throw new ArgumentException(
                   string.Format( Resoursers.Exceptions.Messages.noElementConnection, _element.ID ) );

            return m_elements2Pins[ _element ];
        }

        /***************************************************************************/

        public void addConnection( ILogicalElement _element, int _pin )
        {
            if ( m_sourceElement != null && _element == m_sourceElement.Item1 )
                throw new ArgumentException(
                    Resoursers.Exceptions.Messages.combinationalFeedback );

            if ( hasConnection( _element, _pin ) )
                throw new ArgumentException(
                    string.Format( Resoursers.Exceptions.Messages.duplicateConnection, _element.ID ) );
            
            if( hasConnectionsWithElement( _element ) ) 
                m_elements2Pins[ _element ].Add( _pin );
            else
            {
                PinsSet pinsSet = new PinsSet();
                pinsSet.Add( _pin );
                m_elements2Pins.Add( _element, pinsSet );  
            }         
        }

        /***************************************************************************/

        public void removeConnection( ILogicalElement _element, int _pin )
        {
            if ( !hasConnectionsWithElement( _element ) )
                throw new ArgumentException(
                    string.Format( Resoursers.Exceptions.Messages.noElementConnection, _element.ID ) );

            PinsSet set = m_elements2Pins[ _element ];
            
            if( !set.Contains( _pin ) )
                throw new ArgumentException(
                    string.Format( Resoursers.Exceptions.Messages.noPinConnection, _pin, _element.ID ) );

            set.Remove( _pin );

            if ( set.Count == 0 )
                m_elements2Pins.Remove( _element );
        }

        /***************************************************************************/

        public void removeConnection( ILogicalElement _element )
        {
            if( !hasConnectionsWithElement( _element ) )
                throw new ArgumentException(
                   string.Format( Resoursers.Exceptions.Messages.noElementConnection, _element.ID ) );

            m_elements2Pins.Remove( _element );
        }

        /***************************************************************************/

        public bool hasConnectionsWithElement( ILogicalElement _element )
        {
            return m_elements2Pins.ContainsKey( _element );
        }

        /***************************************************************************/

        public bool hasConnection( ILogicalElement _element, int _pin )
        {
            if ( !hasConnectionsWithElement( _element ) ) 
                return false;

            return m_elements2Pins[ _element ].Contains( _pin );
        }

        /***************************************************************************/

        private ElementPin m_sourceElement;
        private Elements2Pins m_elements2Pins;
        private LogicValue.Enum m_value;

        /***************************************************************************/
    }
}

/***************************************************************************/
