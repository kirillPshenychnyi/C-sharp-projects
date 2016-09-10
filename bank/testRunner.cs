using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class TestRunner
    {
        public TestRunner()
        {
            m_testCases = new Dictionary < string, Action >();
        }

        public void addTest( string _name, Action _case)
        {
            if( m_testCases.ContainsKey( _name ))
                throw new ArgumentException(string.Format("Operation {0} already exists", _name), "op");

            m_testCases.Add(_name, _case);

        }

        public void run()
        {
            System.Console.WriteLine("Running {0} test cases", m_testCases.Count);

            foreach( var test in m_testCases )
            {
                System.Console.WriteLine( "Test case {0}", test.Key );

                try
                {
                    test.Value();
                }

                catch ( Exception _ex)
                {
                    System.Console.WriteLine( _ex.Message );
                }

            }

        }
       
        private Dictionary< string, Action > m_testCases;
    }
}
