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
			// Dostarczona jest baza danych z trzema tabelami: database.Movies, database.Users, database.Ratings
			// Dokładne informacje o dostępnych kolumnach można znaleźć przeglądając klasy Movie, User, RatingEntry
			// W tabeli Users pole Age jest raczej identyfikatorem pewnego zakresu wieku, niż wiekiem samym w sobie, np. 1 oznacza przedział 1-18
			// Każdy film ma unikatowy numer id MovieID, każdy użytkownik ma unikatowy numer id w UserID
			// Obie te tabele (Movies, Users) są połączone przez Ratings: pojedynczy głos na film jest oddawany przez jednego użytkownika na jeden film
            DatabaseMovies database = DatabaseMovies.GetInstance("movies.csv", "users.csv", "ratings.csv");

            // pytanie 1

			// Policz względny udział grup wiekowych wśród użytkowników
			// Posortuj rosnąco po tym udziale
				
			// Miejsce na Twoje rozwiązanie

            Console.WriteLine($"--------------");

            // pytanie 2

            // Jaki jest średni rok oglądanego filmu w poszczególnych grupach wyznaczanych przez wiek i płeć
			// Posortuj rosnąco po płci a potem po wieku

            // Miejsce na Twoje rozwiązanie

            Console.WriteLine($"--------------");

            // pytanie 3

			// Policz liczbę wystąpień każdego gatunku filmowego zaczynającego się na C
			// (każdy gatunek liczymy pojedynczo, więc dla "Crime|Comedy" mamy +1 dla gatunku Crime i +1 dla gatunku Comedy)
			// Posortuj rosnąco po liczbie wystąpień

            // Miejsce na Twoje rozwiązanie
			
			Console.WriteLine($"--------------");

            // pytanie 4

			// Definiujemy remaki pojedynczego filmu jako zbiór filmów których tytuł jest taki sam (ale lata produkcji są różne)
			// Twój wynik powinien zawierać filmy, które są remakami (wszystkie filmy, dla których możemy znaleźć co najmniej jeden inny film z tym samym tytułem)
            // ale tylko te wersje, które są najmłodsze (ich rok produkcji jest największy)
			// Wynik powinien zawierać pierwsze 14 filmów

            // Miejsce na Twoje rozwiązanie
			
			Console.WriteLine($"--------------");

            // pytanie 5
			
			// Posortuj (malejąco) filmy po średniej ocenie głosów rozróżniając na głosy oddane przez kobiety i mężczyzn
			// Weź pod uwagę jedynie filmy, na które oddano 100 głosów lub więcej
			// Wynik powinien zawierać pierwsze 20 filmów

            // Miejsce na Twoje rozwiązanie

            Console.WriteLine($"--------------");



        }
    }
}