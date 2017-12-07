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
        public int weightIncludingChildren;
        public List<string> children;
    }

    class Program
    {
        static string[] lines = File.ReadAllLines("../../input.txt");
        static List<ProgramDisc> programTower = new List<ProgramDisc>();

        static ProgramDisc findDisc(string name)
        {
            return programTower.Find(x => x.name == name);
        }

        static int CalculateWeight(string name)
        {
            ProgramDisc disc = findDisc(name);
            if(disc.children != null)
            {
                List<int> childrenWeights = new List<int>();
                for(int i = 0; i < disc.children.Count; i++)
                {
                    int weight = CalculateWeight(disc.children[i]);
                    childrenWeights.Add(weight);
                    disc.weightIncludingChildren += weight;
                }

                if (childrenWeights.FindAll(x => x == childrenWeights[0]).Count != childrenWeights.Count)
                {
                    Console.WriteLine(disc.name + " has children which are unbalanced");
                    for(int i = 0; i < disc.children.Count; i++)
                    {
                        Console.WriteLine("{0} has a combined weight of {1}", disc.children[i], childrenWeights[i]);
                    }
                    var zipped = disc.children.Zip(childrenWeights, (a, b) => new { name = a, weight = b });
                    var oddChild = zipped.GroupBy(x => x.weight)
                                    .Where(g => g.Count() == 1)
                                    .Select(grp => new { weight = grp.Key, name = grp.Select(x => x.name).First() })
                                    .First();
                    var goodChild = zipped.GroupBy(x => x.weight)
                                    .Where(g => g.Count() > 1)
                                    .Select(grp => new { weight = grp.Key, name = grp.Select(x => x.name).First() })
                                    .First();

                    var difference = oddChild.weight - goodChild.weight;
                    if (difference < 0) Console.WriteLine(oddChild.name + " should be heavier by " + Math.Abs(difference) + " and would weigh " + (findDisc(oddChild.name).weight - Math.Abs(difference)) + " instead of " + findDisc(oddChild.name).weight);
                    else if (difference > 0) Console.WriteLine(oddChild.name + " should be lighter by " + difference + " and would weigh " + (findDisc(oddChild.name).weight - difference) + " instead of " + findDisc(oddChild.name).weight);
                    Console.WriteLine("");
                }

                return disc.weightIncludingChildren += disc.weight;
            }
            else
            {
                return disc.weight;
            }
        }

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

            foreach (ProgramDisc program in programTower)
            {
                if (program.children != null)
                {
                    foreach (string heldProgram in program.children)
                    {
                        programTower.Find(x => x.name == heldProgram).parent = program.name;
                    }
                }
            }

            string bottomProgram = programTower.Find(x => x.parent == null).name;
            Console.WriteLine("The name of the bottom program is: " + bottomProgram);
            Console.WriteLine("");
            CalculateWeight(bottomProgram);
            Console.Read();
        }
    }
}
