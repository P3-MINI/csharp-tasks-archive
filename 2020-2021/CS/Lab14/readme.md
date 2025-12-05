// STAGE_1 (0.5 Pts)
// People -> Surname+Name
// Order -> Surname ascending + Name descending

// STAGE_2 (0.5 Pts)
// Aircrafts -> Brand, Weight, TotalWeight (Weight*1000 + Capacity*70)
// Order -> TotalWeight descending
// Take(3)

// STAGE_3 (1.0 Pts)
// Aircrafts -> Brand, RegistrationNumber
// join flights.Count() < 2
// Order -> Brand ascending

// STAGE_4 (1.5 Pts)
// Airport.CodeIATA
// AverageDuration =flights group by AirportDestinationID  => .Average(Duration)
// FlightsNo=departure flights count

// STAGE_5 (1.5 Pts)
// People.Surname, People.Name, Licenses.AircraftCategory
// person join license join crew where crew.Count() == 0

// Total: 