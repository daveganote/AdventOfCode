using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle14
    {

        #region Part 1
        public static void SolvePuzzle14_P1(string inputfile)
        {
            List<string> inputs = new List<string>();
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    inputs.Add(buffer);
                    buffer = sr.ReadLine();
                }
            }
            ApplyMasking_p1(inputs);
        }

        private static void ApplyMasking_p1(List<string> inputs)
        {
            Dictionary<int, long> memLocations = new Dictionary<int, long>();
            List<string> mems = new List<string>();
            string mask = string.Empty;
            foreach (string s in inputs)
            {
                if (!s.StartsWith("mem"))
                {
                    if (mems.Count > 0)
                        CalculateValues(mask, mems, memLocations);
                    mask = s.Substring(7);
                    mems = new List<string>();
                }
                else
                    mems.Add(s);
            }
            CalculateValues(mask, mems, memLocations);

            long answer = 0;
            foreach (var v in memLocations.Values)
            {
                answer += v;
            }
            Console.WriteLine($"Sum of values: {answer}");
        }

        private static void CalculateValues(string mask, List<string> mems, Dictionary<int, long> values)
        {
            Console.WriteLine(mask);
            foreach (string mem in mems)
            {
                int location = 0;
                long value = 0;
                ParseMem(mem, ref location, ref value);
                long valAftermask = 0;
                string memBits = string.Empty;
                string afterMask = string.Empty;
                for (int p =0; p <= mask.Length - 1; p++)
                {
                    long bitVal = ((long)Math.Pow(2, 35 - p) & value);
                    memBits += bitVal > 0 ? '1' : '0';
                    switch (mask[p])
                    {
                        case 'X':
                            valAftermask += bitVal;
                            break;
                        case '0':
                            break;
                        case '1':
                            valAftermask += (long)Math.Pow(2, 35 - p);
                            break;
                    }
                    afterMask += ((long)Math.Pow(2, 35 - p) & valAftermask) > 0 ? '1' : '0';
                }
                Console.WriteLine($"{mem} - evaluated to {valAftermask}");
                Console.WriteLine(memBits);
                Console.WriteLine(afterMask);
                if (values.ContainsKey(location))
                    values[location] = valAftermask;
                else
                    values.Add(location, valAftermask);
            }
        }

        private static void ParseMem(string mem, ref int location, ref long value)
        {
            var temp = mem.Substring(4).Replace("] =", string.Empty).Split(' ');
            location = int.Parse(temp[0]);
            value = long.Parse(temp[1].Trim());
        }
        #endregion

        #region Part 2

        public static void SolvePuzzle14_P2(string inputfile)
        {
            List<string> inputs = new List<string>();
            using (StreamReader sr = new StreamReader(inputfile))
            {
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    inputs.Add(buffer);
                    buffer = sr.ReadLine();
                }
            }
            ApplyMasking_p2(inputs);
        }


        private static void ApplyMasking_p2(List<string> inputs)
        {
            Dictionary<long, long> values = new Dictionary<long, long>();
            List<string> mems = new List<string>();
            string mask = string.Empty;
            foreach (string s in inputs)
            {
                if (!s.StartsWith("mem"))
                {
                    if (mems.Count > 0)
                        CalculateValues_P2(mask, mems, values);
                    mask = s.Substring(7);
                    mems = new List<string>();
                }
                else
                    mems.Add(s);
            }
            CalculateValues_P2(mask, mems, values);

            long answer = 0;
            foreach (var v in values.Values)
            {
                answer += v;
            }
            Console.WriteLine($"Sum of values: {answer}");
        }

        private static string ApplyMaskToInput(string mask, string input)
        {
            string locationMask = string.Empty;
            for (int p = 0; p <= mask.Length - 1; p++)
            {
                switch (mask[p])
                {
                    case 'X': 
                    case '1':
                        locationMask += mask[p];
                        break;
                    case '0':
                        locationMask += input[p];
                        break;
                }
            }
            return locationMask;
        }

        private static void CalculateValues_P2(string mask, List<string> mems, Dictionary<long, long> values)
        {
            long value = 0;

            foreach (var mem in mems)
            {
                string lstring = ParseMem_P2(mem, ref value);
                string nmask = ApplyMaskToInput(mask, lstring);
                ApplyValueToLocation(nmask, value, values);
            }
        }
        private static string ParseMem_P2(string mem, ref long value)
        {
            var temp = mem.Substring(4).Replace("] =", string.Empty).Split(' ');
            var location = long.Parse(temp[0]);
            value = long.Parse(temp[1].Trim());

            string lasString = string.Empty;
            for (int p = 0; p < 36; p++)
            {
                var posVal = (long)Math.Pow(2, 35 - p) & location;
                lasString += (posVal == 0) ? '0' : '1';
            }
            return lasString;
        }

        private static void ApplyValueToLocation(string lmask, long value, Dictionary<long, long> values)
        {
            Console.WriteLine($"Applying value {value}");
            Console.WriteLine(lmask);
            double lvalue = 0;
            for (int p = 0; p <= lmask.Length - 1; p++)
            {
                switch (lmask[p])
                {
                    case 'X':
                        string nlmask = lmask.Substring(0, p) + '0' + lmask.Substring(p + 1);
                        ApplyValueToLocation(nlmask, value, values);
                        nlmask = lmask.Substring(0, p) + '1' + lmask.Substring(p + 1);
                        ApplyValueToLocation(nlmask, value, values);
                        return;
                    case '1':
                        lvalue += Math.Pow(2, 35 - p);
                        break;
                }
            }
            if (values.ContainsKey((long)lvalue))
                values[(long)lvalue] = value;
            else
                values.Add((long)lvalue, value);
        }
        #endregion
    }
}
