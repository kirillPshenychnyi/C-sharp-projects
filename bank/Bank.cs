using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{

/***************************************************************************/

    class Bank
    {
        public Bank()
        {
            m_accounts = new List<Account>();
        }

/***************************************************************************/
    
        public int AccountsCount { get { return m_accounts.Count; } }

/***************************************************************************/

        public int addAccount( Account _account )
        {
            int id = AccountsCount;

            m_accounts.Add(_account);

            return id;

        }

        public string getAccountOwnerName( int _id )
        {
            return m_accounts[_id].fullName;
        }

        public double getAccountBalance( int _id )
        {
            return m_accounts[_id].Balance;
        }

        public bool isOverdraftAllowed( int _id )
        {
            return getOverdraftLimit( _id ) != 0.0;
        }

        public double getOverdraftLimit( int _id )
        {
            return m_accounts[_id].OverdraftLimit;
        }

        void deposit( int _id, double _summ )
        {
            m_accounts[_id].Balance += _summ;
        }

        void withdraw(int _id, double _summ)
        {
            m_accounts[_id].Balance -= _summ;
        }
        
        bool hasAccount( int _id )
        {
            return _id < AccountsCount;
        }

/***************************************************************************/

        List< Account > m_accounts;

/***************************************************************************/

    }
}
