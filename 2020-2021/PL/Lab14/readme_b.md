// ETAP_1 (0.5 Pts)
// Aircrafts.Brand, Aircrafts.Registration
// Where Registration.StartsWith("D")
// Order by Registration descending

// ETAP_2 (1.0 Pts)
// Airports.CodeICAO
// Count = Group Flights by Flights.AirportOriginID .Count()
// where Airport.Country == "Poland"
// Order by Count descending

// ETAP_3 (1.0 Pts)
// Aircrafts.Registration
// SumDuration = Group Flights by AircraftID => Sum(Duration)
// Where SumDuration > 500
// OverTime = SumDuration - 500
// OrderBy SumDuration descending

// ETAP_4 (1.0 Pts)
// People.Surname, People.Name, Licenses.AircraftCategory
// Where Crews.Count() == 0
// Order by Surname, Name

// ETAP_5 (1.0 Pts)
// Airports.CodeIATA,
// Distinct( Flights join Airports by AirportDestinationID Group by AirportOriginID)
// Order by Origin destinations .Count()

// ETAP_6 (1.5 Pts)
// People, Crews, Flights wit Min Duration
// Order by MinDuration descending, Surname Name ascending

// Total: 