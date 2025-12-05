namespace Lab14;

public class Program
{

    /// <summary>
    /// Task 1 (0.5 pts)
    /// Find all library members who borrowed less than 5 books.
    /// Return their member ID, first name, last name, and a number of borrowed books.
    /// </summary>
    public static IEnumerable<(int Id, string FirstName, string LastName, int Count)> GetMemberWith5LessLoans(Database db)
    {
        return Enumerable.Empty<(int, string, string, int)>();
    }

    /// <summary>
    /// Task 2 (0.5 pts)
    /// Find 10 members with the highest number of borrowed books that were returned (read).
    /// Return member ID, first name, last name, and the number of books read.
    /// </summary>
    public static IEnumerable<(int Id, string FirstName, string LastName, int Count)> GetBestReaders(Database db)
    {
        return Enumerable.Empty<(int, string, string, int)>();
    }

    /// <summary>
    /// Task 3 (1.0 pts)
    /// Find all books which weren't borrowed for at least the last N days.
    /// Return book ID, title, and number of days. For books that were never borrowed, return -1.
    /// Sort them by day value first with the highest day count, books never borrowed (with -1) can be at the end.
    /// Hint: You can use `let` statement to save some intermediate values.
    /// </summary>
    public static IEnumerable<(int Id, string Title, int DaysSincelastLoan)> GetNonPopularBooks(Database db, int N)
    {
        return Enumerable.Empty<(int, string, int)>();
    }

    /// <summary>
    /// Task 4 (1.5 pts)
    /// Find the top 10 members with the highest number of borrowed books, and calculate and include the average duration of their loans.
    /// Results should be sorted based on a number of borrowed books.
    /// </summary>
    public static IEnumerable<(int Id, string FirstName, string LastName, int TotalLoans, double AverageLoanDuration)> GetTop10Members(Database db)
    {
        return Enumerable.Empty<(int, string, string, int, double)>();
    }

    /// <summary>
    /// Task 5 (1.5 pts)
    /// Find the latest loan date for each member. Include also the number of books each member has borrowed in total.
    /// Sort results by the recency of the member's latest loan date.
    /// Hint: FirstOrDefault()
    /// </summary>
    public static IEnumerable<(int Id, string FirstName, string LastName, DateTime? LastLoanTime, int TotalBooksBorrowed)> GetLatestMemberLoan(Database db)
    {
        return Enumerable.Empty<(int, string, string, DateTime?, int)>();
    }

    private static void Main()
    {
        var database = Database.CreateDatabase();

        Console.WriteLine("\n --==== Task 1 (0.5 pts) ====--");
        foreach (var record in GetMemberWith5LessLoans(database))
        {
            Console.WriteLine("{0,3} {1,10} {2, 10} {3}", record.Id, record.FirstName, record.LastName, record.Count);
        }

        Console.WriteLine("\n --==== Task 2 (0.5 pts) ====--");
        foreach (var record in GetBestReaders(database))
        {
            Console.WriteLine("{0,3}. {1,10} {2,-10} {3}", record.Id, record.FirstName, record.LastName, record.Count);
        }

        Console.WriteLine("\n --==== Task 3 (1.0 pts) ====--");
        foreach (var record in GetNonPopularBooks(database, 100))
        {
            Console.WriteLine("{0,3}. {1,30} {2}", record.Id, record.Title, record.DaysSincelastLoan);
        }

        Console.WriteLine("\n --==== Task 4 (1.5 pts) ====--");
        foreach (var record in GetTop10Members(database))
        {
            Console.WriteLine("{0,3}. {1,10} {2,10} {3,3} {4,8:0.00}", record.Id, record.FirstName, record.LastName, record.TotalLoans, record.AverageLoanDuration);
        }

        Console.WriteLine("\n --==== Task 5 (1.5 pts) ====--");
        int i = 1;
        foreach (var record in GetLatestMemberLoan(database))
        {
            Console.WriteLine("{0,3}. {2,20} {1,3} {3,14} {4}",
                i++, record.Id, $"{record.FirstName} {record.LastName}",
                record.LastLoanTime.HasValue ? record.LastLoanTime.Value.ToString("yyyy-MM-dd") : "No loans yet", record.TotalBooksBorrowed);
        }

    }
}
