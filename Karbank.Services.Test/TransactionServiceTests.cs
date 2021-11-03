using Karbank.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Karbank.Services.Test
{
    public class Tests
    {
        private readonly Mock<IAccountRepository> mockAccountRepository = new Mock<IAccountRepository>();
        private TransactionService sut;


        [SetUp]
        public void Setup()
        {
            var accountList = this.GetAccounts();
            mockAccountRepository.Setup(x => x.GetAccounts()).Returns(accountList);
            mockAccountRepository.Setup(x => x.GetAccount(It.IsAny<int>())).Returns((int x)=>accountList.Single(y=>y.AccountNumber==x));

            sut = new TransactionService(mockAccountRepository.Object);
        }

        private IEnumerable<Account> GetAccounts()
        {
            var accounts = new List<Account>()
            {
                new CheckingAccount{AccountNumber = 1, Balance = 500, OwnerId = 1},
                new CheckingAccount{AccountNumber = 2, Balance = 2000, OwnerId = 2},
                new InvestmentAccount {AccountNumber = 3, Balance = 2000, OwnerId = 3, AccountType= InvestmentAccountType.Individual},
                new InvestmentAccount {AccountNumber = 4, Balance = 3700, OwnerId = 4, AccountType = InvestmentAccountType.Corporate},
            };

            return accounts;
        }


        [Test]
        public void DepositMustBeGreaterThanZero()
        {
            Assert.AreEqual(Messages.TransactionGreaterThanZero, sut.Deposit(1, -5));
        }

    }
}