using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void printCircle(int[] circle, int length, int currPosition)
        {
            for(int i = 0; i < circle.Length; i++)
            {
                if (i == currPosition) Console.Write("[");
                Console.Write(circle[i]);
                if (i == currPosition) Console.Write("]");
                Console.Write(" ");
            }

            Console.WriteLine("");
        }

        static int[] selectAndReverse(int[] circle, int length, int startPos)
        {
            List<int> newList = new List<int>();

            int pos = startPos;
            for (int i = 0; i < length; i++)
            {
                //if (pos == circle.Length)
                //{
                //    pos = 0;
                //}
                pos = calculatePos(pos, circle.Length);

                newList.Add(circle[pos]);
                pos++;
            }

            newList.Reverse();
            int[] newArr = newList.ToArray();
            pos = startPos;

            for (int i = 0; i < length; i++)
            {
                //if (pos == circle.Length)
                //{
                //    pos = 0;
                //}
                pos = calculatePos(pos, circle.Length);

                circle[pos] = newArr[i];
                pos++;
            }

            return circle;
        }

        static int XOR(List<int> block)
        {
            int x = block[0];
            for(int i = 1; i < block.Count; i++)
            {
                x = x ^ block[i];
            }

            return x;
        }

        static int calculatePos(int currPos, int max)
        {
            if(currPos >= max)
            {
                return calculatePos(currPos - max, max);
            }
            else
            {
                return currPos;
            }
        }

        static void PartOne() {
            int[] circle = Enumerable.Range(0, 256).Select(i => i).ToArray();
            List<int> inputLengths = File.ReadAllText("../../input.txt").Split(',').Select(c => int.Parse(c.ToString())).ToList();
            int currPosition = 0;
            int skipSize = 0;

            for (int i = 0; i < inputLengths.Count; i++)
            {
                int length = inputLengths[i];
                circle = selectAndReverse(circle, length, currPosition);
                currPosition += length + skipSize;
                currPosition = calculatePos(currPosition, circle.Length);
                skipSize++;
            }

            Console.WriteLine("Multiplying the first two numbers gives: " + (circle[0] * circle[1]));
        }

        static void PartTwo()
        {
            List<byte> inputLengths = Encoding.ASCII.GetBytes(File.ReadAllText("../../input.txt")).ToList();
            inputLengths.AddRange(new byte[] { 17, 31, 73, 47, 23 });
            int[] circle = Enumerable.Range(0, 256).Select(i => i).ToArray();
            int currPosition = 0;
            int skipSize = 0;

            for(int round = 0; round < 64; round++)
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

            Console.WriteLine("The Knot hash of the puzzle input is: " + finalHash);
        }

        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
            Console.Read();
        }
    }
}
