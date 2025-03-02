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

        while (board.DetermineGameState() == Board.GameStatus.InProgress)
        {
            DisplayBoard(board);
            Console.Write("Enter row and column (e.g., 3 4): ");
            string input = Console.ReadLine();

            if (ProcessInput(input, board))
            {
                if (board.DetermineGameState() == Board.GameStatus.Lost)
                {
                    Console.Clear();
                    DisplayBoard(board, revealAll: true);
                    Console.WriteLine("💥 You hit a bomb! Game over.");
                    break;
                }
                else if (board.DetermineGameState() == Board.GameStatus.Won)
                {
                    Console.Clear();
                    DisplayBoard(board, revealAll: true);
                    Console.WriteLine("🎉 Congratulations! You won!");
                    break;
                }
            }
        }
    }

    static void DisplayBoard(Board board, bool revealAll = false)
    {
        Console.Clear();
        Console.WriteLine("Minesweeper Board:");

        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                Cell cell = board.Cells[i, j];

                if (revealAll || cell.IsVisited)
                {
                    if (cell.IsBomb)
                        Console.Write(" B ");
                    else
                        Console.Write(cell.NumberOfBombNeighbors + " ");
                }
                else
                {
                    Console.Write("[] ");
                }
            }
            Console.WriteLine();
        }
    }

    static bool ProcessInput(string input, Board board)
    {
        string[] parts = input.Split(' ');
        if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
        {
            Console.WriteLine("Invalid input. Enter row and column numbers.");
            return false;
        }

        if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
        {
            Console.WriteLine("Out of bounds! Try again.");
            return false;
        }

        if (board.Cells[row, col].IsVisited)
        {
            Console.WriteLine("Already revealed! Try another cell.");
            return false;
        }

        board.Cells[row, col].IsVisited = true;

        return true;
    }
}