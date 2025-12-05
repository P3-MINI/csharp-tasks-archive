#pragma once

#include <vector>

typedef std::vector<std::vector<int>> Board;
typedef unsigned int uint;

class GameSolver
{
public:
	GameSolver(uint _max_value, uint _column_count, uint _row_count, const std::vector<uint>& _column_sum, const std::vector<uint>& _row_sum);

	std::vector<Board> Solve();

	static void Print(uint column_count, uint row_count, const std::vector<uint>& column_sum, const std::vector<uint>& row_sum, Board board);

private:
	uint m_max_value;
	uint m_column_count;
	uint m_row_count;
	std::vector<uint> m_column_sum;
	std::vector<uint> m_row_sum;
	Board m_board;

	//Mo¿na dodaæ potrzebne sk³adowe oraz metody
	std::vector<uint> m_temp_column_sum;
	std::vector<uint> m_temp_row_sum;
	uint m_sum;
	uint m_temp_sum;

	void RecursiveSolve(uint index, std::vector<Board>* solutions);
};