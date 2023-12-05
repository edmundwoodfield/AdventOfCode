using System.ComponentModel.DataAnnotations;

string path = @"C:\Users\edmun\Documents\AdventOfCode\AdventOfCode\day1input.txt";
string[] lines;
lines = File.ReadAllLines(path);
double total = 0;
List<String> numerals = new List<string>();

PopulateListOfNumerals(lines, numerals);

total = AddFirstAndLastDigitsAsTensAndUnits(total, numerals);
Console.WriteLine("the total is " + total);

static void PopulateListOfNumerals(string[] lines, List<string> numerals)
{
    foreach (string line in lines)
    {
        string lineDigits = "";
        foreach (char character in line)
        {
            if (char.IsDigit(character))
            {
                lineDigits = string.Concat(lineDigits, character);
            }
        }
        numerals.Add(lineDigits);
    }
}

static double AddFirstAndLastDigitsAsTensAndUnits(double total, List<string> numerals)
{
    foreach (string numeral in numerals)
    {
        char left = numeral[0];
        char right = numeral[numeral.Length - 1];
        double tens = char.GetNumericValue(left) * 10;
        double units = char.GetNumericValue(right);
        total += tens + units;
    }

    return total;
}