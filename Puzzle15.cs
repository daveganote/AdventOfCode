using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle15
    {
        public static void SolvePuzzle15()
        {
            string input = "2,20,0,4,1,17";
            int lastSpoken = 0;
            Dictionary<int, List<int>> spoken = new Dictionary<int, List<int>>();

            // Implementation assumes inputs are unique, so after initial add last spoken number is 0.
            var inputs = input.Split(',');
            for (int i = 0; i < inputs.Length; i++)
            {
                lastSpoken = int.Parse(inputs[i]);
                spoken.Add(lastSpoken, new List<int>() {i+1});
            }

            lastSpoken = 0;
            for (int i = inputs.Length + 1; i < 30000000; i++)
            {
                if (spoken.ContainsKey(lastSpoken))
                {
                    spoken[lastSpoken].Add(i);
                    var positions = spoken[lastSpoken];
                    lastSpoken = positions[positions.Count - 1] - positions[positions.Count - 2];
                }
                else
                {
                    spoken.Add(lastSpoken, new List<int>() {i});
                    lastSpoken = 0;
                }
            }
            Console.WriteLine($"Last spoken: {lastSpoken}");
        }
    }
}
