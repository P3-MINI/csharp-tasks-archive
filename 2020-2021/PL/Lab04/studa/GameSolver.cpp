#include "GameSolver.h"

#include <iostream>
#include <iomanip>

GameSolver::GameSolver(uint _column_count, uint _row_count, const std::vector<uint>& _column_sum, const std::vector<uint>& _row_sum)
{
	//TODO
}

std::vector<Board> GameSolver::Solve()
{
	//TODO
	return std::vector<Board>();
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
