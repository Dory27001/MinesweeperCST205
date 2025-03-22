
namespace MinesweeperGUI
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any C:\Users\james\source\repos\noahrose82\C-II\MineSweeperClasses\minesweeper-tiles being used.
        /// </summary>
        /// <param name="disposing">true if managed C:\Users\james\source\repos\noahrose82\C-II\MineSweeperClasses\minesweeper-tiles should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            trackBar2 = new TrackBar();
            trackBar1 = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 142);
            label3.Name = "label3";
            label3.Size = new Size(82, 15);
            label3.TabIndex = 11;
            label3.Text = "Percent Bomb";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 70);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 10;
            label2.Text = "Size";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-2, 32);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 9;
            label1.Text = "Play Minesweeper";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(118, 238);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "Play!";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(9, 160);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(290, 45);
            trackBar2.TabIndex = 7;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(3, 85);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(290, 45);
            trackBar1.TabIndex = 6;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(308, 298);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(trackBar2);
            Controls.Add(trackBar1);
            Name = "Form2";
            Text = "Start a new Game";
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label3;
        private Label label2;
        private Label label1;
        private Button button1;
        private TrackBar trackBar2;
        private TrackBar trackBar1;
    }
}