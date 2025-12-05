#include <iomanip>
#include <iostream>
#include <map>
#include <stdlib.h>
#include <string>
#include <time.h> 
#include <vector>
#include <algorithm>
#include <numeric>

struct SkiJumper
{
	uint32_t Id;			// Id zawodnika
	std::string Name;		// Imię
	std::string Surname;	// Nazwisko
	std::string Country;	// Kraj
	uint32_t TotalPoints;	// Punkty w klasyfikacji generalnej

	friend std::ostream& operator<<(std::ostream& stream, const SkiJumper& jumper)
	{
		// TODO: Uzupełnić operator zgodnie z formatem
		// Format: [Id] [Name] [Surname] [Country] [TotalPoints]
		return stream << std::left << std::setw(3) << jumper.Id << std::setw(10) << jumper.Name << std::setw(18) << jumper.Surname << std::setw(9) << jumper.Country
			<< std::right << std::setw(5) << jumper.TotalPoints << std::endl;
	}
};

struct Result
{
	uint32_t Place;		// Zajęte miejsce
	uint32_t JumperId;	// Id zawodnika
	double Distance;	// Długość skoku

	friend std::ostream& operator<<(std::ostream& stream, const Result& result)
	{
		// TODO: Uzupełnić operator zgodnie z formatem
		// Format: [Place]. [JumperId] [Distance]m
		return stream << std::left << result.Place << "." << std::right << std::setw(5) << result.JumperId << std::setw(10) << std::fixed << std::setprecision(1) << result.Distance << "m" << std::endl;
	}
};

struct Competition
{
	std::string Name;				// Nazwa zawodów
	std::vector<Result> Results;	// Wyniki

	friend std::ostream& operator<<(std::ostream& stream, const Competition& competition)
	{
		// TODO: Uzupełnić operator zgodnie z formatem. Należy użyć funkcję std::for_each.
		// Format: [Name]:
		//			* [Result0]
		//			* [Result1]
		//			* ...
		stream << competition.Name << ":" << std::endl;

		std::for_each(competition.Results.begin(), competition.Results.end(),
			[&stream](const Result& result)
			{
				stream <<" * " << result;
			});

		return stream;
	}
};

