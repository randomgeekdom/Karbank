using Karbank.Model;
using Moq;
using NUnit.Framework;
using System;
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
            mockAccountRepository.Setup(x => x.GetAccount(It.IsAny<int>())).Returns((int x) => accountList.SingleOrDefault(y => y.AccountNumber == x));
            mockAccountRepository.Setup(x => x.UpdateAccountBalance(It.IsAny<int>(), It.IsAny<double>())).Callback((int accountNumber, double amount) =>
              {
                  var account = accountList.Single(x => x.AccountNumber == accountNumber);
                  account.Balance = amount;
              });

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
        public void WhenDepositingAmountMustBeGreaterThanZero()
        {
            Assert.AreEqual(Messages.TransactionGreaterThanZero, sut.Deposit(1, -5));
        }

        [Test]
        public void WhenDepositingAccountMustExist()
        {
            Assert.AreEqual(Messages.InvalidAccountNumber, sut.Deposit(29, 3));
        }

        [Test]
        public void WhenDepositingAccountBalanceUpdates()
        {
            var account = this.mockAccountRepository.Object.GetAccount(3);
            var initialBalance = account.Balance;

            var depositAmount = 200;

            this.sut.Deposit(account.AccountNumber, depositAmount);

            account = this.mockAccountRepository.Object.GetAccount(3);

            Assert.AreEqual(initialBalance + depositAmount, account.Balance);
        }


        [Test]
        public void WhenTransferringAmountMustBeGreaterThanZero()
        {
            Assert.AreEqual(Messages.TransactionGreaterThanZero, sut.Transfer(1, 1, 1, -5));
        }

        [Test]
        public void WhenTransferringDebitAccountMustExist()
        {
            Assert.AreEqual(Messages.InvalidAccountNumber, sut.Transfer(1, 29, 1, 3));
        }

        [Test]
        public void WhenTransferringCreditAccountMustExist()
        {
            Assert.AreEqual(Messages.InvalidAccountNumber, sut.Transfer(1, 1, 29, 3));
        }

        [Test]
        public void WhenTransferringDebitBalanceMustExceedAmount()
        {
            Assert.AreEqual(Messages.InsufficientFunds, sut.Transfer(1, 1, 2, 501));
        }

        [Test]
        public void WhenTransferringAmountCannotExceedWithdrawLimit()
        {
            var account = this.mockAccountRepository.Object.GetAccount(3);
            Assert.AreEqual(string.Format(Messages.WithdrawLimit, account.WithdrawalLimit), sut.Transfer(3, 3, 1, account.WithdrawalLimit + 1));
        }

        [Test]
        public void WhenTransferringDebitAndCreditAccountsMustDiffer()
        {
            Assert.AreEqual(Messages.SameAccount, sut.Transfer(1, 1, 1, 200));
        }

        [Test]
        public void WhenTransferringCustomerMustBeTheOwnerOfTheDebitAccount()
        {
            Assert.AreEqual(Messages.InvalidOwner, sut.Transfer(2, 1, 2, 200));
        }

        [Test]
        public void WhenTransferringAmountIsWithdrawnFromDebitAccountAndEnteredInCreditAccount()
        {
            var debitAccount = this.mockAccountRepository.Object.GetAccount(1);
            var creditAccount = this.mockAccountRepository.Object.GetAccount(2);

            var debitAccountBalance = debitAccount.Balance;
            var creditBalance = creditAccount.Balance;
            var transferAmount = 200;

            sut.Transfer(debitAccount.OwnerId, debitAccount.AccountNumber, creditAccount.AccountNumber, transferAmount);

            Assert.AreEqual(debitAccountBalance - transferAmount, debitAccount.Balance);
            Assert.AreEqual(creditBalance + transferAmount, creditAccount.Balance);
        }

        [Test]
        public void WhenWithdrawingAmountMustBeGreaterThanZero()
        {
            Assert.AreEqual(Messages.TransactionGreaterThanZero, sut.Withdraw(1, 1, -5));
        }

        [Test]
        public void WhenWithdrawingDebitAccountMustExist()
        {
            Assert.AreEqual(Messages.InvalidAccountNumber, sut.Withdraw(1, 29, 3));
        }

        [Test]
        public void WhenWithdrawingDebitBalanceMustExceedAmount()
        {
            Assert.AreEqual(Messages.InsufficientFunds, sut.Withdraw(1, 1, 501));
        }

        [Test]
        public void WhenWithdrawingAmountCannotExceedWithdrawLimit()
        {
            var account = this.mockAccountRepository.Object.GetAccount(3);
            Assert.AreEqual(string.Format(Messages.WithdrawLimit, account.WithdrawalLimit), sut.Withdraw(3, 3, account.WithdrawalLimit + 1));
        }

        [Test]
        public void WhenWithdrawingCustomerMustBeTheOwnerOfTheDebitAccount()
        {
            Assert.AreEqual(Messages.InvalidOwner, sut.Withdraw(2, 1, 200));
        }

        [Test]
        public void WhenWithdrawingAmountIsWithdrawnFromDebitAccountAndEnteredInCreditAccount()
        {
            var debitAccount = this.mockAccountRepository.Object.GetAccount(1);

            var debitAccountBalance = debitAccount.Balance;
            var transferAmount = 200;

            sut.Withdraw(debitAccount.OwnerId, debitAccount.AccountNumber, transferAmount);

            Assert.AreEqual(debitAccountBalance - transferAmount, debitAccount.Balance);
        }
    }
}