#include "GameSolver.h"

#include <iostream>
#include <iomanip>
#include <algorithm>
#include <iterator> 
#include <numeric>

GameSolver::GameSolver(uint _column_count, uint _row_count, const std::vector<uint>& _column_sum, const std::vector<uint>& _row_sum) :
	m_column_count(_column_count), m_row_count(_row_count), m_column_sum(_column_sum), m_row_sum(_row_sum), m_sum(0), m_temp_sum(0)
{
	m_board.resize(m_row_count);

	for (uint index_row = 0; index_row < m_row_count; index_row++)
	{
		std::fill_n(std::back_inserter(m_board[index_row]), m_column_count, 0);
	}

	std::fill_n(std::back_inserter(m_temp_column_sum), m_column_count, 0);
	std::fill_n(std::back_inserter(m_temp_row_sum), m_row_count, 0);
}

std::vector<Board> GameSolver::Solve()
{
	uint sum_col = std::accumulate(m_column_sum.begin(), m_column_sum.end(), 0);
	uint sum_row = std::accumulate(m_row_sum.begin(), m_row_sum.end(), 0);

	if (sum_col != sum_row) return std::vector<Board>();

	m_sum = sum_col;

	std::vector<Board> result;
	RecursiveSolve(0, &result);
	return result;
}

void GameSolver::Print(uint column_count, uint row_count, const std::vector<uint>& column_sum, const std::vector<uint>& row_sum, Board board)
{
	std::cout << "    " << char(179);

	for (uint index_column = 0; index_column < column_count; index_column++)
		std::cout << std::setw(4) << column_sum[index_column];

	std::cout << std::endl;

	std::cout << " " << char(196) << char(196) << char(196) << char(197);

	for (uint index_column = 0; index_column < column_count; index_column++)
		std::cout << char(196) << char(196) << char(196) << char(196);

	std::cout << std::endl;

	for (uint index_row = 0; index_row < row_count; index_row++)
	{
		std::cout << std::setw(4) << row_sum[index_row] << char(179);

		for (uint index_column = 0; index_column < column_count; index_column++)
			std::cout << std::setw(4) << board[index_row][index_column];

		std::cout << std::endl;
	}
}


void GameSolver::RecursiveSolve(uint index, std::vector<Board>* solutions)
{
	if (m_sum == m_temp_sum)
	{
		solutions->push_back(m_board);
		return;
	}

	uint size = m_column_count * m_row_count;

	for (; index < size; ++index)
	{
		if (m_sum - m_temp_sum > size - index)
			break;

		uint index_row = index / m_column_count;
		uint index_col = index % m_column_count;

		if (m_temp_column_sum[index_col] != m_column_sum[index_col] && m_temp_row_sum[index_row] != m_row_sum[index_row])
		{
			++m_temp_column_sum[index_col];
			++m_temp_row_sum[index_row];
			++m_temp_sum;
			m_board[index_row][index_col] = 1;

			RecursiveSolve(index + 1, solutions);

			--m_temp_column_sum[index_col];
			--m_temp_row_sum[index_row];
			--m_temp_sum;
			m_board[index_row][index_col] = 0;
		}
	}
}
