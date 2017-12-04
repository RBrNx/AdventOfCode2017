using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Day03
{
    public class Node
    {
        public static int NodeSize = 1;
        public Node parent;
        public Tuple<int, int> position;
        public int distanceToTarget;
        public float cost;
        public float F
        {
            get
            {
                if (distanceToTarget != -1 && cost != -1)
                    return distanceToTarget + cost;
                else
                    return -1;
            }
        }
        public bool walkable;

        public Node(Tuple<int, int> pos, bool isWalkable)
        {
            parent = null;
            position = pos;
            distanceToTarget = -1;
            cost = 1;
            walkable = isWalkable;
        }
    }

    public class AStar
    {
        List<List<Node>> Grid;
        int GridRows
        {
            get
            {
                return Grid[0].Count;
            }
        }
        int GridCols
        {
            get
            {
                return Grid.Count;
            }
        }

        public AStar(List<List<Node>> grid)
        {
            Grid = grid;
        }

        public Stack<Node> FindPath(Tuple<int, int> start, Tuple<int, int> end)
        {
            Node Start = new Node(start, true);
            Node End = new Node(end, true);

            Stack<Node> Path = new Stack<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = Start;

            OpenList.Add(Start);

            while(OpenList.Count != 0 && !ClosedList.Exists(x => x.position.Equals(End.position)))
            {
                current = OpenList[0];
                OpenList.Remove(current);
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);

                foreach(Node n in adjacencies)
                {
                    if(!ClosedList.Contains(n) && n.walkable)
                    {
                        if (!OpenList.Contains(n))
                        {
                            n.parent = current;
                            n.distanceToTarget = Math.Abs(n.position.Item1 - End.position.Item1) + Math.Abs(n.position.Item2 - End.position.Item2);
                            n.cost = 1 + n.parent.cost;
                            OpenList.Add(n);
                            OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                        }
                    }
                }
            }

            if(!ClosedList.Exists(x => x.position.Equals(End.position)))
            {
                return null;
            }

            Node temp = ClosedList[ClosedList.IndexOf(current)];
            while(!temp.Equals(Start) && temp != null)
            {
                Path.Push(temp);
                temp = temp.parent;
            }
            //Path.Push(Start);
            return Path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> temp = new List<Node>();

            int row = (int)n.position.Item1;
            int col = (int)n.position.Item2;

            if(row + 1 < GridRows)
            {
                temp.Add(Grid[col][row + 1]);
            }
            if (row - 1 >= 0)
            {
                temp.Add(Grid[col][row - 1]);
            }
            if (col - 1 >= 0)
            {
                temp.Add(Grid[col - 1][row]);
            }
            if (col + 1 < GridCols)
            {
                temp.Add(Grid[col + 1][row]);
            }

            return temp;
        }
    }
}
