using System;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    public partial class Form2 : Form
    {
        public int GridSize { get; private set; } = 8;  // Default size
        public float Difficulty { get; private set; } = 0.15f; // Default difficulty

        public Form2()
        {
            InitializeComponent();
            trackBar1.Minimum = 5;
            trackBar1.Maximum = 15;
            trackBar1.Value = GridSize;

            trackBar2.Minimum = 1;
            trackBar2.Maximum = 30; // Scale to 0.01 - 0.30
            trackBar2.Value = (int)(Difficulty * 100);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            GridSize = trackBar1.Value;
            label1.Text = $"Grid Size: {GridSize}x{GridSize}";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Difficulty = trackBar2.Value / 100f;
            label2.Text = $"Difficulty: {Difficulty:P0}"; // Shows percentage
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(GridSize, Difficulty);
            gameForm.Show();
            this.Hide();
        }
    }
}