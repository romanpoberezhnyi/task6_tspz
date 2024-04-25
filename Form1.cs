using System;
using System.Windows.Forms;
namespace task6
{
    public partial class Form1 : Form
    {
        private Button[,] buttons;
        private char currentPlayer = 'X';
        private bool gameOver = false;
        private Button sender;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeGame()
        {
            buttons = new Button[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Width = 50,
                        Height = 50,
                        Top = i * 50,
                        Left = j * 50,
                        Tag = new Tuple<int, int>(i, j)
                    };
                    buttons[i, j].Click += Button_Click;
                    Controls.Add(buttons[i, j]);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (gameOver)
                return;

            Button button = (Button)sender;
            button.Text = currentPlayer.ToString();
            button.Enabled = false;

            Tuple<int, int> position = (Tuple<int, int>)button.Tag;
            if (CheckForWinner(position.Item1, position.Item2))
            {
                MessageBox.Show($"Гравець {currentPlayer} виграв!");
                gameOver = true;
                return;
            }

            bool movesLeft = false;
            foreach (Button btn in buttons)
            {
                if (btn.Enabled)
                {
                    movesLeft = true;
                    break;
                }
            }
            if (!movesLeft)
            {
                MessageBox.Show("Нічия!");
                gameOver = true;
                return;
            }

            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';

            if (currentPlayer == 'O')
                MakeComputerMove();
        }

        private void MakeComputerMove()
        {
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(0, 3);
                col = random.Next(0, 3);
            } while (!buttons[row, col].Enabled);

            buttons[row, col].Text = currentPlayer.ToString();
            buttons[row, col].Enabled = false;

            if (CheckForWinner(row, col))
            {
                MessageBox.Show($"Гравець {currentPlayer} виграв!");
                gameOver = true;
                return;
            }

            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        private bool CheckForWinner(int row, int col)
        {
            bool winner = false;


            if (buttons[row, 0].Text == currentPlayer.ToString() &&
                buttons[row, 1].Text == currentPlayer.ToString() &&
                buttons[row, 2].Text == currentPlayer.ToString())
            {
                winner = true;
                HighlightWinnerButtons(buttons[row, 0], buttons[row, 1], buttons[row, 2]);
            }


            if (buttons[0, col].Text == currentPlayer.ToString() &&
                buttons[1, col].Text == currentPlayer.ToString() &&
                buttons[2, col].Text == currentPlayer.ToString())
            {
                winner = true;
                HighlightWinnerButtons(buttons[0, col], buttons[1, col], buttons[2, col]);
            }


            if (row == col &&
                buttons[0, 0].Text == currentPlayer.ToString() &&
                buttons[1, 1].Text == currentPlayer.ToString() &&
                buttons[2, 2].Text == currentPlayer.ToString())
            {
                winner = true;
                HighlightWinnerButtons(buttons[0, 0], buttons[1, 1], buttons[2, 2]);
            }


            if (row + col == 2 &&
                buttons[0, 2].Text == currentPlayer.ToString() &&
                buttons[1, 1].Text == currentPlayer.ToString() &&
                buttons[2, 0].Text == currentPlayer.ToString())
            {
                winner = true;
                HighlightWinnerButtons(buttons[0, 2], buttons[1, 1], buttons[2, 0]);
            }

            return winner;
        }

        private void HighlightWinnerButtons(params Button[] winningButtons)
        {
            foreach (Button button in winningButtons)
            {
                button.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Гра в хрестики нолики\n гра написана Побережним Романом Дмитровичем, студентом групи КН21-1\n варіант №20", "about program", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
