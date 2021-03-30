using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class Board
    {
        #region Private members
        /// <summary>
        /// The 2D array abstractization of the board
        /// </summary>
        private MarkType[,] results;

        /// <summary>
        /// Number of pieces on board
        /// </summary>
        private int numberOfMoves = 0;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Board() => results = new MarkType[3, 3];
        #endregion

        #region Class Methods
        /// <summary>
        /// Reseting board to initial stage
        /// </summary>
        public void ResetBoard() => results = new MarkType[3, 3];

        /// <summary>
        /// Who's turn it is?
        /// </summary>
        /// <returns></returns>
        public Player TurnOfPlayer()
        {
            if (numberOfMoves % 2 == 0)
                return Player.Computer;
            else return Player.Human;
        }

        /// <summary>
        /// Placing a move on the board
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void PlaceMove(int i, int j)
        {
            if (results[i, j] != MarkType.Empty)
                return;

            if (TurnOfPlayer() == Player.Computer)
                results[i, j] = MarkType.X;
            else
                results[i, j] = MarkType.O;
            numberOfMoves++;
        }

        public Player WinningCondition()
        {
            if (numberOfMoves > 5)
            {
                #region Row wins
                // ROW 1
                if (results[0, 0] != MarkType.Empty && (results[0, 0] & results[0, 1] & results[0, 2]) == results[0, 0])
                    return(Player)(results[0, 0] - 1);

                // ROW 2
                if (results[1, 0] != MarkType.Empty && (results[1, 0] & results[1, 1] & results[1, 2]) == results[1, 0])
                    return (Player)(results[0, 0] - 1);

                // ROW 3
                if (results[2, 0] != MarkType.Empty && (results[2, 0] & results[2, 1] & results[2, 2]) == results[2, 0])
                    return (Player)(results[0, 0] - 1);
                #endregion

                #region Col wins
                // COL 1
                if (results[0, 0] != MarkType.Empty && (results[0, 0] & results[1, 0] & results[2, 0]) == results[0, 0])
                    return (Player)(results[0, 0] - 1);

                // COL 2
                if (results[0, 1] != MarkType.Empty && (results[0, 1] & results[1, 1] & results[2, 1]) == results[0, 1])
                    return (Player)(results[0, 0] - 1);

                // COL 3
                if (results[0, 2] != MarkType.Empty && (results[0, 2] & results[1, 2] & results[2, 2]) == results[0, 2])
                    return (Player)(results[0, 0] - 1);
                #endregion

                #region Diagonal wins
                // Diagonal 1
                if (results[0, 0] != MarkType.Empty && (results[0, 0] & results[1, 1] & results[2, 2]) == results[0, 0])
                    return (Player)(results[0, 0] - 1);

                // Diagonal 2
                if (results[0, 2] != MarkType.Empty && (results[0, 2] & results[1, 1] & results[2, 0]) == results[0, 2])
                    return (Player)(results[0, 0] - 1);
                #endregion

                #region Game over - draw
                if (results.Cast<MarkType>().All(element => (element != MarkType.Empty)))
                    return Player.Placeholder;
                #endregion
            }
            // Game not over yet
            return Player.Placeholder;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Properties
        /// </summary>
        public MarkType[,] Results { get { return results; } }
        public int NumberOfMoves { get; }
        #endregion
    }
}