using System.Globalization;

namespace p3a
{
	public class MovieDatabase
	{
		List<Movie> movies;

		List<(DateTime, string)> holidays = new List<(DateTime, string)>() {(DateTime.Parse("2012-01-02"),"New Year Day"),
																			(DateTime.Parse("2012-01-16"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2012-02-20"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2012-05-28"),"Memorial Day"),
																			(DateTime.Parse("2012-07-04"),"Independence Day"),
																			(DateTime.Parse("2012-09-03"),"Labor Day"),
																			(DateTime.Parse("2012-10-08"),"Columbus Day"),
																			(DateTime.Parse("2012-11-12"),"Veterans Day"),
																			(DateTime.Parse("2012-11-22"),"Thanksgiving Day"),
																			(DateTime.Parse("2012-12-25"),"Christmas Day"),
																			(DateTime.Parse("2013-01-01"),"New Year Day"),
																			(DateTime.Parse("2013-01-21"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2013-02-18"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2013-05-27"),"Memorial Day"),
																			(DateTime.Parse("2013-07-04"),"Independence Day"),
																			(DateTime.Parse("2013-09-02"),"Labor Day"),
																			(DateTime.Parse("2013-10-14"),"Columbus Day"),
																			(DateTime.Parse("2013-11-11"),"Veterans Day"),
																			(DateTime.Parse("2013-11-28"),"Thanksgiving Day"),
																			(DateTime.Parse("2013-12-25"),"Christmas Day"),
																			(DateTime.Parse("2014-01-01"),"New Year Day"),
																			(DateTime.Parse("2014-01-20"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2014-02-17"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2014-05-26"),"Memorial Day"),
																			(DateTime.Parse("2014-07-04"),"Independence Day"),
																			(DateTime.Parse("2014-09-01"),"Labor Day"),
																			(DateTime.Parse("2014-10-13"),"Columbus Day"),
																			(DateTime.Parse("2014-11-11"),"Veterans Day"),
																			(DateTime.Parse("2014-11-27"),"Thanksgiving Day"),
																			(DateTime.Parse("2014-12-25"),"Christmas Day"),
																			(DateTime.Parse("2015-01-01"),"New Year Day"),
																			(DateTime.Parse("2015-01-19"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2015-02-16"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2015-05-25"),"Memorial Day"),
																			(DateTime.Parse("2015-07-03"),"Independence Day"),
																			(DateTime.Parse("2015-09-07"),"Labor Day"),
																			(DateTime.Parse("2015-10-12"),"Columbus Day"),
																			(DateTime.Parse("2015-11-11"),"Veterans Day"),
																			(DateTime.Parse("2015-11-26"),"Thanksgiving Day"),
																			(DateTime.Parse("2015-12-25"),"Christmas Day"),
																			(DateTime.Parse("2016-01-01"),"New Year Day"),
																			(DateTime.Parse("2016-01-18"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2016-02-15"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2016-05-30"),"Memorial Day"),
																			(DateTime.Parse("2016-07-04"),"Independence Day"),
																			(DateTime.Parse("2016-09-05"),"Labor Day"),
																			(DateTime.Parse("2016-10-10"),"Columbus Day"),
																			(DateTime.Parse("2016-11-11"),"Veterans Day"),
																			(DateTime.Parse("2016-11-24"),"Thanksgiving Day"),
																			(DateTime.Parse("2016-12-25"),"Christmas Day"),
																			(DateTime.Parse("2017-01-02"),"New Year Day"),
																			(DateTime.Parse("2017-01-16"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2017-02-20"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2017-05-29"),"Memorial Day"),
																			(DateTime.Parse("2017-07-04"),"Independence Day"),
																			(DateTime.Parse("2017-09-04"),"Labor Day"),
																			(DateTime.Parse("2017-10-09"),"Columbus Day"),
																			(DateTime.Parse("2017-11-10"),"Veterans Day"),
																			(DateTime.Parse("2017-11-23"),"Thanksgiving Day"),
																			(DateTime.Parse("2017-12-25"),"Christmas Day"),
																			(DateTime.Parse("2018-01-01"),"New Year Day"),
																			(DateTime.Parse("2018-01-15"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2018-02-19"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2018-05-28"),"Memorial Day"),
																			(DateTime.Parse("2018-07-04"),"Independence Day"),
																			(DateTime.Parse("2018-09-03"),"Labor Day"),
																			(DateTime.Parse("2018-10-08"),"Columbus Day"),
																			(DateTime.Parse("2018-11-12"),"Veterans Day"),
																			(DateTime.Parse("2018-11-22"),"Thanksgiving Day"),
																			(DateTime.Parse("2018-12-25"),"Christmas Day"),
																			(DateTime.Parse("2019-01-01"),"New Year Day"),
																			(DateTime.Parse("2019-01-21"),"Martin Luther King Jr. Day") };

		public MovieDatabase()
		{
			LoadMovies();
		}

		private void LoadMovies()
		{
			string filePath = @"../../../movies.csv";

			if (!File.Exists(filePath))
			{
				Console.WriteLine("File movies.csv doesn't exist in project directory.");
				return;
			}

			movies = new List<Movie>();

			StreamReader reader = new StreamReader(filePath);
			string line;
			line = reader.ReadLine(); //skip header line
			while ((line = reader.ReadLine()) != null)
			{
				string[] splitLine = line.Split(';');
				Movie newMovie = new Movie();
				newMovie.title = splitLine[0];
				newMovie.budget = double.Parse(splitLine[1], CultureInfo.InvariantCulture);
				newMovie.genres = splitLine[2];
				newMovie.origin_language = splitLine[3];
				newMovie.release_date = DateTime.Parse(splitLine[4]);
				newMovie.revenue = double.Parse(splitLine[5], CultureInfo.InvariantCulture);
				newMovie.runtime = int.Parse(splitLine[6]);
				newMovie.vote_average = double.Parse(splitLine[7], CultureInfo.InvariantCulture);
				newMovie.vote_count = int.Parse(splitLine[8]);

				movies.Add(newMovie);
			}

			reader.Close();
		}

