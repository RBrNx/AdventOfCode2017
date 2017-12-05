using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day04
{
    class Program
    {
        static string[] lines = File.ReadAllLines("../../input.txt");

        static void PartOne() {
            int validCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection matchList = Regex.Matches(lines[i], @"(\w+)");
                List<string> row = matchList.Cast<Match>().Select(Match => Match.Value).ToList();

                if (!row.GroupBy(x => x).Any(g => g.Count() > 1))
                {
                    validCount++;
                }
            }

            Console.WriteLine("There are " + validCount + " valid Passphrases");
        }

        static void PartTwo() {
            int validCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection matchList = Regex.Matches(lines[i], @"(\w+)");
                List<string> row = matchList.Cast<Match>().Select(Match => Match.Value).ToList();

                bool valid = true;
                for(int a = 0; a < row.Count; a++)
                {
                    for (int b = 0; b < row.Count; b++)
                    {
                        if(a != b)
                        {
                            if(row[a].OrderBy(c => c).SequenceEqual(row[b].OrderBy(c => c)))
                            {
                                valid = false;
                            }
                        }
                    }
                }

                if (valid) validCount++;
            }

            Console.WriteLine("There are " + validCount + " valid Passphrases");
            Console.Read();
        }

        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}
