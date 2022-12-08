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
            var lines = File.ReadAllLines(@"puzzles\day8\exampleInput.txt");
            var data =
                lines.Select(
                    line => line.Select(
                        number => new Tuple<int, bool>(int.Parse(number.ToString()), false)
                    ).ToList()
                ).ToList();
            var data2 =
                lines.Select(
                    line => line.Select(
                        number => new Tuple<int, Tuple<int, int, int, int>>(int.Parse(number.ToString()), new Tuple<int, int, int, int>(0, 0, 0, 0))
                    ).ToList()
                ).ToList();

            //markVisible(data);
            //Console.WriteLine(data.Select(x => x.Where(n => n.Item2).Count()).Sum());

            for (int x = 0; x < data2.Count; x++)
            {
                for (int y = 0; y < data2[x].Count; y++)
                {
                    data2[x][y] = new Tuple<int, Tuple<int, int, int, int>>(data2[x][y].Item1, rankVisibilityScore(data2, x, y));
                }
            }
        }

        public Tuple<int, int, int, int> rankVisibilityScore(List<List<Tuple<int, Tuple<int, int, int, int>>>> data, int xStart, int yStart)
        {
            int scoreLeft = 0;
            int scoreRight = 0;
            int scoreTop = 0;
            int scoreBottom = 0;
            //left to right
            for (int i = xStart; i < data.Count -1; i++)
            {
                if (data[yStart][i].Item1 > data[yStart][i + 1].Item1)
                {
                    scoreLeft++;
                } else
                {
                    scoreLeft++;
                    break;
                }
            }
            //right to left
            for (int i = xStart; i > 0; i--)
            {
                if (data[yStart][i].Item1 > data[yStart][i - 1].Item1)
                {
                    scoreRight++;
                }
                else
                {
                    scoreRight++;
                    break;
                }
            }
            //bottom to top
            for (int i = yStart; i > 0; i--)
            {
                if (data[i][yStart].Item1 > data[i][yStart-1].Item1)
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
            for (int i = yStart; i < data.Count-1; i++)
            {
                if (data[i][yStart].Item1 > data[i][yStart+1].Item1)
                {
                    scoreBottom++;
                }
                else
                {
                    scoreBottom++;
                    break;
                }
            }

            return new Tuple<int, int, int, int>(scoreTop, scoreLeft, scoreBottom, scoreRight);
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
