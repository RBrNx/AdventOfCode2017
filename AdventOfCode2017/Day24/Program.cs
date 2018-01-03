using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day24
{
    class Component
    {
        public int leftPort;
        public int rightPort;
        public bool leftConnected = false;
        public bool rightConnected = false;
        public int id;

        public Component(int l, int r, int i)
        {
            leftPort = l;
            rightPort = r;
            id = i;
        }

        public Component(Component c)
        {
            leftPort = c.leftPort;
            leftConnected = c.leftConnected;
            rightPort = c.rightPort;
            rightConnected = c.rightConnected;
            id = c.id;
        }
    }

    class Program
    {
        static int strength = 0;
        static List<Component> longestStrongest = null;

        public static bool checkBridgeValidity(List<Component> bridge)
        {
            if (bridge[0].leftPort != 0)
            {
                return false;
            }

            for (int i = 0; i < bridge.Count - 1; i++)
            {
                if(!componentsConnect(bridge[i], bridge[i + 1])){
                    return false;
                }
            }

            return true;
        }

        public static bool componentsConnect(Component a, Component b)
        {
            if (!a.rightConnected)
            {
                if (!b.leftConnected && a.rightPort == b.leftPort)
                {
                    a.rightConnected = true;
                    b.leftConnected = true;
                    return true;
                }
                else if (!b.rightConnected && a.rightPort == b.rightPort)
                {
                    a.rightConnected = true;
                    b.rightConnected = true;
                    return true;
                }
            }
            else if (!a.leftConnected)
            {
                if (!b.leftConnected && a.leftPort == b.leftPort)
                {
                    a.leftConnected = true;
                    b.leftConnected = true;
                    return true;
                }
                else if (!b.rightConnected && a.leftPort == b.rightPort)
                {
                    a.leftConnected = true;
                    b.rightConnected = true;
                    return true;
                }
            }

            return false;
        }

        public static void printCombination(List<Component> combs)
        {
            for (int j = 0; j < combs.Count; j++)
            {
                Console.Write(combs[j].leftPort + "/" + combs[j].rightPort + "---");
            }

            Console.WriteLine("");
        }

        static int calculateStrength(List<Component> bridge)
        {
            int count = 0;
            for (int j = 0; j < bridge.Count; j++)
            {
                count += bridge[j].leftPort + bridge[j].rightPort;
            }

            return count;
        }

        static void findPossibleBridges(Component start, List<Component> components)
        {
            Queue<List<Component>> queue = new Queue<List<Component>>();
            start.leftConnected = true;
            queue.Enqueue(new List<Component>() { start });

            while(queue.Count > 0)
            {
                List<Component> currList = queue.Dequeue();

                int str = calculateStrength(currList);
                if(str > strength)
                {
                    strength = str;
                    printCombination(currList);
                }

                if(longestStrongest == null || currList.Count > longestStrongest.Count || (currList.Count == longestStrongest.Count && str > calculateStrength(longestStrongest)))
                {
                    longestStrongest = currList.Select(x => new Component(x)).ToList();
                }

                for(int i = 0; i < components.Count; i++)
                {
                    List<Component> listCopy = currList.Select(x => new Component(x)).ToList();
                    Component curr = listCopy[listCopy.Count - 1];
                    Component newComp = null;

                    if (components[i].id == curr.id) continue;
                    if (listCopy.Where(c => c.id == components[i].id).Count() > 0) continue;
                    if (!curr.rightConnected)
                    {
                        if(curr.rightPort == components[i].leftPort)
                        {
                            newComp = new Component(components[i]);
                            newComp.leftConnected = true;
                            curr.rightConnected = true;
                            listCopy.Add(newComp);
                        }
                        else if (curr.rightPort == components[i].rightPort)
                        {
                            newComp = new Component(components[i]);
                            newComp.rightConnected = true;
                            curr.rightConnected = true;
                            listCopy.Add(newComp);
                        }
                    }
                    else if (!curr.leftConnected)
                    {
                        if (curr.leftPort == components[i].leftPort)
                        {
                            newComp = new Component(components[i]);
                            newComp.leftConnected = true;
                            curr.leftConnected = true;
                            listCopy.Add(newComp);
                        }
                        else if (curr.leftPort == components[i].rightPort)
                        {
                            newComp = new Component(components[i]);
                            newComp.rightConnected = true;
                            curr.leftConnected = true;
                            listCopy.Add(newComp);
                        }
                    }

                    if (newComp != null)
                    {
                        queue.Enqueue(listCopy);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../input.txt");
            List<Component> components = new List<Component>();

            for(int i = 0; i < lines.Length; i++)
            {
                string[] comp = lines[i].Split('/');
                components.Add(new Component(int.Parse(comp[0]), int.Parse(comp[1]), i));
            }

            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].leftPort == 0)
                {
                    findPossibleBridges(new Component(components[i]), components);
                }
            }
           
            Console.WriteLine("The strength of the strongest bridge is: " + strength);
            Console.WriteLine("The strength of the longest bridge is: " + calculateStrength(longestStrongest));
            Console.Read();
        }
    }
}
