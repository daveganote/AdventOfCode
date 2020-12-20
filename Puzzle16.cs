using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle16
    {
        public static void SolvePuzzle16_P1(string inputfile)
        {
            List<TicketSection> Sections = new List<TicketSection>();
            List<string> tickets = new List<string>();
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                while (!string.IsNullOrEmpty(buffer))
                {
                    Sections.Add(new TicketSection(buffer));
                    buffer = sr.ReadLine();
                }

                while (buffer != "nearby tickets:")
                {
                    buffer = sr.ReadLine();
                }
                buffer = sr.ReadLine();
                while (!string.IsNullOrEmpty(buffer))
                {
                    tickets.Add(buffer);
                    buffer = sr.ReadLine();
                }
            }

            int errorRate = 0;
            foreach (var ticket in tickets)
            {
                GetTicketErrorRate(ticket, Sections, ref errorRate);
            }
            Console.WriteLine($"Error rate: {errorRate}");
        }

        private static bool GetTicketErrorRate(string ticket, List<TicketSection> sections, ref int errorRate)
        {
            bool allSectionsValid = true;
            foreach (var s in ticket.Split(','))
            {
                bool foundValidSection = false;
                int item = int.Parse(s);
                foreach (TicketSection sect in sections)
                {
                    if (sect.IsInValidRange(item))
                    {
                        foundValidSection = true;
                        break;
                    }
                }
                if (!foundValidSection)
                {
                    allSectionsValid = false;
                    errorRate += item;
                }
            }
            return allSectionsValid;
        }


        public static void SolvePuzzle16_P2(string inputfile)
        {
            List<TicketSection> Sections = new List<TicketSection>();
            List<string> tickets = new List<string>();
            string myTicket = string.Empty;
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                while (!string.IsNullOrEmpty(buffer))
                {
                    Sections.Add(new TicketSection(buffer));
                    buffer = sr.ReadLine();
                }

                buffer = sr.ReadLine();
                myTicket = sr.ReadLine();
                while (buffer != "nearby tickets:")
                {
                    buffer = sr.ReadLine();
                }
                buffer = sr.ReadLine();
                while (!string.IsNullOrEmpty(buffer))
                {
                    tickets.Add(buffer);
                    buffer = sr.ReadLine();
                }
            }

            List<string> validTickets = new List<string>();
            foreach (var ticket in tickets)
            {
                if (ticket.Contains(",0"))
                {
                    Console.WriteLine(ticket);
                }
                int errorRate = 0;
                if (GetTicketErrorRate(ticket, Sections, ref errorRate))
                    validTickets.Add(ticket);
            }

            var columns = DetermineColumns(Sections, validTickets);
            var myVals = myTicket.Split(',');
            long answer = 1;
            foreach (var col in columns)
            {
                if (col.Value.ValidSectionNames.First().StartsWith("departure"))
                {
                    Console.WriteLine($"Column {col.Key}, {col.Value.ValidSectionNames.First()}, value {myVals[col.Key]}");
                    answer *= long.Parse(myVals[col.Key]);
                }
            }
            Console.WriteLine($"Final answer: {answer}");
        }


        private static Dictionary<int, TicketColumn> DetermineColumns(List<TicketSection> sections, List<string> validTickts)
        {
            Dictionary<int, TicketColumn> columns = new Dictionary<int, TicketColumn>();
            string validColVals = string.Empty;
            
            // Get the values in each column
            foreach (var s in validTickts)
            {
                var cols = s.Split(',');
                for (int i = 0; i < cols.Length; i++)
                {
                    if (!columns.ContainsKey(i)) columns.Add(i, new TicketColumn(i));
                    columns[i].ColumnValues.Add(int.Parse(cols[i]));
                }
            }

            // Find the sections for which all column values are valid.
            foreach (var ts in sections)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    columns[i].AddSectionIfValid(ts);
                }
            }

            // Debug
            for (int i = 0; i < columns.Count; i++)
            {
                Console.WriteLine($"{i} - {columns[i].EnumerateSections()}");
            }
            Console.WriteLine();

            // Now we have a dictionary of columns and which sections are valid. Find all the columns
            // where there is only one possibile section that fits, and remove that from the other columns.
            while (columns.Any(f => f.Value.ValidSectionNames.Count > 1))
            {
                foreach (var singleColumn in columns.Where(f => f.Value.ValidSectionNames.Count == 1))
                {
                    var sectionName = singleColumn.Value.ValidSectionNames.First();
                    foreach (var col in columns)
                    {
                        if (col.Key != singleColumn.Key && col.Value.ValidSectionNames.Contains(sectionName))
                        {
                            Console.WriteLine($"Removed {sectionName} from column {col.Key} ");
                            col.Value.ValidSectionNames.Remove(sectionName);
                        }
                    }
                }
            }
            Console.WriteLine();
            for (int i = 0; i < columns.Count; i++)
            {
                Console.WriteLine($"{i} - {columns[i].EnumerateSections()}");
            }
            return columns;
        }

        private class TicketColumn
        {
            public int ColumnNumber = 0;
            public HashSet<int> ColumnValues = new HashSet<int>();
            public HashSet<string> ValidSectionNames;

            public TicketColumn(int columnNumber)
            {
                ColumnNumber = columnNumber;
                ValidSectionNames = new HashSet<string>();
            }

            public void AddSectionIfValid(TicketSection section)
            {
                foreach (int i in ColumnValues)
                {
                    if (!section.IsInValidRange(i))
                        return;
                }
                ValidSectionNames.Add(section.SectionName);
            }

            public string EnumerateSections()
            {
                if (ValidSectionNames.Count == 0) return string.Empty;
                string validSections = string.Empty;
                foreach (var s in ValidSectionNames)
                {
                    validSections += s + "| ";
                }
                return validSections.Remove(validSections.Length - 2);
            }
        }

        private class TicketSection
        {
            public string SectionName = string.Empty;
            public int R1Low = 0;
            public int R1High = 9;
            public int R2Low = 0;
            public int R2High = 0;
            public TicketSection(string ticket)
            {
                var ts = ticket.Split(':');
                SectionName = ts[0];
                var validRanges = ts[1].Replace(" or ", "&").Split('&');
                var validRange = validRanges[0].Trim().Split('-');
                R1Low = int.Parse(validRange[0]);
                R1High = int.Parse(validRange[1]);
                validRange = validRanges[1].Trim().Split('-');
                R2Low = int.Parse(validRange[0]);
                R2High = int.Parse(validRange[1]);
            }

            public bool IsInValidRange(int value)
            {
                return ((value >= R1Low && value <= R1High) || (value >= R2Low && value <= R2High));
            }
        }
    }
}
