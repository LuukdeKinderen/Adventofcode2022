using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d02
{
    public class D02
    {

        public static void Start()
        {
            string[] lines = File.ReadAllLines("d2.txt");
            PartOne(lines);
            PartTwo(lines);
        }

        private static void PartOne(string[] lines)
        {

            int totalscore = 0;
            foreach (var line in lines)
            {
                int opponent = line[0] switch
                {
                    'A' => 1,
                    'B' => 2,
                    'C' => 3,
                };
                int you = line[2] switch
                {
                    'X' => 1,
                    'Y' => 2,
                    'Z' => 3,
                };

                int gameScore = 0;
                string result = string.Empty;
                if (opponent == you)
                {
                    result = "draw ";
                    gameScore = 3;
                }
                else if (opponent - 1 == you || opponent == 1 && you == 3)
                {
                    result = "lose ";

                }
                else
                {
                    result = "win  ";
                    gameScore = 6;
                }

                Console.WriteLine($"Game: {line} result: {result} handscore: {you} gamescore: {gameScore}");
                totalscore += you + gameScore;
            }
            Console.WriteLine($"TotalScore {totalscore}");
        }
        private static void PartTwo(string[] lines)
        {

            int totalscore = 0;
            foreach (var line in lines)
            {
                int opponent = line[0] switch
                {
                    'A' => 1,
                    'B' => 2,
                    'C' => 3,
                };
                string result = line[2] switch
                {
                    'X' => "lose ",
                    'Y' => "draw ",
                    'Z' => "win  ",
                };

                int gameScore = 0;
                int you = 0;
                if (result == "draw ")
                {
                    you = opponent;
                    gameScore = 3;
                }
                else if (result == "lose ")
                {
                    you = opponent - 1;
                    if (opponent == 1)
                    {
                        you = 3;
                    }
                }
                else if (result == "win  ")
                {
                    you = opponent + 1;
                    if (opponent == 3)
                    {
                        you = 1;
                    }
                    gameScore = 6;
                }


                Console.WriteLine($"Game: {line} result: {result} handscore: {you} gamescore: {gameScore}");
                totalscore += you + gameScore;
            }
            Console.WriteLine($"TotalScore {totalscore}");
        }
    }
}
