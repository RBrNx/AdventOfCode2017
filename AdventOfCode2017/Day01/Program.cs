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

        public static void partOne() {
            sequence = File.ReadAllLines("../../input.txt")[0].Select(c => int.Parse(c.ToString())).ToList();
            //sequence = "91212129".Select(c => int.Parse(c.ToString())).ToList();
            int solution = 0;

            for (int i = 0; i < sequence.Count; i++)
            {
                int firstDigit = sequence[i];
                int secondDigit = 0;

                if (i == sequence.Count - 1)
                {
                    secondDigit = sequence[0];
                }
                else
                {
                    secondDigit = sequence[i + 1];
                }

                solution += digitMatch(firstDigit, secondDigit);
            }

            Console.WriteLine("The Captcha solution is: " + solution);
        }

        public static void partTwo() {
            sequence = File.ReadAllLines("../../input.txt")[0].Select(c => int.Parse(c.ToString())).ToList();
            //sequence = "12131415".Select(c => int.Parse(c.ToString())).ToList();
            int solution = 0;

            for (int i = 0; i < sequence.Count; i++)
            {
                int firstDigit = sequence[i];
                int secondDigit = 0;
                int half = sequence.Count() / 2;

                if(i + half > sequence.Count() - 1)
                {
                    int loc = (i + half) - (sequence.Count() - 1) - 1;
                    secondDigit = sequence[loc];
                }
                else
                {
                    secondDigit = sequence[i + half];
                }

                solution += digitMatch(firstDigit, secondDigit);
            }

            Console.WriteLine("The Captcha solution is: " + solution);
            Console.Read();
        }

        static void Main(string[] args)
        {
            partOne();
            partTwo();
        }
    }
}
