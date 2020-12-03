using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    static class Puzzle3
    {
        // Took 40 minutes from first looking at the puzzle to answer submission.
        const string INPUT = @"E:\AdventOfCode\Inputs\Puzzle3.txt";
        const char TREE = '#';

        public static void SolvePuzzle3_P1()
        {
            string treeline = string.Empty;
            int treeCount = 0;
            var lineCount = 1;
            using (var sr = new StreamReader(INPUT))
            {
                int hpos = 0;
                treeline = sr.ReadLine();
                while (treeline != null)
                {
                    if (hpos + 1 > treeline.Length)
                        hpos = hpos - treeline.Length;

                    if (treeline[hpos] == TREE)
                        treeCount++;

                    hpos +=3;
                    treeline = sr.ReadLine();
                    lineCount++;
                }
            }

            Console.WriteLine(treeCount);
        }

        /*
Right 1, down 1.
Right 3, down 1. (This is the slope you already checked.)
Right 5, down 1.
Right 7, down 1.
Right 1, down 2.
*/
        public static void SolvePuzzle3_P2()
        {
            List<int> right = new List<int>() { 1, 3, 5, 7};
            List<int> hpos = new List<int>() {0, 0, 0, 0};
            long[] treeCount = new long[5];

            int specialCaseHpos = 0;
            
            string treeline = string.Empty;
            var lineCount = 1;
            using (var sr = new StreamReader(INPUT))
            {
                treeline = sr.ReadLine();
                while (treeline != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (hpos[i] + 1 > treeline.Length)
                            hpos[i] = hpos[i] - treeline.Length;

                        if (treeline[hpos[i]] == TREE)
                            treeCount[i] += 1;

                        hpos[i] = hpos[i] + right[i];
                    }

                    if (lineCount % 2 == 1)
                    {
                        if (specialCaseHpos + 1 > treeline.Length)
                            specialCaseHpos = specialCaseHpos - treeline.Length;
                        // special case
                        if (treeline[specialCaseHpos] == TREE)
                            treeCount[4]++;

                        specialCaseHpos++;
                    }

                    treeline = sr.ReadLine();
                    lineCount++;
                }
            }
            Console.WriteLine($"Counts: {treeCount[0]}, {treeCount[1]},{treeCount[2]},{treeCount[3]},{treeCount[4]}");
            Console.WriteLine($"Product: {treeCount[0] * treeCount[1] * treeCount[2] * treeCount[3] * treeCount[4]}");
        }

    }
}
