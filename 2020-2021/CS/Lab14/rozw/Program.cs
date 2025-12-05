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

            /* REMARKS:

                * Your task is to create five LINQ queries for a flights database.
                * You can create the queries in any order you want.
                * You are obligated to use LINQ query expressions syntax.
                * Dot notation is allowed only if it is necessary.
                * You can use aggregation methods like Sum, Min or Count etc, when required.
                * You cannot process data on your own - with for or foreach etc.
                * When asked for limited output (three top rows) you may use Take method.
                * You should create anonymous class to output/project data from your query.

                * Licenses Table - Stores information about all valid licenses: (Category,ValidSince,ExpirationDate).
                * People Table - Stores information about all flight staff: (Name,Surname,LicenseID).
                * Aircrafts Table - Stores information about all available planes: (RegistrationNumber,Brand,Weight,Capacity).
                * Airports Table - Stores information about all operational airports: (Country,City,CodeIATA,CodeICAO).
                * Flights Table - Stores information about all flights already taken: (FlightNumber,AircraftID,AirportOriginID,AirportDestinationID,Duration).
                * Crews Table - Stores information about all crew members: (FlightID,PersonID,Role,Salary).
            */

            /* STAGE_1 (0.5 Pts)
                * List Surname and Name for all People present in the database.
                * Entries should be sorted Ascending by Surname and then Descending by Name.
            */
            {
                Console.WriteLine("--------------- STAGE_1 (0.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var results = from person in database.People
                                  orderby person.Surname /* ascending */, person.Name descending
                                  select new { person.Surname, person.Name };

                    foreach (var entry in results)
                        Console.WriteLine($"{entry.Surname} {entry.Name}");
                }

                Console.WriteLine();
            }


            /* STAGE_2 (0.5 Pts)
                * List Brand, Weight, TotalWeight for 3 Airplanes with greatest TotalWeight.
                * Entries should be sorted Descending by TotalWeight.
                * REMARK:
                    * TotalWeight is sum of Weight of the Airplane and its Capacity multiplied by 70 kilograms (default weight of a single person).
                    * Use LET statement to compute TotalWeight only once and reuse its value.
                    * Weight of an Airplane is given in Tons, TotalWeight should be outputed in Kilograms.
            */
            {
                Console.WriteLine("--------------- STAGE_2 (0.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var results = from aircraft in database.Aircrafts
                                  let TotalWeight = 1000.0 * aircraft.Weight + 70.0 * aircraft.Capacity
                                  orderby TotalWeight descending
                                  select new { aircraft.Brand, aircraft.Weight, TotalWeight };

                    foreach (var entry in results.Take(3))
                        Console.WriteLine($"{entry.Brand} - Weight {entry.Weight} Tons - Total {entry.TotalWeight} Kilograms");
                }

                Console.WriteLine();
            }


            /* STAGE_3 (1.0 Pts)
                * List Brand and RegistrationNumber for all Airplanes that have less than 2 Flights.
                * Entries should be sorted Ascending by its Brand.
                * REMARK:
                    * Use JOIN .. INTO statement and Count method to group entries and calculate number of Flights taken by an Airplane.
            */
            {
                Console.WriteLine("--------------- STAGE_3 (1.0 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var results = from aircraft in database.Aircrafts
                                  join flight in database.Flights on aircraft.ID equals flight.AircraftID into aircraftFlights
                                  where aircraftFlights.Count() < 2
                                  orderby aircraft.Brand /* ascending */
                                  select new { aircraft.Brand, aircraft.Registration };

                    foreach (var entry in results)
                        Console.WriteLine($"{entry.Brand} - {entry.Registration}");
                }

                Console.WriteLine();
            }

            /* STAGE_4 (1.5 Pts)
                * List CodeIATA,AverageDuration,FlightsNo for all Airports. Where:
                    * AverageDuration represents average Duration of all Departure Flights from given Airport.
                    * FlightsNo represents overall amount of Departure Flights from given Airport.
                * REMARKS:
                    * Use GROUP statement to group entries.
                    * Use Average and Count methods to calculate average Duration in minutes and the amount of Departure Flights from an Airport.
            */
            {
                Console.WriteLine("--------------- STAGE_4 (1.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var results = from flight in database.Flights
                                  group flight by flight.AirportDestinationID into airportFlights
                                  join airport in database.Airports on airportFlights.Key equals airport.ID
                                  /* where airport.CodeIATA == "MAD" */
                                  let AverageDuration = airportFlights.Average(x => x.Duration.TotalMinutes)
                                  select new { airport.CodeIATA, AverageDuration, FlightsNo = airportFlights.Count() };

                    foreach (var entry in results)
                        Console.WriteLine($"{entry.CodeIATA} ({entry.FlightsNo} Arrival Flights) - {entry.AverageDuration} Minutes.");
                }

                Console.WriteLine();
            }


            /* STAGE_5 (1.5 Pts)
                * List Surname,Name,AircraftCategory of those People that didn't attend any flight.
                * Entries should be sorted Ascending by Surname and Name.
                * REMARKS:
                    * Use JOIN .. INTO statement and Count method to group entries and calculate number of Flights taken by a Person.
            */
            {
                Console.WriteLine("--------------- STAGE_5 (1.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
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
        }
    }
}
