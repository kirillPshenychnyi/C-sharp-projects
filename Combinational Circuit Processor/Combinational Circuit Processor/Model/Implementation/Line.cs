﻿using System;
using System.Collections.Generic;

namespace LogicalModel.Implementation
{
    using API;
    using ElementPin    = Tuple< API.ILogicalElement, int > ;
    using PinsSet       = HashSet< int >;
    using Elements2Pins = Dictionary< API.ILogicalElement, HashSet< int > >;

    public class Line : ILine
    {
        public ElementPin SourceElement
        {
            get
            {
                return m_sourceElement;
            }
        }

        public int ElementsConnected
        {
            get
            {
                return m_elements2Pins.Count;
            }
        }

        public Line( ILogicalElement _element, int _pin )
        {
            m_sourceElement = new ElementPin( _element, _pin ); 
            m_elements2Pins = new Elements2Pins();
        }

        public PinsSet getConnections( ILogicalElement _element )
        {
            if( !hasConnectionsWithElement( _element ) )
                throw new ArgumentException(
                   string.Format( Resoursers.Exceptions.Messages.noElementConnection, _element.Name ) );

            return m_elements2Pins[ _element ];
        }

        public void addConnection( ILogicalElement _element, int _pin )
        {
            if ( _element == m_sourceElement.Item1 )
                throw new ArgumentException(
                    Resoursers.Exceptions.Messages.combinationalFeedback );

            if ( hasConnection( _element, _pin ) )
                throw new ArgumentException(
                    string.Format( Resoursers.Exceptions.Messages.duplicateConnection, _element.Name ) );
            
            if( hasConnectionsWithElement( _element ) ) 
                m_elements2Pins[ _element ].Add( _pin );
            else
            {
                PinsSet pinsSet = new PinsSet();
                pinsSet.Add( _pin );
                m_elements2Pins.Add( _element, pinsSet );  
            }         
        }

        public void removeConnection( ILogicalElement _element, int _pin )
        {
            if ( !hasConnectionsWithElement( _element ) )
                throw new ArgumentException(
                    string.Format( Resoursers.Exceptions.Messages.noElementConnection, _element.Name ) );

            PinsSet set = m_elements2Pins[ _element ];
            
            if( !set.Contains( _pin ) )
                throw new ArgumentException(
                    string.Format( Resoursers.Exceptions.Messages.noPinConnection, _pin, _element.Name ) );

            set.Remove( _pin );

            if ( set.Count == 0 )
                m_elements2Pins.Remove( _element );
        }

        public void removeConnection( ILogicalElement _element )
        {
            if( !hasConnectionsWithElement( _element ) )
                throw new ArgumentException(
                   String.Format( Resoursers.Exceptions.Messages.noElementConnection, _element.Name ) );

            m_elements2Pins.Remove( _element );
        }

        public bool hasConnectionsWithElement( ILogicalElement _element )
        {
            return m_elements2Pins.ContainsKey( _element );
        }

        public bool hasConnection( ILogicalElement _element, int _pin )
        {
            if ( !hasConnectionsWithElement( _element ) ) 
                return false;

            return m_elements2Pins[ _element ].Contains( _pin );
        }

        /***************************************************************************/

        private readonly ElementPin m_sourceElement;
        private Elements2Pins m_elements2Pins;
       
        /***************************************************************************/
    }
}
