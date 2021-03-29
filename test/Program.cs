using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        public static Tuple<int, int> Choice;
        public static MarkType[,] results = new MarkType[3, 3];

        static void Main(string[] args)
        {
            results[0, 0] = MarkType.O;
            results[0, 1] = MarkType.X;
            results[0, 2] = MarkType.X;
            results[1, 0] = MarkType.X;
            results[2, 0] = MarkType.X;
            results[2, 1] = MarkType.O;
            results[2, 2] = MarkType.O;

            bool isMinimizing = true;

            while (results.Cast<MarkType>().Any(element => element == MarkType.Empty))
            {
                minimax(results, isMinimizing);
                int i = Choice.Item1;
                int j = Choice.Item1;
                results[i, j] = isMinimizing ? MarkType.O : MarkType.X;

                Console.WriteLine($"{results[0, 0]} | {results[0, 1]} | {results[0, 2]}");
                Console.WriteLine($"----------");
                Console.WriteLine($"{results[1, 0]} | {results[1, 1]} | {results[1, 2]}");
                Console.WriteLine($"----------");
                Console.WriteLine($"{results[2, 0]} | {results[2, 1]} | {results[2, 2]}");


                Console.WriteLine();
                isMinimizing = !isMinimizing;
            }
        }

        public static int minimax(MarkType[,] res, bool playerComputer)
        {
            var result = checkForWinner(true, res);
            if (result != MarkType.Empty)
            {
                if (result == MarkType.X)
                    return 10;
                else
                    return -10;
            }
                
            if (!playerComputer)
            {
                var bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (res[i, j] == MarkType.Empty)
                        {
                            res[i, j] = MarkType.O;
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
                            res[i, j] = MarkType.X;
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


         // MarkType[,] copy = res.Clone() as MarkType[,];
         //
         // if (returnScore(copy) != 0)
         //     return returnScore(copy);
         // else if (copy.Cast<MarkType>().All(element => element != MarkType.Empty))
         //     return 0;
         //
         // List<int> scores = new List<int>();
         // List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
         //
         // for (int i = 0; i < 3; i++)
         // {
         //     for (int j = 0; j < 3; j++)
         //     {
         //         if (copy[i, j] == MarkType.Empty)
         //         {
         //             copy[i, j] = playerComputer ? MarkType.O : MarkType.X;
         //             scores.Add(minimax(copy, !playerComputer));
         //             moves.Add(new Tuple<int, int>(i, j));
         //         }
         //     }
         // }
         //
         // if (!playerComputer)
         // {
         //     int MinScoreIndex = scores.IndexOf(scores.Min());
         //     Choice = moves[MinScoreIndex];
         //     return scores.Min();
         // }
         // else
         // {
         //     int MaxScoreIndex = scores.IndexOf(scores.Max());
         //     Choice = moves[MaxScoreIndex];
         //     return scores.Max();
         //   }
        }

        private static int returnScore(MarkType[,] res)
        {
            MarkType check = checkForWinner(true, res);

            if (check == MarkType.X)
                return 10;
            if (check == MarkType.O)
                return -10;
            else
                return 0;     
        }

        private static MarkType checkForWinner(bool minimax, MarkType[,] res)
        {
            #region Row Wins
            // ROW 1

            if (res[0, 0] != MarkType.Empty && (res[0, 0] & res[0, 1] & res[0, 2]) == res[0, 0])
            {
                if (minimax)
                    return res[0, 0];
                else
                    return MarkType.Empty;
            }

            // ROW 2
            if (res[1, 0] != MarkType.Empty && (res[1, 0] & res[1, 1] & res[1, 2]) == res[1, 0])
            {
                if (minimax)
                    return res[1, 0];
                else
                    return MarkType.Empty;
            }

            // ROW 3
            if (res[2, 0] != MarkType.Empty && (res[2, 0] & res[2, 1] & res[2, 2]) == res[2, 0])
            {
                if (minimax)
                    return res[2, 0];
                else
                    return MarkType.Empty;
            }
            #endregion

            #region Col Wins
            // COL 1
            if (res[0, 0] != MarkType.Empty && (res[0, 0] & res[1, 0] & res[2, 0]) == res[0, 0])
            {
                if (minimax)
                    return res[0, 0];
                else
                    return MarkType.Empty;
            }

            // COL 2
            if (res[0, 1] != MarkType.Empty && (res[0, 1] & res[1, 1] & res[2, 1]) == res[0, 1])
            {
                if (minimax)
                    return res[0, 1];
                else
                    return MarkType.Empty;
            }

            // COL 3
            if (res[0, 2] != MarkType.Empty && (res[0, 2] & res[1, 2] & res[2, 2]) == res[0, 2])
            {
                if (minimax)
                    return res[0, 2];
                else
                    return MarkType.Empty;
            }
            #endregion

            #region Diagonal wins
            // DIAGONAL 1
            if (res[0, 0] != MarkType.Empty && (res[0, 0] & res[1, 1] & res[2, 2]) == res[0, 0])
            {
                if (minimax)
                    return res[0, 0];
                else
                    return MarkType.Empty;
            }

            // DIAGONAL 2
            if (res[0, 2] != MarkType.Empty && (res[0, 2] & res[1, 1] & res[2, 0]) == res[0, 2])
            {
                if (minimax)
                    return res[0, 2];
                else
                    return MarkType.Empty;
            }
            #endregion

            #region No winner
            if (res.Cast<MarkType>().All(element => element != MarkType.Empty))
            {
                return MarkType.Empty;
            }
            #endregion

            return MarkType.Empty;
        }
    }
}
