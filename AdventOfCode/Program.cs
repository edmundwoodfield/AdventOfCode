using System.ComponentModel.DataAnnotations;

string path = @"C:\Users\edmun\Documents\AdventOfCode\AdventOfCode\day1input.txt";
string[] lines;
lines = File.ReadAllLines(path);
Numbers.CalculateTotal(lines);