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
            string inputString = File.ReadAllText($"Input Files\\{MethodBase.GetCurrentMethod().DeclaringType.ToString().Split('.').LastOrDefault()}Input.txt");

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


            List<string> inputList = new List<string>();

            foreach (string stringLine in testString.ReadLines())
            {
                if (string.IsNullOrWhiteSpace(stringLine) == false)
                    Console.WriteLine(stringLine);
            }

            foreach (string customsAnswers in inputList)
            {
                HashSet<char> uniqueLetters = new HashSet<char>(customsAnswers.Trim());
                int countYes = uniqueLetters.Count();
                Console.WriteLine($"Number of Yes: {countYes}");
            }
        }


    }
}