using System;

namespace MineSweeperClasses
{
    public class Board
    {
        public int Size { get; private set; }
        public Cell[,] Cells { get; private set; }

        public enum GameStatus { Playing, Won, Lost }

        public Board(int size, float bombProbability)
        {
            Size = size;
            Cells = new Cell[size, size];
            Random rand = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cells[i, j] = new Cell();
                    if (rand.NextDouble() < bombProbability)
                    {
                        Cells[i, j].IsBomb = true;
                    }
                    else if (rand.NextDouble() < 0.05) // 5% chance for a reward
                    {
                        Cells[i, j].IsReward = true;
                    }
                }
            }

            CalculateNeighborBombs();
        }

        private void CalculateNeighborBombs()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!Cells[i, j].IsBomb)
                    {
                        int count = 0;

                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                int ni = i + x;
                                int nj = j + y;
                                if (ni >= 0 && ni < Size && nj >= 0 && nj < Size && Cells[ni, nj].IsBomb)
                                {
                                    count++;
                                }
                            }
                        }

                        Cells[i, j].NumberOfBombNeighbors = count;
                    }
                }
            }
        }

        // FloodFill for revealing "blocks" of empty cells
        public void FloodFill(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) return; // Out of bounds
            if (Cells[row, col].IsVisited || Cells[row, col].IsBomb || Cells[row, col].IsFlagged) return;

            // Mark the cell as visited
            Cells[row, col].IsVisited = true;

            // Stop recursion if the cell has neighboring bombs
            if (Cells[row, col].NumberOfBombNeighbors > 0) return;

            // Recursively visit neighboring cells
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0) // Avoid the current cell
                    {
                        FloodFill(row + x, col + y);
                    }
                }
            }
        }

        // UseSpecialBonus for revealing neighboring cells
        public void UseSpecialBonus(int row, int col)
        {
            if (!Cells[row, col].IsReward || Cells[row, col].IsVisited) return;

            // Reveal neighboring non-bomb cells
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int ni = row + x;
                    int nj = col + y;

                    if (ni >= 0 && ni < Size && nj >= 0 && nj < Size && !Cells[ni, nj].IsBomb)
                    {
                        FloodFill(ni, nj);
                    }
                }
            }

            // Mark the reward as used
            Cells[row, col].IsReward = false;
        }

        //  Determine the game state (playing, won, or lost)
        public GameStatus DetermineGameState()
        {
            bool allNonBombCellsVisited = true;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!Cells[i, j].IsVisited && !Cells[i, j].IsBomb)
                    {
                        allNonBombCellsVisited = false;
                    }

                    if (Cells[i, j].IsVisited && Cells[i, j].IsBomb)
                    {
                        return GameStatus.Lost;
                    }
                }
            }

            if (allNonBombCellsVisited)
            {
                return GameStatus.Won;
            }

            return GameStatus.Playing;
        }
    }
}
