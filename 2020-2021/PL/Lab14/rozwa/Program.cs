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
                    var results = from person in database.People
                                  join crew in database.Crews on person.ID equals crew.PersonID into crewPerson
                                  where crewPerson.Count() == 0
                                  select new { person.Name, person.Surname };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.Surname} {entity.Name}");
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
                    var results = from crew in database.Crews
                                  group crew.Salary by crew.FlightID
                                  into crewSalary
                                  join flight in database.Flights on crewSalary.Key equals flight.ID
                                  let TotalSalary = crewSalary.Sum()
                                  orderby TotalSalary /* ascending */
                                  select new { flight.FlightNumber, TotalSalary };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.FlightNumber} - {entity.TotalSalary} PLN");
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
                    var results = from flight in database.Flights
                                  group flight.AircraftID by flight.AirportOriginID
                                  into airportAirplanes
                                  join airport in database.Airports on airportAirplanes.Key equals airport.ID
                                  select new { airport.CodeIATA, Count = airportAirplanes.Count() };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.CodeIATA} - {entity.Count} Airplanes");
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

                    var results = from flight in database.Flights
                                  join aircraft in database.Aircrafts on flight.AircraftID equals aircraft.ID
                                  join airportOrigin in database.Airports on flight.AirportOriginID equals airportOrigin.ID
                                  join airportDestination in database.Airports on flight.AirportDestinationID equals airportDestination.ID
                                  where aircraft.Capacity < maxCapacity
                                  orderby flight.FlightNumber
                                  select new { Origin = airportOrigin.CodeIATA, Desrination = airportDestination.CodeIATA, flight.FlightNumber, aircraft.Registration };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.Origin} -> {entity.Desrination} by Plane {entity.Registration} Flight Number {entity.FlightNumber}.");
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
                    var results = from flight in database.Flights
                                  group flight.Duration by flight.AircraftID
                                  into flightsDuration
                                  join airplane in database.Aircrafts on flightsDuration.Key equals airplane.ID
                                  let PlaneAverageTime = flightsDuration.Average(x => x.TotalMinutes)
                                  orderby PlaneAverageTime descending
                                  select new { airplane.Registration, PlaneAverageTime };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.Registration} - {entity.PlaneAverageTime:#,0.00}");
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

                    var results = from crew in database.Crews
                                  join flight in database.Flights on crew.FlightID equals flight.ID
                                  group flight.Duration by crew.PersonID
                                  into flightsDurationByCrew
                                  join person in database.People on flightsDurationByCrew.Key equals person.ID
                                  join license in database.Licenses on person.LicenseID equals license.ID
                                  where DateTime.Now - license.ValidSince > minValidDate
                                  let TotalFlightTime = flightsDurationByCrew.Sum(x => x.TotalMinutes)
                                  let TotalFlightCount = flightsDurationByCrew.Count()
                                  let AverageFlightTime = TotalFlightTime / TotalFlightCount
                                  orderby AverageFlightTime descending
                                  select new { person.Surname, person.Name, license.AircraftCategory, AverageFlightTime };

                    foreach (var entity in results.Take(3))
                        Console.WriteLine($"{entity.Surname} {entity.Name} - {entity.AverageFlightTime:#,0.00} on {entity.AircraftCategory}");
                }

                Console.WriteLine();
            }
        }
    }
}
