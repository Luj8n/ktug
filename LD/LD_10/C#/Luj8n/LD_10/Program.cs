using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace LD_10
{
  class Program
  {
    static List<List<bool>> GenerateMap(int height, int width)
    {
      List<List<bool>> map = new List<List<bool>>();

      for (int y = 0; y < height; y++)
      {
        List<bool> line = new List<bool>();
        for (int x = 0; x < width; x++)
        {
          line.Add(false);
        }
        map.Add(line);
      }
      return map;
    }
    static string Solve(string path)
    {
      List<string> map = File
        .ReadLines(path)
        .Skip(1)
        .ToList();

      int height = map.Count;
      int width = map[0].Length;

      List<List<bool>> visited = GenerateMap(height, width);

      int r(int x, int y)
      {
        if (x < 0 || y < 0 || x >= width || y >= height || visited[y][x] || map[y][x] != 'u') return 0;
        visited[y][x] = true;

        return 1 + r(x + 1, y) + r(x, y + 1) + r(x - 1, y) + r(x, y - 1);
      }

      List<int> moles = new List<int>();

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          int count = r(x, y);
          if (count > 0) moles.Add(count * 5);
        }
      }

      return $"{moles.Count}\n{String.Join('\n', moles.OrderByDescending(x => x))}";
    }
    static void Main(string[] args)
    {
      string answer = Solve("../../../input/U3.txt");
      Console.WriteLine(answer);
    }
  }
}
