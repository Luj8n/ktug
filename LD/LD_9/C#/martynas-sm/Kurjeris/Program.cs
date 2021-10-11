using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Kurjeris
{
    class Program
    {
        const string INPUT_FILE = @"../../../U3.txt";
        const int INF = 1000000;
        static void Main(string[] args)
        {
            int n;
            int[,] Weights;
            List<int> Path = new List<int>();
            int value;

            Read(out n, out Weights);

            value = n * INF + 1;

            Solve(ref Weights, new List<int>(){ 0 }, 0, ref Path, ref value, n);

            Console.Write(String.Join(", ", Path.Select(x => x + 1)));
            Console.WriteLine(" = {0}", value);
        }

        static void Read(out int n, out int[,] Weights)
        {
            List<string> Input = File.ReadAllLines(INPUT_FILE).ToList();
            n = Convert.ToInt32(Input[0]);
            Weights = new int[n, n];
            for(int i = 1; i <= n; i++)
            {
                List<int> Row = Input[i].Split(' ').Select(x => Convert.ToInt32(x)).ToList();
                for(int j = 0; j < n; j++)
                {
                    Weights[i - 1, j] = Row[j];
                    if(i - 1 == j)
                    {
                        Weights[i - 1, j] = INF;
                    }
                }
            }
        }
        static void Solve(ref int[,] Weights, List<int> Path, int value, ref List<int> BestPath, ref int bestValue, int n)
        {
            if (Path.Count == n)
            {
                value += Weights[Path.Last(), 0];
                if(value < bestValue)
                {
                    bestValue = value;
                    BestPath = new List<int>(Path);
                    BestPath.Add(0);
                }
                return;
            }

            for(int i = 0; i < n; i++)
            {
                if (!Path.Any(x => x == i))
                {
                    int edge = Weights[Path.Last(), i];

                    Path.Add(i);
                    Solve(ref Weights, Path, value + edge, ref BestPath, ref bestValue, n);
                    Path.RemoveAt(Path.Count - 1);
                }
            }
        }
    }
}