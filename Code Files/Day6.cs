using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{

    public static class Day6
    {

        public static IEnumerable<string> ReadLines(this string s)
        {
            string line;
            using (var sr = new StringReader(s))
                while ((line = sr.ReadLine()) != null)
                    yield return line;
        }

        public static void Run()
        {
            string inputString = File.ReadAllText($"Input Files/Day6Input.txt");

            string testString = @"
abc

a
b
c

ab
ac

a
a
a
a

b
            ";


            List<string> inputList = ConvertStringToListOfStrings(inputString);

            int total = 0;

            foreach (string customsAnswers in inputList)
            {
                HashSet<char> uniqueLetters = new HashSet<char>(customsAnswers.Trim());
                int countYes = uniqueLetters.Count();
                // Console.WriteLine($"Number of Yes: {countYes} - {customsAnswers}");

                total += countYes;
            }

            Console.WriteLine($"Total: {total}");
        }

        private static List<string> ConvertStringToListOfStrings(string input)
        {
            List<string> returnList = new List<string>();

            string stringLine = "";

            foreach (string singleRead in input.ReadLines())
            {

                if (string.IsNullOrWhiteSpace(singleRead) == false)
                {
                    stringLine += singleRead;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(stringLine) == false)
                        returnList.Add(stringLine);

                    stringLine = string.Empty;
                }
            }

            if (string.IsNullOrWhiteSpace(stringLine) == false)
                returnList.Add(stringLine);

            return returnList;
        }

    }
}