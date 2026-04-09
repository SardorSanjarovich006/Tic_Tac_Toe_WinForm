using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        char[] board = new char[9];
        char human = 'X';
        char ai = 'O';
        Button[] buttons;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttons = new Button[]
   {
        btn0, btn1, btn2,
        btn3, btn4, btn5,
        btn6, btn7, btn8
   };

            for (int i = 0; i < 9; i++)
            {
                board[i] = (i + 1).ToString()[0];
                buttons[i].Click += Button_Click;
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = Array.IndexOf(buttons, btn);

            if (board[index] == 'X' || board[index] == 'O')
                return;

           
            board[index] = human;
            btn.Text = human.ToString();

           
            if (human == 'X')
                btn.ForeColor = Color.Red;
            else
                btn.ForeColor = Color.Blue;

            if (CheckGameOver()) return;

          
            BestMove();
            UpdateUI();

            CheckGameOver();
        }
        void UpdateUI()
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == 'X')
                {
                    buttons[i].Text = "X";
                    buttons[i].ForeColor = Color.Red;
                }
                else if (board[i] == 'O')
                {
                    buttons[i].Text = "O";
                    buttons[i].ForeColor = Color.Blue;
                }
                else
                {
                    buttons[i].Text = "";
                    buttons[i].ForeColor = Color.Black; 
                }
            }
        }
        void BestMove()
        {
            int bestScore = int.MinValue;
            int move = -1;

            for (int i = 0; i < 9; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                {
                    char temp = board[i];
                    board[i] = ai;

                    int score = Minimax(false);

                    board[i] = temp;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = i;
                    }
                }
            }

            board[move] = ai;
        }

        int Minimax(bool isMaximizing)
        {
            if (CheckWin(ai)) return 1;
            if (CheckWin(human)) return -1;
            if (CheckDraw()) return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;

                for (int i = 0; i < 9; i++)
                {
                    if (board[i] != 'X' && board[i] != 'O')
                    {
                        char temp = board[i];
                        board[i] = ai;

                        int score = Minimax(false);

                        board[i] = temp;
                        bestScore = Math.Max(score, bestScore);
                    }
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int i = 0; i < 9; i++)
                {
                    if (board[i] != 'X' && board[i] != 'O')
                    {
                        char temp = board[i];
                        board[i] = human;

                        int score = Minimax(true);

                        board[i] = temp;
                        bestScore = Math.Min(score, bestScore);
                    }
                }

                return bestScore;
            }
        }

        bool CheckWin(char p)
        {
            return
            (board[0] == p && board[1] == p && board[2] == p) ||
            (board[3] == p && board[4] == p && board[5] == p) ||
            (board[6] == p && board[7] == p && board[8] == p) ||
            (board[0] == p && board[3] == p && board[6] == p) ||
            (board[1] == p && board[4] == p && board[7] == p) ||
            (board[2] == p && board[5] == p && board[8] == p) ||
            (board[0] == p && board[4] == p && board[8] == p) ||
            (board[2] == p && board[4] == p && board[6] == p);
        }

        bool CheckDraw()
        {
            foreach (char c in board)
            {
                if (c != 'X' && c != 'O')
                    return false;
            }
            return true;
        }

        bool CheckGameOver()
        {
            if (CheckWin(human))
            {
                MessageBox.Show("🎉 Siz yutdingiz!");
                ResetGame();
                return true;
            }
            else if (CheckWin(ai))
            {
                MessageBox.Show("🤖 Kompyuter yutdi!");
                ResetGame();
                return true;
            }
            else if (CheckDraw())
            {
                MessageBox.Show("🤝 Durang!");
                ResetGame();
                return true;
            }
            return false;
        }

        void ResetGame()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = (i + 1).ToString()[0];
                buttons[i].Text = "";
                buttons[i].ForeColor = Color.Black; 
            }
        }
    }
}
