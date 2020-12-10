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

            foreach (string inputStringLine in inputString.ReadLines())
            {
                int.TryParse(inputStringLine.Trim(), out int parsedInt);
                listOfNumbers.Add(parsedInt);
            }

            int wrongNumber = FindTheBadNumber(listOfNumbers, 25);

            List<int> contiguousSumSet = FindContiguousSumSet(wrongNumber, listOfNumbers);

            if (contiguousSumSet != null)
            {
                var orderedList = contiguousSumSet.OrderBy(x => x).ToList();
                int encryptionWeakness = orderedList.First() + orderedList.Last();
                Console.WriteLine($"The wrong number is {wrongNumber}");
                Console.WriteLine($"The encryptionWeakness is {encryptionWeakness}");
            }
            else
                Console.WriteLine($"The contiguousSumSet is null");


        }

        private static List<int> FindContiguousSumSet(int wrongNumber, List<int> listOfNumbers)
        {

            for (int i = 0; i < listOfNumbers.Count(); i++)
            {
                int runningSum = 0;
                int itt = 0;
                while (runningSum < wrongNumber)
                {
                    runningSum += listOfNumbers[i + itt];

                    if (runningSum == wrongNumber)
                        return listOfNumbers.GetRange(i, itt);

                    itt++;
                }
            }

            return null;
        }

        private static int FindTheBadNumber(List<int> listOfNumbers, int lookBehindNum)
        {
            int wrongNumberReturn = 0;

            bool foundInLookBehind = false;
            for (int i = lookBehindNum; i < listOfNumbers.Count(); i++)
            {
                int numToCheck = listOfNumbers[i];

                List<int> checkRange = listOfNumbers.GetRange(i - lookBehindNum, lookBehindNum);

                (int num1, int num2) sumPair = (0, 0);

                for (int j = i - lookBehindNum; j < i; j++)
                {
                    int diff = numToCheck - listOfNumbers[j];

                    if (checkRange.Contains(diff) == true)
                    {
                        foundInLookBehind = true;
                        sumPair.num1 = diff;
                        sumPair.num2 = listOfNumbers[j];
                        //Console.WriteLine($"pair for {numToCheck}: {sumPair.num1} | {sumPair.num2}");
                        break;
                    }
                }

                if (foundInLookBehind == false)
                {
                    wrongNumberReturn = numToCheck;
                    break;
                }
                foundInLookBehind = false;
            }

            return wrongNumberReturn;
        }
    }
}