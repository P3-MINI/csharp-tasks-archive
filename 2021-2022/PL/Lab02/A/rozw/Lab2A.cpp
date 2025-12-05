#include <algorithm>
#include <iomanip>
#include <iostream>
#include <map>
#include <numeric>
#include <set>
#include <vector>

struct Student
{
	std::string Name;                               // Imię
	std::string Surname;                            // Nazwisko
	int Age;                                        // Wiek
	double AverageGrade;							// Średnia ważona ocen
	std::map<std::string, double> SubjectGradeMap;  // Nazwa przedmiotu z oceną końcową 
	// Pola pomocnicze
	int CompleteSubjectsCount;
};

void PrintStudents(const std::vector<Student>& students, bool showSubjects)
{
	// Korzystając z funkcji std::for_each wypisać studentów wg poniższego wzoru:
	// [Indeks] [Imię] [Nazwisko] [Wiek] [Średnia]
	// - [Nazwa przedmiotu] [Ocena]
	// - ...
	// Lista przedmiotów wyświetlana jest gdy showSubjects = true
	// Do formatowania outputu można skorzystać z funkcji std::left, std::right, std::setw, std::setprecision
	int index = 1;
	std::for_each(students.begin(), students.end(),
		[&index, &showSubjects](const Student& student) -> void
		{
			std::cout << index++ << ". " << std::left << std::setw(10) << student.Name << std::setw(18) << student.Surname
				<< std::right << std::setw(5) << student.Age << std::setw(5) << std::setprecision(2) << student.AverageGrade << std::endl;

			if (showSubjects)
			{
				std::for_each(student.SubjectGradeMap.begin(), student.SubjectGradeMap.end(),
					[](const std::pair<std::string, double>& subjectGrade) -> void
					{
						std::cout << "- " << std::left << std::setw(30) << subjectGrade.first << std::right << std::setw(4) << subjectGrade.second << std::endl;
					});
			}
		});
}

