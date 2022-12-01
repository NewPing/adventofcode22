using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day1
{
    public class Day1
    {

        public Day1()
        {
            var caloriesPerElf = prepInput(@"puzzles\day1\day1_input1.txt");
            part1(caloriesPerElf);
            part2(caloriesPerElf);
        }

        public List<int> prepInput(string pathInputfile)
        {
            var elvenCalories = new List<List<int>>();
            var lines = File.ReadAllLines(pathInputfile).ToList();
            var items = new List<int>();
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    items.Add(Convert.ToInt32(line));
                }
                else
                {
                    elvenCalories.Add(items);
                    items = new List<int>();
                }
            }
            if (items != null && items.Count > 0)
            {
                elvenCalories.Add(items);
            }

            var caloriesPerElf = new List<int>();
            foreach (var list in elvenCalories)
            {
                caloriesPerElf.Add(list.Sum());
            }

            return caloriesPerElf;
        }

        public void part1(List<int> caloriesPerElf)
        {
            Console.WriteLine(caloriesPerElf.Max());
        }

        public void part2(List<int> caloriesPerElf)
        {
            caloriesPerElf.Sort();
            Console.WriteLine(caloriesPerElf[caloriesPerElf.Count() - 1] + caloriesPerElf[caloriesPerElf.Count() - 2] + caloriesPerElf[caloriesPerElf.Count() - 3]);
        }

    }
}