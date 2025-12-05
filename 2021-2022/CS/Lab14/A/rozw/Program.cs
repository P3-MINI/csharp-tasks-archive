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

            // stage 1

            // rozklad plci - liczbowo, posortuj po count

            var res1csA = database.Users.GroupBy(u => u.Gender).Select(u => new { Gender = u.Key, Count = u.Count() }).OrderBy(u => u.Count);
            foreach (var entity in res1csA)
                Console.WriteLine($"{entity.Gender} {entity.Count}");

            Console.WriteLine($"--------------");

            // stage 2

            //// CS
            //// rozklad grup wiekowych i plci - dwuwymiarowo, liczbowo
            //// posortuj po liczbie

            var res2csA = database.Users.GroupBy(u => new { u.Gender, u.Age }).Select(u => new { GenderAge = u.Key, Count = u.Count() }).OrderBy(u => u.Count);
            foreach (var entity in res2csA)
                Console.WriteLine($"{entity.Count} - {entity.GenderAge.Age} {entity.GenderAge.Gender} ");

            Console.WriteLine($"--------------");

            // stage 3

            // wypisz filmy, ktore są Horrorami, pierwsze 8
            var res3csA = database.Movies.Where(u => u.Genres.Contains("Horror")).Take(8);

            foreach (var entity in res3csA)
                Console.WriteLine($"{entity.MovieID} {entity.Title} {entity.Year} {entity.Genres} ");

            // stage 4

            // CS
            // remaki - po prostu, take 14

            var res4csA = database.Movies.GroupBy(u => u.Title).Select(u => new { Title = u.Key, Count = u.Count(), Movies = u }).Where(u => u.Count >= 2).SelectMany(u => u.Movies).Take(14);
            foreach (var entity in res4csA)
                Console.WriteLine($"{entity.MovieID} {entity.Title} {entity.Year} {entity.Genres} ");

            Console.WriteLine($"--------------");

            // pytanie 5

            // CS
            // najlepszy film wszechczasow

            var res5csA = from movie in database.Movies
                          join rating in database.Ratings on movie.MovieID equals rating.MovieID
                          select new { MovieID = movie.MovieID, Title = movie.Title, Rating = rating.Rating };

            var res5csA2 = res5csA.GroupBy(u => u.Title).Select(u => new { Title = u.Key, AverageRating = u.Average(u => u.Rating) }).OrderByDescending(u => u.AverageRating).Take(20);

            foreach (var entity in res5csA2)
                Console.WriteLine($"{entity.Title} {entity.AverageRating}");

            Console.WriteLine($"--------------");




        }
    }
}