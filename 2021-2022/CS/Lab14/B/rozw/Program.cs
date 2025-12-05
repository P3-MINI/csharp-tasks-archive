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

            // rozklad plci - procentowo, posortuj po plci
            var res1csB = database.Users.GroupBy(u => u.Gender).Select(u => new { Gender = u.Key, Fraction = u.Count() / (double)database.Users.Count() }).OrderBy(u => u.Gender);
            foreach (var entity in res1csB)
                Console.WriteLine($"{entity.Gender} {entity.Fraction}");

            // stage 2

            // rozklad grup wiekowych i plci - dwuwymiarowo, procentowo
            // posortuj po wieku a potem plci

            var res2csB = database.Users.GroupBy(u => new { u.Gender, u.Age }).Select(u => new { GenderAge = u.Key, Count = u.Count() / (double)database.Users.Count() }).OrderBy(u => u.GenderAge.Age).ThenBy(u => u.GenderAge.Gender);
            foreach (var entity in res2csB)
                Console.WriteLine($"{entity.Count} - {entity.GenderAge.Age} {entity.GenderAge.Gender} ");

            // stage 3
            // wypisz filmy, które mają gatunek filmowy zaczynający się od litery "M", pomin pierwsze 3, wez nastepne 6

            var res3csB = database.Movies.Where(u => { var genres = u.Genres.Split("|"); return genres.Any(g => g[0] == 'M'); }).Skip(3).Take(6);

            foreach (var entity in res3csB)
                Console.WriteLine($"{entity.MovieID} {entity.Title} {entity.Year} {entity.Genres} ");

            // stage 4

            // remaki - roznica max a min
            var res4csB = database.Movies.GroupBy(u => u.Title).Select(u => new { Title = u.Key, Count = u.Count(), Min = u.Min(u => u.Year), Max = u.Max(u => u.Year), Diff = u.Max(u => u.Year) - u.Min(u => u.Year) }).Where(u => u.Count >= 2);
            foreach (var entity in res4csB)
                Console.WriteLine($"{entity.Title} {entity.Min} {entity.Max} {entity.Diff} ");


            Console.WriteLine($"--------------");

            // pytanie 5

            // CS

            // najlepszy film wszechczasow - podzial na plcie

            var res5csB = from movie in database.Movies
                          join rating in database.Ratings on movie.MovieID equals rating.MovieID
                          join user in database.Users on rating.UserID equals user.UserID
                          select new { MovieID = movie.MovieID, Title = movie.Title, Rating = rating.Rating, Gender = user.Gender };

            var res5csB2 = res5csB.GroupBy(u => new { u.Title, u.Gender }).Select(u => new { TitleGender = u.Key, AverageRating = u.Average(u => u.Rating) }).OrderByDescending(u => u.AverageRating).Skip(40).Take(20);

            foreach (var entity in res5csB2)
                Console.WriteLine($"{entity.TitleGender.Title} {entity.TitleGender.Gender} {entity.AverageRating}");

            Console.WriteLine($"--------------");
        }
    }
}