using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    static class Puzzle4
    {

        public static void SolvePuzzle4_P1(string inputFile)
        {
            int validCount = 0;
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string passport = string.Empty;
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    passport = $"{passport} {buffer.Trim()}".Trim();
                    buffer = sr.ReadLine();
                    if (string.IsNullOrEmpty(buffer))
                    {
                        if (CheckPassport(passport))
                            validCount++;
                        passport = string.Empty;
                    }
                }
            }
            Console.WriteLine($"Valid passports: {validCount}");
        }

        private static bool CheckPassport(string passport)
        {
            var ppTokens = passport.Split(' ').ToList();
            var cid = ppTokens.FirstOrDefault(f => f.StartsWith("cid:"));
            return ppTokens.Count == 8 || (ppTokens.Count == 7 && String.IsNullOrEmpty(cid));
        }


        public static void SolvePuzzle4_P2(string inputFile)
        {
            int validCount = 0;
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string passport = string.Empty;
                string buffer = sr.ReadLine();
                while (buffer != null)
                {
                    passport = $"{passport} {buffer.Trim()}".Trim();
                    buffer = sr.ReadLine();
                    if (string.IsNullOrEmpty(buffer))
                    {
                        if (CheckPassportP2(passport))
                            validCount++;
                        else
                            Console.WriteLine($"Invalid: {passport}");
                        passport = string.Empty;
                    }
                }
            }
            Console.WriteLine($"Valid passports: {validCount}");
        }

        private static bool CheckPassportP2(string passport)
        {
            var ppTokens = passport.Split(' ').ToList();
            var cid = ppTokens.FirstOrDefault(f => f.StartsWith("cid:"));
            if (ppTokens.Count == 8 || (ppTokens.Count == 7 && String.IsNullOrEmpty(cid)))
            {
                return ValidateFields(ppTokens);
            }
            Console.Write("FIELDS:");
            return false;
        }

        private static bool ValidateFields(List<string> ppFields)
        {
            foreach (var ppField in ppFields)
            {
                var ppf = ppField.Split(':');
                switch (ppf[0])
                {
                    case "byr":
                        if (!ValidateNumeric(ppf[1], 1920, 2002)) return false;
                        break;
                    case "iyr":
                        if (!ValidateNumeric(ppf[1], 2010, 2020)) return false;
                        break;
                    case "eyr":
                        if (!ValidateNumeric(ppf[1], 2020, 2030)) return false;
                        break;
                    case "hgt":
                        if (!ValidateHeight(ppf[1])) return false;
                        break;
                    case "hcl":
                        if (!ValidateHairColor(ppf[1])) return false;
                        break;
                    case "ecl":
                        if (!ValidateEyeColor(ppf[1])) return false;
                        break;
                    case "pid":
                        if (!ValidatePassportID(ppf[1])) return false;
                        break;
                    default: //cid
                        break;
                }

            }
            return true;
        }

        private static bool ValidateNumeric(string input, int rangeLow, int rangeHigh)
        {
            int val = 0;
            int.TryParse(input, out val);
            return ValidateNumeric(val, rangeLow, rangeHigh);
        }

        private static bool ValidateNumeric(int input, int rangeLow, int rangeHigh)
        {
            return (input >= rangeLow && input <= rangeHigh);
        }

        private static bool ValidateHeight(string inputHeight)
        {
            if (inputHeight.EndsWith("cm"))
                return ValidateNumeric(inputHeight.Substring(0, inputHeight.Length - 2), 150, 193);

            if (inputHeight.EndsWith("in"))
                return ValidateNumeric(inputHeight.Substring(0, inputHeight.Length - 2), 59, 76);

            return false;
        }

        private static bool ValidateHairColor(string inputHairColor)
        {
            if (!(inputHairColor.Length == 7 && inputHairColor.StartsWith("#")))
                return false;

            int ascRL = (int)'a';
            int ascRH = (int)'f';
            int numRL = (int)'0';
            int numRH = (int)'9';
            for (int i = 1; i < 7; i++)
            {
                if (!(ValidateNumeric((int)inputHairColor[i], ascRL, ascRH) || Char.IsNumber(inputHairColor[i])))
                    return false;
            }
            return true;
        }

        private static bool ValidateEyeColor(string inputEyeColor)
        {
            List<string> validEyeColors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return validEyeColors.Contains(inputEyeColor);
        }
        private static bool ValidatePassportID(string inputPPID)
        {
            if (inputPPID.Length != 9) return false;
            for (int i = 0; i < inputPPID.Length; i++)
            {
                if (!Char.IsNumber(inputPPID[i])) return false;
            }
            return true;
        }

    }
}
