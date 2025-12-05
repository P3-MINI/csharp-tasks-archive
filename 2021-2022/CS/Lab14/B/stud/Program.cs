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
			// You are provided with a database with three tables: database.Movies, database.Users, database.Ratings
			// You can obtain information about available columns (fields) by looking at classes Movie, User, RatingEntry
			// In the Users table a field Age is rather age group (range) identifier than actual age. E.g. 1 means a range 1-18.
			// Every movie has its unique id in MovieID, every user has its unique id in UserID.
			// Both these tables (Movies, Users), are connected by Ratings: a single rating is provided by a certain user for a certain movie.
            DatabaseMovies database = DatabaseMovies.GetInstance("movies.csv", "users.csv", "ratings.csv");

            // stage 1

            // Count a fraction of males and females in a Users table
			// Order (ascending) by a gender
			
            // your solution
			
			Console.WriteLine($"--------------");

            // stage 2
			
			// Count a fraction of males and females are in every age group
			// Sort (ascending) a result by age and nextly by a gender.

            // your solution

			Console.WriteLine($"--------------");
			
            // stage 3
            
			// Filter out (into a result) movies whose one of genres starts with a letter "M".
			// The result should skip first 3 movies and contain next 6 movies.

            // your solution
			
			Console.WriteLine($"--------------");

            // stage 4
			
			// We define remakes of a single movie as a set of movies which title is the same (but years of production are different)
			// Your result should contain all movies, which are remakes (print all movies, for which we can find at least one different movie with the same title).
			// Print all details of movies, but also additionally: minimum year of remake set, maximum year, and a difference between minimum and maximum.			
			
            // your solution

            Console.WriteLine($"--------------");

            // pytanie 5

            // Sort (descending) movies by average rating, but with distinction for votes of males and females.
			// Result should skip first 40 movies and contain the next 20 movies.	
			
			// your solution

            Console.WriteLine($"--------------");
        }
    }
}