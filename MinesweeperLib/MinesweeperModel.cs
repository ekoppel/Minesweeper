﻿// Ezra Koppel and Isaac Lichter HW#10 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperLib
{
    public class MinesweeperModel
    {
        public static readonly int ROW = 20, COL = 20;
        private readonly Cell[,] board = new Cell[ROW, COL];
        private bool firstInspect;
        private Random rand = new Random();
        private int tilesToReveal;

        public MinesweeperModel(int bombAmount)
        {
            for (int i = 0; i < ROW; i++)
                for (int j = 0; j < COL; j++)
                    board[i, j] = new Cell(i, j, board);

            tilesToReveal = ROW * COL - bombAmount;
            while (bombAmount > 0)
            {
                int row = rand.Next() % ROW;
                int col = rand.Next() % COL;

                if (!board[row, col].IsBomb)
                {
                    board[row, col].IsBomb = true;
                    bombAmount--;
                }
            }

            firstInspect = true;
        }

        public Cell[,] GetBoard() => board;

        public int CountNeighbors(int r, int c)
        {
            int count = 0;

            for (int i = Math.Max(0, r - 1); i <= Math.Min(ROW - 1, r + 1); i++)
                for (int j = Math.Max(0, c - 1); j <= Math.Min(COL - 1, c + 1); j++)
                {
                    if (i == r && j == c)
                        continue;
                    if (board[i, j].IsBomb)
                        count++;
                }
            return count;
        }

        public bool IsWinner() => tilesToReveal == 0;

        public List<Cell> Inspect(Cell c)
        {
            return Inspect(c.ROW, c.COL);
        }

        /*
        * @return a list of the revealed points. Returns null if a bomb was hit.
        */
        public List<Cell> Inspect(int row, int col)
        {
            List<Cell> returnList = new List<Cell>();

            if (firstInspect)
            {
                for (int i = Math.Max(0, row - 1); i <= Math.Min(ROW - 1, row + 1); i++)
                    for (int j = Math.Max(0, col - 1); j <= Math.Min(COL - 1, col + 1); j++)
                        if (board[i, j].IsBomb)
                        {
                            board[i, j].IsBomb = false;

                            int r = rand.Next() % ROW;
                            int c = rand.Next() % COL;

                            while (board[r, c].IsBomb)
                            {
                                r = rand.Next() % ROW;
                                c = rand.Next() % COL;
                            }
                            board[r, c].IsBomb = true;
                        }
            }
            firstInspect = false;

            if (board[row, col].IsBomb)
                return null;

            returnList.Add(board[row, col]);

            if (!board[row, col].Visible)
                tilesToReveal--;

            board[row, col].Display = CountNeighbors(row, col);
            board[row, col].Visible = true;

            if (board[row, col].Display == 0)
                for (int i = Math.Max(0, row - 1); i <= Math.Min(ROW - 1, row + 1); i++)
                    for (int j = Math.Max(0, col - 1); j <= Math.Min(COL - 1, col + 1); j++)
                        if (!board[i, j].Visible)
                            returnList.AddRange(Inspect(i, j));

            return returnList;
        }

        public void ResetGame()
        {
            for (int i = 0; i < ROW; i++)
                for (int j = 0; j < COL; j++)
                {
                    board[i, j].IsBomb = false;
                    board[i, j].Visible = false;
                    board[i, j].Display = 0;
                }
            firstInspect = true;
        }

        public override String ToString()
        {
            for (var r = 0; r < ROW; r++)
            {
                for (var c = 0; c < COL; c++)
                {
                    if (board[r, c].Visible)
                        Console.Write($"{board[r, c].Display}");
                    else
                        Console.Write("|");
                }
                Console.Write("|");
                Console.WriteLine();
            }
            return "\n";
        }

        public struct Cell
        {
            public readonly int ROW;
            public readonly int COL;
            public readonly Cell[,] outerBoard;

            public Cell(int row, int col, Cell[,] board)
            {
                ROW = row;
                COL = col;
                IsBomb = false;
                Visible = false;
                Display = 0;
                this.outerBoard = board;
            }
            public bool IsBomb { get; set; }
            public bool Visible { get; set; }
            public int Display { get; set; }

            public Cell[,] GetBoard() { return outerBoard; }
        }
    }
}