using System;
using MineSweeperClasses;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Minesweeper!");

        int boardSize = 10;      // Default board size
        float difficulty = 0.15f; // 15% of cells contain bombs

        Board board = new Board(boardSize, difficulty);

        bool victory = false; // player wins
        bool death = false; // player loses

        while (!victory && !death)
        {
            PrintBoard(board);

            // Prompt for row and column number
            Console.WriteLine("\nEnter the row number:");
            string inputRow = Console.ReadLine();
            int row;
            if (!int.TryParse(inputRow, out row) || row < 0 || row >= boardSize)
            {
                Console.WriteLine("Invalid row number. Try again.");
                continue;
            }

            Console.WriteLine("Enter the column number:");
            string inputCol = Console.ReadLine();
            int col;
            if (!int.TryParse(inputCol, out col) || col < 0 || col >= boardSize)
            {
                Console.WriteLine("Invalid column number. Try again.");
                continue;
            }

            // Prompt for action choice (1: Flag, 2: Visit, 3: Use Reward)
            Console.WriteLine("Enter 1 to flag the cell, 2 to visit the cell, 3 to use a reward (bomb detector):");
            string actionInput = Console.ReadLine();

            if (!int.TryParse(actionInput, out int action) || action < 1 || action > 3)
            {
                Console.WriteLine("Invalid action. Please enter 1, 2, or 3.");
                continue;
            }

            switch (action)
            {
                case 1: // Flag
                    board.Cells[row, col].IsFlagged = !board.Cells[row, col].IsFlagged;
                    Console.WriteLine($"Cell at ({row}, {col}) has been {(board.Cells[row, col].IsFlagged ? "flagged" : "unflagged")}.");
                    break;

                case 2: // Visit
                    board.RevealCell(row, col);
                    break;

                case 3: // Use Reward
                    board.UseSpecialBonus(row, col); // Reward use automatically checks if the game is won
                    break;
            }

            // Check if game is won or lost
            Board.GameStatus gameState = board.DetermineGameState();
            if (gameState == Board.GameStatus.Lost)
            {
                death = true;
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine(":( You hit a bomb! Game over.");
            }
            else if (gameState == Board.GameStatus.Won)
            {
                victory = true;
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine("!! Congratulations! You won!");
            }
            else
            {
                Console.WriteLine("Game in progress...");
            }
        }

        // Ask for row number again at the bottom after the game ends
        Console.WriteLine("\nEnter the row number (or any input to quit):");
        string finalRow = Console.ReadLine();
        Console.WriteLine($"You entered row number: {finalRow}");
    }

    static void PrintBoard(Board board)
    {
        Console.Clear();
        Console.WriteLine("Minesweeper Board:");

        // Show just the current row with cell values

        for (int i = 0; i < board.Size; i++)
        {
            Console.Write(i + " |"); // Row index on the left

            for (int j = 0; j < board.Size; j++)
            {
                Cell cell = board.Cells[i, j];

                if (cell.IsFlagged)
                {
                    Console.Write(" F |"); // Flagged cell
                }
                else if (cell.IsVisited)
                {
                    if (cell.IsBomb)
                        Console.Write(" B |");  // Display bomb
                    else if (cell.IsReward)
                        Console.Write(" R |");  // Display reward
                    else
                    {
                        // Change color based on the number of neighboring bombs
                        Console.ForegroundColor = cell.NumberOfBombNeighbors switch
                        {
                            1 => ConsoleColor.Green,
                            2 => ConsoleColor.Blue,
                            3 => ConsoleColor.Red,
                            4 => ConsoleColor.Cyan,
                            5 => ConsoleColor.Magenta,
                            6 => ConsoleColor.Yellow,
                            7 => ConsoleColor.Gray,
                            8 => ConsoleColor.White,
                            _ => ConsoleColor.White
                        };

                        Console.Write($" {cell.NumberOfBombNeighbors} |");
                        Console.ResetColor(); // Reset color to default
                    }
                }
                else
                {
                    Console.Write(" ? |");  // Unvisited cell
                }
            }
            Console.WriteLine("\n   +---+---+---+---+---+---+---+---+---+---+");
        }
        Console.WriteLine("     0   1   2   3   4   5   6   7   8   9 ");
    }
}