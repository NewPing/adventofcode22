namespace adventofcode22.puzzles.day12
{
    public class Day12{
        int[][] groundMap;
        public Day12(){
            var input = File.ReadAllLines(@"puzzles\day12\input1.txt");
           // groundMap = input.Select( line => line.Select(character => getHeight(character) ).ToArray()).ToArray();
           Tuple<int,int> startPos = null;
           Tuple<int,int> endPos = null;
           groundMap = new int[input.Count()][];
            for(int y = 0;y<input.Count();y++){
                var line = input[y];
                groundMap[y] = new int[line.Count()];
                for(int x = 0;x < line.Length;x++){
                    groundMap[y][x] = getHeight(line[x]);
                    if(line[x].Equals('E')){
                        endPos = new Tuple<int,int>(y,x);
                    }
                    if(line[x].Equals('S')){
                        startPos = new Tuple<int,int>(y,x);
                    }
                } 
            }
            var result = findShortestRoute(startPos,endPos) -3;
            Console.WriteLine(result);
        }
        private int getHeight(char arrayAbuse){
            if(arrayAbuse.Equals('S')){
                arrayAbuse = 'a';
            }
            if(arrayAbuse.Equals('E')){
                arrayAbuse = 'z';
            }
            char[] priorities = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
            return Array.IndexOf(priorities,arrayAbuse) + 1;
        }

        private int findShortestRoute(Tuple<int,int> startPos, Tuple<int,int> endPos){
            var paths = new List<Stack<Tuple<int,int>>>();
            var visitedPos = new List<Tuple<int,int>>();
            paths.Add(new Stack<Tuple<int,int>>());
            paths[0].Push(startPos);
            Boolean found = false;
            while(true){
                paths = paths.Where(path => path is not null).ToList();
                if(paths.Count <1 ){
                    break;
                }
                for(int i = 0; i<paths.Count();i++){
                    var path = paths[i];
                    if(path.Peek().Equals(endPos)){
                        return path.Count;
                    }
                    var positions =  new List<Tuple<int,int>>();
                    var preFilterPos = getAvailablePositions(path.Peek());

                    for(int z = 0 ;z<preFilterPos.Count;z++){
                        if( !(path.Contains(preFilterPos[z])) && !(visitedPos.Contains(preFilterPos[z])) ){
                            positions.Add(preFilterPos[z]);
                        }
                    }
                    if(positions.Count < 1){
                        paths[i] = null;
                        continue;
                    }
                    if(positions.Count > 1){
                        for(int z = 1; z < positions.Count ; z++){
                            var newPath = new Stack<Tuple<int,int>>(paths[i].Reverse());
                            newPath.Push(positions[z]);
                            paths.Add(newPath);
                            visitedPos.Add(positions[z]);
                        }
                    }
                    paths[i].Push(positions[0]);
                    visitedPos.Add(positions[0]);
                }
            }
            return -1;
        }


        private Tuple<int,int> giveCharPosition(char pChar){
            for(int i=0;i<groundMap.Count();i++){
                for(int z = 0;z<groundMap[i].Count();z++){
                    if(groundMap[i][z] == getHeight(pChar) ){
                        return new Tuple<int,int>(i,z);
                    }
                }
            }
            return null;
        }

        private List<Tuple<int,int>> getAvailablePositions(Tuple<int,int> startPos){
            var availablePositions = new List<Tuple<int, int>>();
            //Up
            if(startPos.Item1 >0 ){
                if(groundMap[startPos.Item1 -1][startPos.Item2] - groundMap[startPos.Item1][startPos.Item2] <= 1 ){
                    availablePositions.Add(new Tuple<int,int>(startPos.Item1-1,startPos.Item2));
                }
            }
            //Down
            if(startPos.Item1 < groundMap.Count() -1 ){
                if(groundMap[startPos.Item1 +1][startPos.Item2] - groundMap[startPos.Item1][startPos.Item2] <= 1 ){
                    availablePositions.Add(new Tuple<int,int>(startPos.Item1+1,startPos.Item2));
                }
            }
            //Left
            if(startPos.Item2 > 0 ){
                if(groundMap[startPos.Item1][startPos.Item2-1] - groundMap[startPos.Item1][startPos.Item2] <= 1 ){
                    availablePositions.Add(new Tuple<int,int>(startPos.Item1,startPos.Item2-1));
                }
            }
            //Right
            if(startPos.Item2 < groundMap[0].Count() -1){
                if(groundMap[startPos.Item1][startPos.Item2+1] - groundMap[startPos.Item1][startPos.Item2] <= 1 ){
                    availablePositions.Add(new Tuple<int,int>(startPos.Item1,startPos.Item2+1));
                }
            }
            return availablePositions;
        }


    }
}