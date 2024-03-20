using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperApp
{
    public partial class MinesweeperControl : UserControl
    {
        enum GameStatusEnum { NotStarted, Playing, Loss, Winner }
        GameStatusEnum gamestatus = GameStatusEnum.NotStarted;
        List<Button> lstallbuttons;
        List<Button> bombButtons = new List<Button>();
        private List<Button> clickedButtons = new List<Button>();

        private Random random = new();


        public MinesweeperControl()
        {
            InitializeComponent();
            btnstart.Click += Btnstart_Click;
            DisplayGameStatus();

            lstallbuttons = new List<Button>
            {
                button1, button2, button3, button4, button5, button6, button7, button8, button9, button10,
                button11, button12, button13, button14, button15, button16, button17, button18, button19, button20,
                button21, button22, button23, button24, button25, button26, button27, button28, button29, button30,
                button31, button32, button33, button34, button35, button36, button37, button38, button39, button40,
                button41, button42, button43, button44, button45, button46, button47, button48, button49, button50,
                button51, button52, button53, button54, button55, button56, button57, button58, button59, button60,
                button61, button62, button63, button64, button65, button66, button67, button68, button69, button70,
                button71, button72, button73, button74, button75, button76, button77, button78, button79, button80,
                button81, button82, button83, button84, button85, button86, button87, button88, button89, button90,
                button91, button92, button93, button94, button95, button96, button97, button98, button99, button100
            };
            foreach (Button button in lstallbuttons)
            {
                button.MouseDown += Button_MouseDown;
            }


        }
        private void NumberofBombsAround(Button clickedButton)
        {
            int index = lstallbuttons.IndexOf(clickedButton);
            int row = index / 10;
            int col = index % 10;

            if (clickedButton.Text == "0")
            {
                int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
                int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

                for (int i = 0; i < 8; i++)
                {
                    int newRow = row + dx[i];
                    int newCol = col + dy[i];

                    if (newRow >= 0 && newRow < 10 && newCol >= 0 && newCol < 10)
                    {
                        int newIndex = newRow * 10 + newCol;
                        Button neighborButton = lstallbuttons[newIndex];

                        if (!clickedButtons.Contains(neighborButton) && neighborButton.Text == "")
                        {
                            NumberofBombsAround(neighborButton);
                        }
                    }
                }
            }
            else
            {
                int count = 0;
                int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
                int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

                for (int i = 0; i < 8; i++)
                {
                    int newRow = row + dx[i];
                    int newCol = col + dy[i];

                    if (newRow >= 0 && newRow < 10 && newCol >= 0 && newCol < 10)
                    {
                        int newIndex = newRow * 10 + newCol;
                        Button neighborButton = lstallbuttons[newIndex];

                        if ((bool)neighborButton.Tag)
                        {
                            count++;
                        }
                    }
                }

                clickedButton.Text = count.ToString();
            }

            if (CheckWinCondition())
            {
                gamestatus = GameStatusEnum.Winner;
                DisplayGameStatus();
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (gamestatus == GameStatusEnum.NotStarted)
            {
                return;
            }

            if (bombButtons.Contains(clickedButton))
            {
                LoserReveal();
                gamestatus = GameStatusEnum.Loss;
                DisplayGameStatus();
            }
            else
            {
                ClickCount(clickedButton);

                if (CheckWinCondition())
                {
                    gamestatus = GameStatusEnum.Winner;
                    DisplayGameStatus();
                }
            }
        }

        private void LoserReveal()
        {
            foreach (Button button in lstallbuttons)
            {
                if ((bool)button.Tag && button.Text == "")
                {
                    button.BackColor = Color.Red;
                    button.Font = new Font("Wingdings", 10);
                    button.Text = "M";
                }
            }
        }
        private bool CheckWinCondition()
        {
            foreach (Button button in lstallbuttons)
            {
                if (!(bool)button.Tag && button.Text == "")
                {
                    return false;
                }
            }
            return true; 
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (gamestatus != GameStatusEnum.Playing)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                if (clickedButton.BackColor == SystemColors.Control)
                {
                    clickedButton.BackColor = Color.Blue;
                    clickedButton.Font = new Font("Wingdings", 10);
                    clickedButton.Text = "O";
                }
                else
                {
                    clickedButton.BackColor = SystemColors.Control;
                    clickedButton.Font = null;
                    clickedButton.Text = "";
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (bombButtons.Contains(clickedButton))
                {
                    LoserReveal();
                    gamestatus = GameStatusEnum.Loss;
                    DisplayGameStatus();
                }
                else
                {
                    NumberofBombsAround(clickedButton);
                    ClickCount(clickedButton);
                }
            }
        }


        private void PlaceBombsRandomly()
        {
            bombButtons.Clear();

            foreach (Button button in lstallbuttons)
            {
                button.Tag = false;
                button.BackColor = SystemColors.Control;
                button.Text = "";
            }
            int bombCount = 0;
            while (bombCount < 10)
            {
                int index = random.Next(lstallbuttons.Count);
                Button button = lstallbuttons[index];
                if (!(bool)button.Tag) 
                {
                    button.Tag = true;
                    bombButtons.Add(button); 
                    bombCount++;
                }
            }
        }

        private void ClickCount(Button clickedButton)
        {
            if (clickedButton.Text != "0" && !clickedButtons.Contains(clickedButton))
            {
                int count = int.Parse(lblclickcount.Text);
                count++;
                lblclickcount.Text = count.ToString();

                clickedButtons.Add(clickedButton);
            }
        }


        private void ResetButtons()
        {
            foreach (Button button in lstallbuttons)
            {
                button.BackColor = SystemColors.Control;
                button.Font = null;
                button.Text = "";
            }
        }

        private void StartGame()
        {
            clickedButtons.Clear();
            bombButtons.Clear();
            gamestatus = GameStatusEnum.NotStarted;

            ResetButtons();

            PlaceBombsRandomly();
            lblclickcount.Text = "0";
            gamestatus = GameStatusEnum.Playing;
            DisplayGameStatus();
            btnstart.Text = "Restart";
        }


        private void DisplayGameStatus()
        {
            string msg = "Click Start to begin Game";
            switch (gamestatus)
            {
                case GameStatusEnum.Playing:
                    msg = "Avoid The Bombs";
                    tbl2.BackColor = Color.Black;
                    break;
                case GameStatusEnum.Loss:
                    msg = "Game over, Try again";
                    break;
                case GameStatusEnum.Winner:
                    msg = "Congratulations, You won";
                    tbl2.BackColor = Color.Gold;
                    break;
            }
            lblstatus.Text = msg;
        }
        private void Btnstart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }
    }
}