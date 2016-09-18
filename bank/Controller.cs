using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Controller
    {

/***************************************************************************/

        public Controller()
        {
            m_bank = new Bank();
        }

/***************************************************************************/
        
        public int createAccount( string _fullName, double _initialBalance )
        {
            checkArguments(_fullName, _initialBalance);

            return m_bank.addAccount(_fullName, _initialBalance);

        }

        public int createOverdraftAccount( string _fullName, double _initBalance, double _overdraftLimit )
        {
            checkArguments(_fullName, _initBalance);

            if (_overdraftLimit <= 0)
                throw new ArgumentException(Messages.NegativeOverdraft);

            return m_bank.addOverdraftAccount(_fullName, _initBalance, _overdraftLimit);

        }

        public string getAccountOwnerName( int _id )
        {
            checkAccountExists(_id);

            return m_bank.getAccountOwnerName(_id);
        }

        public double getAccountBalance ( int _id )
        {
            checkAccountExists(_id);

            return m_bank.getAccountBalance(_id);
        }

        public bool isOverdraftAllowed( int _id )
        {
            checkAccountExists(_id);

            return m_bank.isOverdraftAllowed(_id);
        }

        public double getOverdraftLimit( int _id )
        {
            checkAccountExists(_id);

            return m_bank.getOverdraftLimit( _id );
        }

        public void deposit( int _id, double _amount )
        {
            checkAccountExists(_id);

            if (_amount <= 0)
                throw new ArgumentException(Messages.NonPositiveDeposit);

            m_bank.deposit(_id, _amount);
            
        }

        public void withdraw( int _id, double _amount )
        {
            checkAccountExists(_id);

            if (_amount <= 0.0)
                throw new ArgumentException(Messages.NonPositiveWithdrawal);

            double accountBalance = m_bank.getAccountBalance(_id);

            if (accountBalance < _amount && m_bank.getOverdraftLimit(_id) + accountBalance < _amount)
                throw new ArgumentException(Messages.WithdrawalLimitExceeded);

            m_bank.withdraw(_id, _amount );
        }

        public void transfer( int _sourceAccountId, int _targetAccountId, double _amount )
        {
            if (_amount <= 0.0)
                throw new ArgumentException(Messages.NonPositiveTransfer);

            checkAccountExists(_sourceAccountId);

            checkAccountExists(_targetAccountId);

            if (getAccountBalance(_sourceAccountId) + getOverdraftLimit(_sourceAccountId) < _amount)
                throw new ArgumentException(Messages.WithdrawalLimitExceeded);

            m_bank.transfer(_sourceAccountId, _targetAccountId, _amount);

        }

        public double getTotalBankBalance()
        {
            double total = 0.0;

            foreach (Account accountIt in m_bank)
                total += accountIt.Balance;

            return total;

        }

        public double getTotalBankDeposits()
        {
            double total = 0.0;

            foreach (Account accountIt in m_bank)
            {   
                if( accountIt.Balance > 0 )
                    total += accountIt.Balance;
            }
            return total;
        } 

        public double getTotalBankOverdrafts()
        {
            double totalOverdrafts = 0.0;

            foreach ( Account accountIt in m_bank )
                if( accountIt.Balance < 0.0 && accountIt.OverdraftLimit > 0.0 )
                    totalOverdrafts += accountIt.Balance;

            return -totalOverdrafts;

        }

/***************************************************************************/

        private void checkArguments(string _fullName, double _initBalance)
        {
            if ( _initBalance < 0 )
                throw new ArgumentException(Messages.NegativeInitialBalance);

            if ( _fullName.Length == 0 )
                throw new ArgumentException(Messages.OwnerNameIsEmpty);

            if ( m_bank.hasClient( _fullName ) )
                throw new ArgumentException( Messages.OwnerNameNotUnique );
        }

        private void checkAccountExists(int _id)
        {
            if (m_bank.hasAccount(_id))
                throw new ArgumentException(Messages.UnknownAccountID);
        }

/***************************************************************************/

        private Bank m_bank;
        
/***************************************************************************/

    }
}
