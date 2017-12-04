using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    class Program
    {
        static string[] lines = File.ReadAllLines("input.txt");

        static int findDivisor(List<int> numbers)
        {
            int divisorResult = 0;

            for(int i = 0; i < numbers.Count; i++)
            {
                for(int j = 0; j < numbers.Count(); j++)
                {
                    if (numbers[i] == numbers[j]) continue;
                    if (numbers[i] % numbers[j] == 0) divisorResult = numbers[i] / numbers[j];
                }
            }

            return divisorResult;
        }

        static void PartOne() {           
            int checksum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection matchList = Regex.Matches(lines[i], @"(\d+)");
                List<int> row = matchList.Cast<Match>().Select(Match => int.Parse(Match.Value)).ToList();

                checksum += (row.Max() - row.Min());
            }

            Console.WriteLine("Checksum is: " + checksum);
        }

        static void PartTwo() {
            int resultsSum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection matchList = Regex.Matches(lines[i], @"(\d+)");
                List<int> row = matchList.Cast<Match>().Select(Match => int.Parse(Match.Value)).ToList();

                resultsSum += findDivisor(row);
            }

            Console.WriteLine("Sum of the Results is: " + resultsSum);
        }

        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
            Console.Read();
        }
    }
}