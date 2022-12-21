using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day11
{
    public class Day11
    {
        public Day11()
        {
            var input = File.ReadAllText(@"puzzles\day11\input2.txt");
            var monkeys = prepData(input);
            Console.WriteLine(turns(monkeys, 10_000));
        }

        public List<Monkey> prepData(string input)
        {
            input = input.Replace("\r", "");
            var reg1 = Regex.Matches(input, "(?<=Monkey ).+?(?=\n\n|$)", RegexOptions.Singleline).Select(x => x.Value).ToList();
            var monkeys = reg1.Select(x => new Monkey()).ToList();
            for (int i = 0; i < monkeys.Count; i++)
            {
                var monkey = monkeys[i];
                var reg = reg1[i];

                monkey.ID = BigInteger.Parse(Regex.Match(reg, "\\d+").Value);
                monkey.Items = new Queue<BigInteger>(getNumbersFrom(reg, "Starting items"));
                monkey.TestValue = getNumbersFrom(reg, "Test: ").Single();
                var opVal = getNumbersFrom(reg, "Operation: ");
                if (opVal.Count == 1)
                {
                    monkey.OpValue = opVal.Single();
                } else
                {
                    monkey.OpValue = -1;
                }
                if (reg.Contains("*"))
                {
                    monkey.Op = Operator.Mult;
                } else
                {
                    monkey.Op = Operator.Plus;
                }
            }

            for (int i = 0; i < monkeys.Count; i++)
            {
                var monkey = monkeys[i];
                var reg = reg1[i];

                var refTrue = getNumbersFrom(reg, "If true: ").Single();
                var refFalse = getNumbersFrom(reg, "If false: ").Single();

                foreach (var monk in monkeys)
                {
                    if (monk.ID == refTrue)
                    {
                        monkey.RefTrue = monk;
                    }
                    if (monk.ID == refFalse)
                    {
                        monkey.RefFalse = monk;
                    }
                }
            }
            return monkeys;
        }

        public List<BigInteger> getNumbersFrom(string reg, string startsWith)
        {
            return Regex.Matches(Regex.Match(reg, $"(?<={ startsWith }).+?(?=\n|$)").Value, "\\d+").Select(x => BigInteger.Parse(x.Value)).ToList();
        }

        public BigInteger turns(List<Monkey> ugaUga, BigInteger amountTurns)
        {
            BigInteger monkeyBusiness = 0;
            for(int turns = 0; turns < amountTurns;turns++) { 
                for(int i = 0; i < ugaUga.Count; i++)
                {
                    ugaUga[i].makeTurn();
                }
            }
            var topSorted = ugaUga.Select(x => x.InspectionCount).ToList();
            topSorted.Sort();
            topSorted.Reverse();
            monkeyBusiness = topSorted[0] * topSorted[1];
            return monkeyBusiness;
        }
    }

    public class Monkey
    {
        public BigInteger ID { get; set; }
        public Queue<BigInteger> Items { get; set; }
        public BigInteger TestValue { get; set; }
        public Monkey RefTrue { get; set; }
        public Monkey RefFalse { get; set; }
        public Operator Op { get; set; }
        public BigInteger OpValue { get; set; }
        public BigInteger InspectionCount { get; set; }

        public Monkey()
        {
            InspectionCount = 0;
        }

        public BigInteger operation(BigInteger itemValue)
        {
         /*   var tmpItems = Items.ToList();

            if (tmpItems.Count == 0)
            {
                return;
            }*/

            if (this.Op == Operator.Plus)
            {
                if (OpValue == -1)
                {
                    itemValue += itemValue;
                } else
                {
                    itemValue += OpValue;
                }
            }
            else //Operator.Mult
            {
                if (OpValue == -1)
                {
                    itemValue *= itemValue;
                }
                else
                {
                    itemValue *= OpValue;
                }
            }
            return itemValue;
           // Items = new Queue<BigInteger>(tmpItems);
        }

        public bool test(BigInteger item)
        {
            InspectionCount++;
            return item % TestValue == 0;
        }

        public BigInteger calculate(BigInteger item)
        {
            item = operation(item);
         //   item = (BigInteger)item / 3;
            return item;
        }

        public void makeTurn()
        {
            while(Items.Count != 0)
            {
                var calculatedItem = calculate(Items.Dequeue());
                if (test(calculatedItem)){
                    RefTrue.Items.Enqueue(calculatedItem);
                }
                else
                {
                    RefFalse.Items.Enqueue(calculatedItem);
                }
            }
        }
    }

    public enum Operator
    {
        Plus,
        Mult
    }
}
