using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    class Account
    {

/***************************************************************************/

        protected Account( string _fullName, int _id, double _currentBalance )
        {
            m_fullName = _fullName;

            m_id = _id;

            m_currentBalance = _currentBalance;
        }

        public virtual int countOverdraftLimit()
        {
            return 0;
        }

/***************************************************************************/

        public string fullName { get { return m_fullName; }  }

        public int ID { get { return m_id; }  }

        public double Balance { get { return m_currentBalance;  }
                                set { m_currentBalance = value; }
        }

/***************************************************************************/

        private string m_fullName;

        private int m_id;

        private double m_currentBalance;

/***************************************************************************/

    } // class Account
}
