using System.Globalization;

namespace p3z
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
                newMovie.original_language = splitLine[3];
                newMovie.release_date = DateTime.Parse(splitLine[4]);
                newMovie.revenue = double.Parse(splitLine[5], CultureInfo.InvariantCulture);
                newMovie.runtime = int.Parse(splitLine[6]);
                newMovie.vote_average = double.Parse(splitLine[7], CultureInfo.InvariantCulture);
                newMovie.vote_count = int.Parse(splitLine[8]);

                movies.Add(newMovie);
            }

            reader.Close();
        }


		public void PartA() //co najmniej 2 rozwiązania tego etapu OBOWIĄZKOWO napisać z użyciem Query Expression
		{
			//wypisz liczbę filmów wydanych po 2000 roku, 
			//których tytuł zawiera podciąg "MINI" (wielkość liter nie ma znaczenia)
			var a1 = (from m in movies
					  where m.release_date.Year > 2000
					  where m.title.ToUpper().Contains("MINI")
					  select m).Count();

			Console.WriteLine("A1: " + a1);

			//znajdź film (wypisz jego tytuł) z największą średnią oceną (vote_average), 
			//który ma co najmniej 10000 głosów (vote_count)
			var a2 = (from m in movies
					  where m.vote_count > 10000
					  orderby m.vote_average descending
					  select m).FirstOrDefault();

			Console.WriteLine("A2: " + a2.title);

			//podaj sumę czasów trwania (runtime) ostatnich 10 filmów, których premiera (release_date) była w lutym
			var a3 = movies.Where(x => x.release_date.Month == 2).OrderByDescending(x => x.release_date).Take(10).Sum(x => x.runtime);

			Console.WriteLine("A3: " + a3);
		}


		public void PartB()
		{
			//znajdź 11 westernów (wypisz ich tytuły) z najmniejszą liczbą słów w tytule
			//w przypadku remisu (takiej samej liczby słów) weź tytuły z mniejszą liczbą znaków
			var b1 = movies.Where(x => x.genres.Contains("Western"))
						   .OrderBy(x => x.title.Split(' ').Length)
						   .ThenBy(x => x.title.Length).Take(11);

			Console.WriteLine("B1: " + String.Join(", ", b1.Select(x => x.title)));

			//podaj średni budżet filmów, które miały premierę w 2015 roku
			//do obliczenia średniej odrzuć 10 z filmów z największym i 10 z najmniejszym budżetem
			var b2 = movies.Where(x => x.release_date.Year == 2015)
					   .OrderBy(x => x.budget).Skip(10)
					   .OrderByDescending(x => x.budget).Skip(10)
					   .Average(x => x.budget);
			Console.WriteLine("B2: " + b2);
		}

		public void PartC()
		{
			//w którym roku wyprodukowano najwięcej filmów
			//podaj rok oraz liczbę filmów
			var c1 = movies.Where(x => x.revenue > 0).GroupBy(x => x.release_date.Year)
				.Select(x => new { year = x.Key, count = x.Count() })
				.OrderByDescending(x => x.count).First();
			Console.WriteLine("C1: " + c1.year + ":" + c1.count);


			//jakie jest pierwsze słowo tytułów filmów, dla którego
			//suma dochodów (revenue) jest najwyższy
			//wypisz to słowo oraz sumę dochódów
			var c2 = movies.GroupBy(x => x.title.Split(' ').First())
				.Select(x => new { word = x.Key, revenue = x.Sum(y => y.revenue) })
				.OrderByDescending(x => x.revenue).First();

			Console.WriteLine("C2: " + c2.word + ":" + c2.revenue);
		}

		public void PartD() //rozwiązania tego etapu OBOWIĄZKOWO napisać z użyciem Query Expression
		{
			//wykorzystując listę amerykańskich świąt (holidays) znajdź wszystkie filmy, których
			//premiera przypadała w święto oraz tytuł filmu oraz nazwa święta mają co najmniej jedno to samo słowo (wielkość liter nie ma znaczenia)
			//wynik wypisz w postaci par: tytuł filmu, nazwa święta
			var d1 = from m in movies
					 join h in holidays on m.release_date equals h.Item1
					 where m.title.ToUpper().Split(' ')
					 .Intersect(h.Item2.ToUpper().Split(' ')).Count() > 0
					 select new { title = m.title, holiday = h.Item2 };

			Console.WriteLine("D1: " + String.Join(", ", d1.Select(x => "(" + x.title + ", " + x.holiday + ")")));

			//znajdź i wypisz tytuły wyszstkich par różnych filmów, które miały swoją premierę w tym samym dniu
			//oraz pierwsze i ostatnie słowo ich tytułu jest takie samo
			//zwrócone pary muszą być unikalne - nie mogą wystąpić pary (a,b) oraz (b,a)
			var d2 = from m1 in movies
					 join m2 in movies on m1.release_date equals m2.release_date
					 where m1.title != m2.title
					 where m1.title.CompareTo(m2.title) > 0
					 where m1.title.Split(' ').First() == m2.title.Split(' ').First()
					 where m1.title.Split(' ').Last() == m2.title.Split(' ').Last()
					 select new { m1 = m1.title, m2 = m2.title };

			Console.WriteLine("D2: " + String.Join(", ", d2.Select(x => "(" + x.m1 + ", " + x.m2 + ")")));
		}

		public void PartE()
		{
			//znajdź 3 najczęściej występujące gatunki filmów (genres), które ukazały się po 2010 roku
			//zwróć te gatunki i częstotliwość ich wstępowania
			var genres = movies.Where(x => x.release_date.Year > 2010).Select(x => x.genres)
								  .Aggregate((x1, x2) => x1 + " | " + x2).Split('|').Select(x => x.Trim());

			var e1 = genres.GroupBy(x => x)
								  .Select(x => new
								  {
									  genre = x.Key,
									  count = x.Count()
								  })
								  .OrderByDescending(x => x.count)
								  .Take(5);

			Console.WriteLine("E1: " + String.Join(", ", e1.Select(x => x.genre + ":" + x.count)));

			//znajdź i wypisz dwudziestą liczbę Fibbonaciego
			var e2 = Enumerable.Range(1, 20).Skip(2)
							   .Aggregate(new { Current = 1, Prev = 1 }, (x, index) => new { Current = x.Prev + x.Current, Prev = x.Current })
							   .Current;

			Console.WriteLine("E2: " + e2);
		}

	}
}