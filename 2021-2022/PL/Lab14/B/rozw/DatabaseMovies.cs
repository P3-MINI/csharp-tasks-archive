using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
        public int Year { get; set; }

        public Movie(int movieID, string title, string genres, int year)
        {
            MovieID = movieID;
            Title = title;
            Genres = genres;
            Year = year;
        }
    }

    public class User
    {
        public int UserID { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Occupation { get; set; }
        public string ZipCode { get; set; }

        public User(int userID, string gender, int age, int occupation, string zipcode)
        {
            UserID = userID;
            Gender = gender;
            Age = age;
            Occupation = occupation;
            ZipCode = zipcode;
        }
    }

    public class RatingEntry
    {
        public int UserID { get; set; }
        public int MovieID { get; set; }
        public double Rating { get; set; }
        public DateTime Timestamp { get; set; }

        public RatingEntry(int userID, int movieID, double rating, DateTime timestamp)
        {
            UserID = userID;
            MovieID = movieID;
            Rating = rating;
            Timestamp = timestamp;
        }
    }
    internal class DatabaseMovies
    {
        
        public List<Movie> Movies { get; set; }
        public List<User> Users { get; set; }
        public List<RatingEntry> Ratings { get; set; }

        static List<Movie> getMovies(string moviesPath)
        {
            StreamReader sr = new StreamReader(moviesPath);  // stream opened in Open mode
            List<Movie> movies = new List<Movie>();
            string[] buf;
                
            while(!sr.EndOfStream)
            {
                buf = sr.ReadLine().Split('@');
                int movieId = int.Parse(buf[0]);
                string title = buf[1];
                string genres = buf[2];
                int year = int.Parse(buf[3]);
                movies.Add(new Movie(movieId, title, genres, year));
            }
            sr.Close();
            return movies;
        }

        static List<User> getUsers(string moviesPath)
        {
            StreamReader sr = new StreamReader(moviesPath);  // stream opened in Open mode
            List<User> users = new List<User>();
            string[] buf;

            while (!sr.EndOfStream)
            {
                buf = sr.ReadLine().Split('@');
                int userId = int.Parse(buf[0]);
                string gender = buf[1];
                int age = int.Parse(buf[2]);
                int occupation = int.Parse(buf[3]);
                string zipcode = buf[4];
                users.Add(new User(userId, gender, age, occupation, zipcode));
            }
            sr.Close();
            return users;
        }

        static List<RatingEntry> getRatings(string moviesPath)
        {
            StreamReader sr = new StreamReader(moviesPath);  // stream opened in Open mode
            List<RatingEntry> ratings = new List<RatingEntry>();
            string[] buf;

            while (!sr.EndOfStream)
            {
                buf = sr.ReadLine().Split('@');
                int userId = int.Parse(buf[0]);
                int movieId = int.Parse(buf[1]);
                double rating = double.Parse(buf[2]);
                int seconds = int.Parse(buf[3]);
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                DateTime timestamp = dtDateTime.AddSeconds(seconds).ToLocalTime();
                ratings.Add(new RatingEntry(userId, movieId, rating, timestamp));
            }
            sr.Close();
            return ratings;
        }

        public static DatabaseMovies GetInstance(string moviesPath, string usersPath, string ratingsPath)
        {
            List<Movie> movies = getMovies(moviesPath);
            List<User> users = getUsers(usersPath);
            List<RatingEntry> ratings = getRatings(ratingsPath);

            return new DatabaseMovies()
            {
                Movies = movies,
                Users = users,
                Ratings = ratings
            };
        }
    }
}
