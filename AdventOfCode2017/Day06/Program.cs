using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day06
{
    public class MemoryBank
    {
        List<int> banks;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var bank in banks)
                {
                    hash = hash * 31 + bank.GetHashCode();
                }
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MemoryBank))
                return false;

            MemoryBank mB = (MemoryBank)obj;
            return this.GetHashCode() == mB.GetHashCode();
        }

        public List<int> getMemory()
        {
            return banks;
        }

        public void distributeBlocks(int blocks, int memStart)
        {
            int currMem = memStart;

            for(int i = 0; i < blocks; i++)
            {
                if(currMem == banks.Count - 1)
                {
                    currMem = 0;
                }
                else
                {
                    currMem++;
                }

                banks[currMem]++;
            }
        }

        public MemoryBank(List<int> Banks)
        {
            banks = Banks;
        }
    }

    class Program
    {
        static string[] lines = File.ReadAllLines("../../input.txt");

        static void Main(string[] args)
        {
            MatchCollection matchList = Regex.Matches(lines[0], @"(\w+)");
            List<int> list = matchList.Cast<Match>().Select(Match => int.Parse(Match.Value)).ToList();

            MemoryBank bank = new MemoryBank(list);
            HashSet<MemoryBank> previousConfigs = new HashSet<MemoryBank>();
            int cycles = 0;

            //Part 1
            while (!previousConfigs.Contains(bank))
            {
                cycles++;
                previousConfigs.Add(bank);
                bank = new MemoryBank(new List<int>(bank.getMemory()));

                var mem = bank.getMemory();
                int index = mem.IndexOf(mem.Max());
                int blocks = mem[index];
                mem[index] = 0;

                bank.distributeBlocks(blocks, index);
            }
            Console.WriteLine("It took " + cycles + " redistribution cycles to find the infinite loop");

            previousConfigs.Clear();
            cycles = 0;

            while (!previousConfigs.Contains(bank))
            {
                cycles++;
                previousConfigs.Add(bank);
                bank = new MemoryBank(new List<int>(bank.getMemory()));

                var mem = bank.getMemory();
                int index = mem.IndexOf(mem.Max());
                int blocks = mem[index];
                mem[index] = 0;

                bank.distributeBlocks(blocks, index);
            }

            Console.WriteLine("The infinite loop is " + cycles + " long");
            Console.Read();
        }
    }
}
