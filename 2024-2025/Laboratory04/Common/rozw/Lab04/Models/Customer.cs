using System.Text;

namespace Lab04.Models;

public sealed class Customer
{
    public Customer(
        string firstName, 
        string lastName, 
        string phoneNumber, 
        string emailAddress, 
        double[] satisfactionRatings)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
        SatisfactionRatings = satisfactionRatings;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string EmailAddress { get; }
    public double[] SatisfactionRatings { get; }

    public override string ToString()
    {
        var sb = new StringBuilder($"({FirstName} {LastName}, {PhoneNumber}, {EmailAddress}, [");

        for (var i = 0; i < SatisfactionRatings.Length; i++)
        {
            sb.Append(SatisfactionRatings[i].ToString("F1"));
            if (i < SatisfactionRatings.Length - 1)
            {
                sb.Append(';');
            }
        }

        sb.Append("])");

        return sb.ToString();
    }
}