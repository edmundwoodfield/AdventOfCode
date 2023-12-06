public class Numbers {
public static double CalculateTotal(String[] lines){
double total = 0;
List<String> numerals = new List<string>();
foreach (string line in lines){
            string lineDigits = "";
            string lineLetters = "";
    foreach (char character in line){
                if (char.IsDigit(character)){
                    CheckLineLetters(ref lineDigits,ref lineLetters);
                    lineLetters = "";
                    lineDigits = string.Concat(lineDigits,character);
                     }
                else {
                    lineLetters = string.Concat(lineLetters, character);
            }
    CheckLineLetters(ref lineDigits,ref lineLetters);
    }
numerals.Add(lineDigits);
    }
    foreach(string numeral in numerals){
    char left = numeral[0];
    char right = numeral[numeral.Length-1];
    double tens = char.GetNumericValue(left)*10;
    double units = char.GetNumericValue(right);
    total += tens + units;
    }
Console.WriteLine("the total is " + total);
return total;
}

    private static void CheckLineLetters(ref string lineDigits, ref string lineLetters)
    {
        //this currently repeats the text numeral until the next numeral is found or a later text numeral is found
        //this does not affect the result because the first and last digits are correct
        if (lineLetters.Contains("one"))
        {
            int pos = lineLetters.IndexOf("one");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("one", "");
            lineDigits = string.Concat(lineDigits, "1");
        }
        if (lineLetters.Contains("two"))
        {int pos = lineLetters.IndexOf("two");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("two", "");
            lineDigits = string.Concat(lineDigits, "2");
        }
        if (lineLetters.Contains("three"))
        {int pos = lineLetters.IndexOf("three");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("three", "");
            lineDigits = string.Concat(lineDigits, "3");
        }
        if (lineLetters.Contains("four"))
        {int pos = lineLetters.IndexOf("four");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("four", "");
            lineDigits = string.Concat(lineDigits, "4");
        }
        if (lineLetters.Contains("five"))
        {int pos = lineLetters.IndexOf("five");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("five", "");
            lineDigits = string.Concat(lineDigits, "5");
        }
        if (lineLetters.Contains("six"))
        {int pos = lineLetters.IndexOf("six");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("six", "");
            lineDigits = string.Concat(lineDigits, "6");
        }
        if (lineLetters.Contains("seven"))
        {int pos = lineLetters.IndexOf("seven");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("seven", "");
            lineDigits = string.Concat(lineDigits, "7");
        }
        if (lineLetters.Contains("eight"))
        {int pos = lineLetters.IndexOf("eight");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("eight", "");
            lineDigits = string.Concat(lineDigits, "8");
        }
        if (lineLetters.Contains("nine"))
        {int pos = lineLetters.IndexOf("nine");
            lineLetters = lineLetters.Remove(0,pos);
            // lineLetters = lineLetters.Replace("nine", "");
            lineDigits = string.Concat(lineDigits, "9");
        }
    }

    
}