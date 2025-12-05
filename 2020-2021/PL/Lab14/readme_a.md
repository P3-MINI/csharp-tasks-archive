// ETAP_1 (0.5 Pts)
// People.FirstName, People.LastName
// Join Crews where Count() == 0

// ETAP_2 (1.0 Pts)
// Flights.FlightNumber
// TotalSalary= Crews group by Flights.ID Sum(Crew.Salary)
// Order by TotalSalary ascending
// Hints: GROUP, LET

// ETAP_3 (1.0 Pts)
// Airports.CodeIATA
// Count = Flights group by AiportOriginID Count()

// ETAP_4 (1.0 Pts)
// Airports.AirportOriginID.CodeIATA, Airports.AirportDestinationID.CodeIATA, Flights.FlightNumber, Aircraft.Registration
// Aircraft.Capacity < 170
// Order by FlightNumber ascending

// ETAP_5 (1.0 Pts)
// Aircrafts.Registration
// PlaneAverageTime = Flights group by AircraftID .Average(Flights.Duration.TotalMinutes())
// Order by PlaneAverageTime descending 

// ETAP_6 (1.5 Pts) 3 osoby z najkrótszym lotem.
// Crew Join Flights group flight.Duration by crew.PersonID into flightDurationGroupByCrew
// AverageFlightTime= flightDurationGroupByCrew.Sum(x.Duration) / flightDurationGroupByCrew.Count()
// Where Licenses.ValidSince < minValidDate(7 years ago)
// People.Surname, People.Name, Licenses.AircraftCategory, AverageFlightTime
// Order by AverageFlightTime descending
// Take(3)

// Total: 