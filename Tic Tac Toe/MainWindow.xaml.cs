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

        private Tuple<int, int> Choice = new Tuple<int, int>(-1, -1);
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

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

            AIturn();
        }
       

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

            results[row, column] = MarkType.O;
            button.Content = "O";
            openSquares.Remove(index);

            checkForWinner(false, results);

            if (!winnerFound)
                AIturn();
        }
        #endregion

        #region Minimax

        public int minimax(MarkType[,] res, bool playerComputer)
        {
            var result = checkForWinner(true, res);
            if (result != MarkType.Empty)
            {
                if (result == MarkType.X)
                    return 10;
                else
                    return -10;
            }

            if (playerComputer)
            {
                var bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (res[i, j] == MarkType.Empty)
                        {
                            res[i, j] = MarkType.X;
                            var score = minimax(res, false);
                            res[i, j] = MarkType.Empty;

                            if (score > bestScore)
                            {
                                bestScore = score;
                                Choice = new Tuple<int, int>(i, j);
                            }
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                var bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (res[i, j] == MarkType.Empty)
                        {
                            res[i, j] = MarkType.O;
                            var score = minimax(res, true);
                            res[i, j] = MarkType.Empty;

                            if (score < bestScore)
                            {
                                bestScore = score;
                                Choice = new Tuple<int, int>(i, j);
                            }
                        }
                    }
                }
                return bestScore;
            }
        }

        public int returnScore(MarkType[,] res)
        {
            MarkType check = checkForWinner(true, res);

            if (check == MarkType.X)
                return 10;
            if (check == MarkType.O)
                return -10;
            else
                return 0;
        }



        #endregion


        #region AI decision logic and frontend
        private void AIturn()
        {
            if (openSquares.Count != 0)
            {

                var bestScore = int.MinValue;
                int[] move = new int[2];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (results[i, j] == MarkType.Empty)
                        {
                            results[i, j] = MarkType.X;
                            var score = minimax(results, false);
                            results[i, j] = MarkType.Empty;
                                
                            if (score > bestScore)
                            {
                                bestScore = score;
                                move[0] = i;
                                move[1] = j;
                            }
                        }
                    }
                }

                int val = move[0] * 3 + move[1];
                results[move[0], move[1]] = MarkType.X;

                switch (val)
                {
                    case 0:
                        Button0_0.Content = "X";
                        Button0_0.Foreground = Brushes.Red;
                        break;
                    case 1:
                        Button1_0.Content = "X";
                        Button1_0.Foreground = Brushes.Red;
                        break;
                    case 2:
                        Button2_0.Content = "X";
                        Button2_0.Foreground = Brushes.Red;
                        break;
                    case 3:
                        Button0_1.Content = "X";
                        Button0_1.Foreground = Brushes.Red;
                        break;
                    case 4:
                        Button1_1.Content = "X";
                        Button1_1.Foreground = Brushes.Red;
                        break;
                    case 5:
                        Button2_1.Content = "X";
                        Button2_1.Foreground = Brushes.Red;
                        break;
                    case 6:
                        Button0_2.Content = "X";
                        Button0_2.Foreground = Brushes.Red;
                        break;
                    case 7:
                        Button1_2.Content = "X";
                        Button1_2.Foreground = Brushes.Red;
                        break;
                    case 8:
                        Button2_2.Content = "X";
                        Button2_2.Foreground = Brushes.Red;
                        break;
                    default:
                        break;
                }
            //    openSquares.RemoveAt(aiSquare);
                checkForWinner(false, results);
            }
        }
        #endregion

        #region Wincon
        /// <summary>
        /// Checks for winning conditions
        /// </summary>
        private MarkType checkForWinner(bool minimax, MarkType[,] res)
        {
            #region Row Wins
            // ROW 1

            if (res[0, 0] != MarkType.Empty && (res[0, 0] & res[0, 1] & res[0, 2]) == res[0, 0])
            {
                if (minimax)
                {
                    return res[0, 0];
                }
                else
                {
                    Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }

            // ROW 2
            if (res[1, 0] != MarkType.Empty && (res[1, 0] & res[1, 1] & res[1, 2]) == res[1, 0])
            {
                if (minimax)
                {
                    return res[1, 0];
                }
                else
                {
                    Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }

            // ROW 3
            if (res[2, 0] != MarkType.Empty && (res[2, 0] & res[2, 1] & res[2, 2]) == res[2, 0])
            {
                if (minimax)
                {
                    return res[2, 0];
                }
                else
                {
                    Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }
            #endregion

            #region Col Wins
            // COL 1
            if (res[0, 0] != MarkType.Empty && (res[0, 0] & res[1, 0] & res[2, 0]) == res[0, 0])
            {
                if (minimax)
                {
                    return res[0, 0];
                }
                else
                {
                    Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }

            // COL 2
            if (res[0, 1] != MarkType.Empty && (res[0, 1] & res[1, 1] & res[2, 1]) == res[0, 1])
            {
                if (minimax)
                {
                    
                    return res[0, 1];
                }
                else
                {
                    Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }

            // COL 3
            if (res[0, 2] != MarkType.Empty && (res[0, 2] & res[1, 2] & res[2, 2]) == res[0, 2])
            {
                if (minimax)
                {
                    return res[0, 2];
                }
                else
                {
                    Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }
            #endregion

            #region Diagonal wins
            // DIAGONAL 1
            if (res[0, 0] != MarkType.Empty && (res[0, 0] & res[1, 1] & res[2, 2]) == res[0, 0])
            {
                if (minimax)
                {
                    return res[0, 0];
                }
                else
                {
                    Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }

            // DIAGONAL 2
            if (res[0, 2] != MarkType.Empty && (res[0, 2] & res[1, 1] & res[2, 0]) == res[0, 2])
            {

                if (minimax)
                    return res[0, 2];
                else
                {
                    Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                    gameEnded = true;
                    winnerFound = true;
                    return MarkType.Empty;
                }
            }
            #endregion

            #region No winner
            if (res.Cast<MarkType>().All(element => element != MarkType.Empty))
            {
                if (!minimax)
                {
                    gameEnded = true;

                    Container.Children.Cast<Button>().ToList().ForEach(button =>
                    {
                        button.Background = Brushes.Orange;
                    });
                }
                else
                    return MarkType.Empty;
            }
            #endregion

            return MarkType.Empty;
        }

        #endregion
    }
}
