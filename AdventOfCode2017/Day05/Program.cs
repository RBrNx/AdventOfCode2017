using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        static List<int> instructions = new List<int>();

        static void Main(string[] args)
        {
            instructions = File.ReadAllText("../../input.txt").Split(new[] { "\r\n" }, StringSplitOptions.None).Select(x => int.Parse(x)).ToList();
            int currInstruction = 0;
            int steps = 0;

            while(currInstruction >= 0 && currInstruction < instructions.Count)
            {
                int newInstruction = currInstruction + instructions[currInstruction];
                instructions[currInstruction]++;
                currInstruction = newInstruction;
                steps++;
            }

            Console.WriteLine("It took " + steps + " steps to reach the exit");

            instructions = File.ReadAllText("../../input.txt").Split(new[] { "\r\n" }, StringSplitOptions.None).Select(x => int.Parse(x)).ToList();
            currInstruction = 0;
            steps = 0;

            while (currInstruction >= 0 && currInstruction < instructions.Count)
            {
                int newInstruction = currInstruction + instructions[currInstruction];
                if (instructions[currInstruction] >= 3) instructions[currInstruction]--;
                else instructions[currInstruction]++;
                currInstruction = newInstruction;
                steps++;
            }

            Console.WriteLine("It took " + steps + " steps to reach the exit");
            Console.Read();
        }
    }
}
