using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static Tuple<int, int> start = Tuple.Create(0, 0);
        static Tuple<int, int> goal = null;
        static Dictionary<Tuple<int, int>, int> stressValues = new Dictionary<Tuple<int, int>, int>() { { Tuple.Create(0, 0), 1 } };
        static int upperHeight = 0;
        static int upperWidth = 0;
        static int lowerHeight = 0;
        static int lowerWidth = 0;
        static int input = 361527;       

        static void createSpiralGrid(int maxNum)
        {
            // (di, dj) is a vector - direction in which we move right now
            int dx = 1;
            int dy = 0;
            // length of current segment
            int segment_length = 1;

            // current position (i, j) and how much of current segment we passed
            int x = 0;
            int y = 0;
            int segment_passed = 0;
            for (int k = 2; k <= maxNum; ++k)
            {
                // make a step, add 'direction' vector (di, dj) to current position (i, j)
                x += dx;
                y += dy;
                ++segment_passed;
                //Console.WriteLine(i + " " + j + ": " + k);

                if (x > upperWidth) upperWidth = x;
                if (y > upperHeight) upperHeight = y;
                if (x < lowerWidth) lowerWidth = x;
                if (y < lowerHeight) lowerHeight = y;

                if (segment_passed == segment_length)
                {
                    // done with current segment
                    segment_passed = 0;

                    // 'rotate' directions
                    int buffer = dy;
                    dy = -dx;
                    dx = buffer;

                    // increase segment length if necessary
                    if (dy == 0)
                    {
                        ++segment_length;
                    }
                }


            }

            goal = Tuple.Create(x, y);
        }

        static int sumAdjacentSquares(Tuple<int, int> pos)
        {
            return 0;
        }

        static void PartOne() {
            createSpiralGrid(input);
            int maxHeight = upperHeight - lowerHeight;
            int maxWidth = upperWidth - lowerWidth;
            List<List<Node>> grid = new List<List<Node>>();
            for (int y = 0; y <= maxHeight; y++)
            {
                List<Node> temp = new List<Node>();
                for (int x = 0; x <= maxWidth; x++)
                {
                    temp.Add(new Node(Tuple.Create(x, y), true));
                }
                grid.Add(temp);
            }

            start = Tuple.Create(start.Item1 - lowerWidth, start.Item2 - lowerHeight);
            goal = Tuple.Create(goal.Item1 - lowerWidth, goal.Item2 - lowerHeight);
            AStar astar = new AStar(grid);
            Stack<Node> path = astar.FindPath(start, goal);

            Console.WriteLine(path.Count() + " Steps are required to carry the data to Square " + input);
        }

        static void PartTwo() {

        }

        static void Main(string[] args)
        {
            PartOne();
            Console.Read();
        }
    }
}
