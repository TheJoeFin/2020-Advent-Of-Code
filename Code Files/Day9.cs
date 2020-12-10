using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public static class Day9
    {
        public const string TestString = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

        private const string dayNumber = "9";


        public static void Run()
        {
            Console.WriteLine($"Running Day{dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");
            List<int> listOfNumbers = new List<int>();

            foreach (string inputStringLine in TestString.ReadLines())
            {
                int.TryParse(inputStringLine.Trim(), out int parsedInt);
                listOfNumbers.Add(parsedInt);
            }

            int wrongNumber = FindTheBadNumber(listOfNumbers, 5);

            Console.WriteLine($"The wrong number is {wrongNumber}");
        }

        private static int FindTheBadNumber(List<int> listOfNumbers, int lookBehindNum)
        {
            int wrongNumberReturn = 0;

            bool notFoundInLookBehind = true;
            for (int i = lookBehindNum; i < listOfNumbers.Count(); i++)
            {
                int numToCheck = listOfNumbers[i];

                List<int> checkRange = listOfNumbers.GetRange(i - lookBehindNum, lookBehindNum);
                for (int j = i - lookBehindNum; j < lookBehindNum; j++)
                {
                    int diff = numToCheck - listOfNumbers[j];

                    if (checkRange.Contains(diff) == true)
                    {
                        notFoundInLookBehind = false;
                    }
                }

                if (notFoundInLookBehind == true)
                {
                    wrongNumberReturn = numToCheck;
                }
            }

            return wrongNumberReturn;
        }
    }
}