namespace Karbank.Model
{
    public abstract class Account
    {
        public int AccountNumber { get; set; }
        public int OwnerId { get; set; }
        public double Balance { get; set; }
    }
}