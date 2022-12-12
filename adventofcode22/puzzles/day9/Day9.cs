namespace adventofcode22.puzzles.day9{

public class Day9{
    static List<Tuple<int,int>> positions;
    public Day9(){
        var lines = File.ReadAllLines(@"puzzles\day9\input1.txt");
        List<Tuple<char,int>> data = lines.Select(line => new Tuple<char,int>(line.Split(" ")[0].ToCharArray()[0],int.Parse(line.Split(" ")[1]))).ToList();
        part1(data);
        part2(data);
    }
    public void part1(List<Tuple<char,int>> data){
        positions = new List<Tuple<int,int>>();
        var head = new Rope(2).head;
        foreach(var tuple in data){
            moveHead(tuple.Item1,tuple.Item2,head);
        }
        Console.WriteLine(positions.Count +1 );
    }
    
    public void part2(List<Tuple<char,int>> data){
        positions = new List<Tuple<int,int>>();
        var head = new Rope(10).head;
        foreach(var tuple in data){
            moveHead(tuple.Item1,tuple.Item2,head);
        }
        // https://www.reddit.com/r/adventofcode/comments/ziapy8/2022_day_9_part_2_diagonal_rules/?sort=new too lazy
        Console.WriteLine(positions.Count +1 );
    }
    public void moveHead(char direction, int amount ,RopePiece head){
        //Move head in steps, calculate tail position after steps
        for(int steps = 0; steps<amount ; steps++){
            switch(direction){
                case 'U':
                    head.moveUp();
                    break;
                case 'D':
                    head.moveDown();
                    break;
                case 'R':
                    head.moveRight();
                    break;
                case 'L':
                    head.moveLeft();
                    break;
            }
        }
    }
    public static void trackPosition(int xPos,int yPos){
        var position = new Tuple<int,int>(xPos,yPos);
        if( !(positions.Select(tuple => tuple.Equals(position)).Contains(true))){
            positions.Add(position);
        }
    }

}
    public class Rope{

        public RopePiece head;
        public RopePiece tail;

        public Rope(int pieces){
            for(int i = 0;i<pieces;i++){
                addRopePiece();
            }
        }
        public void addRopePiece(){
            RopePiece newRopePiece = new RopePiece();
            if(head == null){
                head = newRopePiece;
            }
            else if(tail == null){
                tail = newRopePiece;
                tail.head = head;
                head.tail = tail;
            }
            else{
                newRopePiece.tail = head;
                head.head = newRopePiece;
                head = newRopePiece;
            }
        }
    }
    public class RopePiece{
        private int xPos;
        private int yPos;

        private Tuple<int,int> lastPosition;
        int XPos
        {
            get{return xPos;}
            set{
                lastPosition = new Tuple<int,int>(xPos,yPos);
                xPos = value;
                if(!(tail == null)){
                    tail.notify();
                }
            }
        }
        int YPos
        {   
            get{return yPos;}
            set{
                lastPosition = new Tuple<int,int>(xPos,yPos);
                yPos = value;
                if(!(tail == null)){
                    tail.notify();
                }
            }
        }
        public RopePiece tail{get;set;}
        public RopePiece head{get;set;}
        public RopePiece(){
            xPos = 0;
            yPos = 0;
        }
        private void notify(){
            if(!headStillThere()){
                XPos = head.lastPosition.Item1;
                YPos = head.lastPosition.Item2;
                if ((tail==null)){
                    Day9.trackPosition(xPos,yPos);
                }
            }
        }
        private Boolean headStillThere(){
            if( Math.Abs(xPos-head.xPos) > 1 || Math.Abs(yPos-head.yPos) >1){
                return false;
            }
            return true;
        }
        public void moveRight(){
            XPos += 1;
        }
        public void moveLeft(){
            XPos -= 1;
        }
        public void moveUp(){
            YPos+= 1;
        }
        public void moveDown(){
            YPos-= 1;
        }
    
    }


}