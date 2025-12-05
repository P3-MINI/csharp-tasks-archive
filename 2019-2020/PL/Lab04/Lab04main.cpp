#include <cstdlib>
#include <iostream>
#include <algorithm>
#include "SudokuSolver.h"

// Sudoku solver
// Zaimplementuj klasę SudokuSolver rozwiązującą Sudoku o podanym rozmiarze regionu (w naszym przypadku od 2 do 4)
// wykorzystując algorytm z nawrotami (ang. backtracking)
//
// Etap 1 ( 1pkt )
// Metody: SetBoard, CheckBoard
// SetBoard - wczytuje planszę podaną w postaci wektora liczb kolejnych wartości zapisanych wierszowo
// CheckBoard - sprawdza czy aktualnie przechowywana plansza w pamięci jest prawidłowa, 
// tzn. czy w każdej kolumnie, wierszu i regionie wartości się nie powtarzają oraz czy są z poprawnego zakresu
// działa również dla częściowo wypełnionych planszy, w szczególności dla początkowej.
// Liczba 0 reprezentuje puste pole
//
// Etap 2 ( 3pkt )
// Metoda: Solve
// Metoda zwraca wszystkie znalezione rozwiązania wykoirzystując metodę z nawrotami.
// Sudoku może nie mieć żadnego rozwiązania, mieć jedno lub więcej rozwiązań.
//
// Etap 3 (1pkt)
// Optymalnie napisana metoda rozwiązywania Sudoku.
// Podpowiedź: przy każdym ruchu nie trzeba sprawdzać czy podana liczba jest unikatowa w wierszu/kolumnie/regione,
// można zapisać tę informację w pomocniczej zmiennej.
//
// Uwaga!
// Testy z sudoku o rozmiarze 4 można w trakcie pisania kodu zakomentować (mogą liczyć się długo w trybie Debug)


// #define PRINT_SUDOKU			//Comment this line for shorter output
#define STAGE2					//Uncomment for stage 2

enum class TestType {
	OK, WrongBoard, NoSolution
};

struct Tests {
	size_t size;
	TestType type;
	std::vector<int> startBoard;
	std::vector<Board> solutions;
};

bool boardComp(const Board& A, const Board& B) {
	if (A.size() != B.size())
		return A.size() < B.size();

	for (unsigned i = 0; i < A.size(); i++) {
		if (A[i].size() != B[i].size())
			return A[i].size() < B[i].size();
		for (unsigned j = 0; j < A[i].size(); j++) {
			if (A[i][j] != B[i][j])
				return A[i][j] < B[i][j];
		}
	}
	return false;
}

bool boardEquiv(const Board& A, const Board& B) {
	if (A.size() != B.size())
		return false;

	for (unsigned i = 0; i < A.size(); i++) {
		if (A[i].size() != B[i].size())
			return false;
		for (unsigned j = 0; j < A[i].size(); j++) {
			if (A[i][j] != B[i][j])
				return false;
		}
	}
	return true;
}

