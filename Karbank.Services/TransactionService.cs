namespace Karbank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository accountRepository;

        public TransactionService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public string Deposit(int accountNumber, double amount)
        {
            if(amount <= 0)
            {
                return Messages.TransactionGreaterThanZero;
            }

            var account = this.accountRepository.GetAccount(accountNumber);
            if(account == null)
            {
                return Messages.InvalidAccountNumber;
            }

            account.Balance += amount;

            this.accountRepository.UpdateAccountBalance(account.AccountNumber, account.Balance);

            return Messages.TransactionSuccessful;
        }

        public string Transfer(int customerId, int debitAccountNumber, int creditAccountNumber, double amount)
        {
            if (amount <= 0)
            {
                return Messages.TransactionGreaterThanZero;
            }

            if (debitAccountNumber == creditAccountNumber)
            {
                return Messages.SameAccount;
            }

            var debitAccount = this.accountRepository.GetAccount(debitAccountNumber);
            var creditAccount = this.accountRepository.GetAccount(creditAccountNumber);

            if (debitAccount == null || creditAccount == null)
            {
                return Messages.InvalidAccountNumber;
            }

            if(amount > debitAccount.WithdrawalLimit)
            {
                return string.Format(Messages.WithdrawLimit, debitAccount.WithdrawalLimit);
            }

            if (debitAccount.Balance < amount)
            {
                return Messages.InsufficientFunds;
            }

            if (debitAccount.OwnerId != customerId)
            {
                return Messages.InvalidOwner;
            }

            debitAccount.Balance -= amount;
            creditAccount.Balance += amount;

            this.accountRepository.UpdateAccountBalance(debitAccount.AccountNumber, debitAccount.Balance);
            this.accountRepository.UpdateAccountBalance(creditAccount.AccountNumber, creditAccount.Balance);

            return Messages.TransactionSuccessful;
        }

        public string Withdraw(int customerId, int accountNumber, double amount)
        {
            if (amount <= 0)
            {
                return Messages.TransactionGreaterThanZero;
            }

            var account = this.accountRepository.GetAccount(accountNumber);
            if (account == null)
            {
                return Messages.InvalidAccountNumber;
            }

            if(account.Balance < amount)
            {
                return Messages.InsufficientFunds;
            }

            if (amount > account.WithdrawalLimit)
            {
                return string.Format(Messages.WithdrawLimit, account.WithdrawalLimit);
            }

            if (account.OwnerId != customerId)
            {
                return Messages.InvalidOwner;
            }

            account.Balance -= amount;

            this.accountRepository.UpdateAccountBalance(account.AccountNumber, account.Balance);

            return Messages.TransactionSuccessful;
        }
    }
}