using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LD_11.Pilis
{
    class Program
    {
        const string file = @"../../../U3.txt";
        static void Main(string[] args)
        {
            int n = new int(),
                m = new int();
            int answer;
            List<List<int>> adj = new List<List<int>>();

            Read(ref adj, ref n, ref m);

            answer = Solve(adj, n, m);

            adj.ForEach( l => Console.WriteLine(String.Join(" ", l)));
            Console.WriteLine("Kambarių kiekis: {0}", answer);
        }

        static void Read(ref List<List<int>> adj, ref int n, ref int m)
        {
            StreamReader FakeConsole = new StreamReader(file);
            n = int.Parse(FakeConsole.ReadLine());
            m = int.Parse(FakeConsole.ReadLine());
            adj = new List<List<int>>(n);
            for (int i = 0; i < n; i++)
            {
                adj.Add(FakeConsole.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList());
            }
        }

        static int Solve(List<List<int>> adj, int n, int m)
        {
            bool[,] visited = new bool[n, m];

            Queue<Tuple<int, int>> Q = new Queue<Tuple<int, int>>();

            int[] dirI = { 0, -1, 0, 1 };
            int[] dirJ = { -1, 0, 1, 0 };

            int cnt = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (visited[i, j])
                        continue;
                    cnt++;
                    visited[i, j] = true;
                    Q.Enqueue(Tuple.Create(i, j));
                    while (Q.Count > 0)
                    {
                        int ci = Q.Peek().Item1;
                        int cj = Q.Peek().Item2;
                        Q.Dequeue();
                        for (int k = 0; k < 4; k++)
                        {
                            if ((adj[ci][cj] & (1 << k)) == 0)
                            {
                                int ii = ci + dirI[k];
                                int jj = cj + dirJ[k];
                                if (!visited[ii, jj])
                                {
                                    visited[ii, jj] = true;
                                    Q.Enqueue(Tuple.Create(ii, jj));
                                }
                            }
                        }
                    }
                }
            }

            return cnt;
        }
    }
}
