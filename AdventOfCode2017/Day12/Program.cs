using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day12
{
    class Program
    {
        class villageProgram
        {
            public int id;
            public List<int> directlyConnected = new List<int>();
            public bool? connectedToZero = null;
        }

        static void checkConnected(villageProgram program)
        {
            for(int i = 0; i < program.directlyConnected.Count; i++)
            {
                villageProgram dcProgram = programs.Find(p => p.id == program.directlyConnected[i]);
                dcProgram.connectedToZero = true;
                dcProgram.directlyConnected.Remove(program.id);
                checkConnected(dcProgram);
            }
        }

        static List<villageProgram> programs = new List<villageProgram>();
        static List<List<villageProgram>> groups = new List<List<villageProgram>>();

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");
            

            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"(\d+) <-> (.+)");
                villageProgram program = new villageProgram();
                program.id = int.Parse(match.Groups[1].Value);
                program.directlyConnected = match.Groups[2].Value.Split(new[] { ", " }, StringSplitOptions.None).Select(p => int.Parse(p)).ToList();
                programs.Add(program);
            }

            programs[0].connectedToZero = true;
            checkConnected(programs[0]);
            Console.WriteLine("There are " + programs.Where(p => p.connectedToZero == true).Count() + " connected to Program 0");
            Console.Read();
        }
    }
}
