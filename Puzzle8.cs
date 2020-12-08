using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle8
    {
        #region part 1
        public static void SolvePuzzle8_P1(string inputFile)
        {
            List<InstructionP1> instructionSet = new List<InstructionP1>();
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    var instruct = buffer.Split(' ');
                    instructionSet.Add(new InstructionP1(instruct[0], instruct[1]));
                    buffer = sr.ReadLine();
                }
            }

            long accumulator = 0;
            int curIndex = 0;
            HashSet<int> visitedIndexes = new HashSet<int>();
            while (true)
            {
                if (visitedIndexes.Contains(curIndex))
                {
                    Console.WriteLine($"Reached operation to be executed a second time. Global value is : {accumulator} ");
                    break;
                }

                visitedIndexes.Add(curIndex);
                InstructionP1 CI = instructionSet[curIndex]; // just for readability.
                switch (CI.Operation)
                {
                    case "acc":
                    case "nop": // OpValue will always be 0
                        accumulator += CI.OpValue;
                        curIndex++;
                        break;
                    case "jmp":
                        curIndex += CI.OpValue;
                        break;
                }
            }
        }

        internal class InstructionP1
        {
            public string Operation;
            public int OpValue = 0;

            public InstructionP1(string op, string val)
            {
                Operation = op;
                if (op != "nop")
                    OpValue = int.Parse(val);
            }
        }
        #endregion

        #region part 2
        public static void SolvePuzzle8_P2(string inputFile)
        {
            List<InstructionP2> instructionSet = new List<InstructionP2>();
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    var instruct = buffer.Split(' ');
                    instructionSet.Add(new InstructionP2(instruct[0], instruct[1]));
                    buffer = sr.ReadLine();
                }
            }

            long accumulator = 0;
            int curIndex = 0;
            HashSet<int> visitedIndexes = new HashSet<int>();
            List<int> suspectIndicies = new List<int>();
            while (true)
            {
                if (visitedIndexes.Contains(curIndex))
                {
                    Console.WriteLine($"Reached operation to be executed a second time. Suspect list contains: {suspectIndicies.Count} items. ");
                    break;
                }

                visitedIndexes.Add(curIndex);
                InstructionP2 CI = instructionSet[curIndex]; // just for readability.
                switch (CI.Operation)
                {
                    case "acc":
                        accumulator += CI.OpValue;
                        curIndex++;
                        break;
                    case "nop": // OpValue will always be 0
                        suspectIndicies.Add(curIndex);
                        curIndex++;
                        break;
                    case "jmp":
                        suspectIndicies.Add(curIndex);
                        curIndex += CI.OpValue;
                        break;
                }
            }

            foreach (int sus in suspectIndicies)
            {
                accumulator = IterateWithSuspectIndex(instructionSet, sus);
                if (accumulator != int.MinValue)
                {
                    Console.WriteLine($"Completed iteration after changing index {sus}, accumulator = {accumulator}");
                    break;
                }
            }
        }

        private static long IterateWithSuspectIndex(List<InstructionP2> instructionSet, int suspectIndex)
        {
            long accumulator = 0;
            int curIndex = 0;
            HashSet<int> visitedIndexes = new HashSet<int>();
            while (true)
            {
                if (visitedIndexes.Contains(curIndex))
                {
                    Console.WriteLine($"Reached operation to be executed a second time. Suspect {suspectIndex} is not the culpruit.");
                    return int.MinValue;
                }

                visitedIndexes.Add(curIndex);
                InstructionP2 CI = instructionSet[curIndex]; // just for readability.
                switch (CI.Operation)
                {
                    case "acc":
                        accumulator += CI.OpValue;
                        curIndex++;
                        break;
                    case "nop": // OpValue will always be 0
                        if (curIndex == suspectIndex)
                            curIndex += CI.OpValue;
                        else
                            curIndex++;
                        break;
                    case "jmp":
                        if (curIndex == suspectIndex)
                            curIndex++;
                        else
                            curIndex += CI.OpValue;
                        break;
                }
                if (curIndex == instructionSet.Count)
                    return accumulator;
            }

        }
        internal class InstructionP2
        {
            public string Operation;
            public int OpValue = 0;

            public InstructionP2(string op, string val)
            {
                Operation = op;
                OpValue = int.Parse(val);
            }
        }
        #endregion

    }
}
