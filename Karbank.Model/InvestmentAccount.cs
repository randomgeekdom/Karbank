namespace Karbank.Model
{
    public class InvestmentAccount : Account
    {
        public InvestmentAccountType AccountType { get; set; }

        public override double WithdrawalLimit => AccountType==InvestmentAccountType.Individual ?  500 : base.WithdrawalLimit;
    }
}