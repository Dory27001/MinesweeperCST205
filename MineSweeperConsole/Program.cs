using System;
using MineSweeperClasses;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Minesweeper!");

        int boardSize = SelectBoardSize();
        float difficulty = 0.15f; // 15% bomb chance

        Board board = new Board(boardSize, difficulty);
        bool victory = false;
        bool death = false;

        while (!victory && !death)
        {
            PrintBoard(board);

            Console.Write("Enter row and column (e.g., 3 4): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
            {
                Console.WriteLine("Invalid input. Enter row and column numbers.");
                continue;
            }

            if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
            {
                Console.WriteLine("Invalid input. Row and column out of range.");
                continue;
            }

            Console.Write("Choose an action (1: Flag, 2: Visit, 3: Use Reward): ");
            string actionInput = Console.ReadLine();

            // **Hidden Feature: Instantly Win**
            if (actionInput == "777")
            {
                SolveGame(board);
                victory = true;
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine("* CHEAT ACTIVATED: Instant Win! *");
                break;
            }

            if (actionInput == "666")
            {
                SolveGame(board);
                death = true;
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine("* CHEAT ACTIVATED: Instant Loss! *");
                break;
            }

            if (!int.TryParse(actionInput, out int action) || action < 1 || action > 3)
            {
                Console.WriteLine("Invalid action. Please enter 1, 2, or 3.");
                continue;
            }

            switch (action)
            {
                case 1: // Flag
                    board.Cells[row, col].IsFlagged = !board.Cells[row, col].IsFlagged;
                    break;

                case 2: // Visit
                    if (board.Cells[row, col].IsBomb)
                    {
                        death = true;
                    }
                    else
                    {
                        board.FloodFill(row, col); // 🚀 FloodFill added here
                    }
                    break;

                case 3: // Use Reward
                    if (board.Cells[row, col].IsReward)
                    {
                        board.UseSpecialBonus(row, col);
                        Console.WriteLine("You used a reward! Safe cells revealed.");
                    }
                    else
                    {
                        Console.WriteLine("No reward available on this cell.");
                    }
                    break;
            }

            // Check game state
            if (board.DetermineGameState() == Board.GameStatus.Lost)
            {
                death = true;
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine("BOOM! You hit a bomb! Game over.");
            }
            else if (board.DetermineGameState() == Board.GameStatus.Won)
            {
                victory = true;
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine("Congratulations! You won!");
            }
        }
    }

    static int SelectBoardSize()
    {
        Console.WriteLine("Select Difficulty:");
        Console.WriteLine("1. Easy (10x10)");
        Console.WriteLine("2. Normal (12x12)");
        Console.WriteLine("3. Hard (20x20)");
        Console.WriteLine("4. Insane (30x30)");
        Console.Write("Enter choice (1-4): ");

        while (true)
        {
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": return 10;
                case "2": return 12;
                case "3": return 20;
                case "4": return 30;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, 3, or 4.");
                    break;
            }
        }
    }

    static void SolveGame(Board board)
    {
        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                if (board.Cells[i, j].IsBomb)
                    board.Cells[i, j].IsFlagged = true;
                else
                    board.Cells[i, j].IsVisited = true;
            }
        }
    }

    static void PrintBoard(Board board)
    {
        Console.Clear();
        Console.WriteLine("Minesweeper Board:");

        Console.Write("   ");
        for (int i = 0; i < board.Size; i++) Console.Write($"{i,2} ");
        Console.WriteLine();

        Console.WriteLine("   +" + new string('-', board.Size * 3) + "+");

        for (int i = 0; i < board.Size; i++)
        {
            Console.Write($"{i,2} |");

            for (int j = 0; j < board.Size; j++)
            {
                Cell cell = board.Cells[i, j];

                if (cell.IsFlagged)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" F ");
                }
                else if (cell.IsVisited)
                {
                    if (cell.IsBomb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" B ");
                    }
                    else if (cell.IsReward)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(" R ");
                    }
                    else
                    {
                        Console.Write($" {cell.NumberOfBombNeighbors} ");
                    }
                }
                else
                {
                    Console.Write(" ? ");
                }
                Console.ResetColor();
                Console.Write("|");
            }
            Console.WriteLine("\n   +" + new string('-', board.Size * 3) + "+");
        }
    }
}
