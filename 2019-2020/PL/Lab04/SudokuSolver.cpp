#include "SudokuSolver.h"

#include <iostream>

SudokuSolver::SudokuSolver(size_t N)
	: m_size(N)
{
	size_t size2 = m_size * m_size;
	m_board = Board(size2);
	for (size_t i = 0; i < size2; i++) {
		m_board[i] = std::vector<int>(size2);
	}
}

void SudokuSolver::SetBoard(const std::vector<int>& board)
{
	size_t size2 = m_size * m_size;
	if (board.size() != size2 * size2)
		throw std::exception("Wrong board size!");

	for (size_t i = 0; i < size2; i++) {
		for (size_t j = 0; j < size2; j++) {
			this->m_board[i][j] = board[i * size2 + j];
		}
	}

	m_rows = std::vector<std::vector<bool>>(size2);
	m_cols = std::vector<std::vector<bool>>(size2);
	for (size_t i = 0; i < size2; i++)
	{
		m_rows[i] = std::vector<bool>(size2);
		m_cols[i] = std::vector<bool>(size2);
	}

	m_regions = std::vector<std::vector<std::vector<bool>>>(m_size);
	for (size_t i = 0; i < m_size; ++i)
	{
		m_regions[i] = std::vector<std::vector<bool>>(m_size);
		for (size_t j = 0; j < m_size; ++j)
			m_regions[i][j] = std::vector<bool>(size2);
	}
}

const Board & SudokuSolver::GetBoard() const
{
	return m_board;
}

bool SudokuSolver::CheckBoard()
{
	size_t size2 = m_size * m_size;
	for (size_t i = 0; i < size2; ++i)
		for (size_t j = 0; j < size2; ++j)
		{
			auto v = m_board[i][j];
			if (v > (int)size2)
				return false;
			if (v > 0)
			{
				v = v - 1;
				int a = i / m_size;
				int b = j / m_size;
				if (m_rows[i][v] || m_cols[j][v] || m_regions[a][b][v])
					return false;
				m_rows[i][v] = m_cols[j][v] = m_regions[a][b][v] = true;
			}
		}
	return true;
}

std::vector<Board> SudokuSolver::Solve()
{
	std::vector<Board> solutions;
	recSolve(0, &solutions);
	return std::move(solutions);
}

void SudokuSolver::recSolve(size_t index, std::vector<Board> *solutions)
{
	size_t size2 = m_size * m_size;

	while (index < size2*size2 && m_board[index / size2][index % size2] != 0) index++;

	if (index >= size2 * size2)
	{
		solutions->push_back(m_board);
		return;
	}

	size_t i = index / size2;
	size_t j = index % size2;

	//check all possible values for cell
	for (size_t v = 1; v <= size2; v++)
	{
		if (!m_rows[i][v - 1] && !m_cols[j][v - 1] && !m_regions[i / m_size][j / m_size][v - 1])
		{
			m_board[i][j] = v;
			m_rows[i][v - 1] = m_cols[j][v - 1] = m_regions[i / m_size][j / m_size][v - 1] = true;
			recSolve(index + 1, solutions);
			m_board[i][j] = 0;
			m_rows[i][v - 1] = m_cols[j][v - 1] = m_regions[i / m_size][j / m_size][v - 1] = false;
		}
	}
}

void SudokuSolver::Print(size_t size, const Board &board)
{
	size_t size2 = size * size;
	for (size_t i = 0; i < size2; i++) {
		for (size_t j = 0; j < size2; j++) {
			auto ch = board[i][j];
			if (ch < 1 || ch > (int)(size2))
				std::cout << "   ";			//Empty cell
			else if (ch < 10)
				std::cout << ' ' << ch << ' ';
			else
				std::cout << ' ' << ch;

			if (j % size == size - 1 && j + 1 != size2)
				std::cout << char(179);		//'│';
		}

		if (i % size == size -1 && i+1 < size2)
		{
			std::cout << std::endl;
			for (size_t j = 0; j < size2; j++) {
				std::cout << char(196) << char(196) << char(196);	//"---";
				if (j % size == size -1 && j + 1 != size2)
					std::cout << char(197);			// '+';
			}
		}
		std::cout << std::endl;
	}
	std::cout << std::endl;
}