using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle7
    {
        #region Part1
        public static void SolvePuzzle7_P1(string inputFile)
        {
            const string BAGDELIMITER = " bags contain ";

            using (StreamReader sr = new StreamReader(inputFile))
            {
                Dictionary<string, BagP1> bagList = new Dictionary<string, BagP1>();

                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    // do stuff
                    var bagID = buffer.Substring(0, buffer.IndexOf(BAGDELIMITER));
                    var constituents = buffer.Substring(buffer.IndexOf(BAGDELIMITER) + BAGDELIMITER.Length);
                    bagList.Add(bagID, new BagP1(bagID, constituents));
                    buffer = sr.ReadLine();
                }

                List<string> masterList = new List<string>();
                AddBagsToMasterList(bagList, "shiny gold", masterList, "shiny gold");
                Console.WriteLine($"Final Bag Count: {masterList.Count}");

            }
        }

        private static void AddBagsToMasterList(Dictionary<string, BagP1> bagList, string bagID, List<string> masterList, string heirarchy)
        {
            foreach (BagP1 b in bagList.Values)
            {
                if (b.Constituents.Contains(bagID) && !masterList.Contains(b.BagID))
                {
                    Console.WriteLine($"Adding Bag {b.BagID} for constituent {bagID}");
                    Console.WriteLine($"    {heirarchy}");
                    masterList.Add(b.BagID);
                    AddBagsToMasterList(bagList, b.BagID, masterList, b.BagID + "->" + heirarchy);
                }
            }
        }

        private class BagP1
        {
            public string BagID;
            public List<string> Constituents = new List<string>();
            public BagP1(string bagID, string constituentBags)
            {
                BagID = bagID;
                var constituents = constituentBags.Split(',');
                foreach (var subBag in constituents)
                {
                    var tokens = subBag.Trim().Split(' ');
                    Constituents.Add(tokens[1] + " " + tokens[2]);
                }
            }
        }
        #endregion

        #region Part2
        public static void SolvePuzzle7_P2(string inputFile)
        {
            const string BAGDELIMITER = " bags contain ";

            using (StreamReader sr = new StreamReader(inputFile))
            {
                Dictionary<string, BagP2> bagList = new Dictionary<string, BagP2>();

                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    // do stuff
                    var bagID = buffer.Substring(0, buffer.IndexOf(BAGDELIMITER));
                    var constituents = buffer.Substring(buffer.IndexOf(BAGDELIMITER) + BAGDELIMITER.Length);
                    bagList.Add(bagID, new BagP2(bagID, constituents));
                    buffer = sr.ReadLine();
                }
                Console.WriteLine($"Bag Count: {CountBags(bagList["shiny gold"], bagList) - 1}"); // don't count the shiny gold bag.
            }
        }

        private static int CountBags(BagP2 bag, Dictionary<string, BagP2> bagList)
        {
            int bagCount = 1; // for this bag
            foreach (var constutuent in bag.Constituents)
            {
                bagCount += CountBags(bagList[constutuent.Key], bagList) * constutuent.Value;
            }
            Console.WriteLine($"Returning {bagCount} for bag {bag.BagID} "); 
            return bagCount;
        }

        private class BagP2
        {
            public string BagID;
            public Dictionary<string, int> Constituents = new Dictionary<string, int>();
            public BagP2(string bagID, string constituentBags)
            {
                BagID = bagID;
                if (!constituentBags.Contains("no other bags"))
                {
                    var constituents = constituentBags.Split(',');
                    foreach (var subBag in constituents)
                    {
                        var tokens = subBag.Trim().Split(' ');
                        Constituents.Add(tokens[1] + " " + tokens[2], int.Parse(tokens[0]));
                    }
                }
            }
        }
        #endregion
    }
}
