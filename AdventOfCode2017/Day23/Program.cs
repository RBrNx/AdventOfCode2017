using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day23
{
    class Program
    {
        public static bool IsPrime(long number)
        {
            if (number == 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (long)Math.Floor(Math.Sqrt(number));

            for (long i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        static void performSubroutine(Dictionary<string, long> pR)
        {
           // long b = 105700;
            long c = 122700;
            long h = 0;

            for(long b = 105700; b <= c; b += 17)
            {
                if (!IsPrime(b)) h++;
            }
            Console.WriteLine("The final value in register H is " + h);
        }

        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines("../../input.txt");
            Dictionary<string, long> programRegisters = new Dictionary<string, long>();
            int mulCount = 0;
            bool debugMode = false;

            if (!debugMode)
            {
                programRegisters.Add("a", 1);
                performSubroutine(programRegisters);
            }
            else
            {
                for (int i = 0; i < instructions.Length; i++)
                {
                    string[] inst = instructions[i].Split(' ');
                    string register = inst[1];
                    if (!programRegisters.ContainsKey(register)) programRegisters.Add(register, 0);

                    long X = -1;
                    long Y = -1;

                    if (!long.TryParse(inst[1], out X)) X = programRegisters[inst[1]];
                    if (inst.Length > 2) if (!long.TryParse(inst[2], out Y)) Y = programRegisters[inst[2]];

                    switch (inst[0])
                    {
                        case "set":
                            programRegisters[register] = Y;
                            break;
                        case "sub":
                            programRegisters[register] -= Y;
                            break;
                        case "mul":
                            programRegisters[register] *= Y;
                            mulCount++;
                            break;
                        case "jnz":
                            if (X != 0) i += (int)Y - 1;
                            break;
                    }
                }
            }

            Console.WriteLine("The mul instruction is called " + mulCount + " times");
            
            Console.Read();
        }
    }
}
