namespace MinesweeperApp
{
    partial class Minesweeper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            minesweeperControl1 = new MinesweeperControl();
            SuspendLayout();
            // 
            // minesweeperControl1
            // 
            minesweeperControl1.Dock = DockStyle.Fill;
            minesweeperControl1.Location = new Point(0, 0);
            minesweeperControl1.Name = "minesweeperControl1";
            minesweeperControl1.Size = new Size(800, 450);
            minesweeperControl1.TabIndex = 0;
            // 
            // Minesweeper
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(minesweeperControl1);
            Name = "Minesweeper";
            Text = "Minesweeper";
            ResumeLayout(false);
        }

        #endregion

        private MinesweeperControl minesweeperControl1;
    }
}