#pragma once

#include <vector>

typedef std::vector<std::vector<int>> Board;

class SudokuSolver
{
public:
	SudokuSolver(size_t N);

	void SetBoard(const std::vector<int> &board);
	const Board& GetBoard() const;
	bool CheckBoard();

	std::vector<Board> Solve();

	static void Print(size_t size, const Board &board);

private:
	size_t m_size;
	Board m_board;
	std::vector<std::vector<bool>> m_rows;
	std::vector<std::vector<bool>> m_cols;
	std::vector<std::vector<std::vector<bool>>> m_regions;

	void recSolve(size_t index, std::vector<Board> *solutions);
};

