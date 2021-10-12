using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Atilos_zirgas
{
    class Program
    {
        const string file = @"../../../U3.txt";
        const int N = 8;

        class Node
        {
            public List<List<string>> Matrix { get; set; }
            public List<List<bool>> Visited { get; set; }
            public int currI { get; set; }
            public int currJ { get; set; }
            public int move { get; set; }
            public bool lookForKnight { get; set; }

            public Node(List<List<string>> Matrix, List<List<bool>> Visited,
                        int currI, int currJ, int move, bool lookForKnight)
            {
                this.Matrix = Matrix;
                this.Visited = Visited;
                this.currI = currI;
                this.currJ = currJ;
                this.move = move;
                this.lookForKnight = lookForKnight;
            }

            public Node(Node origin)
            {
                this.Matrix = new List<List<string>>();
                for(int i = 0; i < origin.Matrix.Count; i++)
                {
                    this.Matrix.Add(new List<string>(origin.Matrix[i]));
                }
                this.Visited = new List<List<bool>>();
                for (int i = 0; i < origin.Visited.Count; i++)
                {
                    this.Visited.Add(new List<bool>(origin.Visited[i]));
                }
                this.currI = origin.currI;
                this.currJ = origin.currJ;
                this.move = origin.move;
                this.lookForKnight = origin.lookForKnight;
            }

            public void Debug()
            {
                Console.WriteLine("Node: {0} {1} move = {2} \n look for knight = {3}",
                                  currI, currJ, move, lookForKnight);

                Console.WriteLine("Visited:");

                Visited.ForEach(list =>
                                Console.WriteLine(String.Join(" ",
                                                  list.Select(val => val ? 1 : 0))));

                Matrix.ForEach(list => Console.WriteLine(String.Join(" ", list)));
            }
        }

        static void Main(string[] args)
        {
            List<List<string>> Matrix = new List<List<string>>();
            List<List<bool>> Visited = new List<List<bool>>();
            int knightI = 0, knightJ = 0;

            Read(ref Matrix, ref knightI, ref knightJ);

            Init(ref Visited);

            var answerNode = Solve(Matrix, Visited, knightI, knightJ, 0, false);

            Print(answerNode);
        }

        static void Read(ref List<List<string>> Matrix, ref int knightI, ref int knightJ)
        {
            StreamReader Reader = new StreamReader(file);

            for(int i = 0; i < N; i++)
            {
                Matrix.Add(
                    Reader.ReadLine()
                          .Split(" ")
                          .ToList());

                for(int j = 0; j < N; j++)
                {
                    if(Matrix[i][j] == "Z")
                    {
                        knightI = i;
                        knightJ = j;
                    }
                }
            }
        }

        static void Init(ref List<List<bool>> Visited)
        {
            for(int i = 0; i < N; i++)
            {
                Visited.Add(new List<bool>());
                for (int j = 0; j < N; j++)
                {
                    Visited[i].Add(false);
                }
            }
        }

        static bool InBounds(int i, int j)
        {
            return i >= 0 && i < N && j >= 0 && j < N;
        }

        static Node Solve(List<List<string>> Matrix, List<List<bool>> Visited,
                         int currI, int currJ, int move, bool lookForKnight)
        {
            Queue<Node> Q = new Queue<Node>();
            Node initNode = new Node(Matrix, Visited, currI, currJ, move, lookForKnight);
            Q.Enqueue(initNode);

            while(Q.Count > 0)
            {
                Node currNode = Q.Peek();
                bool valid = true;
                Q.Dequeue();

                string matrixCell = currNode.Matrix[currNode.currI][currNode.currJ];

                //currNode.Debug();

                if (matrixCell == "0")
                {
                    currNode.Matrix[currNode.currI][currNode.currJ] = currNode.move.ToString();
                }
                else
                {
                    // Zirgas
                    if (matrixCell == "Z")
                    {
                        if (currNode.lookForKnight == true)
                        {
                            return currNode; // radome kelia
                        }
                        // ne pradinis ejimas ir atsidureme ant zirgo
                        else if (currNode.move != 0)
                        {
                            valid = false; // dar nelaikas ieskoti zirgo
                        }
                    }
                    // Karalius
                    else if (matrixCell == "K")
                    {
                        currNode.lookForKnight = true;
                        Q.Clear();
                        Visited = currNode.Visited;
                    }
                }

                if (currNode.move != 0)
                {
                    currNode.Visited[currNode.currI][currNode.currJ] = true;
                    Visited[currNode.currI][currNode.currJ] = true;
                }

                if(valid == false)
                {
                    continue;
                }

                int[] dirI = { 1, 1, 2, 2, -1, -1, -2, -2 };
                int[] dirJ = { 2, -2, 1, -1, 2, -2, 1, -1 };

                for (int k = 0; k < 8; k++)
                {
                    Node newNode = new Node(currNode);
                    newNode.currI = currNode.currI + dirI[k];
                    newNode.currJ = currNode.currJ + dirJ[k];
                    newNode.move++;

                    if (InBounds(newNode.currI, newNode.currJ)
                        && Visited[newNode.currI][newNode.currJ] == false
                        && currNode.Matrix[newNode.currI][newNode.currJ] != "U")
                    {
                        Q.Enqueue(newNode);
                    }
                }
            }
            return initNode;
        }

        static void Print(Node node)
        {
            if(node.move == 0)
            {
                Console.WriteLine("Kelias nerastas");
            }
            else
            {
                Console.WriteLine("  " +
                                  String.Join(" ",
                                  Enumerable.Range(1, 8)
                                            .Select(x =>x.ToString().PadLeft(2, ' '))));
                int counter = 1;
                foreach (var list in node.Matrix)
                {
                    Console.WriteLine(counter.ToString()
                                      + " "
                                      + String.Join(" ",
                                        list.Select(str =>
                                        (str == "0" ? "-" : str).PadLeft(2, ' '))));
                    counter++;
                }
            }
        }
    }
}