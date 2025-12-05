using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Database database = Database.GetInstance();

            /* WAŻNE:

                * Twoim zadaniem jest stworzenie sześciu zapytań LINQ dla bazy danych związanych z lotami.
                * Zapytania można tworzyć z dowolnej kolejności.
                * Wymagane jest korzystanie ze składni wyrażeń kwerendowych LINQ (składnia podobna do SQL).
                * Notacji kropkowej można używac jedynie gdy jest niezbędna.
                * Kiedy potrzeba można użyć metod agregujących takich jak Sum, Min, Count, Average oraz innych.
                * Nie można korzystać z pętli for, foreach lub innych w celu przetworzenia danych.
                * Kiedy wymagane ograniczone wyniki (pierwsze trzy wiersze) można użyć metody Take.
                * Powinieneś skorzystać z klas anonimowych w celu projekcji danych z zapytnia.

                * Licenses Table - Przechowuje informacje o licencjach wszystkich użytkowników: (Kategoria,WażnyOd,WażdyDo) - (Category,ValidSince,ExpirationDate).
                * People Table - Przechowuje informacje o członkach załogi: (Imię,Nazwisko,LicencjaID) - (Name,Surname,LicenseID).
                * Aircrafts Table - Przechowuje informacje o samolotach: (NumerRejestracyjny,Marka,Waga,Pojemność) - (RegistrationNumber,Brand,Weight,Capacity).
                * Airports Table - Przechowuje informacje o lotniskach: (Państwo,Miasto,KodIATA,KodICAO) - (Country,City,CodeIATA,CodeICAO).
                * Flights Table - Przechowuje informacje o lotach: (NumerLotu,SamolotID,LotniskoPoczątkoweID,LotniskoKońcodeID,CzasLotu) - (FlightNumber,AircraftID,AirportOriginID,AirportDestinationID,Duration).
                * Crews Table - Przechowuje informacje o załodze danego lotu: (LotID,OsobaID,Rola,Wynagrodzenie) - (FlightID,PersonID,Role,Salary).
            */

            /* ETAP_1 (0.5 Pts)
                * Wypisz Nazwisko oraz Imię (Surname oraz Name) wszystkich osób niebiorących udziału w żadnym locie.
                * Wskazówka:
                    * Użyj JOIN .. INTO oraz metody Count w celu pogrupowania wyników oraz wyznaczenia ich liczności.
            */
            {
                Console.WriteLine("--------------- ETAP_1 (0.5 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {

                }

                Console.WriteLine();
            }

            /* ETAP_2 (1.0 Pts)
                * Dla każdego lotu posiadającego załogę wypisz NumerLotu oraz WypłatęCałkowitą (FlightNumber oraz TotalSalary) osób obsługujących dany lot. Gdzie:
                    * WypłataCałkowita danego lotu to suma wypłat wszystkich członków załogi.
                * Wyniki powinny być posortowane rosnąco po sumie wypłaty.
                * Wskazówka:
                    * Użyj GROUP oraz metody Sum w celu pogrupowania wyników oraz wyznaczenia sumy wynagrodzenia.
                    * Użyj LET w celu uniknięciu wielokrotnego wyliczania wypłaty całkowitej.
            */
            {
                Console.WriteLine("--------------- ETAP_2 (0.5 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {

                }

                Console.WriteLine();
            }


            /* ETAP_3 (1.0 Pts)
                * Wypisz KodIATA oraz liczbę startujących z danego lotniska samolotów (CodeIATA - N Airplanes).
                * Wskazówka:
                    * Użyj GROUP oraz metody Count w celu pogrupowania wyników oraz wyznaczenia liczby samolotów.
            */
            {
                Console.WriteLine("--------------- ETAP_3 (0.5 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {

                }

                Console.WriteLine();
            }

            /* ETAP_4 (1.0 Pts)
                * Wypisz LotniskoPoczątkowe,LotniskoKońcowe,NumerLotu,NumerRejestracyjny (OriginAirport,DestinationAirport,FlightNumber,Registration) dla wszystkich lotów samolotem o pojemności poniżej 170 osób. Gdzie:
                    * LotniskoPoczątkowe to kod IATA lotniska wylotu.
                    * LotniskoKońcowe to kod IATA lotniska przylotu.
                * Wyniki powinny być posortowane roznąco po numerze lotu.
            */
            {
                Console.WriteLine("--------------- ETAP_4 (1.0 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {
                    int maxCapacity = 170;


                }

                Console.WriteLine();
            }


            /* ETAP_5 (1.0 Pts)
                * Wypisz NumerRejestracyjny oraz ŚredniCzasLotu (Registration,PlaneAverageTime) dla wszystkich lotów. Gdzie:
                    * ŚredniCzasLotu to średnia długość wszystkich lotów wykonana danym samolotem.
                * Wyniki powinny być posortowane malejąco po średnim czasie lotu.
            */
            {
                Console.WriteLine("--------------- ETAP_5 (1.0 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {

                }

                Console.WriteLine();
            }


            /* ETAP_6 (1.5 Pts) 3 osoby z najkrótszym lotem.
                * Dla trzech osób z maksymalną średnią czasu lotu oraz licencją ważną dłużej niż 7 lat wypisz Nazwisko,Imię,TypLicencji,ŚredniCzasLotu (Surname,Name,AircraftCategory,AverageFlightTime). Gdzie:
                    * ŚredniCzasLotu opisuje średnią długość wszystkich lotów odbytych przez daną osobę.
                * Wyniki powinny być posortowane malejąco po średnim czasie lotu.
            */
            {
                Console.WriteLine("--------------- ETAP_6 (1.5 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {
                    TimeSpan minValidDate = new TimeSpan(7 * 365, 0, 0);


                }

                Console.WriteLine();
            }
        }
    }
}
