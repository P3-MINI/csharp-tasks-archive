#include <iomanip>
#include <iostream>
#include <map>
#include <stdlib.h>
#include <string>
#include <time.h> 
#include <vector>
#include <algorithm>
#include <numeric>

struct Driver
{
	uint32_t Id;			
	std::string Name;		
	std::string Surname;	
	std::string Team;	
	uint32_t TotalPoints; // Total points in classification

	friend std::ostream& operator<<(std::ostream& stream, const Driver& driver)
	{
		// TODO: Fill operator
		// Format: [Id] [Name] [Surname] [Team] [TotalPoints]
		return stream << std::left << std::setw(3) << driver.Id << std::setw(10) << driver.Name << std::setw(12) << driver.Surname << std::setw(16) << driver.Team
			<< std::right << std::setw(5) << driver.TotalPoints << std::endl;
	}
};

struct Race
{
	std::string Name;				
	std::vector<uint32_t> Results;	//List of drivers' ids. Position in vector means place in race.

	friend std::ostream& operator<<(std::ostream& stream, const Race& race)
	{
		// TODO: Fill operator. Use function std::for_each.
		// Format: [Name]:
		//			1 -> [Result0]
		//			2 -> [Result1]
		//			...
		stream << race.Name << ":" << std::endl;

		uint32_t index = 1U;
		std::for_each(race.Results.begin(), race.Results.end(),
			[&stream, &index](const uint32_t& result)
			{
				stream << std::left << std::setw(3) << index++ << " -> " << result << std::endl;
			});

		return stream;
	}
};

int main()
{
	// Place in race -> points in classification. Places above 10th are scored 0.
	const std::map<uint32_t, uint32_t> placePointsMap = {
		{ 1U, 25U},
		{ 2U, 20U},
		{ 3U, 15U},
		{ 4U, 10U},
		{ 5U, 8U},
		{ 6U, 6U},
		{ 7U, 5U},
		{ 8U, 3U},
		{ 9U, 2U},
		{ 10U, 1U}
	};

	std::vector<Driver> drivers =
	{
		{ 0U, "Max", "Verstappen", "Red Bull Racing", 0U },
		{ 1U, "Charles", "Leclerc", "Ferrari", 0U },
		{ 2U, "Sergio", "Perez", "Red Bull Racing", 0U },
		{ 3U, "George", "Russell", "Mercedes", 0U },
		{ 4U, "Carlos", "Sainz", "Ferrari", 0U },
		{ 5U, "Lewis", "Hamilton", "Mercedes", 0U },
		{ 6U, "Lando", "Norris", "McLaren", 0U },
		{ 7U, "Esteban", "Ocon", "Alpine", 0U },
		{ 8U, "Fernando", "Alonso", "Alpine", 0U },
		{ 9U, "Valtteri", "Bottas", "Alfa Romeo", 0U },
		{ 10U, "Daniel", "Ricciardo", "McLaren", 0U },
		{ 11U, "Sebastian", "Vettel", "Aston Martin", 0U },
	};

	const uint32_t racesCount = 5U;
	std::vector<Race> races;

	srand(1234); // DON'T REMOVE IT. This should be called before Stage 1.

	// Stage 1. (1.0): Generate raceCount elements into races vector. The race name should be in format Race_[id] (e.g. Race_0, Race_1, ...).
	// Fill results member vector with shuffled list of driver ids. Use std::random_shuffle function for shuffling.
	std::cout << "/************** Stage 1 (1.0p) **************/" << std::endl;
	uint32_t raceId = 0U;
	std::generate_n(std::back_inserter(races), racesCount,
		[&drivers, &raceId]()
		{
			std::vector<uint32_t> results;
			std::transform(drivers.begin(), drivers.end(), std::back_inserter(results), [](const Driver& driver) { return driver.Id; });
			std::random_shuffle(results.begin(), results.end());
			return Race{ "Race_" + std::to_string(raceId++), results };
		});

	// Stage 2. (1.0p): Fill insertion operators (operator<<) in Driver and Race structures, then print drivers and races vectors.
	std::cout << "/************** Stage 2 (1.0p) **************/" << std::endl;

	std::for_each(drivers.begin(), drivers.end(), [](const Driver& driver) { std::cout << driver; });
	std::cout << std::endl;
	std::for_each(races.begin(), races.end(), [](const Race& race) { std::cout << race; });

	// Stage 3. (1.0p): Based on races results, fill total classification points obtained by each driver.
	// Use placePointsMap to map drivers place to points obtained from given race. Print drivers vector.
	std::cout << "/************** Stage 3 (1.0p) **************/" << std::endl;

	std::transform(drivers.begin(), drivers.end(), drivers.begin(),
		[&races, &placePointsMap](Driver& driver)
		{
			uint32_t driverId = driver.Id;
			uint32_t totalPoints = std::accumulate(races.begin(), races.end(), 0U,
				[&driverId, &placePointsMap](uint32_t sum, const Race& race)
				{
					auto foundResult = std::find(race.Results.begin(), race.Results.end(), driverId);
					if (foundResult != race.Results.end())
					{
						auto rank = std::distance(race.Results.begin(), foundResult) + 1U;
						auto foundPlacePointsPair = placePointsMap.find(rank);
						return sum + (foundPlacePointsPair != placePointsMap.end() ? foundPlacePointsPair->second : 0U);
					}

					return sum;
				});
			driver.TotalPoints = totalPoints;
			return driver;
		});

	std::for_each(drivers.begin(), drivers.end(), [](const Driver& driver) { std::cout << driver; });

	// Stage 4. (1.0p): Copy the best driver from each team into to drivers4 vector. Print the results.
	std::cout << "/************** Stage 4 (1.0p) **************/" << std::endl;
	std::vector<Driver> drivers4;

	std::copy(drivers.begin(), drivers.end(), std::back_inserter(drivers4));
	std::sort(drivers4.begin(), drivers4.end(),
		[](const Driver& driver1, const Driver& driver2)
		{	
			if (driver1.Team != driver2.Team)
			{
				return driver1.Team < driver2.Team;
			}

			return driver1.TotalPoints > driver2.TotalPoints;
		});
	auto end = std::unique(drivers4.begin(), drivers4.end(), [](const Driver& driver1, const Driver& driver2) { return driver1.Team == driver2.Team; });
	drivers4.erase(end, drivers4.end());

	std::for_each(drivers4.begin(), drivers4.end(), [](const Driver& driver) { std::cout << driver; });

	// Stage 5. (1.0p): Find driver that has the most number of podiums. Print the result. You can add an additional variable to Driver structure.
	std::cout << "/************** Stage 5 (1.0p) **************/" << std::endl;

	std::vector<uint32_t> podiumsCount;
	std::transform(drivers.begin(), drivers.end(), std::back_inserter(podiumsCount),
		[&races](const Driver& driver)
		{
			uint32_t driverId = driver.Id;
			return static_cast<uint32_t>(std::count_if(races.begin(), races.end(),
				[&driverId](const Race& race)
				{
					auto podiumEnd = race.Results.begin() + 3;
					return std::find(race.Results.begin(), podiumEnd, driverId) != podiumEnd;
				}));
		}
	);

	auto maxPodiumsIndex = std::distance(podiumsCount.begin(), std::max_element(podiumsCount.begin(), podiumsCount.end()));

	std::cout << drivers[maxPodiumsIndex] << std::endl;
}