using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle5
    {
        public static void SolvePuzzle5_P1(string inputFile)
        {
            using (StreamReader sr = new StreamReader(inputFile))
            {
                int maxID = int.MinValue;
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    string row = buffer.Remove(buffer.Length - 3);
                    string seat = buffer.Substring(row.Length);
                    int rowNo = FindValue(row, 0, 127, 64, 'F');
                    int seatNo = FindValue(seat, 0, 7, 4, 'L');
                    int seatID = (8 * rowNo) + seatNo;

                    maxID = Math.Max(seatID, maxID);

                    buffer = sr.ReadLine();
                }
                Console.WriteLine($"Max seat ID: {maxID}");
            }
        }

        public static int FindValue(string input, double low, double high, double diff, char directionIndicator)
        {
            for (int i = 0; i < input.Length -1; i++)
            {
                if (input[i] == directionIndicator)
                    high = high - diff;
                else
                    low = low + diff;

                diff = Math.Ceiling((high - low)/2);
            }

            if (input[input.Length - 1] == directionIndicator)
                return (int)low;
            return (int)high;
        }

        public static void SolvePuzzle5_P2(string inputFile)
        {
            using (StreamReader sr = new StreamReader(inputFile))
            {
                List<int> seatIDs = new List<int>();
                int minID = int.MaxValue;
                int maxID = int.MinValue;
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    string row = buffer.Remove(buffer.Length - 3);
                    string seat = buffer.Substring(row.Length);
                    int rowNo = FindValue(row, 0, 127, 64, 'F');
                    int seatNo = FindValue(seat, 0, 7, 4, 'L');
                    int seatID = (8 * rowNo) + seatNo;

                    minID = Math.Min(seatID, maxID);
                    maxID = Math.Max(seatID, maxID);

                    seatIDs.Add(seatID);

                    buffer = sr.ReadLine();
                }

                for (int i = minID; i < maxID; i++)
                {
                    if (!seatIDs.Contains(i) && seatIDs.Contains(i + 1) && seatIDs.Contains(i - 1))
                    {
                        Console.WriteLine($"Seat ID: {i}");
                        break;
                    }
                }
            }
        }

    }
}
