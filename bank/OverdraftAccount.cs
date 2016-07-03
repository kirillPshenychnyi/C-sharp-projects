using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    class OverdraftAccount : Account
    {

/***************************************************************************/

        public OverdraftAccount( string _fullName, int _id, double _currentBalance, int _overdraftLimit ) 
            :   base( _fullName, _id, _currentBalance)
        {
            m_overdraftLimit = _overdraftLimit;
        }

        public override int countOverdraftLimit()
        {
            return m_overdraftLimit;
        }

/***************************************************************************/

        public int OverdraftLimit { get { return m_overdraftLimit; } }

/***************************************************************************/

        private int m_overdraftLimit;

/***************************************************************************/

    } // class Account
}
