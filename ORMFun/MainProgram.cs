using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinesweeperLib;

namespace Minesweeper
{
    class MinesweeperConsoleApplication
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Welcome to Minesweeper! how many bombs would you like to play with? (20 by 20 board)");

            int bombCount;

            while (!int.TryParse(Console.ReadLine(), out bombCount) || bombCount < 1 || bombCount > 399)
                Console.WriteLine("That was not a valid number. Try again.");

            MinesweeperModel model = new MinesweeperModel(bombCount);

            int row, col;

            do
            {
                Console.WriteLine(model.ToString());

                Console.WriteLine("Which row?");

                while (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row >= 20)
                    Console.WriteLine("Invalid row. please enter a number from 0 to 19");

                Console.WriteLine("Which column?");

                while (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col >= 20)
                    Console.WriteLine("Invalid column. please enter a number from 0 to 19");

            } while (model.Inspect(row, col) != null && !model.IsWinner());

            if (model.IsWinner())
                Console.WriteLine("Congratulations! You win!");

            else
                Console.WriteLine("Sorry, you lose.");

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}