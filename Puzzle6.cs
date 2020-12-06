using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Puzzle6
    {
        public static void SolvePuzzle6_P1(string inputFile)
        {
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string buffer = sr.ReadLine();
                string answers = string.Empty;
                int totalAnswers = 0;
                while (buffer!= null)
                {
                    if (string.IsNullOrEmpty(buffer))
                    {
                        totalAnswers += CountUniqueCharsInString(answers);
                        answers = string.Empty;
                    }
                    else
                    {
                        answers += buffer;
                    }
                    buffer = sr.ReadLine();
                }
                if (!string.IsNullOrEmpty(answers))
                {
                    totalAnswers += CountUniqueCharsInString(answers);
                }
                Console.WriteLine($"Total unqiue answers in group: {totalAnswers}");
            }
        }

        private static int CountUniqueCharsInString(string input)
        {
            List<char> uniqueAnswers = new List<char>();
            foreach (char c in input)
            {
                if (!uniqueAnswers.Contains(c))
                    uniqueAnswers.Add(c);
            }
            return uniqueAnswers.Count;
        }

        public static void SolvePuzzle6_P2(string inputFile)
        {
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string buffer = sr.ReadLine();
                int people = 0;
                string answers = string.Empty;
                int totalAnswers = 0;
                while (buffer != null)
                {
                    if (string.IsNullOrEmpty(buffer))
                    {
                        totalAnswers += CountAllAnsweredYes(answers, people);
                        answers = string.Empty;
                        people = 0;
                    }
                    else
                    {
                        answers += buffer;
                        people += 1;
                    }
                    buffer = sr.ReadLine();
                }
                if (!string.IsNullOrEmpty(answers))
                {
                    totalAnswers += CountAllAnsweredYes(answers, people);
                }
                Console.WriteLine($"Total unqiue answers in group: {totalAnswers}");
            }
        }

        private static int CountAllAnsweredYes(string input, int people)
        {
            Dictionary<char, int> answers = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (!answers.ContainsKey(c))
                    answers.Add(c, 1);
                else
                    answers[c] += 1;
            }

            int allAnswered = 0;
            foreach (char c in answers.Keys)
            {
                if (answers[c] == people) allAnswered += 1;
            }
            return allAnswered;
        }
    }
}
