namespace Bank;

public class BankAccount
{
    public string CustomerName { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string customerName, decimal balance)
    {
        CustomerName = customerName;
        Balance = balance;
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

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Balance += amount;
    }
}