Tests tests[] = {
	/* 1  */	{2, TestType::WrongBoard, {1, 0, 0, 1, 0, 0, 2, 0, 0, 1, 4, 0, 0, 0, 0, 3} },
	/* 2  */	{2, TestType::WrongBoard, {1, 0, 0, 0, 0, 0, 2, 0, 0, 1, 4, 0, 0, 0, 2, 3} },
	/* 3  */	{2, TestType::WrongBoard, {1, 0, 0, 2, 0, 0, 2, 0, 0, 1, 4, 0, 0, 0, 0, 3} },
	/* 4  */	{2, TestType::WrongBoard, {1, 0, 0, 0, 0, 0, 2, 0, 0, 5, 4, 0, 0, 0, 0, 3} },
	/* 5  */	{3, TestType::WrongBoard, {7, 0, 0, 0, 7, 9, 0, 6, 5, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, 0, 5, 0, 6, 0, 0, 9, 3, 3, 4, 0, 0, 5, 0, 1, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 8, 0, 2, 0, 0, 5, 9, 9, 5, 0, 0, 1, 0, 6, 0, 0, 7, 0, 0, 6, 0, 0, 0, 0, 0, 8, 2, 0, 3, 9, 0, 0, 0, 0} },
	/* 6  */	{3, TestType::WrongBoard, {0, 0, 0, 0, 7, 9, 0, 6, 5, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, 0, 5, 0, 6, 0, 0, 9, 3, 3, 4, 0, 0, 5, 0, 1, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 8, 0, 2, 0, 0, 5, 9, 9, 5, 0, 0, 1, 0, 6, 0, 0, 7, 0, 0, 6, 0, 0, 0, 0, 0, 8, 2, 0, 3, 9, 0, 0, 0, 6} },
	/* 7  */	{3, TestType::WrongBoard, {0, 0, 0, 0, 7, 9, 0, 6, 5, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, 0, 5, 0, 6, 0, 0, 9, 3, 3, 4, 0, 2, 5, 0, 1, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 8, 0, 2, 0, 0, 5, 9, 9, 5, 0, 0, 1, 0, 6, 0, 0, 7, 0, 0, 6, 0, 0, 0, 0, 0, 8, 2, 0, 3, 9, 0, 0, 0, 0} },

	/* 8  */	{2, TestType::OK, {1, 0, 0, 0, 0, 0, 2, 0, 0, 1, 4, 0, 0, 0, 0, 3}, { {{1, 2, 3, 4},{4, 3, 2, 1},{3, 1, 4, 2},{2, 4, 1, 3}} } },
	/* 9  */	{3, TestType::OK, {0, 0, 0, 0, 7, 9, 0, 6, 5, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, 0, 5, 0, 6, 0, 0, 9, 3, 3, 4, 0, 0, 5, 0, 1, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 8, 0, 2, 0, 0, 5, 9, 9, 5, 0, 0, 1, 0, 6, 0, 0, 7, 0, 0, 6, 0, 0, 0, 0, 0, 8, 2, 0, 3, 9, 0, 0, 0, 0}, { {{1, 8, 3, 2, 7, 9, 4, 6, 5},{4, 6, 9, 5, 8, 3, 7, 1, 2},{2, 7, 5, 4, 6, 1, 8, 9, 3},{3, 4, 2, 9, 5, 8, 1, 7, 6},{5, 9, 7, 1, 3, 6, 2, 8, 4},{6, 1, 8, 7, 2, 4, 3, 5, 9},{9, 5, 4, 8, 1, 2, 6, 3, 7},{7, 3, 1, 6, 4, 5, 9, 2, 8},{8, 2, 6, 3, 9, 7, 5, 4, 1}} } },
	/* 10 */	{4, TestType::OK, {1, 0, 0, 2, 3, 4, 0, 0, 12, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8, 0, 0, 0, 7, 0, 0, 3, 0, 0, 9, 10, 6, 11, 0, 12, 0, 0, 10, 0, 0, 1, 0, 13, 0, 11, 0, 0, 14, 0, 3, 0, 0, 15, 2, 0, 0, 14, 0, 0, 0, 9, 0, 0, 12, 0, 13, 0, 0, 0, 8, 0, 0, 10, 0, 12, 2, 0, 1, 15, 0, 0, 0, 11, 7, 6, 0, 0, 0, 16, 0, 0, 0, 15, 0, 0, 5, 13, 0, 0, 0, 10, 0, 5, 15, 0, 0, 4, 0, 8, 0, 0, 11, 0, 16, 0, 0, 5, 9, 12, 0, 0, 1, 0, 0, 0, 0, 0, 8, 0, 0, 2, 0, 0, 0, 0, 0, 13, 0, 0, 12, 5, 8, 0, 0, 3, 0, 13, 0, 0, 15, 0, 3, 0, 0, 14, 8, 0, 16, 0, 0, 0, 5, 8, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 13, 9, 15, 0, 0, 0, 12, 4, 0, 6, 16, 0, 13, 0, 0, 7, 0, 0, 0, 5, 0, 3, 0, 0, 12, 0, 0, 0, 6, 0, 0, 4, 11, 0, 0, 16, 0, 7, 0, 0, 16, 0, 5, 0, 14, 0, 0, 1, 0, 0, 2, 0, 11, 1, 15, 9, 0, 0, 13, 0, 0, 2, 0, 0, 0, 14, 0, 0, 0, 14, 0, 0, 0, 11, 0, 2, 0, 0, 13, 3, 5, 0, 0, 12}, { {{1, 5, 10, 2, 3, 4, 9, 11, 12, 16, 6, 14, 15, 13, 7, 8},{14, 16, 8, 13, 5, 15, 7, 12, 4, 3, 1, 2, 9, 10, 6, 11},{9, 12, 4, 7, 10, 16, 6, 1, 8, 13, 15, 11, 3, 5, 14, 2},{3, 6, 11, 15, 2, 13, 8, 14, 7, 5, 10, 9, 4, 16, 12, 1},{13, 4, 14, 3, 8, 7, 11, 10, 5, 12, 2, 6, 1, 15, 16, 9},{8, 11, 7, 6, 4, 1, 2, 16, 9, 10, 14, 15, 12, 3, 5, 13},{12, 9, 1, 10, 13, 5, 15, 6, 3, 4, 16, 8, 14, 2, 11, 7},{16, 15, 2, 5, 9, 12, 14, 3, 1, 11, 7, 13, 10, 6, 8, 4},{6, 2, 16, 14, 11, 9, 4, 13, 15, 1, 12, 5, 8, 7, 10, 3},{7, 13, 9, 1, 15, 2, 3, 5, 11, 14, 8, 10, 16, 12, 4, 6},{5, 8, 3, 11, 1, 10, 12, 7, 2, 6, 4, 16, 13, 9, 15, 14},{15, 10, 12, 4, 14, 6, 16, 8, 13, 9, 3, 7, 2, 11, 1, 5},{2, 3, 5, 8, 12, 14, 10, 15, 6, 7, 9, 4, 11, 1, 13, 16},{10, 7, 13, 12, 16, 3, 5, 9, 14, 8, 11, 1, 6, 4, 2, 15},{11, 1, 15, 9, 6, 8, 13, 4, 16, 2, 5, 12, 7, 14, 3, 10},{4, 14, 6, 16, 7, 11, 1, 2, 10, 15, 13, 3, 5, 8, 9, 12}} } },
	
	/* 11 */	{2, TestType::OK, {1,0,0,0,0,0,0,4,0,0,0,0,0,3,0,0}, { {{1, 4, 2, 3},{3, 2, 1, 4},{4, 1, 3, 2},{2, 3, 4, 1}}, {{1, 4, 3, 2},{3, 2, 1, 4},{2, 1, 4, 3},{4, 3, 2, 1}},{{1, 4, 3, 2},{3, 2, 1, 4},{4, 1, 2, 3},{2, 3, 4, 1}} } },
	/* 12 */	{3, TestType::OK, {0,8,0,0,0,9,7,4,3,0,5,0,0,0,8,0,1,0,0,1,0,0,0,0,0,0,0,8,0,0,0,0,5,0,0,0,0,0,0,8,0,4,0,0,0,0,0,0,3,0,0,0,0,6,0,0,0,0,0,0,0,7,0,0,3,0,5,0,0,0,8,0,9,7,2,4,0,0,0,5,0}, { {{2, 8, 6, 1, 5, 9, 7, 4, 3},{3, 5, 4, 7, 6, 8, 9, 1, 2},{7, 1, 9, 2, 4, 3, 5, 6, 8},{8, 2, 3, 6, 1, 5, 4, 9, 7},{6, 9, 7, 8, 2, 4, 1, 3, 5},{1, 4, 5, 3, 9, 7, 8, 2, 6},{5, 6, 8, 9, 3, 1, 2, 7, 4},{4, 3, 1, 5, 7, 2, 6, 8, 9},{9, 7, 2, 4, 8, 6, 3, 5, 1}},{{2, 8, 6, 1, 5, 9, 7, 4, 3},{3, 5, 4, 7, 6, 8, 9, 1, 2},{7, 1, 9, 2, 4, 3, 5, 6, 8},{8, 9, 3, 6, 1, 5, 4, 2, 7},{6, 2, 7, 8, 9, 4, 1, 3, 5},{1, 4, 5, 3, 2, 7, 8, 9, 6},{5, 6, 8, 9, 3, 1, 2, 7, 4},{4, 3, 1, 5, 7, 2, 6, 8, 9},{9, 7, 2, 4, 8, 6, 3, 5, 1}},{{2, 8, 6, 1, 5, 9, 7, 4, 3},{3, 5, 4, 7, 6, 8, 9, 1, 2},{7, 1, 9, 2, 4, 3, 5, 6, 8},{8, 9, 3, 6, 1, 5, 4, 2, 7},{6, 2, 7, 8, 9, 4, 1, 3, 5},{1, 4, 5, 3, 7, 2, 8, 9, 6},{5, 6, 8, 9, 3, 1, 2, 7, 4},{4, 3, 1, 5, 2, 7, 6, 8, 9},{9, 7, 2, 4, 8, 6, 3, 5, 1}},{{2, 8, 6, 1, 5, 9, 7, 4, 3},{3, 5, 7, 6, 4, 8, 2, 1, 9},{4, 1, 9, 7, 2, 3, 5, 6, 8},{8, 2, 1, 9, 6, 5, 4, 3, 7},{6, 9, 3, 8, 7, 4, 1, 2, 5},{7, 4, 5, 3, 1, 2, 8, 9, 6},{5, 6, 8, 2, 3, 1, 9, 7, 4},{1, 3, 4, 5, 9, 7, 6, 8, 2},{9, 7, 2, 4, 8, 6, 3, 5, 1}},{{2, 8, 6, 1, 5, 9, 7, 4, 3},{3, 5, 7, 6, 4, 8, 2, 1, 9},{4, 1, 9, 7, 3, 2, 5, 6, 8},{8, 2, 1, 9, 6, 5, 4, 3, 7},{6, 9, 3, 8, 7, 4, 1, 2, 5},{7, 4, 5, 3, 2, 1, 8, 9, 6},{5, 6, 8, 2, 1, 3, 9, 7, 4},{1, 3, 4, 5, 9, 7, 6, 8, 2},{9, 7, 2, 4, 8, 6, 3, 5, 1}},{{2, 8, 6, 1, 5, 9, 7, 4, 3},{3, 5, 7, 6, 4, 8, 9, 1, 2},{4, 1, 9, 7, 3, 2, 5, 6, 8},{8, 9, 1, 2, 6, 5, 4, 3, 7},{6, 2, 3, 8, 7, 4, 1, 9, 5},{7, 4, 5, 3, 9, 1, 8, 2, 6},{5, 6, 8, 9, 1, 3, 2, 7, 4},{1, 3, 4, 5, 2, 7, 6, 8, 9},{9, 7, 2, 4, 8, 6, 3, 5, 1}},{{2, 8, 6, 1, 5, 9, 7, 4, 3},{4, 5, 7, 6, 3, 8, 2, 1, 9},{3, 1, 9, 7, 4, 2, 5, 6, 8},{8, 2, 1, 9, 6, 5, 4, 3, 7},{6, 9, 3, 8, 7, 4, 1, 2, 5},{7, 4, 5, 3, 2, 1, 8, 9, 6},{5, 6, 8, 2, 1, 3, 9, 7, 4},{1, 3, 4, 5, 9, 7, 6, 8, 2},{9, 7, 2, 4, 8, 6, 3, 5, 1}},{{2, 8, 6, 1, 5, 9, 7, 4, 3},{4, 5, 7, 6, 3, 8, 9, 1, 2},{3, 1, 9, 7, 4, 2, 5, 6, 8},{8, 9, 1, 2, 6, 5, 4, 3, 7},{6, 2, 3, 8, 7, 4, 1, 9, 5},{7, 4, 5, 3, 9, 1, 8, 2, 6},{5, 6, 8, 9, 1, 3, 2, 7, 4},{1, 3, 4, 5, 2, 7, 6, 8, 9},{9, 7, 2, 4, 8, 6, 3, 5, 1}}, }},

	/* 13 */	{2, TestType::NoSolution, {1, 0, 0, 0, 0, 0, 2, 0, 0, 2, 4, 0, 0, 0, 0, 3}},
	/* 14 */	{3, TestType::NoSolution, {0, 0, 0, 0, 4, 9, 0, 6, 5, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, 0, 5, 0, 6, 0, 0, 9, 3, 3, 4, 0, 0, 5, 0, 1, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 8, 0, 2, 0, 0, 5, 9, 9, 5, 0, 0, 1, 0, 6, 0, 0, 7, 0, 0, 6, 0, 0, 0, 0, 0, 8, 2, 0, 3, 9, 0, 0, 0, 0}},
	/* 15 */	{4, TestType::NoSolution, {9, 0, 0, 2, 3, 4, 0, 0, 12, 0, 6, 0, 0, 0, 7, 0, 0, 0, 8, 0, 0, 0, 7, 0, 0, 3, 0, 0, 9, 10, 6, 11, 0, 12, 0, 0, 10, 0, 0, 1, 0, 13, 0, 11, 0, 0, 14, 0, 3, 0, 0, 15, 2, 0, 0, 14, 0, 0, 0, 9, 0, 0, 12, 0, 13, 0, 0, 0, 8, 0, 0, 10, 0, 12, 2, 0, 1, 15, 0, 0, 0, 11, 7, 6, 0, 0, 0, 16, 0, 0, 0, 15, 0, 0, 5, 13, 0, 0, 0, 10, 0, 5, 15, 0, 0, 4, 0, 8, 0, 0, 11, 0, 16, 0, 0, 5, 9, 12, 0, 0, 1, 0, 0, 0, 0, 0, 8, 0, 0, 2, 0, 0, 0, 0, 0, 13, 0, 0, 12, 5, 8, 0, 0, 3, 0, 13, 0, 0, 15, 0, 3, 0, 0, 14, 8, 0, 16, 0, 0, 0, 5, 8, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 13, 9, 15, 0, 0, 0, 12, 4, 0, 6, 16, 0, 13, 0, 0, 7, 0, 0, 0, 5, 0, 3, 0, 0, 12, 0, 0, 0, 6, 0, 0, 4, 11, 0, 0, 16, 0, 7, 0, 0, 16, 0, 5, 0, 14, 0, 0, 1, 0, 0, 2, 0, 11, 1, 15, 9, 0, 0, 13, 0, 0, 2, 0, 0, 0, 14, 0, 0, 0, 14, 0, 0, 0, 11, 0, 2, 0, 0, 13, 3, 5, 0, 0, 12}}
};

