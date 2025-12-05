#include <iostream>
#include <vector>
#include <algorithm>
#include <iterator>
#include <numeric>
#include <tuple>

using namespace std;


// Korzystaj¹c z biblioteki STL nale¿y rozwi¹zaæ znajduj¹ce siê w funkcji main 5 zadañ (ka¿de po 1 pkt). Etapy 2-5 mo¿na rozwi¹zaæ w dowolnej kolejnoœci, 
// natomiast etap 1 nale¿y rozwi¹zaæ najpierw. Nale¿y korzystaæ z algorytmów i funktorów z biblioteki STL oraz wyra¿eñ lambda. 
// Zakazane jest korzystanie z pêtli!!! Wszystkie operacje modyfikuj¹ce zbiór wejœciowy powinny byæ wykonywane na jego kopii (Etapy 2-5)!!!

// Funkcja wypisuje na konsoli wszystkie elementy wektora za pomoc¹ for_each
void PrintVector(vector<int> input)
{
	//TODO
};

// Funkcja zwraca element wektora najbli¿szy wartoœci 6 oraz wektor zmodyfikowany w nastêpuj¹cy sposób:
//   - elementy znajduj¹ce siê po wyznaczonym elemencie zostaj¹ zast¹pione wartoœci¹ 0
//   - pozosta³e elementy nie s¹ modyfikowane
//   - w przypadku gdy istnieje kilka elementów o wartoœci najbli¿szej 6 modyfikowane s¹ elementy
//     po pierwszym wyst¹pieniu takiego elementu
std::tuple<int, std::vector<int>> GetTask2Output(std::vector<int> input)
{
	std::vector<int> output_vector(input);
	int min_value = 0;

	//TODO

	return { min_value, output_vector };
};

// Funkcja zwraca œredni¹ oraz odchylenie standardowe dla elementów podanego wektora
std::tuple<double, double> GetTask3Output(std::vector<int> input)
{
	double average = 0.0;
	double standard_deviation = 0.0;

	//TODO

	return { average, standard_deviation };
}

int main()
{
	srand(0xFULL);

	cout << "Etap 1." << endl;
	//1. (1 pkt) Zaimplementowaæ funkcje PrintVector, która wypisuje elementy wektora
	// oraz uzupe³niæ nastêpuj¹ce wektory losowymi liczbami z przedzia³u:
	// *v1 - [-5;5);
	// *v2 - [0;20);
	// *v3 - [-6;10);
	// *v4 - [-10;10);

	vector<int> v1(11);
	vector<int> v2(11);
	vector<int> v3(16);
	vector<int> v4(23);

	//TODO

	PrintVector(v1);
	PrintVector(v2);
	PrintVector(v3);
	PrintVector(v4);

	cout << "Etap 2." << endl;
	//2. (1 pkt) Wybraæ z wektora v1 wszystkie liczby nieparzyste oraz z wektora v2 wszystkie liczby parzyste i po³¹czyæ je w wektor v5
	// (najpierw elementy z v1 potem z v2), a nastêpnie zaimplementowaæ funkcjê GetTask2Output
	vector<int> v5;
	int min_value;
	vector<int> output_task2;

	//TODO

	tie(min_value, output_task2) = GetTask2Output(v5);

	cout << "Element wektora najbli¿szy wartoœci 6: " << min_value << endl;
	PrintVector(output_task2);

	cout << "Etap 3." << endl;
	//3. (1 pkt) Zaimplementowaæ funkcjê GetTask3Output
	double average;
	double standard_deviation;

	tie(average, standard_deviation) = GetTask3Output(v3);
	cout << "Wektor v3 | Srednia : " << average << " Odchylenie standardowe: " << standard_deviation << endl;

	tie(average, standard_deviation) = GetTask3Output(v4);
	cout << "Wektor v4 | Srednia : " << average << " Odchylenie standardowe: " << standard_deviation << endl;

	cout << "Etap 4." << endl;
	//4. (1 pkt) Zmodyfikowaæ wektor v4 w taki sposób, aby na zmianê wystêpowa³a liczba parzysta i liczba nieparzysta
	// (np. maj¹c wektor -5,-9,3,3,-6,-8,0,8,-3 nale¿y po liczbie -5 usun¹æ ci¹g liczb nieparzystych dochodz¹c do liczby -6, gdzie zmienia siê parzystoœæ)
	// Nastêpnie w uzyskanym wektorze wyznaczyæ liczbê elementów ujemnych
	int count_negative = 0;
	vector<int> output_task4(v4);

	//TODO

	cout << "Liczba elementow ujemnych wektora: " << count_negative << endl;
	PrintVector(output_task4);

	cout << "Etap 5." << endl;
	//5. (1 pkt) Usun¹æ z wektora v4 wszystkie liczby parzyste, nastêpnie uzyskany wektor posortowaæ wed³ug kwadratu wartoœci oraz znaleŸæ ostatni ujemny element
	// podzielny przez 3
	int last_element = -100;
	vector<int> output_task5(v4);

	//TODO

	cout << "Ostatni ujemny element wektora podzielny przez 3: " << last_element << endl;
	PrintVector(output_task5);


	return 0;
}