int main()
{
	// Zajęte miejsce -> liczba punktów do klasyfikacji generalnej
	const std::map<uint32_t, uint32_t> placePointsMap = {
		{ 1U, 100U},
		{ 2U, 80U},
		{ 3U, 60U},
		{ 4U, 50U},
		{ 5U, 45U},
		{ 6U, 40U},
		{ 7U, 36U},
		{ 8U, 32U},
		{ 9U, 29U},
		{ 10U, 26U},
		{ 11U, 24U},
		{ 12U, 22U},
	};

	// Lista zawodników
	std::vector<SkiJumper> jumpers =
	{
		{ 0U, "Jan", "Hoerl", "Austria", 0U },
		{ 1U, "Yukiya", "Sato", "Japan", 0U },
		{ 2U, "Karl", "Geiger", "Germany", 0U },
		{ 3U, "Ryoyu", "Kobayashi", "Japan", 0U },
		{ 4U, "Marius", "Lindvik", "Norway", 0U },
		{ 5U, "Halvor", "Granerud", "Norway", 0U },
		{ 6U, "Stefan", "Kraft", "Austria", 0U },
		{ 7U, "Markus", "Eisenbichler", "Germany", 0U },
		{ 8U, "Anze", "Lanisek", "Slovenia", 0U },
		{ 9U, "Timi", "Zajc", "Slovenia", 0U },
		{ 10U, "Piotr", "Zyla", "Poland", 0U },
		{ 11U, "Kamil", "Stoch", "Poland", 0U },
	};

	const uint32_t competitionsCount = 5U;
	// Lista zawodów
	std::vector<Competition> competitions(competitionsCount);

	srand(1234);
	for (uint32_t competitionIndex = 0U; competitionIndex < competitionsCount; ++competitionIndex)
	{
		std::vector<Result> results(jumpers.size());
		
		for (uint32_t jumperIndex = 0U; jumperIndex < jumpers.size(); ++jumperIndex) 
		{
			results[jumperIndex] = { 1U, jumperIndex, 0.5 * (240 + rand() % 40) };
		}

		competitions[competitionIndex] = { "Competition_" + std::to_string(competitionIndex),results };
	}

	// Etap 1. (1.0p): Uzupełnić operatory wypisania (<<) dla struktur SkiJumper, Result, Competition, a następnie wypisać listę zawodników (jumpers) i konkursów (competitions).
	std::cout << "/************** Etap 1 (1.0p) **************/" << std::endl;

	std::for_each(jumpers.begin(), jumpers.end(), [](const SkiJumper& jumper) { std::cout << jumper; });
	std::cout << std::endl;
	std::for_each(competitions.begin(), competitions.end(), [](const Competition& competition) { std::cout << competition; });

	// Etap 2. (1.5p): Dla każdego konkursu posortować malejąco listę wyników według długości oddanego skoku. Następnie uzupełnić zajęte przez zawodników miejsca.
	// Jeżeli dwóch zawodników ma ten sam wynik to zajmuje dane miejsce ex aequo. Wypisać listę konkursów. 
	std::cout << "/************** Etap 2 (1.5p) **************/" << std::endl;

	std::transform(competitions.begin(), competitions.end(), competitions.begin(),
		[](Competition& competition)
		{
			std::sort(competition.Results.begin(), competition.Results.end(),
				[](const Result& result1, const Result& result2) { return result1.Distance > result2.Distance; });

			double prevDistance = competition.Results.front().Distance;
			uint32_t place = 1U;
			std::transform(competition.Results.begin(), competition.Results.end(), competition.Results.begin(),
				[&prevDistance, &place](Result& result) 
				{ 
					if (prevDistance != result.Distance)
					{
						++place;
						prevDistance = result.Distance;
					}
					result.Place = place;

					return result; 
				});

			return competition;
		});

	std::for_each(competitions.begin(), competitions.end(), [](const Competition& competition) { std::cout << competition; });

	// Etap 3. (1.0p): Dla każdego zawodnika uzupełnić uzyskaną przez niego w klasyfikacji generalnej liczbę punktów - suma punktów uzyskanych we wszystkich zawodach.
	// W mapie placePointsMap znajduje się liczba punktów uzyskanych przez zawodnika za zajęte w konkursie miejsce. Wypisać zawodników.
	std::cout << "/************** Etap 3 (1.0p) **************/" << std::endl;
	std::transform(jumpers.begin(), jumpers.end(), jumpers.begin(),
		[&competitions, &placePointsMap](SkiJumper& jumper)
		{
			uint32_t id = jumper.Id;
			jumper.TotalPoints = std::accumulate(competitions.begin(), competitions.end(), 0U,
				[&id, &placePointsMap](uint32_t sum, const Competition& competition)
				{
					auto currentJumper = std::find_if(competition.Results.begin(), competition.Results.end(), [&id](const Result& result) {return result.JumperId == id; });
					if (currentJumper != competition.Results.end())
					{
						return sum + placePointsMap.at(currentJumper->Place);
					}

					return sum;
				});

			return jumper;
		});

	std::for_each(jumpers.begin(), jumpers.end(), [](const SkiJumper& jumper) { std::cout << jumper; });

	// Etap 4. (1.0p): Do wektora jumpers4 skopiować wszystkich zawodników, którzy wygrali chociaż raz, a następnie wypisać wynik.
	std::cout << "/************** Etap 3 (1.0p) **************/" << std::endl;
	std::vector<SkiJumper> jumpers4;

	std::copy_if(jumpers.begin(), jumpers.end(), std::back_inserter(jumpers4),
		[&competitions](const SkiJumper& jumper)
		{
			uint32_t id = jumper.Id;
			auto winsCount = std::count_if(competitions.begin(), competitions.end(),
				[&id](const Competition& competition) 
				{ 
					auto currentJumper = std::find_if(competition.Results.begin(), competition.Results.end(), [&id](const Result& result) {return result.JumperId == id; });
					if (currentJumper != competition.Results.end())
					{
						return currentJumper->Place == 1U;
					}
					return false;
				});

			return winsCount > 0;
		});

	std::for_each(jumpers4.begin(), jumpers4.end(), [](const SkiJumper& jumper) { std::cout << jumper; });

	// Etap 5. (0.5): Wyznaczyć wszystkie kraje biorące udział w zawodach. Nazwy krajów nie powinny się powtarzać. Wypisać wynik.
	std::cout << "/************** Etap 3 (0.5p) **************/" << std::endl;
	std::vector<std::string> countries;

	std::transform(jumpers.begin(), jumpers.end(), std::back_inserter(countries), [](const SkiJumper& jumper) {return jumper.Country; });
	std::sort(countries.begin(), countries.end());
	auto end = std::unique(countries.begin(), countries.end());
	std::for_each(countries.begin(), end, [](const std::string& country) { std::cout << country << std::endl; });
}