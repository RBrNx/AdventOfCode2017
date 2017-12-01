using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day01
{
    class Program
    {
        public static List<int> sequence = new List<int>();

        public static int digitMatch(int a, int b)
        {
            if (a == b) return a;
            else return 0;
        }

        public static int findCaptchaSol(List<int> input, int nextDigitLoc)
        {
            int solution = 0;

            for (int i = 0; i < input.Count; i++)
            {
                int firstDigit = input[i];
                int secondDigit = 0;

                if (i + nextDigitLoc > input.Count() - 1)
                {
                    int loc = (i + nextDigitLoc) - (input.Count() - 1) - 1;
                    secondDigit = input[loc];
                }
                else
                {
                    secondDigit = input[i + nextDigitLoc];
                }

                solution += digitMatch(firstDigit, secondDigit);
            }

            return solution;
        }

        static void Main(string[] args)
        {
            List<int> sequence = File.ReadAllLines("../../input.txt")[0].Select(c => int.Parse(c.ToString())).ToList();
            Console.WriteLine("Part 1 Solution: " + findCaptchaSol(sequence, 1));
            Console.WriteLine("Part 2 Solution: " + findCaptchaSol(sequence, sequence.Count() / 2));
            Console.Read();
        }
    }
}
