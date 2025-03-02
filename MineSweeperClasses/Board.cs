namespace MineSweeperClasses
{
    public class Board
    {
        public int Size { get; set; }
        public float Difficulty { get; set; }
        public Cell[,] Cells { get; set; }
        public int RewardsRemaining { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public enum GameStatus { InProgress, Won, Lost }

        private Random random = new Random();

        public Board(int size, float difficulty)
        {
            Size = size;
            Difficulty = difficulty;
            Cells = new Cell[size, size];


            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cells[i, j] = new Cell();  // Ensure every cell is instantiated
                }
            }

            RewardsRemaining = 0;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            SetupBombs();
            SetupRewards();
            CalculateNumberOfBombNeighbors();
            StartTime = DateTime.Now;
        }

        // Used when player selects a cell and chooses to play the reward
        public void UseSpecialBonus() { }

        // Used after game is over to calculate final score
        public int DetermineFinalScore() { return 0; }

        // Helper function to determine if a cell is out of bounds
        private bool IsCellOnBoard(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        // Used during setup to calculate the number of bomb neighbors for each cell
        private void CalculateNumberOfBombNeighbors()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cells[i, j].NumberOfBombNeighbors = GetNumberOfBombNeighbors(i, j);
                }
            }
        }

        // Helper function to determine the number of bomb neighbors for a cell
        private int GetNumberOfBombNeighbors(int i, int j)
        {
            int count = 0;
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int d = 0; d < 8; d++)
            {
                int ni = i + dx[d];
                int nj = j + dy[d];

                if (IsCellOnBoard(ni, nj) && Cells[ni, nj].IsBomb)
                {
                    count++;
                }
            }
            return count;
        }

        // Used during setup to place bombs on the board
        private void SetupBombs()
        {
            int totalBombs = (int)(Size * Size * Difficulty);
            int placedBombs = 0;

            while (placedBombs < totalBombs)
            {
                int row = random.Next(Size);
                int col = random.Next(Size);

                if (!Cells[row, col].IsBomb)
                {
                    Cells[row, col].IsBomb = true;
                    placedBombs++;
                }
            }
        }

        // Used during setup to place rewards on the board
        private void SetupRewards()
        {
            int totalRewards = (int)(Size * Size * 0.05); // Example: 5% of cells get rewards
            int placedRewards = 0;

            while (placedRewards < totalRewards)
            {
                int row = random.Next(Size);
                int col = random.Next(Size);

                if (!Cells[row, col].IsBomb && !Cells[row, col].IsFlagged)
                {
                    RewardsRemaining++;
                    placedRewards++;
                }
            }
        }

        // Used every turn to determine the current game state
        public GameStatus DetermineGameState()
        {
            int unrevealedCells = 0;

            foreach (var cell in Cells)
            {
                if (!cell.IsVisited && !cell.IsBomb)
                {
                    unrevealedCells++;
                }
            }

            if (unrevealedCells == 0)
                return GameStatus.Won;

            foreach (var cell in Cells)
            {
                if (cell.IsVisited && cell.IsBomb)
                    return GameStatus.Lost;
            }

            return GameStatus.InProgress;
        }
    }
}