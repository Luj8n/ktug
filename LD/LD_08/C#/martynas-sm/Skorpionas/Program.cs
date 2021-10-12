using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Skorpionas
{
    class Program
    {
        const string INPUT_FILE = @"../../../U3_3.txt";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int n;
            List<string> Adj;
            int waist, tail, sting;

            Read(out Adj, out n);
            bool found = Solve(Adj, n, out waist, out tail, out sting);

            if(!found)
            {
                Console.WriteLine("Skorpionas nerastas");
            }
            else
            {
                Print(n, waist, tail, sting);
            }
        }

        static void Read(out List<string> Adj, out int n)
        {
            List<string> Inputs = File.ReadAllLines(INPUT_FILE).ToList();

            n = Convert.ToInt32(Inputs[0]);

            Inputs.RemoveAt(0);
            Adj = Inputs;
        }

        static bool Solve(List<string> Adj, int n,
                          out int waist, out int tail, out int sting)
        {
            waist = tail = sting = -1;

            for (int i = 0; i < n; i++)
            {
                string row = Adj[i];
                int cnt = row.Count(c => c == '+');
                if (cnt == n - 2)
                {
                    if (waist == -1)
                    {
                        waist = i;
                    }
                    else
                    {
                        // negali tureti keletos liemeniu
                        return false;
                    }
                }
            }

            if(waist == -1)
            {
                // turi tureti liemeni
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                if (i == waist) continue;
                if(Adj[i][waist] == '-' && Adj[i].Count(c => c == '+') == 1)
                {
                    sting = i;
                    tail = Adj[i].IndexOf('+');
                    if(Adj[tail].Count(c => c == '+') == 2)
                    {
                        return true;
                    }
                    else
                    {
                        // uodega negali buti sujungta su kojomis
                        return false;
                    }
                }
            }

            // nerasta uodegos ir geluonies
            return false;
        }

        static void Print(int n, int waist, int tail, int sting)
        {
            Console.WriteLine("Grafas yra skorpionas");
            Console.WriteLine("Geluonis: {0} viršūnė", sting + 1);
            Console.WriteLine("Uodega: {0} viršūnė", tail + 1);
            Console.WriteLine("Liemuo: {0} viršūnė", waist + 1);
            for(int i = 0, cnt = 1; i < n; i++)
            {
                if(i != sting && i != tail && i != waist)
                {
                    Console.WriteLine("{0} koja: {1} viršūnė", cnt++, i + 1);
                }
            }
        }
    }
}