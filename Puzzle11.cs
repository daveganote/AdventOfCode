using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    public static class Puzzle11
    {

        #region P1
        public static void SolvePuzzle11_P1(string inputfile)
        {
            List<string> layout = new List<string>();
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                // padding the layout to avoid the edge special cases.
                layout.Add(new string('.', buffer.Length + 2));
                while (buffer != null)
                {
                    layout.Add($".{buffer}.");
                    buffer = sr.ReadLine();
                }
                layout.Add(layout[0]);
            }

            bool seatsChanged = true;
            while (seatsChanged)
            {
                layout = ApplySeatingRulesP1(layout, out seatsChanged);
            }
            Console.WriteLine($"Seat count after stabilization {CountSeats(layout)}");
        }


        private static List<string> ApplySeatingRulesP1(List<string> existingLayout, out bool seatsChanged)
        {
            seatsChanged = false;
            List<string> newlayout = new List<string>();
            newlayout.Add(existingLayout[0]); // padding
            for (int i = 1; i < existingLayout.Count - 1; i++)
            {
                string newRow = ".";
                for (int j = 1; j < existingLayout[i].Length - 1; j++)
                {
                    int adjacentCount = 0;
                    if ((existingLayout[i][j] == '.'))
                        newRow += '.';
                    else
                    {
                        for (int k = i - 1; k < i + 2; k++)
                        {
                            for (int l = j - 1; l < j + 2; l++)
                            {
                                if (!(k == i && j == l))
                                {
                                    adjacentCount += existingLayout[k][l] == '#' ? 1 : 0;
                                }
                            }
                        }
                        if (adjacentCount == 0 && existingLayout[i][j] == 'L')
                            newRow += '#';
                        else if (adjacentCount >= 4 && existingLayout[i][j] == '#')
                            newRow += 'L';
                        else
                            newRow += existingLayout[i][j];
                    }
                }
                newRow += '.'; // padding
                newlayout.Add(newRow);
                seatsChanged = seatsChanged || string.Compare(newRow, existingLayout[i]) != 0;
            }
            newlayout.Add(existingLayout[0]); //padding
            return newlayout;
        }
        #endregion
        private static long CountSeats(List<string> layout)
        {
            long total = 0;
            foreach (string row in layout)
            {
                total += row.Count(s => s=='#');
            }
            return total;

        }

        #region P2
        public static void SolvePuzzle11_P2(string inputfile)
        {
            List<string> layout = new List<string>();
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                // padding the layout to avoid the edge special cases.
                layout.Add(new string('.', buffer.Length + 2));
                while (buffer != null)
                {
                    layout.Add($".{buffer}.");
                    buffer = sr.ReadLine();
                }
                layout.Add(layout[0]);
            }

            bool seatsChanged = true;
            while (seatsChanged)
            {
                layout = ApplySeatingRulesP2(layout, out seatsChanged);
            }
            Console.WriteLine($"Seat count after stabilization {CountSeats(layout)}");
        }

        private static List<string> ApplySeatingRulesP2(List<string> existingLayout, out bool seatsChanged)
        {
            seatsChanged = false;
            List<string> newlayout = new List<string>();
            newlayout.Add(existingLayout[0]); // padding
            for (int row = 1; row < existingLayout.Count - 1; row++)
            {
                string newRow = ".";
                for (int col = 1; col < existingLayout[row].Length - 1; col++)
                {
                    if (existingLayout[row][col] == '.')
                        newRow += '.';
                    else
                    {
                        int foundChairs = 0;
                        if (FindHorizontal(row, col, 1, existingLayout)) foundChairs++;
                        if (FindHorizontal(row, col, -1, existingLayout)) foundChairs++;
                        if (FindVertical(row, col, 1, existingLayout)) foundChairs++;
                        if (FindVertical(row, col, -1, existingLayout)) foundChairs++;
                        if (FindDiagonal(row, col, 1, 1, existingLayout)) foundChairs++;
                        if (FindDiagonal(row, col, 1, -1, existingLayout)) foundChairs++;
                        if (FindDiagonal(row, col, -1, 1, existingLayout)) foundChairs++;
                        if (FindDiagonal(row, col, -1, -1, existingLayout)) foundChairs++;

                        if (foundChairs == 0 && existingLayout[row][col] == 'L')
                            newRow += '#';
                        else if (foundChairs >= 5 && existingLayout[row][col] == '#')
                            newRow += 'L';
                        else
                            newRow += existingLayout[row][col];
                    }
                }
                newRow += '.';
                newlayout.Add(newRow);
                seatsChanged = seatsChanged || string.Compare(newRow, existingLayout[row]) != 0;
            }
            newlayout.Add(existingLayout[existingLayout.Count - 1]);
            return newlayout;
        }

        private static bool FindHorizontal(int row, int col, int vinc, List<string> layout)
        {
            while (true)
            {
                col += vinc;
                if (col == 0 || col == layout[row].Length) return false;
                if (layout[row][col] != '.') return layout[row][col] == '#';
            }
        }

        private static bool FindVertical(int row, int col, int hinc, List<string> layout)
        {
            while (true)
            {
                row += hinc;
                if (row == 0 || row == layout.Count) return false;
                if (layout[row][col] != '.') return layout[row][col] == '#';
            }
        }

        private static bool FindDiagonal(int row, int col, int hinc, int vinc, List<string> layout)
        {
            while (true)
            {
                row += vinc;
                col += hinc;
                if (row == 0 || row == layout.Count) return false;
                if (col == 0 || col == layout[row].Length) return false;
                if (layout[row][col] != '.') return layout[row][col] == '#';
            }
        }
        #endregion
    }
}
