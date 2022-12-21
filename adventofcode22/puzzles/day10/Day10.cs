using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day10
{
    public class Day10
    {
        public Day10()
        {
            var lines = File.ReadAllLines(@"puzzles\day10\input1.txt").ToList();
            var data = prepData(lines);
            //part1(data);
            part2(data);
        }

        private List<CLine> prepData(List<string> lines)
        {
            var data = new List<CLine>();
            foreach(var line in lines)
            {
                var cline = new CLine();
                if (line.Equals("noop"))
                {
                    cline.Instruction = Instr.noop;
                    cline.ExecutionTime = 1;
                } 
                else if (line.StartsWith("addx"))
                {
                    var arg = line.Replace("addx ", "");
                    cline.Instruction = Instr.addx;
                    cline.Arg = int.Parse(arg);
                    cline.ExecutionTime = 2;
                }
                data.Add(cline);
            }
            return data;
        }

        public void part1(List<CLine> _instList)
        {
            var regX = 1;
            var signalStrength = 0;
            var maxExectionTime = _instList.Select(x => x.ExecutionTime).Sum();
            _instList.Reverse();
            var instructions = new Stack<CLine>(_instList);
            for (int i = 1; i <= maxExectionTime; i++)
            {
                if (i == 20 || (i % 40) -20 == 0)
                {
                    signalStrength += i * regX;
                }
                var inst = instructions.Peek();
                inst.ExecutionTime--;

                if (inst.Instruction == Instr.noop)
                {
                    //do nothing
                } else if (inst.Instruction == Instr.addx)
                {
                    if (inst.ExecutionTime == 0)
                    {
                        regX += inst.Arg;
                    }
                }

                if (inst.ExecutionTime == 0)
                {
                    instructions.Pop();
                }
            }
            Console.WriteLine(signalStrength);
        }

        public void part2(List<CLine> _instList)
        {
            bool[][] grid = new bool[6][];
            grid = grid.Select(x => x = new bool[40]).ToArray();
            var regX = 1;
            var signalStrength = 0;
            var maxExectionTime = _instList.Select(x => x.ExecutionTime).Sum();
            _instList.Reverse();
            var instructions = new Stack<CLine>(_instList);
            for (int i = 1; i <= maxExectionTime; i++)
            {
                if (i == 20 || (i % 40) - 20 == 0)
                {
                    signalStrength += i * regX;
                }
                //grid stuff
                var point = getPoint(i-1);
                var row = point.Item1;
                var column = point.Item2;
                if(checkOverlap(i, regX))
                {
                    grid[row][column] = true;
                }
                else
                {
                    grid[row][column] = false;
                }
                

                //instruction stuff
                var inst = instructions.Peek();
                inst.ExecutionTime--;

                if (inst.Instruction == Instr.noop)
                {
                    //do nothing
                }
                else if (inst.Instruction == Instr.addx)
                {
                    if (inst.ExecutionTime == 0)
                    {
                        regX += inst.Arg;
                    }
                }

                if (inst.ExecutionTime == 0)
                {
                    instructions.Pop();
                }
            }
            printScreen(grid);
        }

        private void printScreen(bool[][] grid)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                {
                    if (grid[y][x])
                    {
                        Console.Write("#");
                    } else
                    {
                        Console.Write("_");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool checkOverlap(int cycle, int spritePos)
        {
            var cyclePoint = getPoint(cycle - 1);
            var spritePoint = getPoint(spritePos);

            //if (cyclePoint.Item1 != spritePoint.Item1)
            //{
            //    //not on the same row...
            //    return false;
            //}

            //now they are on the same row, otherwise we would have returned earlier..
            if (cyclePoint.Item2 == spritePoint.Item2 - 1)
            {
                return true;
            }
            if (cyclePoint.Item2 == spritePoint.Item2)
            {
                return true;
            }
            if (cyclePoint.Item2 == spritePoint.Item2 + 1)
            {
                return true;
            }

            return false;
        }

        public Tuple<int, int> getPoint(int i)
        {
            //57 => row = 1; column = 16
            var row = (int)(i / 40);
            var column = i % 40;
            return new Tuple<int, int>(row, column);
        }
    }

    public class CLine
    {
        public Instr Instruction { get; set; }
        public int Arg { get; set; }
        public int ExecutionTime { get; set; }
    }

    public enum Instr : int
    {
        noop = 1,
        addx = 2
    }
}
