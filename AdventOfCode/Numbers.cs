using System.Linq;
using System.Text.RegularExpressions;

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
    public static List<int> Day3IdentifySymbolIndices(String line){
        List<int> indexList = new List<int>();
        foreach(char character in line){
            if(!char.IsDigit(character) && !char.Equals('.',character)){
                int pos = line.IndexOf(character);
                if(pos != 0){
                    indexList.Add(pos-1);
                }
                indexList.Add(pos);
                if(pos < line.Length-1){
                    indexList.Add(pos+1);
                }
                //the following line changes the symbol to a . to make sure the next occurence of the symbol is counted
                line = line.Substring(0,pos)+"."+line.Substring(pos + 1);
            }
        }
        return indexList;
    }
    public static List<int> GetIndicesOfNumber(string number, string line){
        List<int> indices = new List<int>();
        int index1 = Regex.Match(line,"(?:^|[^0-9])("+number+")(?:$|[^0-9])").Groups[1].Index;
        indices.Add(index1);
        if(number.Length > 1){
            for(int i = 1; i < number.Length; i++){
                int indexN = index1 + i;
                indices.Add(indexN);
            }
        }
        return indices;
    }
    public static List<string> GetAllNumbersFromString(string line){
        List<string> numberList = new List<string>();
        MatchCollection matches = Regex.Matches(line,"(?:^|[^0-9])?([0-9]+)(?:$|[^0-9])?");
        foreach(Match match in matches){
            numberList.Add(match.Groups[1].Value);
        }
        return numberList;
    }
    public static List<List<int>> GetIndexLists(string[] lines){
        List<List<int>> indexList = new List<List<int>>();
        foreach(string line in lines){
            List<int> indices = Day3IdentifySymbolIndices(line);
            indexList.Add(indices);
        }
        List<List<int>> mergedIndices = new List<List<int>>();
        foreach(List<int> lineIndices in indexList){
            List<int> mergedLineIndices = new List<int>();
            mergedLineIndices = lineIndices;
            int lineIndex = indexList.IndexOf(lineIndices);
            if (lineIndex >0){
                List<int> lineAbove = indexList[lineIndex - 1];
                mergedLineIndices = mergedLineIndices.Concat(lineAbove).ToList();
            }
            if (lineIndex < lines.Length - 1){
                List<int> lineBelow = indexList[lineIndex + 1];
                mergedLineIndices = mergedLineIndices.Concat(lineBelow).ToList();
            }
        mergedIndices.Add(mergedLineIndices);
        }
        return mergedIndices;


    }
    public static List<int> FilterToNumbersWithValidIndices(string line, List<int> validIndices){
        List<string> numberList = GetAllNumbersFromString(line);
        List<int> validNumbers = new List<int>();
        foreach(string number in numberList){
            bool indexMatches = false;
            List<int> numberIndices = GetIndicesOfNumber(number,line);
            foreach(int index in numberIndices){
                if(validIndices.Contains(index)){
                    indexMatches = true;
                    //the following loop removes the number to prevent a later invalid occurence of the same number counting
                    for(int i = 0; i < numberIndices.Count; i++){
                    line = line.Substring(0,numberIndices[i])+"."+line.Substring(numberIndices[i] + 1);}
                    break;
                }
            }
            if (indexMatches){
                validNumbers.Add(int.Parse(number));
            }            
        }
        return validNumbers;
    }
}