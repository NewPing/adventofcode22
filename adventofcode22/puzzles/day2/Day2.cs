using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace adventofcode22.puzzles.day2
{
    public class Day2
    {
        public Day2() 
        {
            var input = prepData(@"puzzles\day2\input2.txt");
            part1(input);
            part2(input);
        }

        private List<List<Symbols>> prepData(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);
            var data = lines.Select(x => x.Split(" ").Select(y => convertToSymbol(y)).ToList()).ToList();
            return data;
        }
        
        public void part1(List<List<Symbols>> data)
        {
            var totalScore = 0;
            foreach(var list in data)
            {
                totalScore += (int) list[1];
                totalScore += calculateGameScore(list[0], list[1]);
            }
            Console.WriteLine(totalScore);
        }

        public void part2(List<List<Symbols>> data)
        {
            var totalScore = 0;
            foreach (var list in data)
            {
                totalScore += (int)getWinSymbol(list);
                totalScore += calculateGameScore(list[0], getWinSymbol(list));
            }
            Console.WriteLine(totalScore);
        }

        public Symbols getWinSymbol(List<Symbols> hands)
        {
            if (hands[0] == Symbols.Rock)
            {
                switch(hands[1])
                {
                    case Symbols.Rock:
                        return Symbols.Scissors;
                    case Symbols.Paper:
                        return Symbols.Rock;
                    default: return Symbols.Paper;
                }
            }
            if (hands[0] == Symbols.Paper)
            {
                switch (hands[1])
                {
                    case Symbols.Rock:
                        return Symbols.Rock;
                    case Symbols.Paper:
                        return Symbols.Paper;
                    default: return Symbols.Scissors;
                }
            }
            if (hands[0] == Symbols.Scissors)
            {
                switch (hands[1])
                {
                    case Symbols.Rock:
                        return Symbols.Paper;
                    case Symbols.Paper:
                        return Symbols.Scissors;
                    default: return Symbols.Rock;
                }
            }
            throw new Exception("shit went south");
        }

        int calculateGameScore(Symbols one, Symbols two){
            if (one == two)
            {
                return 3; //draw
            }
            if (one == Symbols.Rock && two == Symbols.Paper)
            {
                return 6;
            }
            if (one == Symbols.Paper && two == Symbols.Scissors)
            {
                return 6;
            } 
            if (one == Symbols.Scissors && two == Symbols.Rock)
            {
                return 6;
            }
            return 0;
        }

        //private int calculateGameScore(char one, char two)
        //{
        //    return 0;
        //}

        private Symbols convertToSymbol(string symbol)
        {
            switch(symbol)
            {
                case "A":
                    return Symbols.Rock;
                case "B":
                    return Symbols.Paper;
                case "C":
                    return Symbols.Scissors;
                case "X":
                    return Symbols.Rock;
                case "Y":
                    return Symbols.Paper;
                case "Z":
                    return Symbols.Scissors;
                default:
                    throw new Exception("not a symbol");
            }
        }
    }

    public enum Symbols : int
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }
}
