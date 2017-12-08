using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Day08
{
    class Program
    {
        static string[] lines = File.ReadAllLines("../../input.txt");
        static Dictionary<string, int> registers = new Dictionary<string, int>();

        static void executeInstruction(string register, string instruction, int amount)
        {
            if (!registers.ContainsKey(register))
            {
                registers.Add(register, 0);
            }

            if (instruction == "inc")
            {
                registers[register] += amount;
            }
            else if(instruction == "dec")
            {
                registers[register] -= amount;
            }
        }

        static bool conditionIsValid(string register, string condition, int value) {
            if (!registers.ContainsKey(register))
            {
                registers.Add(register, 0);
            }

            switch (condition)
            {
                case ">":
                    if (registers[register] > value) return true;
                    return false;
                case "<":
                    if (registers[register] < value) return true;
                    return false;
                case ">=":
                    if (registers[register] >= value) return true;
                    return false;
                case "<=":
                    if (registers[register] <= value) return true;
                    return false;
                case "==":
                    if (registers[register] == value) return true;
                    return false;
                case "!=":
                    if (registers[register] != value) return true;
                    return false;
                default:
                    return false;
            }
        }

        static KeyValuePair<string, int> largestRegister(Dictionary<string, int> registers)
        {
            return registers.First(x => x.Value == registers.Values.Max());
        }

        static void Main(string[] args)
        {
            int highestValue = 0;

            for(int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"(\w+)\s(\w+)\s(-?\d+)\sif (\w+)\s(\W+)\s(-?\d+)");
                string register = match.Groups[1].Value;
                string instruction = match.Groups[2].Value;
                int amount = int.Parse(match.Groups[3].Value);
                string conditionRegister = match.Groups[4].Value;
                string condition = match.Groups[5].Value;
                int conditionValue = int.Parse(match.Groups[6].Value);

                if(conditionIsValid(conditionRegister, condition, conditionValue))
                {
                    executeInstruction(register, instruction, amount);
                }

                if (largestRegister(registers).Value > highestValue) highestValue = largestRegister(registers).Value;
            }

            Console.WriteLine("The largest register is " + largestRegister(registers).Key + " with a value of " + largestRegister(registers).Value);
            Console.WriteLine("The highest value in any register was: " + highestValue);
            Console.Read();
        }
    }
}
