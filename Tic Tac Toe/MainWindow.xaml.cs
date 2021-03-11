using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Holds the value of the cells of the game
        /// </summary>
        private MarkType[] results;

        /// <summary>
        /// True if Player 1's turn (X), False if Player 2's turn (O)
        /// </summary>
        private bool winnerFound;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool gameEnded;

        private List<int> openSquares = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            results = new MarkType[9];

            for (var i = 0; i < 9; i++)
                results[i] = 0;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;

                openSquares = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                winnerFound = false;
            });

            gameEnded = false;
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (results[index] != MarkType.Empty) 
                return;

            results[index] = MarkType.X;
            button.Content = "X";
            openSquares.Remove(index);

            checkForWinner();

            if (!winnerFound)
                AIturn();
        }

        private void AIturn()
        {
            if (openSquares.Count != 0)
            {
                Random rnd = new Random();
                int aiSquare = rnd.Next(0, openSquares.Count);
                results[openSquares[aiSquare]] = MarkType.O;

                switch (openSquares[aiSquare])
                {
                    case 0:
                        Button0_0.Content = "O";
                        Button0_0.Foreground = Brushes.Red;
                        break;
                    case 1:
                        Button1_0.Content = "O";
                        Button1_0.Foreground = Brushes.Red;
                        break;
                    case 2:
                        Button2_0.Content = "O";
                        Button2_0.Foreground = Brushes.Red;
                        break;
                    case 3:
                        Button0_1.Content = "O";
                        Button0_1.Foreground = Brushes.Red;
                        break;
                    case 4:
                        Button1_1.Content = "O";
                        Button1_1.Foreground = Brushes.Red;
                        break;
                    case 5:
                        Button2_1.Content = "O";
                        Button2_1.Foreground = Brushes.Red;
                        break;
                    case 6:
                        Button0_2.Content = "O";
                        Button0_2.Foreground = Brushes.Red;
                        break;
                    case 7:
                        Button1_2.Content = "O";
                        Button1_2.Foreground = Brushes.Red;
                        break;
                    case 8:
                        Button2_2.Content = "O";
                        Button2_2.Foreground = Brushes.Red;
                        break;
                    default:
                        break;
                }

                openSquares.RemoveAt(aiSquare);
                checkForWinner();
            }
        }

        private void checkForWinner()
        {
            #region Row Wins
            // ROW 1
            if (results[0] != MarkType.Empty && (results[0] & results[1] & results[2]) == results[0])
            {
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // ROW 2
            if (results[3] != MarkType.Empty && (results[3] & results[4] & results[5]) == results[3])
            {
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // ROW 3
            if (results[6] != MarkType.Empty && (results[6] & results[7] & results[8]) == results[6])
            {
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }
            #endregion

            #region Col Wins
            // COL 1
            if (results[0] != MarkType.Empty && (results[0] & results[3] & results[6]) == results[0])
            {
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // COL 2
            if (results[1] != MarkType.Empty && (results[1] & results[4] & results[7]) == results[1])
            {
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // COL 3
            if (results[2] != MarkType.Empty && (results[2] & results[5] & results[8]) == results[2])
            {
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }
            #endregion

            #region Diagonal wins
            // DIAGONAL 1
            if (results[0] != MarkType.Empty && (results[0] & results[4] & results[8]) == results[0])
            {
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // DIAGONAL 2
            if (results[2] != MarkType.Empty && (results[2] & results[4] & results[6]) == results[2])
            {
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }
            #endregion

            #region No winner
            if (!results.Any(result => result == MarkType.Empty))
            {
                gameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
