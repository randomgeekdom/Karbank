namespace Karbank.Model
{
    public abstract class Account
    {
        public virtual double WithdrawalLimit => double.MaxValue;
        public int AccountNumber { get; set; }
        public int OwnerId { get; set; }
        public double Balance { get; set; }
    }
}