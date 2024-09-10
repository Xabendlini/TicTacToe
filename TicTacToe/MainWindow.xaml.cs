using System.Windows;
using System.Windows.Controls;


namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool isOTurn = false;
        private Button[] buttons;
        private char[] board = new char[9];
        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[] { Button0, Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8 };
            ResetBoard();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = int.Parse(button.Name.Substring(6));

            if (board[index] == ' ')
            {
                board[index] = isOTurn ? 'O' : 'X';
                button.Content = board[index];
                button.IsEnabled = false;

                if (CheckWin())
                {
                    TurnIndicator.Text = $"Player {board[index]} Wins!";
                    DisableAllButtons();
                }
                else if (CheckDraw())
                {
                    TurnIndicator.Text = "It's a Draw!";
                }
                else
                {
                    isOTurn = !isOTurn;
                    TurnIndicator.Text = $"Turn: {(isOTurn ? 'O' : 'X')}";
                }
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            ResetBoard();
        }

        private void ResetBoard()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Content = string.Empty;
                buttons[i].IsEnabled = true;
                board[i] = ' ';
            }
            isOTurn = false;
            TurnIndicator.Text = "Turn: X";
        }

        private bool CheckWin()
        {
            int[][] winningCombinations = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            foreach (int[] combination in winningCombinations)
            {
                if (board[combination[0]] != ' ' &&
                    board[combination[0]] == board[combination[1]] &&
                    board[combination[1]] == board[combination[2]])
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckDraw()
        {
            foreach (char c in board)
            {
                if (c == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        private void DisableAllButtons()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }
    }
}