int main()
{
	const std::map<std::string, int> subjectsEctsMap = {
		{"Programowanie 1", 5 },
		{"Analiza matematyczna 1", 6 }, 
		{"Metody numeryczne 2", 4 },
		{"Podstawy elektroniki", 4 },
		{"UNIX", 3 },
		{"Grafika komputerowa 1", 4 },
		{"Fizyka 1", 4 }
	};

	std::vector<Student> students =
	{
		{ "Jan", "Kowalski", 20, 0.0, {{"Programowanie 1", 4.0 }, {"Analiza matematyczna 1", 4.5 }, {"Metody numeryczne 2", 3.0 }, {"Podstawy elektroniki", 3.0 }, {"UNIX", 5.0 }, {"Grafika komputerowa 1", 5.0 }, {"Fizyka 1", 4.0 }}},
		{ "Albert", "Nowak", 22, 0.0, {{"Programowanie 1", 3.0 }, {"Analiza matematyczna 1", 3.5 }, {"Metody numeryczne 2", 4.0 }, {"Podstawy elektroniki", 2.0 }, {"UNIX", 3.0 }, {"Grafika komputerowa 1", 4.0 }, {"Fizyka 1", 3.5 }}},
		{ "Lech", "Nowicki", 27, 0.0, {{"Programowanie 1", 2.0 }, {"Analiza matematyczna 1", 2.0 }, {"Metody numeryczne 2", 2.0 }, {"Podstawy elektroniki", 2.0 }, {"UNIX", 2.0 }, {"Grafika komputerowa 1", 2.0 }, {"Fizyka 1", 2.0 }}},
		{ "Adam", "Homenda", 19, 0.0, {{"Programowanie 1", 3.0 }, {"Analiza matematyczna 1", 3.5 }, {"Metody numeryczne 2", 2.0 }, {"Podstawy elektroniki", 2.0 }, {"UNIX", 4.0 }, {"Grafika komputerowa 1", 3.0 }, {"Fizyka 1", 3.0 }}},
		{ "Jakub", "Lewicki", 23, 0.0, {{"Programowanie 1", 3.0 }, {"Analiza matematyczna 1", 3.5 }, {"Metody numeryczne 2", 2.0 }, {"Podstawy elektroniki", 3.5 }, {"UNIX", 4.0 }, {"Grafika komputerowa 1", 3.0 }, {"Fizyka 1", 2.0 }}},
		{ "Kamil", "Wasilewski", 24, 0.0, {{"Programowanie 1", 5.0 }, {"Analiza matematyczna 1", 4.5 }, {"Metody numeryczne 2", 5.0 }, {"Podstawy elektroniki", 4.5 }, {"UNIX", 4.0 }, {"Grafika komputerowa 1", 4.0 }, {"Fizyka 1", 4.0 }}},
	};

	// Etap 1. (1.0p): Uzupełnić funkcję PrintStudents.
	std::cout << "/************** Etap 1 (1.0p) **************/" << std::endl;

	PrintStudents(students, true);

	// Etap 2. (1.0p): Uzupełnić średnią ważoną ocen dla każdego studenta. Skorzystać z danych zapisanych w subjectEctsMap (liczba ECTS przypisana do danego przedmiotu).
	// Można przyjąć, że każdy student chodził na takie same przedmioty.
	std::cout << "/************** Etap 2 (1.0p) **************/" << std::endl;

	int totalEcts = std::accumulate(subjectsEctsMap.begin(), subjectsEctsMap.end(), 0,
		[](int sum, const std::pair<std::string, int>& subjectEcts) -> int
		{
			return sum + subjectEcts.second;
		});

	std::transform(students.begin(), students.end(), students.begin(),
		[&totalEcts, &subjectsEctsMap](Student& student)-> Student&
		{
			student.AverageGrade = std::accumulate(student.SubjectGradeMap.begin(), student.SubjectGradeMap.end(), 0.0,
				[&subjectsEctsMap](double sum, const std::pair<std::string, double>& subjectGrade) -> double
				{
					auto subject = subjectsEctsMap.find(subjectGrade.first);
					return subject == subjectsEctsMap.end() ? sum : sum + subjectGrade.second * subject->second;
				}) / totalEcts;
				return student;
		});

	PrintStudents(students, false);

	// Etap 3. (1.0p): Skopiować do studentsResult3 wszystkich studentów, którzy mają niezaliczony co najmniej jeden przedmiot (ocena 2.0).
	std::cout << "/************** Etap 3 (1.0p) **************/" << std::endl;
	std::vector<Student> studentsResult3;

	std::copy_if(students.begin(), students.end(), std::back_inserter(studentsResult3),
		[](const Student& student)->bool
		{
			return std::find_if(student.SubjectGradeMap.begin(), student.SubjectGradeMap.end(),
				[](const std::pair<std::string, double>& subjectGrade) -> bool
				{
					return subjectGrade.second == 2.0;
				}) != student.SubjectGradeMap.end();
		});

	PrintStudents(studentsResult3, false);

	// Etap 4. (1.5p): Posortować listę studentów według liczby zaliczonych przedmiotów od największej do najmniejszej (można dodać pole pomocnicze w strukturze).
	// Dla takiej samej liczby zaliczonych przedmiotów należy ustawić elementy według średniej od najniższej do najwyższej.
	std::cout << "/************** Etap 4 (1.5p) **************/" << std::endl;

	std::transform(students.begin(), students.end(), students.begin(),
		[](Student& student)-> Student&
		{
			student.CompleteSubjectsCount = static_cast<int>(std::count_if(student.SubjectGradeMap.begin(), student.SubjectGradeMap.end(),
				[](const std::pair<std::string, double>& subjectGrade) -> bool
				{
					return subjectGrade.second > 2.0;
				}));
			return student;
		});

	std::sort(students.begin(), students.end(),
		[](const Student& stud1, const Student& stud2) -> bool
		{
			if (stud1.CompleteSubjectsCount == stud2.CompleteSubjectsCount)
			{
				return stud1.AverageGrade < stud2.AverageGrade;
			}

			return stud1.CompleteSubjectsCount < stud2.CompleteSubjectsCount;
		});

	PrintStudents(students, false);

	// Etap 5. (0.5p): Wyznaczyć i wypisać (można skorzystać z std::for_each) dopełnienie części wspólnej iloczynu zbiorów set1 i set2 tzn: (set1 U set2) - (set1 ^ set2).
	std::cout << "/************** Etap 5 (0.5p) **************/" << std::endl;

	std::set<int> set1 = { 2, 3, 10, 14, 15, 18, 20, 21, 25, 29, 31, 35, 36, 37 };
	std::set<int> set2 = { 1, 3, 9, 15, 17, 21, 22, 24, 25, 30, 32, 35, 36, 40 };
	std::vector<int> result5;

	std::set_symmetric_difference(set1.begin(), set1.end(), set2.begin(), set2.end(), std::back_inserter(result5));

	std::for_each(result5.begin(), result5.end(), 
		[](int value) -> void { std::cout << value << ", "; });
}
