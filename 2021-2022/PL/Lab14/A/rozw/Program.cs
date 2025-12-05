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

            // PL
            // rozklad grup wiekowych - liczbowo, posortuj po wieku
            var res1plA = database.Users.GroupBy(u => u.Age).Select(u => new { Age = u.Key, Count = u.Count() }).OrderBy(u => u.Age); ;
            foreach (var entity in res1plA)
                Console.WriteLine($"{entity.Age} {entity.Count}");

            Console.WriteLine($"--------------");

            // pytanie 2

            // PL
            // Jaki jest minimalny rok oglądanego filmu w poszczególnych grupach wiekowych i plci - posortuj po minimalnym roku

            var res2plA = from movie in database.Movies
                          join rating in database.Ratings on movie.MovieID equals rating.MovieID
                          join user in database.Users on rating.UserID equals user.UserID
                          group movie.Year by new { user.Age, user.Gender };
            var res2plA2 = res2plA.Select(u => new { GenderAge = u.Key, MinYear = u.Min() }).OrderBy(u => u.MinYear);

            foreach (var entity in res2plA2)
                Console.WriteLine($"{entity.GenderAge.Age} {entity.GenderAge.Gender}  - {entity.MinYear} ");

            Console.WriteLine($"--------------");

            // pytanie 3

            // PL
            // jaki gatunek filmowy jest najczestszy, posortuj alfabetycznie

            var res3plA = database.Movies.SelectMany(u => u.Genres.Split("|")).GroupBy(u => u).Select(u => new { Genre = u.Key, Count = u.Count() }).OrderBy(u => u.Genre);

            foreach (var entity in res3plA)
                Console.WriteLine($"{entity.Genre} {entity.Count}");

            // pytanie 4

            // PL
            // remaki - posortowane od najmlodszego do najstarszego

            var res4plA = database.Movies.GroupBy(u => u.Title).Select(u => new { Title = u.Key, Count = u.Count(), Movies = u.OrderBy(u => u.Year) }).Where(u => u.Count >= 2).SelectMany(u => u.Movies).Take(14);
            foreach (var entity in res4plA)
                Console.WriteLine($"{entity.MovieID} {entity.Title} {entity.Year} {entity.Genres} ");

            Console.WriteLine($"--------------");


            // pytanie 5

            // PL
            // najlepszy film wszechczasow - od pewnej liczby glosow

            var res5plA = from movie in database.Movies
                          join rating in database.Ratings on movie.MovieID equals rating.MovieID
                          select new { MovieID = movie.MovieID, Title = movie.Title, Rating = rating.Rating };

            var res5plA2 = res5plA.GroupBy(u => u.Title).Select(u => new { Title = u.Key, Count=u.Count(), AverageRating = u.Average(u => u.Rating) }).Where(u => u.Count >= 100).OrderByDescending(u => u.AverageRating).Take(20);

            foreach (var entity in res5plA2)
                Console.WriteLine($"{entity.Title} {entity.AverageRating}");

            Console.WriteLine($"--------------");

        }
    }
}