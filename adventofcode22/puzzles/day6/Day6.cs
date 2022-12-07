using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day6
{
    public class Day6
    {
        public Day6()
        {
            var input = File.ReadAllText(@"puzzles\day6\input1.txt");
            //part1(input);
            part2(input);
        }

        public void part2(string input)
        {
            var noDuplicateIndex = 0;
            for (int i = 0; i < input.Length - 14; i++)
            {

                if (Regex.IsMatch(input.Substring(i, 14), @"^(?:(.)(?!.*\1))*$"))
                {
                    noDuplicateIndex = i + 14;
                    break;
                }
            }
            Console.WriteLine(noDuplicateIndex);
        }

        public void part1(string input)
        {
            var noDuplicateIndex = 0;
            for (int i = 0; i < input.Length - 4; i++)
            {
                if ((new char[] { input[i + 1], input[i + 2], input[i + 3] }).Contains(input[i])) { }
                else if ((new char[] { input[i], input[i + 2], input[i + 3] }).Contains(input[i + 1])) { }
                else if ((new char[] { input[i], input[i + 1], input[i + 3] }).Contains(input[i + 2])) { }
                else
                {
                    noDuplicateIndex = i + 4;
                    break;
                }
            }
            Console.WriteLine(noDuplicateIndex);
        }
    }
}
