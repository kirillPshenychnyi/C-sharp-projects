using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Bank{ 
namespace Test{ 

/***************************************************************************/

        class TestCases
        {
            public static void fillRunner(TestRunner _runner)
            {
                _runner.addTest("simple account", createSimpleAccount);
                _runner.addTest("simple overdraft account", createOverdraftAccount);
                _runner.addTest("different account", createDifferentAccounts);
                _runner.addTest("create same account same owner", createSameAccountSameOwner);
                _runner.addTest("create accounts with wrong parameters", createAccountsWrongParameters);
                _runner.addTest("simple deposit", simpleDeposit);
                _runner.addTest("few deposits", fewDeposits);
                _runner.addTest("zero deposits", zeroDeposit);
                _runner.addTest("negative deposits", negativeDeposit);
                _runner.addTest("simple withdraw", simpleWithdraw);
                _runner.addTest("couple withdraws", coupleWithdraws);
                _runner.addTest("zero withdraw", zeroWithdraw);
                _runner.addTest("negative withdraw", negativeWithdraw);
                _runner.addTest("overlimitWithdraw", overLimitWithdraw);
                _runner.addTest("withdraw overdraft account", withdrawOverdraftAccount);
                _runner.addTest("withdraw overlimit from overdraft account", createOverdraftAccount);
                _runner.addTest("simple transfer", simpleTransfer);
                _runner.addTest("transfer from overdraft", overdraftTransfer);
                _runner.addTest("overlimit transfer from overdraft", overlimitTransferFromOverdraft);
                _runner.addTest("non positive transfer", nonPositiveTransfer);
                _runner.addTest("totals", totals);
                _runner.addTest("totals with overdrafts", totalsWithOverdrafts);
                _runner.addTest("unknown account operations", unknownAccountOperations);
            }

    /***************************************************************************/

            private static void createSimpleAccount()
            {
                Controller c = new Controller();

                int accountID = c.createAccount("Wasya", 100.0);

                Debug.Assert(c.getAccountOwnerName(accountID) == "Wasya");
                Debug.Assert(c.getAccountBalance(accountID) == 100.0);
                Debug.Assert(!c.isOverdraftAllowed(accountID));
                Debug.Assert(c.getOverdraftLimit(accountID) == 0.0);
            }

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
                int WASYA_ID = c.createAccount("Wasya", 100.0);
                int PETYA_ID = c.createAccount("Petya", 200.0);

                Debug.Assert(c.getAccountOwnerName(WASYA_ID) == "Wasya");
                Debug.Assert(c.getAccountOwnerName(PETYA_ID) == "Petya");

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 100.0);
                Debug.Assert(c.getAccountBalance(PETYA_ID) == 200.0);
            }

            private static void createSameAccountSameOwner()
            {
                Controller c = new Controller();
                c.createAccount("Wasya", 100.0);

                try
                {
                    c.createAccount("Wasya", 200.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(Messages.OwnerNameNotUnique == _ex.Message);
                }
            }

            private static void createAccountsWrongParameters()
            {
                Controller c = new Controller();

                try
                {
                    c.createAccount("", 0.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(Messages.OwnerNameIsEmpty == _ex.Message);
                }

                try
                {
                    c.createOverdraftAccount("", 0.0, 1.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(Messages.OwnerNameIsEmpty == _ex.Message);
                }

                try
                {
                    c.createAccount("Vasya", -1.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(Messages.NegativeInitialBalance == _ex.Message);
                }

                try
                {
                    c.createOverdraftAccount("Vasya", -1.0, 10.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(Messages.NegativeInitialBalance == _ex.Message);
                }

                try
                {
                    c.createOverdraftAccount("Vasya", 1.0, -10.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(Messages.NegativeOverdraft == _ex.Message);
                }

            }

            private static void simpleDeposit()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createAccount("Vasya", 0.0);
                int PETYA_ID = c.createAccount("Petya", 100.0);

                c.deposit(WASYA_ID, 150.0);

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 150.0);
                Debug.Assert(c.getAccountBalance(PETYA_ID) == 100.0);
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
                int WASYA_ID = c.createAccount("Wasya", 0.0);

                try
                {
                    c.deposit(WASYA_ID, 0.0);
                    Debug.Assert(false);
                }

                catch (Exception _ex)
                {
                    Debug.Assert(_ex.Message == Messages.NonPositiveDeposit);
                }
            }

            private static void negativeDeposit()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createAccount("Wasya", 0.0);

                try
                {
                    c.deposit(WASYA_ID, -1.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.NonPositiveDeposit);
                }
            }

            private static void simpleWithdraw()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createAccount("Wasya", 50.0);
                int PETYA_ID = c.createAccount("Petya", 100.0);

                c.withdraw(WASYA_ID, 15.0);
                c.withdraw(PETYA_ID, 100.0);

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 35.0); // 50 - 15
                Debug.Assert(c.getAccountBalance(PETYA_ID) == 0.0); // 100 - 100
            }

            private static void coupleWithdraws()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createAccount("Wasya", 50.0);
                int PETYA_ID = c.createAccount("Petya", 100.0);

                c.withdraw(WASYA_ID, 15.0);
                c.withdraw(PETYA_ID, 90.0);
                c.withdraw(WASYA_ID, 25.0);
                c.withdraw(PETYA_ID, 10.0);

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 10.0); // 50 - 15 - 25
                Debug.Assert(c.getAccountBalance(PETYA_ID) == 0.0); // 100 - 90 - 10
            }

            private static void zeroWithdraw()
            {
                Controller c = new Controller();

                int WASYA_ID = c.createAccount("Wasya", 50.0);

                try
                {
                    c.withdraw(WASYA_ID, 0.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.NonPositiveWithdrawal);
                }

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 50.0);
            }

            private static void negativeWithdraw()
            {
                Controller c = new Controller();

                int WASYA_ID = c.createAccount("Wasya", 50.0);

                try
                {
                    c.withdraw(WASYA_ID, -10.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.NonPositiveWithdrawal);
                }

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 50.0);
            }

            private static void overLimitWithdraw()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createAccount("Wasya", 50.0);

                try
                {
                    c.withdraw(WASYA_ID, 60.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.WithdrawalLimitExceeded);
                }

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 50.0);
            }

            private static void withdrawOverdraftAccount()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createOverdraftAccount("Wasya", 50.0, 20.0);

                c.withdraw(WASYA_ID, 70.0);
                Debug.Assert(c.getAccountBalance(WASYA_ID) == -20.0);

           }

            private static void withdrawOverlimitOverdraftAccount()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createOverdraftAccount("Wasya", 50.0, 20.0);

                try
                {
                    c.withdraw(WASYA_ID, 71.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.WithdrawalLimitExceeded);
                }

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 50.0);
            }
    
            private static void simpleTransfer()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createAccount("Wasya", 100.0);
                int PETYA_ID = c.createAccount("Petya", 50.0);

                Debug.Assert(c.getTotalBankDeposits() == 150.0);

                c.transfer(WASYA_ID, PETYA_ID, 30.0);

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 70.0);
                Debug.Assert(c.getAccountBalance(PETYA_ID) == 80.0);

                Debug.Assert(c.getTotalBankDeposits() == 150.0);

            }

            private static void overdraftTransfer()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createOverdraftAccount("Wasya", 100.0, 50.0);
                int PETYA_ID = c.createAccount("Petya", 50.0);

                Debug.Assert(c.getTotalBankDeposits() == 150.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 0.0);

                c.transfer(WASYA_ID, PETYA_ID, 120.0);

                Debug.Assert(c.getAccountBalance(WASYA_ID) == -20.0);
                Debug.Assert(c.getAccountBalance(PETYA_ID) == 170.0);
                
                Debug.Assert(c.getTotalBankDeposits() == 170.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 20.0);
            }

            private static void overlimitTransferFromOverdraft()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createOverdraftAccount("Wasya", 100.0, 50.0);
                int PETYA_ID = c.createAccount("Petya", 50.0);

                Debug.Assert(c.getTotalBankDeposits() == 150.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 0.0);

                try
                {
                    c.transfer(WASYA_ID, PETYA_ID, 200.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert( _ex.Message ==  Messages.WithdrawalLimitExceeded);
                }

                Debug.Assert(c.getAccountBalance(WASYA_ID) == 100.0);
                Debug.Assert(c.getAccountBalance(PETYA_ID) == 50.0);
            }

            private static void nonPositiveTransfer()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createOverdraftAccount("Wasya", 100.0, 50.0);
                int PETYA_ID = c.createAccount("Petya", 50.0);

                try
                {
                    c.transfer(WASYA_ID, PETYA_ID, 0.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.NonPositiveTransfer);
                }

                try
                {
                    c.transfer(WASYA_ID, PETYA_ID, -10.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.NonPositiveTransfer);
                }
            }

            private static void totals()
            {
                Controller c = new Controller();

                Debug.Assert(c.getTotalBankBalance() == 0.0);
                Debug.Assert(c.getTotalBankDeposits() == 0.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 0.0);

                c.createAccount("Wasya", 100.0);

                Debug.Assert(c.getTotalBankBalance() == 100.0);
                Debug.Assert(c.getTotalBankDeposits() == 100.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 0.0);

                c.createAccount("Petya", 200.0);

                Debug.Assert(c.getTotalBankBalance() == 300.0);
                Debug.Assert(c.getTotalBankDeposits() == 300.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 0.0);
            }

            private static void totalsWithOverdrafts()
            {
                Controller c = new Controller();
                int WASYA_ID = c.createOverdraftAccount("Wasya", 0.0, 50.0);
                int PETYA_ID = c.createOverdraftAccount("Petya", 0.0, 30.0);

                Debug.Assert(c.getTotalBankBalance() == 0.0);
                Debug.Assert(c.getTotalBankDeposits() == 0.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 0.0);

                c.withdraw(WASYA_ID, 20.0);

                Debug.Assert(c.getTotalBankBalance() == -20.0);
                Debug.Assert(c.getTotalBankDeposits() == 0.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 20.0);

                c.withdraw(PETYA_ID, 30.0);

                Debug.Assert(c.getTotalBankBalance() == -50.0);
                Debug.Assert(c.getTotalBankDeposits() == 0.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 50.0);

                c.deposit(WASYA_ID, 100.0);

                Debug.Assert(c.getTotalBankBalance() == 50.0);
                Debug.Assert(c.getTotalBankDeposits() == 80.0);
                Debug.Assert(c.getTotalBankOverdrafts() == 30.0);
            }

            private static void unknownAccountOperations()
            {
                Controller c = new Controller();

                const int RANDOM_ID = 175;

                try
                {
                    c.getAccountBalance(RANDOM_ID);
                    Debug.Assert(false);
                }

                catch ( ArgumentException _ex )
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

                try
                {
                    c.getAccountBalance(RANDOM_ID);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

                try
                {
                    c.isOverdraftAllowed(RANDOM_ID);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

                try
                {
                    c.getAccountOwnerName(RANDOM_ID);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

                try
                {
                    c.getOverdraftLimit(RANDOM_ID);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

                try
                {
                    c.deposit(RANDOM_ID, 100.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

                try
                {
                    c.withdraw(RANDOM_ID, 100.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

                try
                {
                    int WASYA_ID = c.createAccount("Wasya", 0.0);
                    c.transfer(RANDOM_ID, WASYA_ID, 100.0);
                    Debug.Assert(false);
                }

                catch (ArgumentException _ex)
                {
                    Debug.Assert(_ex.Message == Messages.UnknownAccountID);
                }

            }

/***************************************************************************/

    } // class TestCases
  } // namespace Test
} // namespace Bank
