namespace Karbank.Model
{
    public abstract class Account
    {
        public User Owner { get; set; }
        public double Balance { get; set; }
    }
}