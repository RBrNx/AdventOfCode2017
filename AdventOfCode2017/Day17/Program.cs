using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {

        static int calcBuffer(int input, int toFind)
        {
            int currentPos = 0;
            int bufferLength = 1;

            for (int i = 1; i <= toFind; i++)
            {
                int pos = ((currentPos + input) % bufferLength) + 1;
                currentPos = pos;

                if (i == toFind) return i;
            }

            return 0;
        }

        static void Main(string[] args)
        {
            int currentPos = 0;
            int input = 369;
            int besideZero = 0;
            List<int> buffer = new List<int>() { 0 };
            int bufferSize = buffer.Count();

            for (int i = 1; i <= 50000000; i++)
            {
                int pos = ((currentPos + input) % bufferSize) + 1;
                if(i < 2018) buffer.Insert(pos, i);
                bufferSize++;
                if (pos == 1) besideZero = i;
                currentPos = pos;
            }

            Console.WriteLine("The value beside 2017 is: " + buffer[buffer.IndexOf(2017) + 1]);
            Console.WriteLine("The value beside 0 is: " + besideZero);
            Console.Read();
        }
    }
}
