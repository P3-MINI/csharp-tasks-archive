using Bank;

namespace BankTests;

[TestClass]
public class BankAccountTests
{
    [TestMethod]
    public void Debit_WithValidAmount_UpdatesBalance()
    {
        Assert.Fail();
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
        debitAction.Should().Throw<ArgumentOutOfRangeException>("Amount is less than zero");
    }

    [TestMethod]
    public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
    {
        Assert.Fail();
    }
    
    [TestMethod]
    public void Constructor_WhenAccountCreated_InitialBalanceShouldBeLogged()
    {
        Assert.Fail();
    }
    
    [TestMethod]
    public void Debit_WhenDebitValidAmount_TransactionShouldBeLogged()
    {
        Assert.Fail();
    }
}