int main(int argc, char** argv)
{
	size_t i = 0;
	for (const auto& test : tests) {
		std::cout << "Test " << ++i << std::endl;
		//Create solver for specific size of sudoku
		SudokuSolver solver(test.size);
		//Set start board
		solver.SetBoard(test.startBoard);

#ifdef PRINT_SUDOKU
		solver.Print(test.size, solver.GetBoard());
#endif // PRINT_SUDOKU

		//Check if loaded board is correct
		bool boardOk = solver.CheckBoard();

		if (test.type == TestType::WrongBoard) {
			if (boardOk)
				std::cout << "Error, Board should be incorrect!" << std::endl;
			else
				std::cout << "OK, incorect board detected!" << std::endl;
			continue;
		}
#ifdef STAGE2
		else {
			//Try to solve a sudoku
			auto solutions = solver.Solve();

			if (test.type == TestType::NoSolution) {
				if (solutions.size() > 0)
					std::cout << "Error, solver found solution for board with no solution!" << std::endl;
				else
					std::cout << "OK, solver not found solution!" << std::endl;
				continue;
			}

			if (solutions.size() == 0) {
				std::cout << "Error, Solver did not find a solution!" << std::endl;
				continue;
			}

			if (solutions.size() != test.solutions.size())
			{
				std::cout << "Error, Solver did find only " << solutions.size() << " of " << test.solutions.size() << " solutions!" << std::endl;
				continue;
			}

			std::sort(solutions.begin(), solutions.end(), boardComp);
			int countOK = 0;
			for (unsigned i = 0; i < solutions.size(); i++) {
				
				if (!boardEquiv(solutions[i], test.solutions[i]))
					std::cout << "Error, diff " << i+1 << std::endl;
				else {
					std::cout << "Solution no " << i + 1 << " is OK!" << std::endl;
					countOK++;
				}
			}
			if (countOK == test.solutions.size())
				std::cout << "OK, found all correct solutions! ";
			else 
				std::cout << "Error, solver did not found all solutions! " << std::endl;

			std::cout << "(" << countOK << "/" << test.solutions.size() << ")" << std::endl;

#ifdef PRINT_SUDOKU
			for (const auto& s : solutions) {
				solver.Print(test.size, s);

				/*std::cout << "{";
				for (const auto& s2 : s) {
					std::cout << "{";
					for (const auto& s3 : s2)
						std::cout << s3 << ", ";
					std::cout << "},";
				}
				std::cout << "},";
				std::cout << std::endl;*/

			}
#endif // PRINT_SUDOKU
		}
#endif // STAGE2

	}

	return EXIT_SUCCESS;
}