using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private members
        /// <summary>
        /// Holds the value of the cells of the game
        /// </summary>
        private MarkType[,] results;

        /// <summary>
        /// True if Player 1's turn (X), False if Player 2's turn (O)
        /// </summary>
        private bool winnerFound;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool gameEnded;

        private List<int> openSquares = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            results = new MarkType[3,3];

            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    results[i, j] = 0;

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

        #region Clicks
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

            if (results[row, column] != MarkType.Empty) 
                return;

            results[row, column] = MarkType.X;
            button.Content = "X";
            openSquares.Remove(index);

            checkForWinner();

            if (!winnerFound)
                AIturn();
        }
        #endregion

        public int minimax(MarkType[,] results)
        {
            if (winnerFound)
            {

            }

            return 1;
        }

        #region AI decision logic and frontend
        private void AIturn()
        {
            if (openSquares.Count != 0)
            {
                Random rnd = new Random();
                int aiSquare = rnd.Next(0, openSquares.Count);
                int value = openSquares[aiSquare];

                results[value / 3, value - (3 * (value / 3))] = MarkType.O;

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
        #endregion

        #region Wincon
        /// <summary>
        /// Checks for winning conditions
        /// </summary>
        private void checkForWinner()
        {
            #region Row Wins
            // ROW 1

            if (results[0, 0] != MarkType.Empty && (results[0, 0] & results[0, 1] & results[0, 2]) == results[0, 0])
            {
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // ROW 2
            if (results[1, 0] != MarkType.Empty && (results[1, 0] & results[1, 1] & results[1, 2]) == results[1, 0])
            {
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // ROW 3
            if (results[2, 0] != MarkType.Empty && (results[2, 0] & results[2, 1] & results[2, 2]) == results[2, 0])
            {
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }
            #endregion

            #region Col Wins
            // COL 1
            if (results[0, 0] != MarkType.Empty && (results[0, 0] & results[1, 0] & results[2, 0]) == results[0, 0])
            {
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // COL 2
            if (results[0, 1] != MarkType.Empty && (results[0, 1] & results[1, 1] & results[2, 1]) == results[0, 1])
            {
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // COL 3
            if (results[0, 2] != MarkType.Empty && (results[0, 2] & results[1, 2] & results[2, 2]) == results[0, 2])
            {
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }
            #endregion

            #region Diagonal wins
            // DIAGONAL 1
            if (results[0, 0] != MarkType.Empty && (results[0, 0] & results[1, 1] & results[2, 2]) == results[0, 0])
            {
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }

            // DIAGONAL 2
            if (results[0, 2] != MarkType.Empty && (results[0, 2] & results[1, 1] & results[2, 0]) == results[0, 2])
            {
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                gameEnded = true;
                winnerFound = true;
                return;
            }
            #endregion

            #region No winner
            if (results.Cast<MarkType>().All(element => element != MarkType.Empty))
            {
                gameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }

        #endregion
    }
}
