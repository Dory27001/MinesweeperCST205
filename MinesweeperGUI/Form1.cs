using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MineSweeperClasses;

namespace MinesweeperGUI
{
    public partial class Form1 : Form
    {
        private Board board;
        private Button[,] buttons;
        private int gridSize;
        private float difficulty;
        private int secondsElapsed;
        private int score;
        private System.Windows.Forms.Timer gameTimer;
        private Label timeLabel;
        private Label scoreLabel;
        private Dictionary<string, Image> tileImages;
        private Image[] numberTiles;

        private void LoadImages()
        {
            tileImages = new Dictionary<string, Image>
            {
                { "hidden", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\hidden.png") },
                { "flag", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\flag.png") },
                { "bomb", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\bomb.png") },
                { "0", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\empty.png") },
                { "1", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\one.png") },
                { "2", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\two.png") },
                { "3", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\three.png") },
                { "4", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\four.png") },
                { "5", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\five.png") },
                { "6", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\six.png") },
                { "7", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\seven.png") },
                { "8", Image.FromFile(@"C:\Users\doryr_no8qyxh\source\repos\MinesweeperGUI\bin\Debug\net8.0-windows\eight.png") }
            };
        }
        public Form1(int size, float bombProbability)
        {
            gridSize = size;
            difficulty = bombProbability;
            board = new Board(gridSize, difficulty);

            LoadImages(); // Ensure this is called before InitializeComponent
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            this.ClientSize = new Size(gridSize * 30, gridSize * 30 + 50);
            buttons = new Button[gridSize, gridSize];

            // Load numbered tiles (1-8)
            numberTiles = new Image[9];
            for (int i = 1; i <= 8; i++)
            {
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\one.png");
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\two.png");
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\three.png");
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\four.png");
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\five.png");
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\six.png");
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\seven.png");
                numberTiles[i] = Image.FromFile($"C:\\Users\\doryr_no8qyxh\\source\\repos\\MinesweeperGUI\\bin\\Debug\\net8.0-windows\\eight.png");

            }
            this.ClientSize = new Size(gridSize * 60, gridSize * 60 + 100);
            buttons = new Button[gridSize, gridSize];

            InitializeUI();
            gameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            gameTimer.Tick += GameTimer_Tick;
            secondsElapsed = 0;
            score = 0;
            gameTimer.Start();

            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    Button btn = new Button
                    {
                        Width = 60,
                        Height = 60,
                        Location = new Point(c * 60, r * 60),
                        Tag = new Point(r, c),
                        BackgroundImage = tileImages["hidden"],
                        BackgroundImageLayout = ImageLayout.Stretch
                    };
                    btn.MouseDown += OnCellClick;
                    buttons[r, c] = btn;
                    Controls.Add(btn);
                }
            }

            Button restartButton = new Button
            {
                Text = "Restart",
                Location = new Point(20, gridSize * 60 + 40),
                Width = 80,
                Height = 30
            };
            restartButton.Click += RestartGame;
            Controls.Add(restartButton);
        }

        private void InitializeUI()
        {
            timeLabel = new Label
            {
                Text = "Time: 00:00",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Navy,
                AutoSize = true,
                Location = new Point(10, gridSize * 60 + 20)
            };
            Controls.Add(timeLabel);

            scoreLabel = new Label
            {
                Text = "Score: 0",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Navy,
                AutoSize = true,
                Location = new Point(150, gridSize * 60 + 20)
            };
            Controls.Add(scoreLabel);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            TimeSpan timeSpan = TimeSpan.FromSeconds(secondsElapsed);
            timeLabel.Text = $"Time: {timeSpan:mm\\:ss}";
        }

        private void OnCellClick(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Point pos = (Point)btn.Tag;
            int r = pos.X, c = pos.Y;

            if (e.Button == MouseButtons.Right)
            {
                board.Cells[r, c].IsFlagged = !board.Cells[r, c].IsFlagged;
                btn.BackgroundImage = board.Cells[r, c].IsFlagged ? tileImages["flag"] : tileImages["hidden"];
                return;
            }

            if (board.Cells[r, c].IsBomb)
            {
                gameTimer.Stop();
                MessageBox.Show($"You hit a bomb! Game Over!\nTime: {timeLabel.Text}\nScore: {score}", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RevealAllCells();
                return;
            }

            if (!board.Cells[r, c].IsVisited)
            {
                board.FloodFill(r, c);
                score += 10;
                scoreLabel.Text = $"Score: {score}";
            }

            UpdateBoard();

            if (board.DetermineGameState() == Board.GameStatus.Won)
            {
                gameTimer.Stop();
                MessageBox.Show($"Congratulations! You won!\nTime: {timeLabel.Text}\nScore: {score}", "Victory!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RevealAllCells();
            }
        }

        private void UpdateBoard()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    var cell = board.Cells[r, c];
                    if (cell.IsFlagged)
                    {
                        buttons[r, c].BackgroundImage = tileImages["flag"];
                        continue;
                    }
                    if (cell.IsVisited)
                    {
                        buttons[r, c].Enabled = false;
                        buttons[r, c].BackgroundImage = tileImages[cell.IsBomb ? "bomb" : cell.NumberOfBombNeighbors.ToString()];
                        buttons[r, c].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
        }

        private void RevealAllCells()
        {
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    var cell = board.Cells[r, c];
                    buttons[r, c].Enabled = false;
                    buttons[r, c].BackgroundImage = tileImages[cell.IsBomb ? "bomb" : cell.NumberOfBombNeighbors.ToString()];
                    buttons[r, c].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        private void RestartGame(object sender, EventArgs e)
        {
            gameTimer.Stop();
            secondsElapsed = 0;
            score = 0;
            timeLabel.Text = "Time: 00:00";
            scoreLabel.Text = "Score: 0";
            Controls.Clear();
            board = new Board(gridSize, difficulty);
            InitializeGame();
        }
    }
}
