using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    // https://grouplens.org/datasets/movielens/1m/
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseMovies database = DatabaseMovies.GetInstance("movies.csv", "users.csv", "ratings.csv");

            // pytanie 1

            // rozklad grup wiekowych - procentowo, posortuj po udziale
            var res1plB = database.Users.GroupBy(u => u.Age).Select(u => new { Age = u.Key, Fraction = u.Count() / (double)database.Users.Count() }).OrderBy(u => u.Fraction); ;
            foreach (var entity in res1plB)
                Console.WriteLine($"{entity.Age} {entity.Fraction}");

            Console.WriteLine($"--------------");

            // pytanie 2

            // Jaki jest średni rok oglądanego filmu w poszczególnych grupach wiekowych i plci - posortuj po plci a potem po wieku

            var res2plB = from movie in database.Movies
                          join rating in database.Ratings on movie.MovieID equals rating.MovieID
                          join user in database.Users on rating.UserID equals user.UserID
                          group movie.Year by new { user.Age, user.Gender };
            var res2plB2 = res2plB.Select(u => new { GenderAge = u.Key, AverageYear = u.Average() }).OrderBy(u => u.GenderAge.Gender).ThenBy(u => u.GenderAge.Age);

            foreach (var entity in res2plB2)
                Console.WriteLine($"{entity.GenderAge.Age} {entity.GenderAge.Gender}  - {entity.AverageYear} ");

            Console.WriteLine($"--------------");

            // pytanie 3


            // jaki gatunek filmowy jest najczestszy z tych zaczynajacych sie na C, posortuj po liczbie

            var res3plB = database.Movies.SelectMany(u => u.Genres.Split("|")).Where(u => u[0] == 'C').GroupBy(u => u).Select(u => new { Genre = u.Key, Count = u.Count() }).OrderBy(u => u.Count);

            foreach (var entity in res3plB)
                Console.WriteLine($"{entity.Genre} {entity.Count}");

            Console.WriteLine($"--------------");

            // pytanie 4


            // remaki - tylko najmlodsze

            var res4plB = database.Movies.GroupBy(u => u.Title).Select(u => new { Title = u.Key, Count = u.Count(), Movies = u, MaxYear = u.Max(u => u.Year) }).Where(u => u.Count >= 2);
            var res4plB2 = res4plB.SelectMany(u => u.Movies.Where(w => u.MaxYear == w.Year)).Take(14);

            foreach (var entity in res4plB2)
                Console.WriteLine($"{entity.MovieID} {entity.Title} {entity.Year} {entity.Genres} ");

            Console.WriteLine($"--------------");

            // pytanie 5

            // najlepszy film wszechczasow - od pewnej liczby glosow, podzial na plcie

            var res5plB = from movie in database.Movies
                          join rating in database.Ratings on movie.MovieID equals rating.MovieID
                          join user in database.Users on rating.UserID equals user.UserID
                          select new { MovieID = movie.MovieID, Title = movie.Title, Rating = rating.Rating, Gender = user.Gender };

            var res5plB2 = res5plB.GroupBy(u => new { u.Title, u.Gender }).Select(u => new { TitleGender = u.Key, Count = u.Count(), AverageRating = u.Average(u => u.Rating) }).Where(u => u.Count >= 100).OrderByDescending(u => u.AverageRating).Take(20);

            foreach (var entity in res5plB2)
                Console.WriteLine($"{entity.TitleGender.Title} {entity.TitleGender.Gender} {entity.AverageRating}");

            Console.WriteLine($"--------------");



        }
    }
}