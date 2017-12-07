using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day07
{
    public class ProgramDisc
    {
        public string name;
        public string parent;
        public int weight;
        public List<string> children;
    }

    class Program
    {
        static string[] lines = File.ReadAllLines("../../input.txt");
        static List<ProgramDisc> programTower = new List<ProgramDisc>();

        static void Main(string[] args)
        {
            for(int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"(\w+)\s\((\d+)\)(?:\s->\s(.+))?");
                ProgramDisc disc = new ProgramDisc();
                disc.name = match.Groups[1].Value;
                disc.weight = int.Parse(match.Groups[2].Value);
                disc.parent = null;
                if(match.Groups[3].Value != "") disc.children = match.Groups[3].Value.Split(new string[] { ", " }, StringSplitOptions.None).ToList();
                programTower.Add(disc);
            }

            foreach(ProgramDisc program in programTower)
            {
                if(program.children != null)
                {
                    foreach(string heldProgram in program.children)
                    {
                        programTower.Find(x => x.name == heldProgram).parent = program.name;
                    }
                }
            }

            Console.WriteLine("The name of the bottom program is: " + programTower.Find(x => x.parent == null).name);
            Console.Read();
        }
    }
}
