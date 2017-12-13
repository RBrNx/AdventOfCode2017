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
            public int? connectedTo = null;
        }

        static void checkConnected(villageProgram program)
        {
            if (program.connectedTo == null) {
                groups.Add(new List<villageProgram> { program });
                program.connectedTo = groups.Count - 1;
            }

            if (!groups[(int)program.connectedTo].Contains(program))
            {
                groups[(int)program.connectedTo].Add(program);
            }

            for (int i = 0; i < program.directlyConnected.Count; i++)
            {
                villageProgram dcProgram = programs.Find(p => p.id == program.directlyConnected[i]);
                dcProgram.connectedTo = program.connectedTo;
                dcProgram.directlyConnected.Remove((int)program.connectedTo);
                for(int j = 0; j < groups[(int)program.connectedTo].Count; j++)
                {
                    dcProgram.directlyConnected.Remove(groups[(int)program.connectedTo][j].id);
                }
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

            while(programs.Where(p => p.connectedTo == null).Count() > 0)
            {
                checkConnected(programs.Where(p => p.connectedTo == null).First());
            }
            
            Console.WriteLine("There are " + groups[0].Count + " connected to Program 0");
            Console.WriteLine("There are " + groups.Count + " groups in total");
            Console.Read();
        }
    }
}
