namespace MinesweeperGUI
{
    partial class Form1
    {
        // Automatically generated designer components for the Windows Form
        private System.ComponentModel.IContainer components = null;

      
        /// Disposes of the resources used by the form.
        /// <param name="disposing">true if managed</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose(); // Releases resources if they exist
            }
            base.Dispose(disposing); // Calls the base Dispose method
        }

        #region Windows Form Designer generated code

        /// Required method for Designer support. 
        /// This method initializes and sets up the form components.

        private void InitializeComponent()
        {
            // Labels for displaying game information
            label1 = new Label(); // Label for displaying "Start Time:"
            label3 = new Label(); // Label for displaying "Score:"
            button1 = new Button(); // Restart button
            label4 = new Label(); // Label to hold a dynamic value (e.g., score)
            label2 = new Label(); // Label for displaying "Time:"

            SuspendLayout(); // Temporarily suspends layout logic to optimize performance

            // 
            // label1 - Displays "Start Time:"
            // 
            label1.AutoSize = true;
            label1.Location = new Point(765, 47);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 1;
            label1.Text = "Start Time:";

            // 
            // label3 - Displays "Score:"
            // 
            label3.AutoSize = true;
            label3.Location = new Point(765, 121);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 3;
            label3.Text = "Score:";

            // 
            // button1 - Restart button to reset the game
            // 
            button1.Location = new Point(270, 600);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "Restart";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click; // Event handler for button click

            // 
            // label4 - Dynamic label for displaying updated score or game info
            // 
            label4.AutoSize = true;
            label4.Location = new Point(765, 145);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 4;
            label4.Text = "label4"; // Placeholder text

            // 
            // label2 - Displays "Time:"
            // 
            label2.AutoSize = true;
            label2.Location = new Point(765, 71);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 2;
            label2.Text = "Time:";

            // 
            // Form1 - Main Minesweeper game window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(921, 849);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Minesweeper";
            ResumeLayout(false);
            PerformLayout(); // Resumes layout logic after all components are added
        }

        /// Event handler for the Restart button click event.
        /// This should contain logic to reset the game.

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException(); // Placeholder for restart functionality
        }

        #endregion

        // Private fields for labels and button
        private Label label1; // Label for Start Time
        private Label label2; // Label for Time
        private Label label3; // Label for Score
        private Label label4; // Label for dynamic game information
        private Button button1; // Restart button
    }
}
