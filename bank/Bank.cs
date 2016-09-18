using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Bank
{

/***************************************************************************/

    class Bank : IEnumerable
    {
        public Bank()
        {
            m_accounts = new List < Account >();
            m_clients = new HashSet< string >();

        }

/***************************************************************************/
    
        public int AccountsCount { get { return m_accounts.Count; } }

/***************************************************************************/

        private void addAccountInternal( Account _accout )
        {
            m_accounts.Add(_accout);

            m_clients.Add(_accout.fullName);
        }

        public int addAccount( string _fullName, double _initialBalance )
        {
            int id = AccountsCount;

            Account account = new Account(_fullName, id, _initialBalance);

            addAccountInternal(account);
            
            return id;
        }

        public int addOverdraftAccount(string _fullName, double _initialBalance, double _overdraftLimit )
        {
            int id = AccountsCount;

            Account account = new OverdraftAccount(_fullName, id, _initialBalance, _overdraftLimit );

            addAccountInternal(account);

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

        public void deposit( int _id, double _summ )
        {
            m_accounts[_id].Balance += _summ;
        }

        public void withdraw(int _id, double _summ)
        {
            m_accounts[_id].Balance -= _summ;
        }
        
        public void transfer(int _sourceAccountId, int _targetAccountId, double _amount)
        {
            withdraw(_sourceAccountId, _amount);

            deposit(_targetAccountId, _amount);
        }

        public bool hasAccount( int _id )
        {
            return m_accounts.Count < _id;
        }

        public bool hasClient( string _name )
        {
            return m_clients.Contains( _name );
        }

        public IEnumerator GetEnumerator()
        {
            return m_accounts.GetEnumerator();
        }

/***************************************************************************/

       private List< Account> m_accounts;

       private HashSet< string > m_clients;

/***************************************************************************/

    }
}
