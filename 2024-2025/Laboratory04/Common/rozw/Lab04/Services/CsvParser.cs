using Lab04.Models;
using Lab04.Services.Validators;

namespace Lab04.Services;

public sealed class CsvParser
{
    private readonly NameValidator NameValidator;
    private readonly PhoneNumberValidator PhoneNumberValidator;
    private readonly EmailAddressValidator EmailAddressValidator;

    public CsvParser(
        NameValidator nameValidator,
        PhoneNumberValidator phoneNumberValidator,
        EmailAddressValidator emailAddressValidator)
    {
        NameValidator = nameValidator;
        PhoneNumberValidator = phoneNumberValidator;
        EmailAddressValidator = emailAddressValidator;
    }

    public Customer[] ParseCustomers(string content)
    {
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var customers = new List<Customer>();
        var counter = 0;

        foreach (var line in lines)
        {
            counter++;
            var entries = line.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (entries.Length != 4)
            {
                Console.WriteLine($"[{DateTime.Now:g}] Invalid Customer in line {counter}.");
                continue;
            }

            var names = entries[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (names.Length != 2)
            {
                Console.WriteLine($"[{DateTime.Now:g}] Invalid Customer in line {counter}.");
                continue;
            }

            var firstName = names[0];
            var lastName = names[1];
            var phoneNumber = entries[1];
            var emailAddress = entries[2];
            var satisfactionRatings = ParseSatisfactionRatings(entries[3]);

            if (!NameValidator.Validate(firstName) ||
                !NameValidator.Validate(lastName) ||
                !PhoneNumberValidator.Validate(phoneNumber) ||
                !EmailAddressValidator.Validate(emailAddress))
            {
                Console.WriteLine($"[{DateTime.Now:g}] Invalid Customer in line ${counter}.");
                continue;
            }

            customers.Add(
                new Customer(
                    firstName.ToUpper(),
                    lastName.ToUpper(),
                    phoneNumber.Replace('6', '9'),
                    emailAddress, satisfactionRatings
                    )
            );
        }

        return [.. customers];
    }

    private double[] ParseSatisfactionRatings(string value)
    {
        var ratings = value
            .Trim('[', ']')
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var doubleRatings = new List<double>();

        foreach (var rating in ratings)
        {
            doubleRatings.Add(double.Parse(rating));
        }

        return [.. doubleRatings];
    }
}