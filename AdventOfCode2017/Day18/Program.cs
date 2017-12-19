using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day18
{
    class Program
    {
      
        static void PartOne() {
            string[] instructions = File.ReadAllLines("../../input.txt");
            Dictionary<string, long> registers = new Dictionary<string, long>();
            List<long> sounds = new List<long>();
            bool firstRecover = true;

            for (int i = 0; i < instructions.Length; i++)
            {
                string[] inst = instructions[i].Split(' ');
                string register = inst[1];
                if (!registers.ContainsKey(register)) registers.Add(register, 0);

                long X = -1;
                long Y = -1;

                if (!long.TryParse(inst[1], out X)) X = registers[inst[1]];
                if (inst.Length > 2) if (!long.TryParse(inst[2], out Y)) Y = registers[inst[2]];

                switch (inst[0])
                {
                    case "set":
                        registers[register] = Y;
                        break;
                    case "add":
                        registers[register] += Y;
                        break;
                    case "mul":
                        registers[register] *= Y;
                        break;
                    case "mod":
                        registers[register] = X % Y;
                        break;
                    case "rcv":
                        if (registers[register] != 0)
                        {
                            if (firstRecover)
                            {
                                firstRecover = false;
                                Console.WriteLine("The last sound played had a value of: " + sounds.Last());
                                return;
                            }
                        }
                        break;
                    case "jgz":
                        if (X > 0)
                        {
                            i += (int)Y - 1;
                            if (i > instructions.Length || i < 0) return;
                        }
                        break;
                    case "snd":
                        sounds.Add(registers[register]);
                        break;
                }
            }

            Console.WriteLine("Finished!");
        }

        static void PartTwo() {
            string[] instructions = File.ReadAllLines("../../input.txt");
            int programOneSends = 0;
            //Queue<long>[] programQueue = new Queue<long>[2];
            Queue<long>[] programQueue = Enumerable.Range(0, 2).Select(p => new Queue<long>()).ToArray();
            Dictionary<string, long>[] programRegisters = Enumerable.Range(0, 2).Select(p => new Dictionary<string, long>() { { "p", p } }).ToArray();
            int[] programIndex = new int[2] { 0, 0 };
            bool[] programRunning = new bool[2] { true, true };
            int activeProgram = 0;

            for (int i = 0; i < instructions.Length; i++)
            {
                activeProgram = 1 - activeProgram;
                if (!programRunning[activeProgram])
                {
                    if (!programRunning[1 - activeProgram]) break;
                    continue;
                }
                i = programIndex[activeProgram];
                string[] inst = instructions[i].Split(' ');
                string register = inst[1];
                if (!programRegisters[activeProgram].ContainsKey(register)) programRegisters[activeProgram].Add(register, 0);

                long X = -1;
                long Y = -1;

                if (!long.TryParse(inst[1], out X)) X = programRegisters[activeProgram][inst[1]];
                if (inst.Length > 2) if (!long.TryParse(inst[2], out Y)) Y = programRegisters[activeProgram][inst[2]];

                switch (inst[0])
                {
                    case "set":
                        programRegisters[activeProgram][register] = Y;
                        break;
                    case "add":
                        programRegisters[activeProgram][register] += Y;
                        break;
                    case "mul":
                        programRegisters[activeProgram][register] *= Y;
                        break;
                    case "mod":
                        programRegisters[activeProgram][register] = X % Y;
                        break;
                    case "rcv":
                        if (programQueue[activeProgram].Count() == 0)
                        {
                            programRunning[activeProgram] = false;
                            programIndex[activeProgram]--;
                        }
                        else programRegisters[activeProgram][register] = programQueue[activeProgram].Dequeue();
                        break;
                    case "jgz":
                        if (X > 0)
                        {
                            programIndex[activeProgram] += (int)Y - 1;
                            if (programIndex[activeProgram] > instructions.Length || programIndex[activeProgram] < 0) programRunning[activeProgram] = false;
                        }
                        break;
                    case "snd":
                        programQueue[1 - activeProgram].Enqueue(X);
                        programRunning[1 - activeProgram] = true;
                        if (activeProgram == 1) programOneSends++;
                        break;
                }

                programIndex[activeProgram]++;
            }

            Console.WriteLine("Program 1 sent " + programOneSends + " values");
        }

        static void Main(string[] args)
        {
            PartTwo();
            Console.Read();
        }
    }
}
