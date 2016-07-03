using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    class Messages
    {
        public static string UnknownAccountID = "Account ID is not recognized";

        public static string AccountIDNotUnique       = "Account ID must be unique";
        public static string OwnerNameNotUnique       = "Owner name must be unique";
        public static string OwnerNameIsEmpty         = "Owner name may not be empty";
        public static string NegativeInitialBalance   = "Cannot create account with negative initial balance";
        public static string NegativeOverdraft        = "Cannot create account with negative overdraft setting";
        public static string NonPositiveDeposit       = "Cannot deposit negative or zero amount of money";
        public static string NonPositiveWithdrawal    = "Cannot withdraw negative or zero amount of money";
        public static string NonPositiveTransfer      = "Cannot transfer negative or zero amount of money";
        public static string WithdrawalLimitExceeded  = "Withdrawal limit exceeded";
    }
}
