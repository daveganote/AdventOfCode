using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    static class Puzzle2
    {
        public static void SolvePuzzle2()
        {
            string inputFile = @"E:\AdventOfCode\Inputs\Puzzle2.txt";

            int validCountP1 = 0;
            int invalidCountP1 = 0;

            int validCountP2 = 0;
            int invalidCountP2 = 0;

            using (StreamReader sr = new StreamReader(inputFile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    var reqspw = buffer.Split(new char[] { ':' });
                    string reqs = reqspw[0];
                    string pw = reqspw[1].Trim();

                    var countsChar = reqs.Split(new char[] { ' ' });
                    string counts = countsChar[0];
                    string pwChar = countsChar[1];

                    var minMaxPos = counts.Split(new char[] { '-' });
                    int minPos1 = int.Parse(minMaxPos[0]);
                    int maxPos2 = int.Parse(minMaxPos[1]);

                    int instanceCount = pw.Length - pw.Replace(pwChar, string.Empty).Length;

                    if (instanceCount >= minPos1 && instanceCount <= maxPos2)
                        validCountP1++;
                    else
                        invalidCountP1++;

                    if ((pw[minPos1 - 1] == pwChar[0] && pw[maxPos2 - 1] != pwChar[0]) || (pw[minPos1 - 1] != pwChar[0] && pw[maxPos2 - 1] == pwChar[0]))
                    {
                        validCountP2++;
                    }
                    else
                    {
                        invalidCountP2++;
                    }

                    buffer = sr.ReadLine();
                }
            }
            Console.WriteLine($"Valid Part 1: {validCountP1}, Invalid Part 1: {invalidCountP1}");
            Console.WriteLine($"Valid Part 2: {validCountP2}, Invalid Part 1: {invalidCountP2}");
        }
    }
}
