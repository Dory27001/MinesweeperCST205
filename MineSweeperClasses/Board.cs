namespace MineSweeperClasses
{

    public class Board
    {
        // The size of the board (square)
        public int Size { get; private set; }

        // The 2D array of cells that make up the board
        public Cell[,] Cells { get; private set; }

        // The possible game statuses
        public enum GameStatus { Playing, Won, Lost }

        // Constructor that initializes the board
        public Board(int size, float bombProbability)
        {
            // Set the board size
            Size = size;

            // Create the 2D array of cells
            Cells = new Cell[size, size];

            // Create a random number generator
            Random rand = new Random();

            // Populate the board with cells and randomly place bombs
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Create a new cell and add it to the board
                    Cells[i, j] = new Cell(i, j);

                    // Randomly set the cell as a bomb based on the provided probability
                    if (rand.NextDouble() < bombProbability)
                        Cells[i, j].IsBomb = true;
                }
            }

            // Calculate the number of neighboring bombs for each cell
            CalculateNeighborBombs();
        }

        // Private method to calculate the number of neighboring bombs for each cell
        private void CalculateNeighborBombs()
        {
            // Iterate through each cell on the board
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // Skip cells that are bombs
                    if (!Cells[i, j].IsBomb)
                    {
                        int count = 0;

                        // Check the 8 neighboring cells for bombs
                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                int ni = i + x, nj = j + y;

                                // Ensure the neighboring cell is within the board bounds
                                if (ni >= 0 && ni < Size && nj >= 0 && nj < Size && Cells[ni, nj].IsBomb)
                                    count++;
                            }
                        }

                        // Set the number of neighboring bombs for the current cell
                        Cells[i, j].NumberOfBombNeighbors = count;
                    }
                }
            }
        }

        // Method to reveal all non-bomb cells around the specified cell
        public void UseSpecialBonus(int row, int col)
        {
            // Iterate through the 8 neighboring cells
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int ni = row + x, nj = col + y;

                    // Ensure the neighboring cell is within the board bounds and is not a bomb
                    if (ni >= 0 && ni < Size && nj >= 0 && nj < Size && !Cells[ni, nj].IsBomb)
                        Cells[ni, nj].IsVisited = true;
                }
            }
        }

        // Method to reveal a cell and recursively reveal neighboring cells
        public void RevealCell(int row, int col)
        {
            if (Cells[row, col].IsBomb)
            {
                // Player hit a bomb, game over
                return;
            }

            if (!Cells[row, col].IsVisited)
            {
                Cells[row, col].IsVisited = true;

                if (Cells[row, col].NumberOfBombNeighbors == 0)
                {
                    // Recursively reveal all neighboring cells
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int newRow = row + i;
                            int newCol = col + j;

                            if (newRow >= 0 && newRow < Size && newCol >= 0 && newCol < Size && !Cells[newRow, newCol].IsBomb)
                            {
                                RevealCell(newRow, newCol);
                            }
                        }
                    }
                }
            }
        }

        // Method to determine the current game state
        public GameStatus DetermineGameState()
        {
            // Iterate through all the cells on the board
            foreach (var cell in Cells)
            {
                // If there are any unvisited non-bomb cells, the game is still in progress
                if (!cell.IsVisited && !cell.IsBomb)
                    return GameStatus.Playing;
            }

            // If all non-bomb cells have been visited, the player has won the game
            return GameStatus.Won;
        }
    }
}
