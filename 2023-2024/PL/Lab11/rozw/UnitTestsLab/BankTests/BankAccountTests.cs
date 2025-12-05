using Bank;
using FluentAssertions;
using NSubstitute;

namespace BankTests;

[TestClass]
public class BankAccountTests
{
    [TestMethod]
    public void Debit_WithValidAmount_UpdatesBalance()
    {
        // Arrange
        decimal beginningBalance = 11.99m;
        decimal debitAmount = 4.55m;
        decimal expected = 7.44m;
        ITransactionLogger logger = Substitute.For<ITransactionLogger>();
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, logger);

        // Act
        account.Debit(debitAmount);

        // Assert
        decimal actual = account.Balance;
        Assert.AreEqual(expected, actual, "Account should be debited correctly");
        // actual.Should().Be(expected, "Account should be debited correctly");
    }

    [TestMethod]
    public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        decimal beginningBalance = 11.99m;
        decimal debitAmount = -100.00m;
        ITransactionLogger logger = Substitute.For<ITransactionLogger>();
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, logger);

        // Act
        Action debitAction = () => account.Debit(debitAmount);
        
        // Assert
        // Assert.ThrowsException<ArgumentOutOfRangeException>(debit, "Amount is less than zero");
        debitAction.Should().Throw<ArgumentOutOfRangeException>("Amount is less than zero");
    }

    [TestMethod]
    public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        decimal beginningBalance = 11.99m;
        decimal debitAmount = 20.00m;
        ITransactionLogger logger = Substitute.For<ITransactionLogger>();
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, logger);

        // Act
        Action debitAction = () => account.Debit(debitAmount);
        
        // Assert
        // Assert.ThrowsException<ArgumentOutOfRangeException>(debit);
        debitAction.Should().Throw<ArgumentOutOfRangeException>("Amount is more than balance");
    }
    
    [TestMethod]
    public void Constructor_WhenAccountCreated_InitialBalanceShouldBeLogged()
    {
        // Arrange
        decimal beginningBalance = 11.99m;
        string client = "Mr. Bryan Walton";
        ITransactionLogger logger = Substitute.For<ITransactionLogger>();

        // Act
        _ = new BankAccount(client, beginningBalance, logger);

        // Assert
        logger.Received().LogInitialBalance(client, beginningBalance);
    }
    
    [TestMethod]
    public void Debit_WhenDebitValidAmount_TransactionShouldBeLogged()
    {
        // Arrange
        decimal beginningBalance = 11.99m;
        decimal debitAmount = 10.00m;
        ITransactionLogger logger = Substitute.For<ITransactionLogger>();
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance, logger);

        // Act
        account.Debit(debitAmount);
        
        // Assert
        logger.Received().LogDebit(debitAmount);
    }
}