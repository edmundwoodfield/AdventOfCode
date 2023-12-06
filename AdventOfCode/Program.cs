using System.ComponentModel.DataAnnotations;

string path = @"..\..\..\day1input.txt";
string[] lines;
lines = File.ReadAllLines(path);
Numbers.CalculateTotal(lines);