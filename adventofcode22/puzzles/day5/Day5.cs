using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day5
{
    public class Day5
    {
        public Day5()
        {
            //new
            var i1 = new Stack<string>(new string[] { "W", "R", "F" });
            var i2 = new Stack<string>(new string[] { "T", "H", "M", "C", "D", "V", "W", "P" });
            var i3 = new Stack<string>(new string[] { "P", "M", "Z", "N", "L" });
            var i4 = new Stack<string>(new string[] { "J", "C", "H", "R" });
            var i5 = new Stack<string>(new string[] { "C", "P", "G", "H", "Q", "T", "B" });
            var i6 = new Stack<string>(new string[] { "G", "C", "W", "L", "F", "Z" });
            var i7 = new Stack<string>(new string[] { "W", "V", "L", "Q", "Z", "J", "G", "C" });
            var i8 = new Stack<string>(new string[] { "P", "N", "R", "F", "W", "T", "V", "C" });
            var i9 = new Stack<string>(new string[] { "J", "W", "H", "G", "R", "S", "V" });

            //aro
            //var i1 = new Stack<string>(new string[] { "J", "H", "P", "M", "S", "F", "N", "V" });
            //var i2 = new Stack<string>(new string[] { "S", "R", "L", "M", "J", "D", "Q" });
            //var i3 = new Stack<string>(new string[] { "N", "Q", "D", "H", "C", "S", "W", "B" });
            //var i4 = new Stack<string>(new string[] { "R", "S", "C", "L" });
            //var i5 = new Stack<string>(new string[] { "M", "V", "T", "P", "F", "B" });
            //var i6 = new Stack<string>(new string[] { "T", "R", "Q", "N", "C" });
            //var i7 = new Stack<string>(new string[] { "G", "V", "R" });
            //var i8 = new Stack<string>(new string[] { "C", "Z", "S", "P", "D", "L", "R" });
            //var i9 = new Stack<string>(new string[] { "D", "S", "J", "V", "G", "P", "B", "F" });

            var stacks = new List<Stack<string>>(new Stack<string>[] { i1, i2, i3, i4, i5, i6, i7, i8, i9 });

            var lines = File.ReadAllLines(@"puzzles\day5\input1.txt");
            foreach(var line in lines)
            {
                var inputs = Regex.Matches(line, "\\d+").Select(x => Convert.ToInt32(x.Value)).ToList();
                var tmp = new Stack<string>();
                for(int i = 0; i < inputs[0]; i++)
                {
                    tmp.Push(stacks[inputs[1] - 1].Pop());
                }
                for (int i = 0; i < inputs[0]; i++)
                {
                    stacks[inputs[2] - 1].Push(tmp.Pop());
                }
            }
            foreach(var stack in stacks)
            {
                Console.Write(stack.Peek());
            }
            Console.WriteLine();
        }
        
    }
}
