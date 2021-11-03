using Karbank.Model;

namespace Karbank.Services
{
    public interface IAccountRepository
    {
        public Account GetAccount(int accountNumber);
        public IEnumerable<Account> GetAccounts();

        public void UpdateAccountBalance (int accountNumber, double newBalance);
    }
}