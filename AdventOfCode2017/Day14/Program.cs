using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static int[] selectAndReverse(int[] circle, int length, int startPos)
        {
            List<int> newList = new List<int>();

            int pos = startPos;
            for (int i = 0; i < length; i++)
            {
                pos = calculatePos(pos, circle.Length);

                newList.Add(circle[pos]);
                pos++;
            }

            newList.Reverse();
            int[] newArr = newList.ToArray();
            pos = startPos;

            for (int i = 0; i < length; i++)
            {
                pos = calculatePos(pos, circle.Length);

                circle[pos] = newArr[i];
                pos++;
            }

            return circle;
        }

        static int XOR(List<int> block)
        {
            int x = block[0];
            for (int i = 1; i < block.Count; i++)
            {
                x = x ^ block[i];
            }

            return x;
        }

        static int calculatePos(int currPos, int max)
        {
            if (currPos >= max)
            {
                return calculatePos(currPos - max, max);
            }
            else
            {
                return currPos;
            }
        }

        static string knotHash(string input)
        {
            List<byte> inputLengths = Encoding.ASCII.GetBytes(input).ToList();
            inputLengths.AddRange(new byte[] { 17, 31, 73, 47, 23 });
            int[] circle = Enumerable.Range(0, 256).Select(i => i).ToArray();
            int currPosition = 0;
            int skipSize = 0;

            for (int round = 0; round < 64; round++)
            {
                for (int i = 0; i < inputLengths.Count; i++)
                {
                    int length = inputLengths[i];
                    circle = selectAndReverse(circle, length, currPosition);
                    currPosition += length + skipSize;
                    currPosition = calculatePos(currPosition, circle.Length);
                    skipSize++;
                }
            }

            string finalHash = "";
            for (int i = 0; i < circle.Length; i += 16)
            {
                int x = XOR(circle.Skip(i).Take(16).ToList());
                finalHash += x.ToString("x2");
            }

            return finalHash;
        }

        static void floodFill(int[,] grid, int x, int y, int regionNum)
        {
            Queue<Tuple<int, int>> floodQueue = new Queue<Tuple<int, int>>();
            floodQueue.Enqueue(Tuple.Create(x, y));

            while(floodQueue.Count() > 0)
            {
                Tuple<int, int> coord = floodQueue.Dequeue();
                grid[coord.Item1, coord.Item2] = regionNum;

                if(coord.Item1 + 1 < grid.GetLength(0) && grid[coord.Item1 + 1, coord.Item2] == -1)
                {
                    floodQueue.Enqueue(Tuple.Create(coord.Item1 + 1, coord.Item2));
                }
                if (coord.Item1 - 1 >= 0 && grid[coord.Item1 - 1, coord.Item2] == -1)
                {
                    floodQueue.Enqueue(Tuple.Create(coord.Item1 - 1, coord.Item2));
                }
                if (coord.Item2 - 1 >= 0 && grid[coord.Item1, coord.Item2 - 1] == -1)
                {
                    floodQueue.Enqueue(Tuple.Create(coord.Item1, coord.Item2 - 1));
                }
                if (coord.Item2 + 1 < grid.GetLength(1) && grid[coord.Item1, coord.Item2 + 1] == -1)
                {
                    floodQueue.Enqueue(Tuple.Create(coord.Item1, coord.Item2 + 1));
                }
            }
        }

        static string input = "hwlqcszp";

        static void Main(string[] args)
        {
            int squares = 0;
            int[,] grid = new int[128, 128];

            for (int i = 0; i < 128; i++)
            {
                string key = input + "-" + i.ToString();
                string knothash = knotHash(key);
                string binary = "";

                for(int j = 0; j < knothash.Length; j++)
                {
                    binary += Convert.ToString(Convert.ToInt32(knothash[j].ToString(), 16), 2).PadLeft(4, '0');
                }

                squares += binary.Where(c => c == '1').Count();
                
                for(int j = 0; j < binary.Length; j++)
                {
                    grid[j, i] = (binary[j] == '1') ? -1 : 0;
                }
            }

            int regionNum = 1;
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                for(int x = 0; x < grid.GetLength(0); x++)
                {
                    if(grid[x, y] == -1)
                    {
                        floodFill(grid, x, y, regionNum);
                        regionNum++;
                    }
                }
            }

            Console.WriteLine("The number of squares used across the grid is: " + squares);
            Console.WriteLine("The number of regions in the grid is: " + (regionNum - 1));
            Console.Read();
        }
    }
}
