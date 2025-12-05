#include <algorithm>
#include <iomanip>
#include <iostream>
#include <map>
#include <numeric>
#include <set>
#include <vector>

struct Worker
{
	std::string Name;									// Imię
	std::string Surname;								// Nazwisko
	int Age;											// Wiek
	double Salary;										// Pensja
	std::map<std::string, double> TaskEstimateTimeMap;	// Nazwa zadania z przeznaczonym czasem w godzinach 
	// Pola pomocnicze
	double TotalServiceTime;
	int TotalServiceCount;
};

void PrintWorkers(const std::vector<Worker>& workers, bool showTasks)
{
	// Korzystając z funkcji std::for_each wypisać pracowników wg poniższego wzoru:
	// [Indeks] [Imię] [Nazwisko] [Wiek] [Pensja]
	// - [Nazwa zadania] [Czas]h
	// - ...
	// Lista zadań wyświetlana jest gdy showTasks = true
	// Do formatowania outputu można skorzystać z funkcji std::left, std::right, std::setw, std::setprecision
	int index = 1;
	std::for_each(workers.begin(), workers.end(),
		[&index, &showTasks](const Worker& worker) -> void
		{
			std::cout << index++ << ". " << std::left << std::setw(10) << worker.Name << std::setw(14) << worker.Surname
				<< std::right << std::setw(4) << worker.Age << std::setw(6) << std::setprecision(6) << worker.Salary << std::endl;

			if (showTasks)
			{
				std::for_each(worker.TaskEstimateTimeMap.begin(), worker.TaskEstimateTimeMap.end(),
					[](const std::pair<std::string, double>& taskEstimate) -> void
					{
						std::cout << "- " << std::left << std::setw(30) << taskEstimate.first << std::right << std::setw(4) << taskEstimate.second << "h" << std::endl;
					});
			}
		});
}