        //solutions for this stage MUST be written with Query Expressions
        public void StageA() 
		{
			//find and print titles of all movies released in 2006 (release_date),
			//with budget greater than 100 millions
			var a1 = from m in movies
					 where m.release_date.Year == 2006
					 where m.budget > 100000000
					 select m.title;

			Console.WriteLine("A1: " + String.Join(", ", a1));

			//find a movie (print its title) released in the previous century with the greatest revenue
			var a2 = (from m in movies
					  where m.release_date.Year < 2000
					  orderby m.revenue descending
					  select m).FirstOrDefault();

			Console.WriteLine("A2: " + a2.title);

			//find 5 comedies (print their titles) with the greatest average rate (vote_average)
			//which number of votes (votes_count) is greater than 5000
			var a3 = (from m in movies
					  where m.genres.Contains("Comedy")
					  where m.vote_count > 5000
					  orderby m.vote_average descending
					  select m).Take(5);

			Console.WriteLine("A3: " + String.Join(", ", a3.Select(x => x.title)));
		}

		public void StageB()
		{
			//print average number of words in non-English titles (origin_language != "en")
			var b1 = movies.Where(x => x.origin_language != "en").Average(x => x.title.Split(' ').Length);

			Console.WriteLine("B1: " + b1);

			//print average number of votes for movies with release date in 2016
			//but don't count 10 with the greatest and 10 with the lowest number of votes
			var b2 = movies.Where(x => x.release_date.Year == 2016)
					   .OrderBy(x => x.vote_count).Skip(10)
					   .OrderByDescending(x => x.vote_count).Skip(10)
					   .Average(x => x.vote_count);
			Console.WriteLine("B2: " + b2);
		}

		public void StageC()
		{
			//find the day (date) with the most number of movies released
			//print which day it was and how many movies was released
			var c1 = (from x in
						  (from m in movies
						   group m by m.release_date into g
						   select new { release_date = g.Key, movies_count = g.Count() }
						  )
					  orderby x.movies_count descending
					  select x).FirstOrDefault();

			Console.WriteLine("C1: " + c1.release_date.ToShortDateString() + " number of movies: " + c1.movies_count);

			//for which language (origin_language) excluding english (en) sum of movies budgets was the greatest
			//print this language and sum
			var c2 = movies.Where(x => x.origin_language != "en")
				.GroupBy(x => x.origin_language)
				.Select(x => new { lang = x.Key, sum = x.Sum(y => y.budget) })
				.OrderByDescending(x => x.sum).First();

			Console.WriteLine("C2: " + c2.lang + ":" + c2.sum);
		}

        //solutions for this stage MUST be written with Query Expressions
        public void StageD()
		{
			//using list of American holidays created at the beginning of this class find all movies
			//which release date was holiday day and title of the movie has at least one common word with holiday name (case insensitive)
			//the results print as pairs: movie title, holiday name
			var d1 = from m in movies
					 join h in holidays on m.release_date equals h.Item1
					 where m.title.ToUpper().Split(' ')
					 .Intersect(h.Item2.ToUpper().Split(' ')).Count() > 0
					 select new { title = m.title, holiday = h.Item2 };

			Console.WriteLine("D1: " + String.Join(", ", d1.Select(x => "(" + x.title + ", " + x.holiday + ")")));

			//find and print titles of all movies pairs which were released in the same day, have the same average vote,
			//and number of votes both of them are greater than 1000
			//all returned pairs have to be unique - print only (a,b), not (b,a)
			var d2 = from m1 in movies
					 join m2 in movies on m1.release_date equals m2.release_date
					 where m1.title != m2.title
					 where m1.vote_average == m2.vote_average
					 where m1.vote_count > 1000
					 where m2.vote_count > 1000
					 where m1.title.CompareTo(m2.title) > 0
					 select new { m1 = m1.title, m2 = m2.title };

			Console.WriteLine("D2: " + String.Join(", ", d2.Select(x => "(" + x.m1 + ", " + x.m2 + ")")));
		}

		public void StageE()
		{
			//find 5 the most frequent words (case insensitive) which has at least 4 letters in titles of movies
			//which were released after 2010
			//print those words and their frequency (number of titles)
			var wordsInTitles = movies.Where(x => x.release_date.Year > 2010).Select(x => x.title.ToUpper())
								  .Aggregate((x1, x2) => x1 + " " + x2).Split(' ').Where(x => x.Length > 3);

			var e1 = wordsInTitles.GroupBy(x => x)
								  .Select(x => new
								  {
									  word = x.Key,
									  count = x.Count()
								  })
								  .OrderByDescending(x => x.count)
								  .Take(5);

			Console.WriteLine("E1: " + String.Join(", ", e1.Select(x => x.word + ":" + x.count)));

			//find and print all prime numbers less than 100
			var e2 = Enumerable.Range(2, 99)
					.Where(value => !Enumerable.Range(2, (int)Math.Ceiling(Math.Sqrt(value)))
					.Any(divisor => value != divisor && value % divisor == 0));

			Console.WriteLine("E2: " + string.Join(", ", e2));
		}

	}
}