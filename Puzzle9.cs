using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle9
    {
        #region Part 1
        public static void SolvePuzzle9_P1(string inputFile)
        {
            List<long> inputData = new List<long>();
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    inputData.Add(long.Parse(buffer));
                    buffer = sr.ReadLine();
                }
            }

            int targetIndex = 25;
            while (true)
            {
                if (!IsValid(inputData, targetIndex))
                {
                    Console.WriteLine($"Found invalid number {inputData[targetIndex]} at index {targetIndex}");
                    return;
                }
                targetIndex++;
            }
        }

        public static bool IsValid(List<long> inputData, int targetIndex)
        {
            long targetValue = inputData[targetIndex];
            int startIndex = targetIndex - 25;
            int endIndex = targetIndex - 1;
            for (int i = startIndex; i < targetIndex; i++)
            {
                for (int j = i; j < targetIndex; j++)
                {
                    if (i != j && inputData[i] + inputData[j] == targetValue)
                        return true;
                }
            }
            return false;
        }
        #endregion

        #region Part 1
        public static void SolvePuzzle9_P2(string inputFile)
        {
            List<long> inputData = new List<long>();
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    inputData.Add(long.Parse(buffer));
                    buffer = sr.ReadLine();
                }
            }

            int targetIndex = 0;
            while (true)
            {
                if (FoundWeakness(inputData, targetIndex))
                {
                    return;
                }
                targetIndex++;
            }
        }

        public static bool FoundWeakness(List<long> inputData, int startIndex)
        {
            const long targetValue = 27911108;
            long curValue = inputData[startIndex];
            long maxValue = inputData[startIndex];
            long minValue = inputData[startIndex];
            for (int i = startIndex + 1; i < inputData.Count; i++)
            {
                maxValue = Math.Max(maxValue, inputData[i]);
                minValue = Math.Min(minValue, inputData[i]);
                curValue += inputData[i];
                if (curValue == targetValue)
                {
                    Console.WriteLine($"Found range of {startIndex} - {i}: Max {maxValue}, Min {minValue}: Answer {maxValue + minValue}");
                    return true;
                }
                if (curValue > targetValue)
                    return false;
            }
            return false;
        }
        #endregion
    }
}
