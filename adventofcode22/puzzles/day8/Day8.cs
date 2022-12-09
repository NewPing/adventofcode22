using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode22.puzzles.day8
{
    public class Day8
    {
        public Day8()
        {
            var lines = File.ReadAllLines(@"puzzles\day8\input2.txt");
            var data =
                lines.Select(
                    line => line.Select(
                        number => new Tuple<int, bool>(int.Parse(number.ToString()), false)
                    ).ToList()
                ).ToList();

            markVisible(data);
            Console.WriteLine(data.Select(x => x.Where(n => n.Item2).Count()).Sum());
            var data2 = lines.Select(line => line.Select(number =>(int.Parse(number.ToString()))).ToList()).ToList();
            var scores = new List<List<int>>();
            for (int x = 0; x < data.Count; x++)
            {
                var scoresLine = new List<int>();
                for (int y = 0; y < data[x].Count; y++)
                {
                    scoresLine.Add(rankVisibilityScore(data2, x, y));
                }
                scores.Add(scoresLine);
            }
            Console.WriteLine(scores.Select(x => x.Max()).Max());
        }
    

        public int rankVisibilityScore(List<List<int>> data, int xStart, int yStart)
        {
            int scoreLeft = 0;
            int scoreRight = 0;
            int scoreTop = 0;
            int scoreBottom = 0;
            int viewingHeight = data[yStart][xStart];
            //left to right
            for (int i = xStart; i < data.Count-1; i++)
            {
                if (viewingHeight > data[yStart][i + 1])
                {
                    scoreRight++;
                } else
                {
                    scoreRight++;
                    break;
                }
            }
            //right to left
            for (int i = xStart ; i > 0; i--)
            {
                if (viewingHeight > data[yStart][i - 1])
                {
                    scoreLeft++;
                }
                else
                {
                    scoreLeft++;
                    break;
                }
            }
            //bottom to top
            for (int i = yStart ; i > 0; i--)
            {
                if (viewingHeight  > data[i-1][xStart])
                {
                    scoreTop++;
                }
                else
                {
                    scoreTop++;
                    break;
                }
            }
            //top to bottom
            for (int i = yStart; i < data.Count - 1; i++)
            {
                if (viewingHeight > data[i+1][xStart])
                {
                    scoreBottom++;
                }
                else
                {
                    scoreBottom++;
                    break;
                }
            }

            return scoreTop * scoreLeft * scoreBottom * scoreRight;
        }

        public void markVisible(List<List<Tuple<int, bool>>> data)
        {
            //left & right
            for (int i = 0; i < data.Count; i++)
            {
                var max = -1;
                for (int n = 0; n < data[i].Count; n++)
                {
                    if (data[i][n].Item1 > max)
                    {
                        data[i][n] = new Tuple<int, bool>(data[i][n].Item1, true);
                        max = data[i][n].Item1;
                    }
                }
                max = -1;
                for (int n = data[i].Count - 1; n >= 0; n--)
                {
                    if (data[i][n].Item1 > max)
                    {
                        data[i][n] = new Tuple<int, bool>(data[i][n].Item1, true);
                        max = data[i][n].Item1;
                    }
                }
            }
            //top
            for (int x = 0; x < data[0].Count; x++)
            {
                int max = -1;
                for (int n = 0; n < data.Count; n++)
                {
                    if (data[n][x].Item1 > max)
                    {
                        max = data[n][x].Item1;
                        data[n][x] = new Tuple<int, bool>(data[n][x].Item1, true);
                    }
                }
            }
            //bottom
            for (int x = 0; x < data[0].Count; x++)
            {
                int max = -1;
                for (int n = data.Count - 1; n >= 0; n--)
                {
                    if (data[n][x].Item1 > max)
                    {
                        max = data[n][x].Item1;
                        data[n][x] = new Tuple<int, bool>(data[n][x].Item1, true);
                    }
                }
            }
        }
    }
}
