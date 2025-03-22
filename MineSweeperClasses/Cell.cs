namespace MineSweeperClasses
{
    public class Cell
    {
        public bool IsBomb { get; set; }
        public bool IsVisited { get; set; }
        public bool IsFlagged { get; set; }
        public int NumberOfBombNeighbors { get; set; }
        public bool IsReward { get; set; }
    }
}
