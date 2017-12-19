using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day19
{
    class Packet
    {
        public Point pos;
        public int dir = 2;
        public List<string> letters = new List<string>();

        public void move(int x, int y)
        {
            pos = new Point(pos.x + x, pos.y + y);
        }
    }
    
    class Point
    {
        public int x;
        public int y;

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");
            string[,] grid = new string[lines.Max().Length, lines.Length];
            int[][] dirs = new int[4][] { new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { -1, 0 } };
            bool endFound = false;
            int steps = 0;

            for(int y = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[y].Length; x++)
                {
                    grid[x, y] = lines[y][x].ToString();
                }
            }

            Packet packet = new Packet();
            packet.pos = new Point(lines[0].IndexOf('|'), 0);
            while (!endFound)
            {
                steps++;
                packet.move(dirs[packet.dir][0], dirs[packet.dir][1]);

                if (packet.pos.x < 0 || packet.pos.x > grid.GetLength(0) || packet.pos.y < 0 || packet.pos.y > grid.GetLength(1) || grid[packet.pos.x, packet.pos.y] == " ")
                {
                    endFound = true;
                }
                else if(char.IsLetter(grid[packet.pos.x, packet.pos.y], 0))
                {
                    packet.letters.Add(grid[packet.pos.x, packet.pos.y]);
                }
                else if(grid[packet.pos.x, packet.pos.y] == "+")
                {
                    bool newDirFound = false;

                    for(int i = 0; i < dirs.Length; i++)
                    {
                        if (i != packet.dir && i != (packet.dir + 2) % 4)
                        {
                            string c = grid[packet.pos.x + dirs[i][0], packet.pos.y + dirs[i][1]];
                            if (c == "-" || c == "|" || char.IsLetter(c, 0))
                            {
                                packet.dir = i;
                                newDirFound = true;
                                break;
                            }
                        }
                    }

                    if (!newDirFound) endFound = true;
                }
            }

            packet.letters.ForEach(x => Console.Write(x));
            Console.WriteLine("");
            Console.WriteLine("It takes " + steps + " steps");
            Console.Read();
        }
    }
}
