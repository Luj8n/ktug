using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zodziu_paieska
{
    class Program
    {
        const string INPUT_FILE1 = @"../../../Trecias.txt";
        const string INPUT_FILE2 = @"../../../Zodziai_1.txt";
        static void Main(string[] args)
        {
            string inputLine;
            List<string> Words;
            List<string> Matrix;
            Dictionary<string, int> Map;
            int n;

            Read(out inputLine, out Words);
            Init(out Matrix, out Map, out n, inputLine, Words);
            Solve(Matrix, ref Map, n);
            Print(Map, n);
        }

        static void Read(out string inputLine, out List<string> Words)
        {
            inputLine = String.Concat(File.ReadAllLines(INPUT_FILE1));
            Words = File.ReadAllLines(INPUT_FILE2).ToList();
        }

        static void Init(out List<string> Matrix, out Dictionary<string, int> Map, out int n, string inputLine, List<string> Words)
        {
            n = (int)Math.Ceiling(Math.Sqrt(inputLine.Length));

            Matrix = new List<string>(n);
            Map = new Dictionary<string, int>();

            for(int i = 0; i < n; i++)
            {
                string line = "";
                for(int j = 0; j < n; j++)
                {
                    int idx = i * n + j;
                    if(idx >= inputLine.Length)
                    {
                        line += " ";
                    }
                    else
                    {
                        line += inputLine[idx];
                    }
                }
                Matrix.Add(line);
            }

            foreach(var word in Words)
            {
                Map[word.ToLower()] = 0;
            }
        }

        static void Solve(List<string> Matrix, ref Dictionary<string, int> Map, int n)
        {
            // each row
            for(int i = 0; i < n; i++)
            {
                string str = "";
                for (int j = 0; j < n; j++)
                {
                    str += Matrix[i][j];
                }
                for(int l = 1; l < n; l++)
                {
                    for (int s = 0; s + l <= n; s++)
                    {
                        string lookFor = str.Substring(s, l);
                        if(Map.ContainsKey(lookFor))
                        {
                            Map[lookFor]++;
                        }
                    }
                }
            }
            // each col
            for (int j = 0; j < n; j++)
            {
                string str = "";
                for (int i = 0; i < n; i++)
                {
                    str += Matrix[i][j];
                }
                for (int l = 1; l < n; l++)
                {
                    for (int s = 0; s + l <= n; s++)
                    {
                        string lookFor = str.Substring(s, l);
                        if (Map.ContainsKey(lookFor))
                        {
                            Map[lookFor]++;
                        }
                    }
                }
            }
            // diagonal that starts on 1st row
            for (int j = 0; j < n; j++)
            {
                string str = "";
                int jj = j;
                for (int i = 0; i < n && jj < n; i++, jj++)
                {
                    str += Matrix[i][jj];
                }
                for (int l = 1; l < n; l++)
                {
                    for (int s = 0; s + l <= str.Length; s++)
                    {
                        string lookFor = str.Substring(s, l);
                        if (Map.ContainsKey(lookFor))
                        {
                            Map[lookFor]++;
                        }
                    }
                }
            }
            // diagonal that starts on 1st col
            for (int i = 0; i < n; i++)
            {
                string str = "";
                int ii = i;
                for (int j = 0; j < n && ii < n; j++, ii++)
                {
                    str += Matrix[ii][j];
                }
                for (int l = 1; l < n; l++)
                {
                    for (int s = 0; s + l <= str.Length; s++)
                    {
                        string lookFor = str.Substring(s, l);
                        if (Map.ContainsKey(lookFor))
                        {
                            Map[lookFor]++;
                        }
                    }
                }
            }
        }

        static void Print(Dictionary<string, int> Map, int n)
        {
            Console.WriteLine("n = {0}", n);
            foreach(KeyValuePair<string, int> p in Map)
            {
                Console.WriteLine("{0} {1}", p.Key, p.Value);
            }
        }
    }
}
