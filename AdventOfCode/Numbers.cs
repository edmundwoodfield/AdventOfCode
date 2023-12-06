public class Numbers {
public static double CalculateTotal(String[] lines){
double total = 0;
List<String> numerals = new List<string>();
foreach (string line in lines){
            string lineDigits = "";
    foreach (char character in line){
        if(char.IsDigit(character)){
            lineDigits = string.Concat(lineDigits,character);
        }
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

}