using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day4
{
    public class Day4
    {
        public Day4()
        {
            var lines = File.ReadAllLines(@"puzzles\day4\input1.txt").ToList();
            var pairs = lines.Select(x => x.Split(",").Select(y => y.Split("-").Select(z => Convert.ToInt32(z)).ToList()).ToList()).ToList();

            part1(pairs);
            part2(pairs);
            
        }

        public void part1(List<List<List<int>>> pairs)
        {
            var count = 0;
            foreach (var pair in pairs)
            {
                if ((pair[0][0] <= pair[1][0] && pair[0][1] >= pair[1][1]) || (pair[1][0] <= pair[0][0] && pair[1][1] >= pair[0][1]))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        public void part2(List<List<List<int>>> pairs)
        {
            var count = 0;
            foreach (var pair in pairs)
            {
                if (doesOverlap(pair[0][0], pair[0][1], pair[1][0], pair[1][1]))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        public bool doesOverlap(int i1, int i2, int i3, int i4)
        {
            // 1 - 3 | 4 - 6 //negative - last return: return false
            // 4 - 6 | 1 - 3 //copy upper line with swapped inputs
            // 1 - 4 | 3 - 6 //positive
            // 3 - 6 | 1 - 4 //copy upper line with swapped inputs
            // 1 - 6 | 3 - 4 //positive
            // 3 - 4 | 1 - 6 //copy upper line with swapped inputs
            if ((i2 < i3 || i1 > i4))
                return false;
            return true;
        }


    }
}
