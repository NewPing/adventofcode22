
namespace adventofcode22.puzzles.day3 {
    public class Day3 {

        public Day3(){
            var bags = readData(@"puzzles\day3\inputAlex.txt");
            var simpleBags = readDataSimple(@"puzzles\day3\inputAlex.txt");
            part1(bags);
            part2(simpleBags);
        }
        void part1(List<List<String>> bags){
            var prioritySum = 0;
            foreach(var bag in bags){
                foreach(var duplicate in giveDuplicates(bag)){
                    prioritySum += getPriority(duplicate);
                }
            }
            Console.WriteLine(prioritySum);
        }
        void part2(List<String> simpleBags){
            var prioritySum = 0;
            for(int i = 0;i<simpleBags.Count;i+=3){
                prioritySum += getPriority(giveBadge(simpleBags[i],simpleBags[i+1],simpleBags[i+2]));
            }
            Console.WriteLine(prioritySum);
        }
        List<List<String>> readData(string inputPath){
            var data = new List<List<string>>();
            var lines = File.ReadAllLines(inputPath);
            foreach(string line in lines){
                var compartments = new List<String>();
                compartments.Add(line.Substring(0,line.Length/2));
                compartments.Add(line.Substring(line.Length/2,line.Length/2));
                data.Add(compartments);
            }
            return data;
        }
        List<String> readDataSimple(string inputPath){
            var lines = File.ReadAllLines(inputPath);
            return new List<string>(lines);
        }
        List<Char> giveDuplicates(List<String> bag){
            var duplicates = new List<Char>();
            for(int i = 0; i<bag[0].Length;i++){
                if(bag[1].Contains(bag[0][i])){
                    duplicates.Add(bag[0][i]);
                }
            }
            var removeDuplicates = duplicates.Distinct().ToList();
            return removeDuplicates;
        }
        
        char giveBadge(String bag1, String bag2, String bag3){
            for(int i = 0;i<bag1.Length;i++){
                if(bag1.Contains(bag1[i]) && bag2.Contains(bag1[i]) && bag3.Contains(bag1[i]) ){
                    return bag1[i];
                }
            }
            return '@';
        }

        int getPriority(char arrayAbuse){
            char[] priorities = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            return Array.IndexOf(priorities,arrayAbuse) + 1;
        }

    }
}