public class Days{
    public static void Day1(){
        string path = @"..\..\..\day1input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        Numbers.CalculateDay1Total(lines);
    }

    public static void Day2(){
        string path = @"..\..\..\day2input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        double total = 0;
        double powerTotal = 0;
        foreach(string line in lines){
            CubeBag cubeBag = new CubeBag(line);
            powerTotal += cubeBag.powerOfColours;
            if(cubeBag.maxObservedRed <= 12 && cubeBag.maxObservedGreen <=13 && cubeBag.maxObservedBlue <= 14){
                total += cubeBag.gameId;
            }
        }
        Console.WriteLine("The total is " + total);
        Console.WriteLine("The power total is " + powerTotal);
    }
}