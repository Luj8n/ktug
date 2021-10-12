using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apskritimas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<List<int>> Matrix;
            int r;

            Read(out r);
            Solve(out Matrix, r);
            Console.WriteLine(String.Join('\n',
                              Matrix.Select(line => "\n" +
                              String.Join(" ",
                              line.Select(x =>
                              x.ToString().PadLeft(3))))));
        }

        static void Read(out int r)
        {
            Console.WriteLine("Įveskite r (1 <= r <= 16):");
            r = Convert.ToInt32(Console.ReadLine());
        }

        static void Solve(out List<List<int>> Matrix, int r)
        {
            Matrix = new List<List<int>>(2 * r);
            for (int i = 0; i < 2 * r; i++)
            {
                Matrix.Add(Enumerable.Range(0, 2 * r).Select(x => 0).ToList());
            }
            int counter = 1;
            for (int i = 0; i < 2 * r; i++)
            {
                for (int j = 0; j < 2 * r; j++)
                {
                    int x, y;
                    x = (2 * i) - (2 * r - 1);
                    y = (2 * j) - (2 * r - 1);
                    if (x * x + y * y <= (2 * r - 1) * (2 * r - 1))
                    {
                        Matrix[i][j] = counter++;
                    }
                }
            }
        }
    }
}
