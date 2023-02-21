using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day13
{
    public class Day13
    {
        private Stack<int> numbers = new Stack<int>();
        private int lastPos = 0;
        public Day13()
        {
            var input = File.ReadAllText(@"puzzles\day13\exampleInput.txt");
            prepData(input);
        }
        /*
         * [[4,[4, 4]],4,4,4]
depth = 0;
[ depth++;
[ depth++;
tmpTuple = (4, depth) //depth = 2
[ depth++;
tmpTuple = (4, depth) //depth = 3
tmpTuple = (4, depth) //depth = 3
] depth--;
        */
        public void prepData(string input)
        {

            var pac = input.Replace("\r", "").Split("\n\n").ToList().Select(x => x.Split("\n").ToList());
            var packages = new List<dynamic>();
            foreach(var pair in pac)
            {
                var pairs = new List<dynamic>();
                foreach(var entry in pair)
                {
                    //[[1],[2,3,4]]
                    var n = Regex.Matches(entry, "\\d+").Select(x => Convert.ToInt32(x.Value)).ToList();
                    n.Reverse();
                    numbers = new Stack<int>(n);
                    lastPos = -1;
                    pairs.Add(test(entry, new List<dynamic>()));
                }
            }
        }

        public List<dynamic> test(string line, List<dynamic> list)
        {
            lastPos++;
            for (int i = lastPos; i < line.Length; i++)
            {
                Console.WriteLine(line[i]);
                if (line[i].Equals(']'))
                {
                    lastPos = i;
                    return list;
                } else if (line[i].Equals('['))
                {
                    list.Add(test(line, new List<dynamic>()));
                    if (lastPos == line.Length - 1)
                    {
                        return list;
                    }
                    i = lastPos;
                    do
                    {
                        i++;
                        Console.WriteLine(line[i]);
                    } while (line[i].Equals(']'));
                    i--;
                } else if (line.Substring(i).StartsWith(numbers.Peek().ToString()))
                {
                    list.Add(numbers.Pop());
                    do
                    {
                        i++;
                    } while (line[i].Equals(','));
                    i--;
                }
            }
            throw new Exception("should never hit this point");
        }

    }
}
