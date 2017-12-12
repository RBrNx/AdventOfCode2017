using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> instructions = File.ReadAllText("../../input.txt").Split(',').ToList();
            Tuple<int, int, int> curr = Tuple.Create(0, 0, 0);
            List<int> dists = new List<int>();

            for (int i = 0; i < instructions.Count; i++)
            {
                switch (instructions[i])
                {
                    case "n":
                        curr = Tuple.Create(curr.Item1, curr.Item2 + 1, curr.Item3 - 1);
                        break;
                    case "ne":
                        curr = Tuple.Create(curr.Item1 + 1, curr.Item2, curr.Item3 - 1);
                        break;
                    case "se":
                        curr = Tuple.Create(curr.Item1 + 1, curr.Item2 - 1, curr.Item3);
                        break;
                    case "s":
                        curr = Tuple.Create(curr.Item1, curr.Item2 - 1, curr.Item3 + 1);
                        break;
                    case "sw":
                        curr = Tuple.Create(curr.Item1 - 1, curr.Item2, curr.Item3 + 1);
                        break;
                    case "nw":
                        curr = Tuple.Create(curr.Item1 - 1, curr.Item2 + 1, curr.Item3);
                        break;
                }

                dists.Add((Math.Abs(curr.Item1) + Math.Abs(curr.Item2) + Math.Abs(curr.Item3)) / 2);
            }

            Console.WriteLine((Math.Abs(curr.Item1) + Math.Abs(curr.Item2) + Math.Abs(curr.Item3)) / 2 + " is the fewest number of steps to find the child process");
            Console.WriteLine("The furthest he gets from the starting position is: " + dists.Max());
        }
    }
}
