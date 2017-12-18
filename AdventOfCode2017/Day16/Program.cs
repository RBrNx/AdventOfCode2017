using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace Day16
{
    class Program
    {
        private static void spin(ref string[] programs, int count)
        {
            string[] newArr = (string[])programs.Clone();
            for(int loop = 0; loop < count; loop++)
            {
                for (int i = 0; i < programs.Length; i++)
                {
                    newArr[(i + 1) % newArr.Length] = programs[i];
                }
                programs = (string[])newArr.Clone();
            }
        }

        static void exchange(ref string[] programs, int posA, int posB)
        {
            string A = programs[posA];
            programs[posA] = programs[posB];
            programs[posB] = A;
        }

        static void partner(ref string[] programs, string nameA, string nameB)
        {
            int posA = Array.IndexOf(programs, nameA);
            int posB = Array.IndexOf(programs, nameB);
            exchange(ref programs, posA, posB);
        }

        static void printPrograms(string[] programs)
        {
            for (int i = 0; i < programs.Count(); i++)
            {
                Console.Write(programs[i]);
            }
            Console.WriteLine("");
        }

        static void performDance(ref string[] programs, List<string> danceMoves)
        {
            for (int i = 0; i < danceMoves.Count; i++)
            {
                string dMove = danceMoves[i];

                if (dMove[0] == 's')
                {
                    int spinCount = int.Parse(dMove.Substring(1));
                    spin(ref programs, spinCount);
                }
                else if (dMove[0] == 'x')
                {
                    int[] programsPos = dMove.Substring(1).Split('/').Select(x => int.Parse(x)).ToArray();
                    exchange(ref programs, programsPos[0], programsPos[1]);
                }
                else if (dMove[0] == 'p')
                {
                    string[] programNames = dMove.Substring(1).Split('/');
                    partner(ref programs, programNames[0], programNames[1]);
                }
            }
        }

        static void Main(string[] args)
        {
            string[] programs = Enumerable.Range('a', 16).Select(x => ((char)x).ToString()).ToArray();
            string[] programsCopy = (string[])programs.Clone();
            List<string> danceMoves = File.ReadAllText("../../input.txt").Split(',').ToList();
            Dictionary<int, int> exchanges = new Dictionary<int, int>();
            int backToStart = 1;

            performDance(ref programsCopy, danceMoves);
            printPrograms(programsCopy);

            while (!Enumerable.SequenceEqual(programsCopy, programs))
            {
                backToStart++;
                performDance(ref programsCopy, danceMoves);               
            }

            for(int i = 0; i < (1000000000 % backToStart); i++)
            {
                performDance(ref programsCopy, danceMoves);
            }
            
            printPrograms(programsCopy);
            Console.Read();
        }
    }
}
