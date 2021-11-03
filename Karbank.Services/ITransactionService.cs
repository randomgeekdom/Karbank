using Karbank.Model;

namespace Karbank.Services
{
    public interface ITransactionService
    {
        public string Deposit(int accountNumber, double amount);
        public string Withdraw(int customerId, int accountNumber, double amount);
        public string Transfer(int customerId, int debitAccountNumber, int creditAccountNumber, double amount);
    }
}