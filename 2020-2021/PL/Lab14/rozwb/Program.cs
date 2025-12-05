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
                * Wypisz Markę oraz NumerRejestracyjny (Brand oraz Registration) wszystkich samolotów zarejestrowanych w Niemczech.
                    * NumerRejestracyjny niemieckich samolotów zaczyna się od litery 'D'.
                * Wyniki powinny być posortowane malejąco po sumie NumerzeRejestracyjnym.
                * Wskazówka:
                    * Użyj JOIN oraz WHERE w celu złaczenia tabel oraz wyznaczenia warunku rejestracji samolotu.
            */
            {
                Console.WriteLine("--------------- ETAP_1 (0.5 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {
                    var results = from aircraft in database.Aircrafts
                                  where aircraft.Registration.StartsWith("D")
                                  orderby aircraft.Registration descending
                                  select new { aircraft.Brand, aircraft.Registration };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.Brand} - {entity.Registration}");
                }

                Console.WriteLine();
            }

            /* ETAP_2 (1.0 Pts)
                * Dla każdego lotniska z Polski wypisz KodICAO oraz LiczbęWylotów (CodeICAO oraz FlightsCount). Gdzie:
                    * LiczbaWylotów to suma wykonanych z danego lotniska lotów.
                * Wyniki powinny być posortowane malejąco po liczbie wylotów.
                * Wskazówka:
                    * Użyj GROUP oraz metody Count w celu pogrupowania wyników oraz wyznaczenia liczby wylotów.
                    * Użyj LET w celu uniknięciu wielokrotnego wyliczania liczby wylotów.
            */
            {
                Console.WriteLine("--------------- ETAP_2 (1.0 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {
                    var results = from flight in database.Flights
                                  group flight by flight.AirportOriginID
                                  into flightAirport
                                  join airport in database.Airports on flightAirport.Key equals airport.ID
                                  where airport.Country == "POLAND"
                                  let Count = flightAirport.Count()
                                  orderby Count descending
                                  select new { airport.CodeICAO, Count };

                    foreach (var entity in results)
                        Console.WriteLine($"Kod ICAO \"{entity.CodeICAO}\" - {entity.Count} Wylotów.");
                }

                Console.WriteLine();
            }

            /* ETAP_3 (1.0 Pts)
                * Wypisz NumerRejestracyjny oraz Nadgodziny (Registration,FlightOvertime) dla wszystkich samolotów wymagających przegladu. Gdzie:
                    * Nadgodziny różnica między aktualnym przebiegiem samolotu oraz maksumalnym czasem do przegldu - zmienna maxPlaneTime.
                * Samolot wymaga przegldu kiedy przeleciał więcej niż 500 minut łącznie - zmienna maxPlaneTime.
                * Wyniki powinny być posortowane melejąco po liczbie nadgodzin.
            */
            {
                Console.WriteLine("--------------- ETAP_3 (1.0 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {
                    const int maxPlaneTime = 500;

                    var results = from flight in database.Flights
                                  group flight.Duration by flight.AircraftID
                                  into flightsDuration
                                  join airplane in database.Aircrafts on flightsDuration.Key equals airplane.ID
                                  let planeFlightTime = flightsDuration.Sum(x => x.TotalMinutes)
                                  where planeFlightTime > maxPlaneTime
                                  orderby planeFlightTime descending
                                  select new { airplane.Registration, FlightOvertime = planeFlightTime - maxPlaneTime };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.Registration} - Nadgodziny: {entity.FlightOvertime} Minut.");
                }

                Console.WriteLine();
            }

            /* ETAP_4 (1.0 Pts)
                * Dla każej osoby, która nie wykonała żadnego lotu wypisz Nazwisko,Imię,KategorięLicencji (Surname,Name,AircraftCategory). Gdzie:
                * Wyniki powinny być posortowane rosnąco po nazwisku i imieniu.
            */
            {
                Console.WriteLine("--------------- ETAP_4 (1.0 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {

                    var results = from person in database.People
                                  join license in database.Licenses on person.LicenseID equals license.ID
                                  join crew in database.Crews on person.ID equals crew.PersonID into personCrew
                                  where personCrew.Count() == 0
                                  orderby person.Surname, person.Name
                                  select new { person.Surname, person.Name, license.AircraftCategory };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.Surname} {entity.Name} - {entity.AircraftCategory} License");

				}
				
                Console.WriteLine();
            }


            /* ETAP_5 (1.0 Pts)
                * Dla każdego lotniska wypisz jego KodIATA oraz listę lotnisk, do których możemy dolecieć (CodeIATA -> CodeIATA CodeIATA etc).
                * W przypadku gdy daną trasę obsługuje więcej niż jedno połączenie wyniki nie powinny zawierać powtórzeń.
                * Wyniki powinny być posortowane melejąco po liczbie dostępnych połączeń.
            */
            {
                Console.WriteLine("--------------- ETAP_5 (1.0 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {
                    var results = from flight in database.Flights
                                  join airportDestination in database.Airports on flight.AirportDestinationID equals airportDestination.ID
                                  group airportDestination by flight.AirportOriginID
                                  into orginDestinations
                                  join airportOrigin in database.Airports on orginDestinations.Key equals airportOrigin.ID
                                  orderby orginDestinations.Count() descending
                                  select new { Origin = airportOrigin.CodeIATA, Destination = orginDestinations.Distinct() };

                    foreach (var entity in results)
                    {
                        Console.Write($"{entity.Origin} ->");

                        foreach (var destination in entity.Destination)
                            Console.Write($" {destination.CodeIATA}");

                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
            }

            /* ETAP_6 (1.5 Pts)
                * Dla każdej osoby wypisz Nazwisko,Imię,MinimalnąLiczbęGodzin (Surname,Name,Hours) z lotów, które wykonała. Gdzie:
                    * MinimalnąLiczbęGodzin to czas najkrótszego lotu odbytego przez daną osobę.
                * Wyniki powinny być posortowane melejąco po czasie minimalnym oraz rosnąco po nazwisku i imieniu.
            */
            {
                Console.WriteLine("--------------- ETAP_6 (1.5 Pts) ---------------");

                /* TUTAJ IMPLEMENTACJA */
                {
                    var results = from flight in database.Flights
                                  join crew in database.Crews on flight.ID equals crew.FlightID
                                  join person in database.People on crew.PersonID equals person.ID
                                  group flight.Duration by new {person.Name, person.Surname} into flightCrew
                                  let MinHours = flightCrew.Min()
                                  orderby MinHours descending, flightCrew.Key.Surname, flightCrew.Key.Name
                                  select new { flightCrew.Key.Surname, flightCrew.Key.Name, MinHours };

                    foreach (var entity in results)
                        Console.WriteLine($"{entity.Surname} {entity.Name} - {entity.MinHours}");
                }

                Console.WriteLine();
            }
        }
    }
}
