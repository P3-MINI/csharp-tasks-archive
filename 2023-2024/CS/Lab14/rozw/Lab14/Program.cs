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
        return from member in db.Members
               join loans in db.Loans on member.MemberId equals loans.MemberId into MemberLoans
               where MemberLoans.Count() < 5
               orderby member.LastName, member.FirstName
               select (member.MemberId, member.FirstName, member.LastName, MemberLoans.Count());
    }

    /// <summary>
    /// Task 2 (0.5 pts)
    /// Find 10 members with the highest number of borrowed books that were returned (read).
    /// Return member ID, first name, last name, and the number of books read.
    /// </summary>
    public static IEnumerable<(int Id, string FirstName, string LastName, int Count)> GetBestReaders(Database db)
    {
        return (from loan in db.Loans
                where loan.ReturnDate != null
                group loan by loan.MemberId into loanGroup
                join member in db.Members on loanGroup.Key equals member.MemberId
                let count = loanGroup.Count()
                orderby count descending
                select (member.MemberId, member.FirstName, member.LastName, count)
                ).Take(10);
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
        return from book in db.Books
               join loan in db.Loans on book.BookId equals loan.BookId into BookLoans
               let lastLoanDate = BookLoans.Max(bl => (DateTime?)bl.LoanDate)
               let daysSincelastLoan = lastLoanDate.HasValue ? (DateTime.UtcNow - lastLoanDate.Value).Days : -1
               where daysSincelastLoan > N || daysSincelastLoan == -1
               orderby daysSincelastLoan descending
               select (book.BookId, book.Title, daysSincelastLoan);
    }

    /// <summary>
    /// Task 4 (1.5 pts)
    /// Find the top 10 members with the highest number of borrowed books, and calculate and include the average duration of their loans.
    /// Results should be sorted based on a number of borrowed books.
    /// </summary>
    public static IEnumerable<(int Id, string FirstName, string LastName, int TotalLoans, double AverageLoanDuration)> GetTop10Members(Database db)
    {
        return (from loan in db.Loans
                group loan by loan.MemberId into loanGroup
                join member in db.Members on loanGroup.Key equals member.MemberId
                let loanedBooks = loanGroup.Where(l => l.ReturnDate.HasValue)
                let totalLoans = loanGroup.Count()
                let averageLoanDuration = loanedBooks.Count() == 0 ? double.NaN : loanedBooks.Average(l => (l.ReturnDate.Value - l.LoanDate).TotalDays)
                orderby totalLoans descending, averageLoanDuration descending
                select (member.MemberId, member.FirstName, member.LastName, totalLoans, averageLoanDuration)).Take(10);
    }

    /// <summary>
    /// Task 5 (1.5 pts)
    /// Find the latest loan date for each member. Include also the number of books each member has borrowed in total.
    /// Sort results by the recency of the member's latest loan date.
    /// Hint: FirstOrDefault()
    /// </summary>
    public static IEnumerable<(int Id, string FirstName, string LastName, DateTime? LastLoanTime, int TotalBooksBorrowed)> GetLatestMemberLoan(Database db)
    {
        return from member in db.Members
               join loan in db.Loans on member.MemberId equals loan.MemberId into memberLoans
               let latestLoan = memberLoans.OrderByDescending(l => l.LoanDate).FirstOrDefault()
               orderby latestLoan.LoanDate descending
               select (member.MemberId, member.FirstName, member.LastName,
                        memberLoans.Count() == 0 ? null : (DateTime?)latestLoan.LoanDate, memberLoans.Count());
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
