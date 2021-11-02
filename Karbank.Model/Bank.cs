namespace Karbank.Model
{
    public class Bank
    {
        public string Name { get; set; }

        public IEnumerable<Account> Accounts { get; set; }
    }
}