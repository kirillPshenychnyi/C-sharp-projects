using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Bank
{

/***************************************************************************/

    class TestCases
    {
        public static void fillRunner( TestRunner _runner )
        {
            _runner.addTest( "simple account" ,createSimpleAccount  );
            _runner.addTest( "simple overdraft account", createOverdraftAccount );
            _runner.addTest( "different account", createDifferentAccounts );
            _runner.addTest( "create same account same owner", createSameAccountSameOwner );
            _runner.addTest( "create accounts with wrong parameters", createAccountsWrongParameters );
            _runner.addTest( "simple deposit", simpleDeposit );
            _runner.addTest( "few deposits", fewDeposits );
            _runner.addTest( "zero deposits", zeroDeposit );
            _runner.addTest( "negative deposits", negativeDeposit );
        }

        /***************************************************************************/

        private static void createSimpleAccount()
        {
            Controller c = new Controller();

            int accountID = c.createAccount( "Wasya", 100.0 );

            Debug.Assert(c.getAccountOwnerName( accountID ) == "Wasya");
            Debug.Assert(c.getAccountBalance( accountID ) == 100.0);
            Debug.Assert(!c.isOverdraftAllowed( accountID ));
            Debug.Assert(c.getOverdraftLimit( accountID ) == 0.0);
        }

/***************************************************************************/

        private static void createOverdraftAccount()
        {
            Controller c = new Controller();
            int accountID = c.createOverdraftAccount("Wasya", 100.0, 50.0);

            Debug.Assert(c.getAccountOwnerName(accountID) == "Wasya");
            Debug.Assert(c.getAccountBalance(accountID) == 100.0);
            Debug.Assert(c.isOverdraftAllowed(accountID));
            Debug.Assert(c.getOverdraftLimit(accountID) == 50.0);
        }

        private static void createDifferentAccounts()
        {
            Controller c = new Controller();
            int WASYA_ID = c.createAccount("Wasya", 100.0 );
            int PETYA_ID = c.createAccount("Petya", 200.0 );

            Debug.Assert( c.getAccountOwnerName(WASYA_ID) == "Wasya" );
            Debug.Assert( c.getAccountOwnerName(PETYA_ID) == "Petya" );

            Debug.Assert( c.getAccountBalance(WASYA_ID) == 100.0 );
            Debug.Assert( c.getAccountBalance(PETYA_ID) == 200.0 );
        }

        private static void createSameAccountSameOwner()
        {
            Controller c = new Controller();
            c.createAccount( "Wasya", 100.0 );

            try
            {
                c.createAccount( "Wasya", 200.0 );
            }

            catch ( ArgumentException _ex )
            {
                Debug.Assert( Messages.OwnerNameNotUnique == _ex.Message );
            }
        }
        
        private static void createAccountsWrongParameters()
        {
            Controller c = new Controller();

            try
            {
                c.createAccount("", 0.0);
            }
          
            catch( ArgumentException _ex )
            {
                Debug.Assert( Messages.OwnerNameIsEmpty == _ex.Message ); 
            }

            try
            {
                c.createOverdraftAccount( "", 0.0 , 1.0 );
            }

            catch ( ArgumentException _ex )
            {
                Debug.Assert( Messages.OwnerNameIsEmpty == _ex.Message );
            }

            try
            {
                c.createAccount( "Vasya", -1.0 );
            }

            catch ( ArgumentException _ex )
            {
                Debug.Assert(Messages.NegativeInitialBalance == _ex.Message);
            }

            try
            {
                c.createOverdraftAccount( "Vasya", -1.0, 10.0 );
            }

            catch (ArgumentException _ex)
            {
                Debug.Assert(Messages.NegativeInitialBalance == _ex.Message);
            }

            try
            {
                c.createOverdraftAccount("Vasya", 1.0, -10.0);
            }

            catch (ArgumentException _ex)
            {
                Debug.Assert(Messages.NegativeOverdraft == _ex.Message);
            }

        }

        private static void simpleDeposit()
        {
            Controller c = new Controller();
            int WASYA_ID = c.createAccount( "Vasya", 0.0 );
            int PETYA_ID = c.createAccount( "Petya", 100.0 );

            c.deposit( WASYA_ID, 150.0 );

            Debug.Assert( c.getAccountBalance(WASYA_ID) == 150.0 );
            Debug.Assert( c.getAccountBalance(PETYA_ID) == 100.0 );
        }

        private static void fewDeposits()
        {
            Controller c = new Controller();
            int WASYA_ID = c.createAccount("Wasya", 0.0);
            int PETYA_ID = c.createAccount("Petya", 100.0);

            c.deposit(WASYA_ID, 70.0);
            c.deposit(PETYA_ID, 20.0);
            c.deposit(PETYA_ID, 25.0);
            c.deposit(WASYA_ID, 40.0);

            Debug.Assert(c.getAccountBalance(WASYA_ID) == 110.0); // 70 + 40
            Debug.Assert(c.getAccountBalance(PETYA_ID) == 145.0); // 100 + 20 + 25
        }

        private static void zeroDeposit()
        {
            Controller c = new Controller();
            int WASYA_ID = c.createAccount( "Wasya", 0.0 );

            try
            {
                c.deposit(WASYA_ID, 0.0);
            }

            catch ( ArgumentException _ex )
            {
                Debug.Assert( _ex.Message == Messages.NonPositiveDeposit );
            }
        }

        private static void negativeDeposit()
        {
            Controller c = new Controller();
            int WASYA_ID = c.createAccount("Wasya", 0.0);

            try
            {
                c.deposit(WASYA_ID, -1.0);
            }

            catch (ArgumentException _ex)
            {
                Debug.Assert(_ex.Message == Messages.NonPositiveDeposit);
            }
        }

        /***************************************************************************/

    }
}
