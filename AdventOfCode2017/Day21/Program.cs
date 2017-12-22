using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day21
{
    public class GridComparer : IEqualityComparer<string[][]>
    {
        public bool Equals(string[][] a, string[][] b)
        {
            for(int i = 0; i < a.Length; i++)
            {
                if (!a[i].SequenceEqual(b[i])) return false;
            }

            return true;
        }

        public int GetHashCode(string[][] grid)
        {
            return base.GetHashCode();
        }
    }

    class Program
    {
        static string[][] Convert1Dto2D(string pattern)
        {
            string[] lines = pattern.Split('/');
            string[][] Pattern2D = new string[lines.Length][].Select(x => new string[lines.Length]).ToArray();

            for(int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    Pattern2D[y][x] = lines[y][x].ToString();
                }
            }

            return Pattern2D;
        }

        static List<string[][]> splitGrid(string[][] grid)
        {
            List<string[][]> split = new List<string[][]>();
            int divisor = (grid.GetLength(0) % 2 == 0) ? 2 : 3;
            int splitLength = grid.GetLength(0) / divisor;

            for(int yDim = 0; yDim < grid.GetLength(0); yDim += divisor)
            {
                for (int xDim = 0; xDim < grid.GetLength(0); xDim += divisor)
                {
                    string[][] newGrid = new string[divisor][].Select(x => new string[divisor]).ToArray();

                    for (int y = 0; y < divisor; y++)
                    {
                        for (int x = 0; x < divisor; x++)
                        {
                            newGrid[y][x] = grid[yDim + y][xDim + x];
                        }
                    }

                    split.Add(newGrid);
                }
            }
            
 
            return split;
        }

        static string[][] findMatch(string[][] grid, Dictionary<string[][], string[][]> patternRules)
        {
            string[][] rotation = grid.Select(a => a.ToArray()).ToArray();
            Dictionary<string[][], int> test = new Dictionary<string[][], int>(new GridComparer());

            for (int rotations = 0; rotations < 4; rotations++)
            {
                rotation = rotateGrid(rotation);
                string[][] flippedH = flipGridHorz(rotation.Select(a => a.ToArray()).ToArray());

                if(!test.ContainsKey(rotation)) test.Add(rotation, 0);
                if (!test.ContainsKey(flippedH)) test.Add(flippedH, 0);

                if (patternRules.ContainsKey(rotation))
                {
                    return patternRules[rotation];
                }
                else if (patternRules.ContainsKey(flippedH))
                {
                    return patternRules[flippedH];
                }
            }

            return null;
        }

        static string[][] rotateGrid(string[][] grid)
        {
            grid = transpose(grid);
            for(int i = 0; i < grid.Length; i++)
            {
                grid[i] = grid[i].Reverse().ToArray();
            }

            return grid;
        }

        static string[][] flipGridHorz(string[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = grid[i].Reverse().ToArray();
            }

            return grid;
        }

        static string[][] flipGridVert(string[][] grid)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                string temp = grid[0][y];
                grid[0][y] = grid[grid.Length - 1][y];
                grid[grid.Length - 1][y] = temp;
            }

            return grid;
        }

        static string[][] transpose(string[][] grid)
        {
            var result = new string[grid.Length][];
            for (var i = 0; i < grid[0].Length; i++)
            {
                result[i] = new string[grid.Length];
                for (var j = grid.Length - 1; j > -1; j--)
                {
                    result[i][j] = grid[j][i];
                }
            }
            return result;
        }

        static string[][] pieceTogether(List<string[][]> grids)
        {
            int divisor = (grids.Count % 2 == 0) ? 2 : 3;
            int combinedgridDimension = (int)Math.Sqrt(grids.Count()) * grids[0].Length;
            string[][] newGrid = new string[combinedgridDimension][].Select(x => new string[combinedgridDimension]).ToArray();
            var curr = 0;

            for (int yDim = 0; yDim < combinedgridDimension; yDim += grids[0].Length)
            {
                for (int xDim = 0; xDim < combinedgridDimension; xDim += grids[0].Length)
                {
                    
                    string[][] currGrid = grids[curr];

                    for (int y = 0; y < currGrid.Length; y++)
                    {
                        for (int x = 0; x < currGrid.Length; x++)
                        {
                            newGrid[yDim + y][xDim + x] = currGrid[y][x];
                        }
                    }

                    curr++;
                }
            }

            return newGrid;
        }

        static void printGrid(string[][] grid)
        {
            for(int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid.Length; x++)
                {
                    Console.Write(grid[y][x]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            string[][] grid = {
                new []{ ".", "#", "." },
                new []{ ".", ".", "#" },
                new []{ "#", "#", "#" },
            };

            Dictionary<string[][], string[][]> patternRules = new Dictionary<string[][], string[][]>(new GridComparer());

            string[] lines = File.ReadAllLines("../../input.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"(.+) => (.+)");
                string[][] input = Convert1Dto2D(match.Groups[1].Value);
                string[][] output = Convert1Dto2D(match.Groups[2].Value);

                patternRules.Add(input, output);
            }

            for (int i = 0; i < 18; i++)
            {
                List<string[][]> splitGrids = splitGrid(grid);
                for (int j = 0; j < splitGrids.Count; j++)
                {
                    splitGrids[j] = findMatch(splitGrids[j], patternRules);
                }

                grid = pieceTogether(splitGrids);

                if(i == 4 || i == 17)
                {
                    int pixelsOn = 0;
                    for (int j = 0; j < grid.Length; j++)
                    {
                        pixelsOn += grid[j].Where(x => x == "#").Count();
                    }

                    Console.WriteLine("There are " + pixelsOn + " pixels on after " + (i + 1) + " iterations");
                }
            }

            Console.Read();
        }
    }
}
