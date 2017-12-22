using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day22
{
    class virusCarrier
    {
        public Point currentNode;
        public int facingDir;
        int[][] dirs = new int[4][] { new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { -1, 0 } };

        public virusCarrier(Point p, int facing)
        {
            currentNode = p;
            facingDir = facing;
        }

        public void turn(int change)
        {
            facingDir += change;
            if(facingDir < 0 || facingDir > 3)
            {
                int diff = Math.Abs(facingDir - 3) - 1;
                facingDir = diff;
            }
        }

        public void move()
        {
            int x = currentNode.x + dirs[facingDir][0];
            int y = currentNode.y + dirs[facingDir][1];
            currentNode = new Point(x, y);
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

        public override bool Equals(object obj)
        {
            Point p = (Point)obj;
            return (p.x == this.x && p.y == this.y);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }
    }

    class Program
    {
        static int getPositionState(Point p, ref Dictionary<Point, int> nodes)
        {
            if (!nodes.ContainsKey(p))
            {
                nodes.Add(p, 0);
            }

            return nodes[p];
        }

        static int getTurningDir(int state)
        {
            if (state == 0) return -1;
            else if (state == 2) return 1;
            else if (state == 1) return 0;
            else return 2;
        }

        static void PartOne(virusCarrier virus, Dictionary<Point, int> nodes) {
            int bursts = 10000;
            int nodesInfected = 0;

            for (int i = 0; i < bursts; i++)
            {
                int posState = getPositionState(virus.currentNode, ref nodes);
                int turningDir = (posState == 2) ? 1 : -1;
                virus.turn(turningDir);

                if (posState == 2) nodes[virus.currentNode] = 0;
                else
                {
                    nodesInfected++;
                    nodes[virus.currentNode] = 2;
                }

                virus.move();
            }

            Console.WriteLine("There were " + nodesInfected + " nodes infected after " + bursts + " bursts");
        }

        static void PartTwo(virusCarrier virus, Dictionary<Point, int> nodes)
        {
            int bursts = 10000000;
            int nodesInfected = 0;

            for (int i = 0; i < bursts; i++)
            {
                int posState = getPositionState(virus.currentNode, ref nodes);
                int turningDir = getTurningDir(posState);
                virus.turn(turningDir);

                if (posState == 0) nodes[virus.currentNode] = 1;
                else if(posState == 1)
                {
                    nodesInfected++;
                    nodes[virus.currentNode] = 2;
                }
                else if(posState == 2) nodes[virus.currentNode] = 3;
                else if(posState == 3) nodes[virus.currentNode] = 0;

                virus.move();
            }

            Console.WriteLine("There were " + nodesInfected + " nodes infected after " + bursts + " bursts");
        }

        static void Main(string[] args)
        {

            Dictionary<Point, int> nodes = new Dictionary<Point, int>();
            string[] lines = File.ReadAllLines("../../input.txt");
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    Point p = new Point(x, y);
                    int state = (lines[y][x] == '#') ? 2 : 0;
                    nodes.Add(p, state);
                }
            }

            PartOne(new virusCarrier(new Point(lines[0].Length / 2, lines.Length / 2), 0), new Dictionary<Point, int>(nodes));
            PartTwo(new virusCarrier(new Point(lines[0].Length / 2, lines.Length / 2), 0), new Dictionary<Point, int>(nodes));

            Console.Read();
        }
    }
}
