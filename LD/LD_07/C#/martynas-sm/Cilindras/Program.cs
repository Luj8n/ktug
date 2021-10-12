using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cilindras
{
    class Program
    {
        const string INPUT_FILE = @"../../../U3_2.txt";
        static void Main(string[] args)
        {
            List<List<int>> Matrix;
            int n, m;
            int answer;
            Read(out Matrix, out n, out m);
            answer = FloodFill(Matrix, n, m);
            Print(Matrix, answer);
        }

        static void Read(out List<List<int>> Matrix, out int n, out int m)
        {
            List<int> l = File.ReadAllLines(INPUT_FILE)[0].Split(' ').Select(x => Convert.ToInt32(x)).ToList();
            n = l[0];
            m = l[1];
            Matrix = File.ReadAllLines(INPUT_FILE).Skip(1).Select(line => line.Split(' ').Select(x => Convert.ToInt32(x)).ToList()).ToList();
        }

        static int FloodFill(List<List<int>> Matrix, int n, int m)
        {
            List<List<bool>> Visited = new List<List<bool>>(n);
            for(int i = 0; i < n; i++)
            {
                Visited.Add(Enumerable.Range(0, m).Select(x => false).ToList());
            }
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if(Matrix[i][j] != 0 && Visited[i][j] == false)
                    {
                        cnt++;
                        DFS(ref Matrix, ref Visited, i, j, n, m);
                    }
                }
            }
            return cnt;
        }

        static void DFS(ref List<List<int>> Matrix, ref List<List<bool>> Visited, int i, int j, int n, int m)
        {
            if(i < 0 || i >= n)
            {
                return;
            }
            
            j = (j + m) % m;
            
            if(Visited[i][j] == true || Matrix[i][j] == 0)
            {
                return;
            }

            Visited[i][j] = true;

            DFS(ref Matrix, ref Visited, i - 1, j, n, m);
            DFS(ref Matrix, ref Visited, i, j - 1, n, m);
            DFS(ref Matrix, ref Visited, i + 1, j, n, m);
            DFS(ref Matrix, ref Visited, i, j + 1, n, m);
        }

        static void Print(List<List<int>> Matrix, int answer)
        {
            Console.WriteLine("Pradiniai duomenys:");

            Console.WriteLine(String.Join("\n", Matrix.Select(row => String.Join(" ", row))));

            Console.WriteLine("\n{0} gabaliukai", answer);
        }
    }
}
