using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    class tapePosition
    {
        public int position;
        public int value;

        public tapePosition(int pos, int val)
        {
            position = pos;
            value = val;
        }
    }

    class Program
    {
        static int getTapeValueAt(List<tapePosition> tape, int pos)
        {
            if(tape.Where(t => t.position == pos).Count() > 0)
            {
                return tape.Where(t => t.position == pos).First().value;
            }
            else
            {
                tape.Add(new tapePosition(pos, 0));
                return 0;
            }
        }

        static void setTapeValueAt(List<tapePosition> tape, int pos, int val)
        {
            tape.Where(t => t.position == pos).First().value = val;
        }


        static void printTape(List<tapePosition> tape)
        {
            for(int i = 0; i < tape.Count; i++)
            {
                Console.Write(tape[i].value + " ");
            }

            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            List<tapePosition> tape = new List<tapePosition>() { new tapePosition(0, 0) };
            int cursor = 0;
            char currState = 'A';
            int steps = 12425180;

            for (int i = 0; i < steps; i++)
            {
                int currValue;

                switch (currState)
                {
                    case 'A':
                        currValue = getTapeValueAt(tape, cursor);
                        if(currValue == 0)
                        {
                            setTapeValueAt(tape, cursor, 1);
                            cursor++;
                            currState = 'B';
                        }
                        else if(currValue == 1)
                        {
                            setTapeValueAt(tape, cursor, 0);
                            cursor++;
                            currState = 'F';
                        }
                        break;
                    case 'B':
                        currValue = getTapeValueAt(tape, cursor);
                        if (currValue == 0)
                        {
                            setTapeValueAt(tape, cursor, 0);
                            cursor--;
                            currState = 'B';
                        }
                        else if (currValue == 1)
                        {
                            setTapeValueAt(tape, cursor, 1);
                            cursor--;
                            currState = 'C';
                        }
                        break;
                    case 'C':
                        currValue = getTapeValueAt(tape, cursor);
                        if (currValue == 0)
                        {
                            setTapeValueAt(tape, cursor, 1);
                            cursor--;
                            currState = 'D';
                        }
                        else if (currValue == 1)
                        {
                            setTapeValueAt(tape, cursor, 0);
                            cursor++;
                            currState = 'C';
                        }
                        break;
                    case 'D':
                        currValue = getTapeValueAt(tape, cursor);
                        if (currValue == 0)
                        {
                            setTapeValueAt(tape, cursor, 1);
                            cursor--;
                            currState = 'E';
                        }
                        else if (currValue == 1)
                        {
                            setTapeValueAt(tape, cursor, 1);
                            cursor++;
                            currState = 'A';
                        }
                        break;
                    case 'E':
                        currValue = getTapeValueAt(tape, cursor);
                        if (currValue == 0)
                        {
                            setTapeValueAt(tape, cursor, 1);
                            cursor--;
                            currState = 'F';
                        }
                        else if (currValue == 1)
                        {
                            setTapeValueAt(tape, cursor, 0);
                            cursor--;
                            currState = 'D';
                        }
                        break;
                    case 'F':
                        currValue = getTapeValueAt(tape, cursor);
                        if (currValue == 0)
                        {
                            setTapeValueAt(tape, cursor, 1);
                            cursor++;
                            currState = 'A';
                        }
                        else if (currValue == 1)
                        {
                            setTapeValueAt(tape, cursor, 0);
                            cursor--;
                            currState = 'E';
                        }
                        break;
                }
            }

            tape = tape.OrderBy(t => t.position).ToList();
            printTape(tape);
            Console.WriteLine("The diagnostic checksum is: " + tape.Where(t => t.value == 1).Count());
            Console.Read();
        }
    }
}
