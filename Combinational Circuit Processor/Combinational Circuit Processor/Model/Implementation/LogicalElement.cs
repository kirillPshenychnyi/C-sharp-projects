
/***************************************************************************/

using System;

/***************************************************************************/

namespace LogicalModel.Implementation
{
    /***************************************************************************/

    using API;
    using EvalFunction = Action< API.ILogicalElement >;
    using ModifyFunction = Func< int, int >;
    using ElementPin = Tuple< API.ILogicalElement, int >;

    /***************************************************************************/

    public class LogicalElement : ILogicalElement
    {
        /***************************************************************************/

        public LogicalElement(
            int _id
          , LibraryElementKind.Enum _kind
          , EvalFunction _eval
          , ModifyFunction _modify
          , ILineCollection _inputs
          , ILineCollection _outPuts
        )
        {
            m_modifyFunction = _modify;
            m_evalFunction = _eval;
            m_inputLines = _inputs;
            m_outputLines = _outPuts;
            m_elementKind = _kind;
            m_id = _id;

            for (int i = 0; i < m_inputLines.Size; i++)
            {
                m_inputLines[ i ] = new Line();
                m_inputLines[ i ].addConnection(this, i);
            }

            for ( int i = 0; i < m_outputLines.Size; i++ )
                m_outputLines[ i ].SourceElement = new ElementPin( this, i );
        }

        /***************************************************************************/

        public int ID
        {
            get
            {
                return m_id;
            }
        }

        public LibraryElementKind.Enum Kind
        {
            get
            {
                return m_elementKind;
            }
        }

        public ILineCollection Inputs
        {
            get
            {
                return m_inputLines;
            }
        }

        public ILineCollection Outputs
        {
            get
            {
                return m_outputLines;
            }
        }

        /***************************************************************************/

        public void makeConnection(
            ILogicalElement _source
          , int _inputPin
          , int _outputPin
        )
        {
            m_inputLines[ _inputPin ] = _source.Outputs[ _outputPin ];
            
            _source.Outputs[ _outputPin ].addConnection( this, _inputPin );
        }

        /***************************************************************************/

        public void evaluate()
        {
            m_evalFunction( this );
        }

        /***************************************************************************/

        public void updateValue( int _pin, LogicValue.Enum _value )
        {
            m_inputLines[ _pin ].Value = _value;
        }

        /***************************************************************************/

        private ModifyFunction m_modifyFunction;

        private EvalFunction m_evalFunction;

        private ILineCollection m_inputLines;

        private ILineCollection m_outputLines;

        private readonly LibraryElementKind.Enum m_elementKind;

        private readonly int m_id;
  
        /***************************************************************************/
    }
}

/***************************************************************************/
