using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace adventofcode22.puzzles.day7
{
    public class Day7
    {
        public Day7()
        {
            var input = File.ReadAllText(@"puzzles\day7\input1.txt");
            var dirs = prepData(input);

            part1(dirs);
            part2(dirs);
        }

        public List<AOCDirectory> prepData(string input)
        {
            //(?<=cd ).+?(?=\$ cd|$) ::match for dir (0 line = dirname, 1 line should be ignored, 2-n = entrys in dir
            
            var matches = Regex.Matches(input, @"(?<=cd ).+?(?=\$ cd|$)", RegexOptions.Singleline)
                .Select(x => x.Value.Replace("\r", "").Split("\n").Where(x => x != "").ToList()).ToList();
            foreach (var match in matches)
            {
                if (match.Count() > 1)
                {
                    match.RemoveAt(1);
                }
            }

            var dirs = new List<AOCDirectory>();
            var navigatedDirs = new Stack<AOCDirectory>();
            navigatedDirs.Push(null);
            foreach (var item in matches)
            {
                if (item.Count() > 1)
                {
                    var dir = new AOCDirectory();
                    var lastDir = navigatedDirs.Peek();
                    dir.ParentDir = lastDir;
                    dir.Name = item[0];
                    foreach (var entry in item.GetRange(1, item.Count - 1))
                    {
                        if (!entry.StartsWith("dir "))
                        {
                            dir.Files.Add(new Tuple<int, string>(Convert.ToInt32(entry.Split(" ")[0]), entry.Split(" ")[1]));
                        }
                    }

                    dirs.Add(dir);
                    if (lastDir != null)
                    {
                        lastDir.Dirs.Add(dir);
                    }
                    navigatedDirs.Push(dir);
                }
                else
                {
                    navigatedDirs.Pop();
                }
            }

            return dirs;
        }

        public void part1(List<AOCDirectory> dirs)
        {
            Console.WriteLine(dirs.Select(x => x.GetSize()).Where(x => x < 100000).Sum());
        }

        public void part2(List<AOCDirectory> dirs)
        {
            var dirTree = dirs[0];
            var diskSpace = 70_000_000;
            var usedSpace = dirTree.GetSize();
            var freeSpace = diskSpace - usedSpace;
            var needSpace = 30_000_000 - freeSpace;

            var filteredDirs = dirs.Where(x => x.GetSize() >= needSpace).Select(x => x.GetSize()).ToList();
            filteredDirs.Sort();
            Console.WriteLine(filteredDirs[0]);
        }
    }

    public class AOCDirectory 
    {
        public string Name { get; set; }
        public AOCDirectory ParentDir { get; set; }
        public List<AOCDirectory> Dirs { get; set; } = new List<AOCDirectory>();
        public List<Tuple<int, string>> Files { get; set; } = new List<Tuple<int, string>>(); // files of /
        
        public int GetSize()
        {
            return Files.Select(x => x.Item1).Sum() + Dirs.Select(x => x.GetSize()).Sum();
        }
    }
}
