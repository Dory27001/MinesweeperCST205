
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperClasses
{
    public class Cell
    {
        // The row index of the cell on the board
        public int Row { get; set; }

        // The column index of the cell on the board
        public int Column { get; set; }

        // Indicates whether the cell has been visited by the player
        public bool IsVisited { get; set; } = false;

        // Indicates whether the cell contains a bomb
        public bool IsBomb { get; set; } = false;

        // Indicates whether the cell has been flagged by the player
        public bool IsFlagged { get; set; } = false;

        // Indicates whether the cell contains a reward (added for reward feature)
        public bool IsReward { get; set; } = false;

        // The number of neighboring cells that contain bombs
        public int NumberOfBombNeighbors { get; set; } = 0;

        // Constructor that sets the row and column indices of the cell
        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}



