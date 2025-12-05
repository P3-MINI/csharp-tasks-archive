namespace Bank;

public interface ITransactionLogger
{
    public void LogInitialBalance(string customerName, decimal balance);
    public void LogDebit(decimal amount);
    public void LogCredit(decimal amount);
}

public class BankAccount
{
    public string CustomerName { get; }
    public decimal Balance { get; private set; }
    private ITransactionLogger Logger { get; }

    public BankAccount(string customerName, decimal balance, ITransactionLogger logger)
    {
        CustomerName = customerName;
        Balance = balance;
        Logger = logger;
        logger.LogInitialBalance(customerName, balance);
    }

    public void Debit(decimal amount)
    {
        if (amount > Balance)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Logger.LogDebit(amount);
        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Logger.LogCredit(amount);
        Balance += amount;
    }
}