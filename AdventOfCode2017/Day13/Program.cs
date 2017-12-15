using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day13
{
    class firewallLayer
    {
        public int depth;
        public int range;
        public int scannerPos;
        public int direction;

        public firewallLayer(int d, int r, int sPos, int dir)
        {
            depth = d;
            range = r;
            scannerPos = sPos;
            direction = dir;
        }

        public firewallLayer(firewallLayer layer)
        {
            depth = layer.depth;
            range = layer.range;
            scannerPos = layer.scannerPos;
            direction = layer.direction;
        }
    }

    class Program
    {
        static void moveScanners(List<firewallLayer> firewall) {
            for(int i = 0; i < firewall.Count; i++)
            {
                if(firewall[i].direction == 1)
                {
                    if (firewall[i].scannerPos < firewall[i].range - 1) firewall[i].scannerPos++;
                    else { firewall[i].scannerPos--; firewall[i].direction = 0; }
                }
                else
                {
                    if (firewall[i].scannerPos > 0) firewall[i].scannerPos--;
                    else { firewall[i].scannerPos++; firewall[i].direction = 1; }
                }
            }
        }

        static void PartOne(List<firewallLayer> firewall, int maxDepth) {
            int packetPos = 0;
            int tripSeverity = 0;

            do
            {
                firewallLayer caught = firewall.Where(l => l.depth == packetPos && l.scannerPos == 0).FirstOrDefault();
                if (caught != null)
                {
                    tripSeverity += (caught.depth * caught.range);
                }

                moveScanners(firewall);
                packetPos++;
            }
            while (packetPos <= maxDepth);

            Console.WriteLine("The severity of the whole trip is: " + tripSeverity);
        }

        static void PartTwo(List<firewallLayer> firewall, int maxDepth) {
            int packetPos = -1;
            int delay = 0;
            int picosecond = -1;
            bool solFound = false;
            List<firewallLayer> currFirewall = firewall.Select(l => new firewallLayer(l)).ToList();
            Dictionary<int, List<firewallLayer>> picoseconds = new Dictionary<int, List<firewallLayer>>();

            if (!picoseconds.ContainsKey(picosecond)) picoseconds.Add(-1, currFirewall);

            do
            {
                packetPos++;

                if (currFirewall.Where(l => l.depth == packetPos && l.scannerPos == 0).Count() > 0)
                {
                    moveScanners(currFirewall);
                    picosecond++;
                    if (!picoseconds.ContainsKey(picosecond)) picoseconds.Add(picosecond, currFirewall.Select(l => new firewallLayer(l)).ToList());
                    currFirewall = picoseconds[delay].Select(l => new firewallLayer(l)).ToList();
                    for (int i = picoseconds.Keys.Min(); i < delay - 1; i++) {
                        if (picoseconds.ContainsKey(i)) picoseconds.Remove(i);
                    }
                    packetPos = -1;
                    picosecond = delay;
                    delay++; 
                }
                else
                {
                    if (packetPos == maxDepth)
                    {
                        solFound = true;
                    }
                    else
                    {
                        moveScanners(currFirewall);
                        picosecond++;
                        if (!picoseconds.ContainsKey(picosecond)) picoseconds.Add(picosecond, currFirewall.Select(l => new firewallLayer(l)).ToList());                        
                    }  
                }   
            }
            while (solFound == false);

            Console.WriteLine("The fewest number of Picoseconds delay is: " + delay);
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");
            List<firewallLayer> firewall = new List<firewallLayer>();
            int maxDepth = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"(\d+)\: (\d+)");
                int depth = int.Parse(match.Groups[1].Value);
                int range = int.Parse(match.Groups[2].Value);

                if (depth > maxDepth) maxDepth = depth;

                firewall.Add(new firewallLayer(depth, range, 0, 1));
            }

            PartOne(firewall.Select(l => new firewallLayer(l)).ToList(), maxDepth);
            PartTwo(firewall.Select(l => new firewallLayer(l)).ToList(), maxDepth);
            Console.Read();
        }
    }
}
