using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace Bank
{
    class Program
    {
        static void Main( string[] args )
        {
            TestRunner runner = new TestRunner();

            TestCases.fillRunner(runner);

            runner.run();

        }
    }
}
