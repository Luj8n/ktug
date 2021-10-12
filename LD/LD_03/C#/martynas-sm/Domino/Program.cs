using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domino
{
    class Program
    {
        const string inputFile = @"../../../Kur3.txt";
        const string outputFile = @"../../../Kur3_out.txt";

        static List<List<string>> Combinations;
        static void Main(string[] args)
        {
            Combinations = new List<List<string>>();
            List<string> Domino = new List<string>();
            List<bool> Visited = new List<bool>();
            List<string> Curr = new List<string>();

            Read(ref Domino);

            Init(Domino.Count, ref Visited);

            Recur(Domino, Visited, Curr);

            if (Combinations.Count > 0)
            {
                File.WriteAllLines(outputFile, Combinations.Select(combo => String.Join(" ", combo)));
            }
            else
            {
                File.WriteAllText(outputFile, "Galimų grandinių nėra");
            }
        }

        static void Read(ref List<string> Domino)
        {
            StreamReader FileStream = new StreamReader(inputFile);
            Domino = FileStream.ReadLine().Split(' ').ToList();
        }

        static void Init(int n, ref List<bool> Visited)
        {
            for (int i = 0; i < n; i++)
            {
                Visited.Add(false);
            }
        }

        static void Recur(List<string> Domino, List<bool> Visited, List<string> Curr)
        {
            if(Curr.Count == Domino.Count)
            {
                Combinations.Add(new List<string>(Curr));
                return;
            }
            for(int i = 0; i < Domino.Count; i++)
            {
                if(Visited[i] == false)
                {
                    Visited[i] = true;
                    char l, r;
                    l = Domino[i][0];
                    r = Domino[i][1];
                    if(Curr.Count == 0 || Curr.Last()[1] == l)
                    {
                        string domino = "";
                        domino += l;
                        domino += r;
                        Curr.Add(domino);
                        Recur(Domino, Visited, Curr);
                        Curr.RemoveAt(Curr.Count - 1);
                    }
                    if (Curr.Count == 0 || Curr.Last()[1] == r)
                    {
                        string domino = "";
                        domino += r;
                        domino += l;
                        Curr.Add(domino);
                        Recur(Domino, Visited, Curr);
                        Curr.RemoveAt(Curr.Count - 1);
                    }
                    Visited[i] = false;
                }
            }
        }
    }
}