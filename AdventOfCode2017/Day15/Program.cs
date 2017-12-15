using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Day15
{
    class Judge
    {
        public static List<BigInteger> genANums = new List<BigInteger>();
        public static List<BigInteger> genBNums = new List<BigInteger>();

        public static bool compareValues(BigInteger a, BigInteger b)
        {
            string binaryA = BigIntToBinary(a).PadLeft(32, '0');
            string binaryB = BigIntToBinary(b).PadLeft(32, '0');

            if (binaryA.Substring(16, 16) == binaryB.Substring(16, 16))
            {
                return true;
            }

            return false;
        }

        public static string BigIntToBinary(BigInteger bigint)
        {
            var bytes = bigint.ToByteArray();
            var idx = bytes.Length - 1;

            // Create a StringBuilder having appropriate capacity.
            var base2 = new StringBuilder(bytes.Length * 8);

            // Convert first byte to binary.
            var binary = Convert.ToString(bytes[idx], 2);

            // Ensure leading zero exists if value is positive.
            if (binary[0] != '0' && bigint.Sign == 1)
            {
                base2.Append('0');
            }

            // Append binary string to StringBuilder.
            base2.Append(binary);

            // Convert remaining bytes adding leading zeros.
            for (idx--; idx >= 0; idx--)
            {
                base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
            }

            return base2.ToString();
        }
    }

    class Program
    {
        static int genA = 703;
        static int genB = 516;
        //static int genA = 65;
        //static int genB = 8921;
        static int factorA = 16807;
        static int factorB = 48271;
        static int divider = 2147483647;

        public static void PartOne() {
            BigInteger currentA = genA;
            BigInteger currentB = genB;
            int matches = 0;

            for (BigInteger i = 0; i < 40000000; i++)
            {
                BigInteger nextValueA = (currentA * factorA) % divider;
                BigInteger nextValueB = (currentB * factorB) % divider;

                if(Judge.compareValues(nextValueA, nextValueB))
                {
                    matches++;
                }

                currentA = nextValueA;
                currentB = nextValueB;
            }

            Console.WriteLine("The judge will find " + matches + " matches");
        }

        public static void PartTwo() {
            BigInteger currentA = genA;
            BigInteger currentB = genB;
            int matches = 0;
            int currValue = 0;

            while(currValue < 5000000)
            {
                BigInteger nextValueA = (currentA * factorA) % divider;
                BigInteger nextValueB = (currentB * factorB) % divider;

                if (nextValueA % 4 == 0) Judge.genANums.Add(nextValueA);
                if (nextValueB % 8 == 0) Judge.genBNums.Add(nextValueB);

                if(currValue < Judge.genANums.Count && currValue < Judge.genBNums.Count)
                {
                    BigInteger a = Judge.genANums[currValue];
                    BigInteger b = Judge.genBNums[currValue];

                    if (Judge.compareValues(a, b))
                    {
                        matches++;
                    }

                    currValue++;
                }              

                currentA = nextValueA;
                currentB = nextValueB;
            }

            Console.WriteLine("The judge will find " + matches + " matches");
        }

        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
            Console.Read();
        }
    }
}
