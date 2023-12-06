public class Numbers {
public static double CalculateDay1Total(String[] lines){
double total = 0;
List<String> numerals = new List<string>();
        foreach (string line in lines) numerals.Add(GenerateNumberString(line));
        foreach (string numeral in numerals) total += GenerateLeftRightNumber(numeral);
        Console.WriteLine("the total is " + total);
return total;
}

    private static double GenerateLeftRightNumber(string numeral)
    {
            char left = numeral[0];
            char right = numeral[numeral.Length - 1];
            double tens = char.GetNumericValue(left) * 10;
            double units = char.GetNumericValue(right);
            return tens + units;
    }

    private static string GenerateNumberString(string line)
    {
        string lineDigits = "";
        string lineLetters = "";
        foreach (char character in line)
        {
            if (char.IsDigit(character))
            {
                CheckLineLetters(ref lineDigits, ref lineLetters);
                lineLetters = "";
                lineDigits = string.Concat(lineDigits, character);
            }
            else
            {
                lineLetters = string.Concat(lineLetters, character);
            }
            CheckLineLetters(ref lineDigits, ref lineLetters);
        }
        return lineDigits;
    }

    private static void CheckLineLetters(ref string lineDigits, ref string lineLetters)
    {
        //this currently repeats the text numeral until the next numeral is found or a later text numeral is found
        //this does not affect the result because the first and last digits are correct
        string[] numberWords = {"one","two","three","four","five","six","seven","eight","nine"};
        foreach(string numberWord in numberWords){
            if(lineLetters.Contains(numberWord)){
                int pos = lineLetters.IndexOf(numberWord);
                lineLetters = lineLetters.Remove(0,pos);
                lineDigits = string.Concat(lineDigits,(Array.FindIndex(numberWords, x => x.Contains(numberWord))+1));
            }
        }
    }
}