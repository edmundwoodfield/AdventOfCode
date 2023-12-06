public class Days{
    public static void Day1(){
        string path = @"..\..\..\day1input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        Numbers.CalculateTotal(lines);
    }
}