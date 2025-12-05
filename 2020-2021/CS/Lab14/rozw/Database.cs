using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    public interface IEntity
    {
        int ID { get; set; }
    }

    public enum CrewRole
    {
        Capitan, Officer, Steward
    }

    public enum Category
    {
        None, Jet, Piston, Turbofan, Turboprop, Steward
    }

    public class License : IEntity
    {
        public int ID { get; set; }

        public DateTime ValidSince { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Category AircraftCategory { get; set; }
    }

    public class Person : IEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public int LicenseID { get; set; }
    }

    public class Aircraft : IEntity
    {
        public int ID { get; set; }

        public string Registration { get; set; }

        public double Weight { get; set; }
        public int Capacity { get; set; }

        public string Brand { get; set; }
    }

    public class Airport : IEntity
    {
        public int ID { get; set; }

        public string CodeIATA { get; set; }
        public string CodeICAO { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Flight : IEntity
    {
        public int ID { get; set; }

        public string FlightNumber { get; set; }

        public int AircraftID { get; set; }

        public int AirportOriginID { get; set; }
        public int AirportDestinationID { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class Crew : IEntity
    {
        public int ID { get; set; }

        public int FlightID { get; set; }
        public int PersonID { get; set; }

        public CrewRole Role { get; set; }

        public int Salary { get; set; }
    }

    public class Database
    {
        public List<License> Licenses { get; set; }
        public List<Person> People { get; set; }
        public List<Aircraft> Aircrafts { get; set; }
        public List<Airport> Airports { get; set; }
        public List<Flight> Flights { get; set; }
        public List<Crew> Crews { get; set; }

        public static Database GetInstance()
        {
            return new Database()
            {
                Licenses = new List<License>()
                {
                    new License() { ID = 0, AircraftCategory = Category.Piston, ValidSince = new DateTime(2020, 12, 01), ExpirationDate = new DateTime(2025, 12, 01) },
                    new License() { ID = 1, AircraftCategory = Category.Piston, ValidSince = new DateTime(2005, 01, 07), ExpirationDate = new DateTime(2010, 01, 07) },
                    new License() { ID = 2, AircraftCategory = Category.Jet, ValidSince = new DateTime(2018, 01, 01), ExpirationDate = new DateTime(2020, 01, 01) },
                    new License() { ID = 3, AircraftCategory = Category.Jet, ValidSince = new DateTime(2019, 01, 01), ExpirationDate = new DateTime(2021, 01, 01) },
                    new License() { ID = 4, AircraftCategory = Category.Jet, ValidSince = new DateTime(2020, 05, 30), ExpirationDate = new DateTime(2022, 05, 30) },
                    new License() { ID = 5, AircraftCategory = Category.Turboprop, ValidSince = new DateTime(2020, 10, 10), ExpirationDate = new DateTime(2021, 10, 10) },
                    new License() { ID = 6, AircraftCategory = Category.Turbofan, ValidSince = new DateTime(2020, 03, 10), ExpirationDate = new DateTime(2020, 08, 10) },
                    new License() { ID = 7, AircraftCategory = Category.Jet, ValidSince = new DateTime(2009, 07, 14), ExpirationDate = new DateTime(2011, 07, 14) },
                    new License() { ID = 8, AircraftCategory = Category.Jet, ValidSince = new DateTime(2012, 12, 12), ExpirationDate = new DateTime(2014, 12, 12) },
                    new License() { ID = 9, AircraftCategory = Category.Piston, ValidSince = new DateTime(2021, 01, 01), ExpirationDate = new DateTime(2026, 01, 01) },
                    new License() { ID = 10, AircraftCategory = Category.Turbofan, ValidSince = new DateTime(2021, 01, 01), ExpirationDate = new DateTime(2026, 01, 01) },
                },

                People = new List<Person>()
                {
                    new Person() { ID = 0, Name = "Raoul", Surname = "Frantsev", LicenseID = 1 },
                    new Person() { ID = 1, Name = "Davin", Surname = "Surgeoner", LicenseID = 0 },
                    new Person() { ID = 2, Name = "Vivienne", Surname = "Cruddas", LicenseID = 3 },
                    new Person() { ID = 3, Name = "Cameron", Surname = "Gullan", LicenseID = 4 },
                    new Person() { ID = 4, Name = "Kattie", Surname = "Gullan", LicenseID = 7 },
                    new Person() { ID = 5, Name = "Gerianna", Surname = "Dilston", LicenseID = 2 },
                    new Person() { ID = 6, Name = "Wallas", Surname = "Peron", LicenseID = 9 },
                    new Person() { ID = 7, Name = "Luther", Surname = "Wythill", LicenseID = 8 },
                    new Person() { ID = 8, Name = "Lilias", Surname = "Belden", LicenseID = 5 },
                    new Person() { ID = 9, Name = "Joelynn", Surname = "Camell", LicenseID = 6 },
                    new Person() { ID = 10, Name = "Wallas", Surname = "Gamddsev", LicenseID = 10 },
                },

                Aircrafts = new List<Aircraft>()
                {
                    new Aircraft() { ID = 0, Registration = "SP-XDD", Brand="Cesna 152", Weight = 0.5, Capacity = 2 },
                    new Aircraft() { ID = 1, Registration = "SP-FXM", Brand="Cesna 172", Weight = 0.7, Capacity = 4 },
                    new Aircraft() { ID = 2, Registration = "SP-FXM", Brand="Airbus A340", Weight = 177, Capacity = 380 },
                    new Aircraft() { ID = 3, Registration = "EC-TYZ", Brand="Boeing 737-800", Weight = 38.5, Capacity = 189 },
                    new Aircraft() { ID = 4, Registration = "EC-TYZ", Brand="Boeing 737-300", Weight = 31.5, Capacity = 133 },
                    new Aircraft() { ID = 5, Registration = "D-TRYZ", Brand="Airbus A319", Weight = 40.8, Capacity = 160 },
                    new Aircraft() { ID = 6, Registration = "D-QZZK", Brand="Airbus A319", Weight = 40.8, Capacity = 160 },
                    new Aircraft() { ID = 7, Registration = "G-OTMY", Brand="Boeing 747-400", Weight = 178.8, Capacity = 450 },
                    new Aircraft() { ID = 8, Registration = "G-KATM", Brand="Boeing 777-300", Weight = 155.7, Capacity = 430 },
                    new Aircraft() { ID = 9, Registration = "EC-TER", Brand="Boeing 737-800", Weight = 38.5, Capacity = 189 },
                },

                Airports = new List<Airport>()
                {
                    new Airport() { ID =  0, Country = "POLAND", City="WARSAW", CodeIATA = "WAW", CodeICAO="EPWA" },
                    new Airport() { ID =  1, Country = "POLAND", City="WROCLAW", CodeIATA = "WRO", CodeICAO="EPWR" },
                    new Airport() { ID =  2, Country = "POLAND", City="CRACOW", CodeIATA = "KRK", CodeICAO="EPKK" },
                    new Airport() { ID =  3, Country = "GERMANY", City="BERLIN", CodeIATA = "BER", CodeICAO="EDDB" },
                    new Airport() { ID =  4, Country = "GERMANY", City="COLOGNE", CodeIATA = "CGN", CodeICAO="EDDK" },
                    new Airport() { ID =  5, Country = "GERMANY", City="MUNICH", CodeIATA = "MUC", CodeICAO="EDDM" },
                    new Airport() { ID =  6, Country = "SPAIN", City="BARCELONA", CodeIATA = "BCN", CodeICAO="LEBL" },
                    new Airport() { ID =  7, Country = "SPAIN", City="MADRID", CodeIATA = "MAD", CodeICAO="LEMD" },
                    new Airport() { ID =  8, Country = "ITALY", City="ROME", CodeIATA = "FCO", CodeICAO="LIRF" },
                    new Airport() { ID =  9, Country = "ITALY", City="NAPLES", CodeIATA = "NAP", CodeICAO="LIRN" },
                    new Airport() { ID = 10, Country = "FRANCE", City="PARIS", CodeIATA = "CDG", CodeICAO="LFPG" },
                    new Airport() { ID = 11, Country = "FRANCE", City="MONTPELLIER", CodeIATA = "MPL", CodeICAO="LFMT" },
                    new Airport() { ID = 12, Country = "UNITED KINGDOM", City="LONDON", CodeIATA = "LHR", CodeICAO="EGLL" },
                    new Airport() { ID = 13, Country = "SCOTLAND", City="EDINBURGH", CodeIATA = "EDI", CodeICAO="EGPH" },
                    new Airport() { ID = 14, Country = "AUSTRIA", City="VIENNA", CodeIATA = "VIE", CodeICAO="LOWW" },
                },

                Flights = new List<Flight>()
                {
                    new Flight() { ID =  0, FlightNumber = "FR-7872", AircraftID = 3, AirportOriginID = 0, AirportDestinationID = 6, Duration = new TimeSpan(3,15,0) },
                    new Flight() { ID =  1, FlightNumber = "FR-7487", AircraftID = 0, AirportOriginID = 4, AirportDestinationID = 6, Duration = new TimeSpan(1,15,0) },
                    new Flight() { ID =  2, FlightNumber = "LH-1492", AircraftID = 1, AirportOriginID = 2, AirportDestinationID = 5, Duration = new TimeSpan(1,45,0) },
                    new Flight() { ID =  3, FlightNumber = "LH-1835", AircraftID = 7, AirportOriginID = 4, AirportDestinationID = 1, Duration = new TimeSpan(1,45,0) },
                    new Flight() { ID =  4, FlightNumber = "LH-1576", AircraftID = 8, AirportOriginID = 3, AirportDestinationID = 4, Duration = new TimeSpan(0,45,0) },
                    new Flight() { ID =  5, FlightNumber = "W-63901", AircraftID = 3, AirportOriginID = 8, AirportDestinationID = 12, Duration = new TimeSpan(3,0,0) },
                    new Flight() { ID =  6, FlightNumber = "FR-7864", AircraftID = 4, AirportOriginID = 13, AirportDestinationID = 10, Duration = new TimeSpan(2,15,0) },
                    new Flight() { ID =  7, FlightNumber = "LO-385", AircraftID = 2, AirportOriginID = 9, AirportDestinationID = 14, Duration = new TimeSpan(1,55,0) },
                    new Flight() { ID =  8, FlightNumber = "LO-492", AircraftID = 1, AirportOriginID = 5, AirportDestinationID = 4, Duration = new TimeSpan(1,15,0) },
                    new Flight() { ID =  9, FlightNumber = "LO-535", AircraftID = 1, AirportOriginID = 7, AirportDestinationID = 11, Duration = new TimeSpan(1,20,0) },
                    new Flight() { ID = 10, FlightNumber = "EK-76", AircraftID = 0, AirportOriginID = 14, AirportDestinationID = 6, Duration = new TimeSpan(1,50,0) },
                    new Flight() { ID = 11, FlightNumber = "EK-39", AircraftID = 6, AirportOriginID = 1, AirportDestinationID = 6, Duration = new TimeSpan(3,5,0) },
                    new Flight() { ID = 12, FlightNumber = "EK-78", AircraftID = 6, AirportOriginID = 12, AirportDestinationID = 13, Duration = new TimeSpan(1,10,0) },
                    new Flight() { ID = 13, FlightNumber = "LO-379", AircraftID = 5, AirportOriginID = 11, AirportDestinationID = 5, Duration = new TimeSpan(1,25,0) },
                    new Flight() { ID = 14, FlightNumber = "KL-921", AircraftID = 8, AirportOriginID = 7, AirportDestinationID = 3, Duration = new TimeSpan(1,50,0) },
                    new Flight() { ID = 15, FlightNumber = "KL-835", AircraftID = 7, AirportOriginID = 14, AirportDestinationID = 3, Duration = new TimeSpan(1,35,0) },
                    new Flight() { ID = 16, FlightNumber = "KL-763", AircraftID = 4, AirportOriginID = 12, AirportDestinationID = 8, Duration = new TimeSpan(3,0,0) },
                    new Flight() { ID = 17, FlightNumber = "W-63901", AircraftID = 3, AirportOriginID = 9, AirportDestinationID = 1, Duration = new TimeSpan(3,20,0) },
                    new Flight() { ID = 18, FlightNumber = "FR-7245", AircraftID = 2, AirportOriginID = 7, AirportDestinationID = 0, Duration = new TimeSpan(2,45,0) },
                    new Flight() { ID = 19, FlightNumber = "FR-7697", AircraftID = 1, AirportOriginID = 8, AirportDestinationID = 11, Duration = new TimeSpan(2,15,0) },
                    new Flight() { ID = 20, FlightNumber = "W-71492", AircraftID = 6, AirportOriginID = 8, AirportDestinationID = 2, Duration = new TimeSpan(2,10,0) },
                    new Flight() { ID = 21, FlightNumber = "W-83524", AircraftID = 8, AirportOriginID = 6, AirportDestinationID = 13, Duration = new TimeSpan(2,40,0) },
                    new Flight() { ID = 22, FlightNumber = "EK-76", AircraftID = 2, AirportOriginID = 1, AirportDestinationID = 10, Duration = new TimeSpan(2,25,0) },
                    new Flight() { ID = 23, FlightNumber = "W-63723", AircraftID = 7, AirportOriginID = 9, AirportDestinationID = 7, Duration = new TimeSpan(2,55,0) },
                    new Flight() { ID = 24, FlightNumber = "KL-835", AircraftID = 0, AirportOriginID = 2, AirportDestinationID = 12, Duration = new TimeSpan(2,50,0) },
                    new Flight() { ID = 25, FlightNumber = "FR-7845", AircraftID = 6, AirportOriginID = 3, AirportDestinationID = 14, Duration = new TimeSpan(1,35,0) },
                    new Flight() { ID = 26, FlightNumber = "W-73648", AircraftID = 4, AirportOriginID = 6, AirportDestinationID = 0, Duration = new TimeSpan(3,15,0) },
                    new Flight() { ID = 27, FlightNumber = "KL-764", AircraftID = 4, AirportOriginID = 0, AirportDestinationID = 7, Duration = new TimeSpan(3,45,0) },
                    new Flight() { ID = 28, FlightNumber = "W-83543", AircraftID = 7, AirportOriginID = 0, AirportDestinationID = 6, Duration = new TimeSpan(2,45,0) },
                },

                Crews = new List<Crew>()
                {
                    new Crew() { ID = 0, FlightID = 0, PersonID = 9, Role = CrewRole.Capitan, Salary = 13000 },
                    new Crew() { ID = 1, FlightID = 0, PersonID = 3, Role = CrewRole.Officer, Salary = 11000 },
                    new Crew() { ID = 2, FlightID = 0, PersonID = 2, Role = CrewRole.Steward, Salary = 7000 },
                    new Crew() { ID = 3, FlightID = 0, PersonID = 5, Role = CrewRole.Steward, Salary = 7000 },

                    new Crew() { ID = 4, FlightID = 1, PersonID = 4, Role = CrewRole.Capitan, Salary = 700 },

                    new Crew() { ID = 5, FlightID = 2, PersonID = 8, Role = CrewRole.Capitan, Salary = 800 },
                    new Crew() { ID = 6, FlightID = 2, PersonID = 4, Role = CrewRole.Officer, Salary = 650 },

                    new Crew() { ID = 7, FlightID = 3, PersonID = 1, Role = CrewRole.Capitan, Salary = 11500 },
                    new Crew() { ID = 8, FlightID = 3, PersonID = 9, Role = CrewRole.Officer, Salary = 10000 },
                    new Crew() { ID = 9, FlightID = 3, PersonID = 6, Role = CrewRole.Steward, Salary = 6500 },

                    new Crew() { ID = 10, FlightID = 4, PersonID = 7, Role = CrewRole.Capitan, Salary = 9500},
                    new Crew() { ID = 11, FlightID = 4, PersonID = 8, Role = CrewRole.Officer, Salary = 9000},
                    new Crew() { ID = 12, FlightID = 4, PersonID = 2, Role = CrewRole.Steward, Salary = 5000},

                    new Crew() { ID = 13, FlightID = 5, PersonID = 3, Role = CrewRole.Capitan, Salary = 15000},
                    new Crew() { ID = 14, FlightID = 5, PersonID = 1, Role = CrewRole.Officer, Salary = 13000},
                    new Crew() { ID = 15, FlightID = 5, PersonID = 6, Role = CrewRole.Steward, Salary = 7500},
                    new Crew() { ID = 16, FlightID = 5, PersonID = 2, Role = CrewRole.Steward, Salary = 7500},

                    new Crew() { ID = 17, FlightID = 6, PersonID = 3, Role = CrewRole.Capitan, Salary = 9000},
                    new Crew() { ID = 18, FlightID = 6, PersonID = 9, Role = CrewRole.Officer, Salary = 7000},
                    new Crew() { ID = 19, FlightID = 6, PersonID = 6, Role = CrewRole.Steward, Salary = 5000},

                    new Crew() { ID = 20, FlightID = 7, PersonID = 3, Role = CrewRole.Capitan, Salary = 10500},
                    new Crew() { ID = 21, FlightID = 7, PersonID = 9, Role = CrewRole.Officer, Salary = 9500},
                    new Crew() { ID = 22, FlightID = 7, PersonID = 6, Role = CrewRole.Steward, Salary = 6500},

                }
            };
        }
    }
}