int main()
{
	const std::map<std::string, double> taskSalaryPerHourMap = {
		{ "Szkolenie", 50.0 },
		{ "Serwis male RTV", 70.0 },
		{ "Serwis duze RTV", 100.0 },
		{ "Serwis komputery", 150.0 },
		{ "Inwentaryzacja", 50.0 },
		{ "Obsluga klienta", 30.0 }
	};

	std::vector<Worker> workers =
	{
		{ "Maciej", "Maciejewski", 40, 0.0, { { "Szkolenie", 2.0 }, { "Serwis male RTV", 0.0 }, { "Serwis duze RTV", 0.0 }, { "Serwis komputery", 40.0 }, { "Inwentaryzacja", 2.0 }, { "Obsluga klienta", 10.0 }}},
		{ "Jan", "Kowalski", 25, 0.0, { { "Szkolenie", 5.0 }, { "Serwis male RTV", 30.0 }, { "Serwis duze RTV", 10.0 }, { "Serwis komputery", 0.0 }, { "Inwentaryzacja", 2.0 }, { "Obsluga klienta", 1.0 }}},
		{ "Albert", "Nowak", 30, 0.0, { { "Szkolenie", 10.0 }, { "Serwis male RTV", 20.0 }, { "Serwis duze RTV", 20.0 }, { "Serwis komputery", 3.0 }, { "Inwentaryzacja", 0.0 }, { "Obsluga klienta", 5.0 }}},
		{ "Milosz", "Wasilewski", 20, 0.0, { { "Szkolenie", 20.0 }, { "Serwis male RTV", 4.0 }, { "Serwis duze RTV", 4.0 }, { "Serwis komputery", 0.0 }, { "Inwentaryzacja", 10.0 }, { "Obsluga klienta", 12.0 }}},
		{ "Jakub", "Nowicki", 35, 0.0, { { "Szkolenie", 2.0 }, { "Serwis male RTV", 20.0 }, { "Serwis duze RTV", 20.0 }, { "Serwis komputery", 5.0 }, { "Inwentaryzacja", 8.0 }, { "Obsluga klienta", 0.0 }}}
	};

	// Etap 1. (1.0p): Uzupełnić funkcję PrintWorkers.
	std::cout << "/************** Etap 1 (1.0p) **************/" << std::endl;

	PrintWorkers(workers, true);

	// Etap 2. (1.0p): Uzupełnić pensje dla każdego pracownika. Należy skorzystać z przelicznika PLN/h umieszczonego w taskSalaryPerHourMap.
	std::cout << "/************** Etap 2 (1.0p) **************/" << std::endl;

	std::transform(workers.begin(), workers.end(), workers.begin(),
		[&taskSalaryPerHourMap](Worker& worker)-> Worker&
		{
			worker.Salary = std::accumulate(worker.TaskEstimateTimeMap.begin(), worker.TaskEstimateTimeMap.end(), 0.0,
				[&taskSalaryPerHourMap](double sum, const std::pair<std::string, double> taskEstimateTime) -> double
				{
					auto task = taskSalaryPerHourMap.find(taskEstimateTime.first);
					return task == taskSalaryPerHourMap.end() ? sum : sum + taskEstimateTime.second * task->second;
				});
				return worker;
		});

	PrintWorkers(workers, false);

	// Etap 3. (1.0p): Skopiować do workersResult3 wszystkich pracowników, którzy nie pracują we wszystkich serwisach (liczba godzin 0.0 przy danym serwisie).
	std::cout << "/************** Etap 3 (1.0p) **************/" << std::endl;
	std::vector<Worker> workersResult3;

	std::copy_if(workers.begin(), workers.end(), std::back_inserter(workersResult3),
		[](const Worker& worker)->bool
		{
			return std::find_if(worker.TaskEstimateTimeMap.begin(), worker.TaskEstimateTimeMap.end(),
				[](const std::pair<std::string, double>& taskEstimateTime) -> bool
				{
					return taskEstimateTime.first.find("Serwis") != std::string::npos && taskEstimateTime.second == 0.0;
				}) != worker.TaskEstimateTimeMap.end();
		});

	PrintWorkers(workersResult3, false);

	// Etap 4. (1.5p): Posortować listę pracowników według liczby godzin przeznaczonych na serwis od największej do najmniejszej (można dodać pole pomocnicze w strukturze).
	// Dla takiej samej liczby godzin należy ustawić elementy według liczby obsługiwanych serwisów od największej do najmniejszej.
	std::cout << "/************** Etap 4 (1.5p) **************/" << std::endl;

	std::transform(workers.begin(), workers.end(), workers.begin(),
		[](Worker& workers)-> Worker&
		{
			workers.TotalServiceCount = static_cast<int>(std::count_if(workers.TaskEstimateTimeMap.begin(), workers.TaskEstimateTimeMap.end(),
				[](const std::pair<std::string, double>& taskEstimateTime) -> bool
				{
					return taskEstimateTime.first.find("Serwis") != std::string::npos && taskEstimateTime.second > 0.0;
				}));

			workers.TotalServiceTime = std::accumulate(workers.TaskEstimateTimeMap.begin(), workers.TaskEstimateTimeMap.end(), 0.0,
				[](double sum, const std::pair<std::string, double>& taskEstimateTime) -> double
				{
					return taskEstimateTime.first.find("Serwis") == std::string::npos ? sum : sum + taskEstimateTime.second;
				});

			return workers;
		});

	std::sort(workers.begin(), workers.end(),
		[](const Worker& worker1, const Worker& worker2) -> bool
		{
			if (worker1.TotalServiceTime == worker2.TotalServiceTime)
			{
				return worker1.TotalServiceCount > worker2.TotalServiceCount;
			}

			return worker1.TotalServiceTime > worker2.TotalServiceTime;
		});

	PrintWorkers(workers, false);

	// Etap 5. (0.5p): Wyznaczyć i wypisać (można skorzystać z std::for_each) różnicę dwóch zbiorów set1 i set2.
	std::cout << "/************** Etap 5 (0.5p) **************/" << std::endl;

	std::vector<int> set1 = { 29, 10, 15, 14, 18, 36, 25, 35, 2, 31, 21, 20, 37, 3 };
	std::vector<int> set2 = { 30, 3, 35, 15, 21, 17, 32, 22, 25, 40, 24, 9, 36, 1 };
	std::vector<int> result5;

	std::sort(set1.begin(), set1.end());
	std::sort(set2.begin(), set2.end());
	std::set_difference(set1.begin(), set1.end(), set2.begin(), set2.end(), std::back_inserter(result5));

	std::for_each(result5.begin(), result5.end(),
		[](int value) -> void { std::cout << value << ", "; });
}