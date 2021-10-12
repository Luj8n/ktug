using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace LD_9
{
  class Program
  {
    static List<int> AddCopy(List<int> list, int x)
    {
      var a = new List<int>(list);
      a.Add(x);
      return a;
    }
    static string Solve(string path)
    {
      List<List<int>> map = File.ReadLines(path).Skip(1).Select(r => r.Split(" ").Select(x => int.Parse(x)).ToList()).ToList();

      int N = map.Count;

      int minPrice = int.MaxValue;
      List<int> minPath = new List<int>();

      void recur(int currentPrice, List<int> collectedNodes)
      {
        if (currentPrice >= minPrice) return;
        if (collectedNodes.Count == N)
        {
          int returnPrice = map[collectedNodes.Last()][0];
          currentPrice += returnPrice;
          collectedNodes.Add(0);
          if (currentPrice < minPrice)
          {
            minPrice = currentPrice;
            minPath = collectedNodes;
          }
          return;
        }
        for (int nextNode = 0; nextNode < N; nextNode++)
        {
          if (!collectedNodes.Contains(nextNode))
          {
            int price = map[collectedNodes.Last()][nextNode];
            recur(currentPrice + price, AddCopy(collectedNodes, nextNode));
          }
        }
      }

      recur(0, new List<int>() { 0 });

      return String.Join(", ", minPath.Select(x => x + 1)) + $" = {minPrice}";
    }
    static void Main(string[] args)
    {
      string answer = Solve("../../../input/U3_2.txt");
      Console.WriteLine(answer);
    }
  }
}
