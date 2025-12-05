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
        Assert.Fail();
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