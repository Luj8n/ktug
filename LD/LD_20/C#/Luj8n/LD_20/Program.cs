using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace LD_20
{
  class Program
  {
    static List<string> AddCopy(List<string> list, string x)
    {
      var a = new List<string>(list);
      a.Add(x);
      return a;
    }
    static string Solve(string path)
    {
      List<string> input = File
        .ReadLines(path)
        .ToList();

      int N = int.Parse(input[0].Trim());
      string start = input[1].Trim();

      List<string> cities = new List<string>();

      Dictionary<string, Dictionary<string, int>> g = new Dictionary<string, Dictionary<string, int>>();

      for (int i = 0; i < N; i++)
      {
        string[] line = input[i + 2].Trim().Split(' ');

        string a = line[0]; // city a
        string b = line[1]; // city b
        int d = int.Parse(line[2]); // distance

        if (!cities.Contains(a)) cities.Add(a);
        if (!cities.Contains(b)) cities.Add(b);

        if (!g.ContainsKey(a)) g.Add(a, new Dictionary<string, int>());
        if (!g.ContainsKey(b)) g.Add(b, new Dictionary<string, int>());

        g[a].Add(b, d);
        g[b].Add(a, d);
      }

      int longestDistance = -1;
      List<string> longestPath = new List<string>();

      void recurse(List<string> visitedCities, int currentDistance)
      {
        string currentCity = visitedCities.Last();

        List<string> leftCities = cities.Where(c => !visitedCities.Contains(c) && g[currentCity].ContainsKey(c)).ToList();

        if (leftCities.Count == 0)
        {
          if (currentDistance > longestDistance)
          {
            longestDistance = currentDistance;
            longestPath = visitedCities;
          }
        }
        else
        {
          leftCities.ForEach(nextCity => recurse(AddCopy(visitedCities, nextCity), currentDistance + g[currentCity][nextCity]));
        }
      }

      recurse(new List<string>() { start }, 0);

      return String.Join(" - ", longestPath) + $" ({longestDistance}) km";
    }
    static void Main(string[] args)
    {
      string answer = Solve("U3.txt");
      Console.WriteLine(answer);
    }
  }
}
