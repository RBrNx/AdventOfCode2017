using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            Regex inputRegex = new Regex(@"(\d+)");
            int checksum = 0;

            for(int i = 0; i < lines.Length; i++)
            {
                MatchCollection matchList = Regex.Matches(lines[i], @"(\d+)");
                List<int> row = matchList.Cast<Match>().Select(Match => int.Parse(Match.Value)).ToList();

                checksum += (row.Max() - row.Min());
            }

            Console.WriteLine("Checksum is: " + checksum);
            Console.Read();
        }
    }
}