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
        private bool player1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool gameEnded;

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

            player1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
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

            results[index] = player1Turn ? MarkType.X : MarkType.O;

            button.Content = player1Turn ? "X" : "O";

            if (!player1Turn)
                button.Foreground = Brushes.Red;

            player1Turn ^= true;

            checkForWinner();
        }

        private void checkForWinner()
        {
            #region Row Wins
            // ROW 1
            if (results[0] != MarkType.Empty && (results[0] & results[1] & results[2]) == results[0])
            {
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                gameEnded = true;
            }

            // ROW 2
            if (results[3] != MarkType.Empty && (results[3] & results[4] & results[5]) == results[3])
            {
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                gameEnded = true;
            }

            // ROW 3
            if (results[6] != MarkType.Empty && (results[6] & results[7] & results[8]) == results[6])
            {
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
            }
            #endregion

            #region Col Wins
            // COL 1
            if (results[0] != MarkType.Empty && (results[0] & results[3] & results[6]) == results[0])
            {
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                gameEnded = true;
            }

            // COL 2
            if (results[1] != MarkType.Empty && (results[1] & results[4] & results[7]) == results[1])
            {
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                gameEnded = true;
            }

            // COL 3
            if (results[2] != MarkType.Empty && (results[2] & results[5] & results[8]) == results[2])
            {
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
            }
            #endregion

            #region Diagonal wins
            // DIAGONAL 1
            if (results[0] != MarkType.Empty && (results[0] & results[4] & results[8]) == results[0])
            {
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                gameEnded = true;
            }

            // DIAGONAL 2
            if (results[2] != MarkType.Empty && (results[2] & results[4] & results[6]) == results[2])
            {
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                gameEnded = true;
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
