using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class bufferElement
    {
        public int position;
        public int value;

        public bufferElement(int p, int v)
        {
            position = p;
            value = v;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            int currentPos = 0;
            int position2018 = 0;
            int position0 = 0;
            int input = 369;
            List<bufferElement> buffer = new List<bufferElement>() { new bufferElement(0, 0) };

            for (int i = 1; i <= 50000000; i++)
            {
                int pos = ((currentPos + input) % buffer.Count) + 1;
                buffer.Where(e => e.position >= pos).ToList().ForEach(e => e.position++);
                buffer.Add(new bufferElement(pos, i));
                currentPos = pos;

                if (i == 2017)
                {
                    int p = buffer.Find(x => x.value == 2017).position;
                    Console.WriteLine("The value after 2017 is: " + buffer.Find(x => x.position == p + 1).value);
                }
                if (i == 50000000)
                {
                    int p = buffer.FindIndex(x => x.value == 0);
                    Console.WriteLine("The value after 2017 is: " + buffer.Find(x => x.position == p + 1).value);
                }
            }


            Console.Read();
        }
    }
}
