using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KvadrataiKvadrate
{
    class Program
    {
        const string file = @"../../../U3.txt";
        static void Main(string[] args)
        {
            int n = 0;
            List<List<int>> Matrix = new List<List<int>>();

            Read(ref Matrix, ref n);

            List<List<Tuple<int, int>>> Squares = new List<List<Tuple<int, int>>>();

            Solve(n, Matrix, ref Squares);

            Print(Squares);
        }

        static void Read(ref List<List<int>> Matrix, ref int n)
        {
            StreamReader FileStream = new StreamReader(file);
            n = int.Parse(FileStream.ReadLine());
            Matrix = new List<List<int>>(n);
            for (int i = 0; i < n; i++)
            {
                Matrix.Add(FileStream.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList());
            }
        }

        static bool InBounds(int i, int j, int n)
        {
            return i >= 0 && i < n && j >= 0 && j < n;
        }

        static void Solve(int n, List<List<int>> Matrix, ref List<List<Tuple<int, int>>> Squares)
        {
            for (int x = 0; x < n; x++)
            {
                for (int y = 1; y < n; y++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            List<Tuple<int, int>> Coords = new List<Tuple<int, int>>();
                            int ci = i, cj = j;
                            int addI, addJ;
                            addI = y;
                            addJ = x;
                            bool square = true;
                            // 4 - kvadratas turi keturias krastines
                            for (int k = 0; k < 4; k++)
                            {
                                if (!InBounds(ci, cj, n) || Matrix[ci][cj] != 1)
                                {
                                    square = false;
                                    break;
                                }
                                Coords.Add(Tuple.Create(ci, cj));
                                ci += addI;
                                cj += addJ;
                                int tempI = addI, tempJ = addJ;
                                addI = -tempJ;
                                addJ = tempI;
                            }
                            if (square)
                            {
                                Squares.Add(Coords.OrderBy(tuple => tuple.Item1 * n + tuple.Item2).ToList());
                            }
                        }
                    }
                }
            }
        }

        static void Print(List<List<Tuple<int, int>>> Squares)
        {
            Console.WriteLine(Squares.Count);
            if (Squares.Count > 0)
            {
                // Rikiavimas pagal simbolius veikia, nes n priklauso [1;5] => vienas skaitmuo
                Squares.OrderBy(x =>
                     String.Join("",
                         x.Select(tuple =>
                         String.Format("{0}{1}", tuple.Item1, tuple.Item2)
                         )
                    )
                ).ToList().ForEach(Coords =>
                {
                    Console.WriteLine("{0}",
                        String.Join(" ir ",
                            Coords.Select(tuple => String.Format("({0};{1})", tuple.Item1 + 1, tuple.Item2 + 1)
                        ))
                    );
                });
            }
        }
    }
}
