using System;

namespace Sudoku_Solver
{
    class Sudoku
    {
        private int[,] _sudokuBoard;

        public Sudoku(string board)
        {
            _sudokuBoard = GenerateBoard(board);
        }

        public void SolveBasic()
        {
            for (int row = 0; row < _sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < _sudokuBoard.GetLength(1); col++)
                {
                    if (_sudokuBoard[row, col] == 0)
                    {
                        int solutions = 0;
                        int correctNum = 0;
                        for (int i = 9; i > 0; i--)
                        {
                            if (solutions > 1)
                            {
                                solutions = 0;
                                break;
                            }
                            if (ControlRowColBox(row, col, i))
                            {
                                correctNum = i;
                                solutions++;
                            }
                        }
                        if (solutions == 1 && correctNum != 0)
                        {
                            _sudokuBoard[row, col] = correctNum;
                        }
                    }
                }
            }
            PrintSuduko();
            Console.WriteLine("Denna Sudoku gick inte att lösa...");
            Console.ReadLine();
            Environment.Exit(0);
        }



        public bool Solve()
        {
            for (int row = 0; row < _sudokuBoard.GetLength(0); row++)
            {
                for (int column = 0; column < _sudokuBoard.GetLength(1); column++)
                {
                    if (_sudokuBoard[row, column] == 0)
                    {
                        for (int numbers = 9; numbers > 0; numbers--)
                        {
                            if (ControlRowColBox(row, column, numbers))
                            {
                                _sudokuBoard[row, column] = numbers; // Placera nummer
                                if (Solve())
                                {
                                    return true;
                                }
                                _sudokuBoard[row, column] = 0; // Om rekursionen kommer ut som false, sätt nummer till 0 och fortsätt loopa nummer i denna.
                            }
                        }
                        return false; // Inget nummer fungerar i denna rekursion
                    }
                }
            }
            return true; // Alla rows kontrollerade i denna rekursion
        }

        public int[,] GenerateBoard(string board)
        {
            char[] boardCharArray = board.ToCharArray();
            int[,] sudokuBoard = new int[9, 9];

            int row = 0;
            int col = 0;

            foreach (var ch in boardCharArray)
            {
                if (!int.TryParse(ch.ToString(), out int parsed))
                {
                    Console.WriteLine("Ogiltigt bräde");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                sudokuBoard[row, col] = parsed;
                col++;
                if (col == 9)
                {
                    col = 0; row++;
                }
            }
            return sudokuBoard;
        }

        private bool ControlRowColBox(int row, int col, int num)
        {
            int rowStart = (row / 3) * 3;
            int colStart = (col / 3) * 3;

            for (int i = 0; i < 9; i++)
            {
                if (_sudokuBoard[row, i] == num) return false; // Kontrollerar hela raden
                if (_sudokuBoard[i, col] == num) return false; // Kontrollerar hela kolumen
                if (_sudokuBoard[rowStart + (i % 3), colStart + (i / 3)] == num) return false; // Kontrollerar Box
                // 0-2 rowStart, 0-2 colStart
            }
            return true;
        }

        public void PrintSuduko()
        {
            int tableHeight = 10; // 9 rader + 1
            int tableWidth = 11; // För att placera ett | på var del av sidorna 1 + 9 + 1

            Console.WriteLine("+-----------------------------+");
            for (int row = 1; row < tableHeight; ++row)
            {
                Console.Write("|");
                for (int col = 1; col < tableWidth; ++col)
                {
                    if (col == 4 || col == 7)
                    {
                        Console.Write("|");
                    }
                    else if (col == 10)
                    {
                        Console.Write("|");
                    }
                    if (col != 10)
                    {
                        Console.Write(" {0} ", _sudokuBoard[row - 1, col - 1]);
                    }
                }
                Console.WriteLine();
                if (row % 3 == 0) // 9 % 3 ger 0 i rest, då har raden nått slutet. Printar ut botten.
                {
                    Console.WriteLine("+-----------------------------+");
                }
            }
        }
    }
}
