using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day09
{
    class Program
    {
        static void findGroupScore(string startingGroup)
        {
            Stack<int> openingBraces = new Stack<int>();
            Stack<int> openingGarbage = new Stack<int>();
            List<string> groups = new List<string>();
            List<string> garbage = new List<string>();
            bool insideGarbage = false;
            int currScore = 0;
            int garbageScore = 0;

            for(int i = 0; i < startingGroup.Length; i++)
            {
                if (startingGroup[i] == '{' && !insideGarbage)
                {
                    openingBraces.Push(i);
                }
                else if (startingGroup[i] == '}' && !insideGarbage)
                {
                    currScore += openingBraces.Count();
                    int start = openingBraces.Pop();
                    string newGroup = startingGroup.Substring(start, i - start + 1);
                    groups.Add(newGroup);
                }
                else if(startingGroup[i] == '!')
                {
                    i++;
                }
                else if(startingGroup[i] == '<' && !insideGarbage)
                {
                    openingGarbage.Push(i);
                    insideGarbage = true;
                }
                else if (startingGroup[i] == '>')
                {
                    int start = openingGarbage.Pop();
                    string newGarbage = startingGroup.Substring(start, i - start + 1);
                    garbage.Add(newGarbage);
                    insideGarbage = false;
                    garbageScore += Regex.Replace(newGarbage, @"(!.)", "").Length - 2;
                }
            }

            Console.WriteLine("The total score is: " + currScore);
            Console.WriteLine("The amount of non-canceled characters in the garbage is: " + garbageScore);
            Console.Read();
        }

        static void Main(string[] args)
        {
            string input = File.ReadAllText("../../input.txt");
            //string input = "<{o\"i!a,<{i<a>";
            findGroupScore(input);
        }
    }
}
