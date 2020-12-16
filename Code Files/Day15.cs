using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public static class Day15
    {
        public const string TestString1 = @"0,3,6"; // 0,3,6,0,3,3,1,0,4,0
        public const string TestString2 = @"1,3,2"; // 1
        public const string TestString3 = @"2,1,3"; // 10
        public const string TestString4 = @"1,2,3"; // 27
        public const string TestString5 = @"2,3,1"; // 78
        public const string TestString6 = @"3,2,1"; // 438
        public const string TestString7 = @"3,1,2"; // 1836

        private const string dayNumber = "15";

        private const string InputString = @"6,3,15,13,1,0";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            // Rambunctious Recitation

            List<long> memoryGameSequence = new List<long>();

            List<string> memoryGameSequenceRaw = InputString.Split(',').ToList();

            foreach (string rawLine in memoryGameSequenceRaw)
            {
                memoryGameSequence.Add(long.Parse(rawLine));
            }

            for (int i = 2; i <= 10; i++)
            {
                long previousStep = memoryGameSequence[i];

                int? previousIndexOfPreviousStep = tryFindPreviousIndex(memoryGameSequence, previousStep);
                // previousIndexOfPreviousStep;

                Console.WriteLine($"{i} | {string.Join(',', memoryGameSequence)}");

                if (previousIndexOfPreviousStep != null)
                {
                    long numToAdd = i - (long)previousIndexOfPreviousStep;

                    if (numToAdd == 0)
                        memoryGameSequence.Add(1);
                    else
                        memoryGameSequence.Add(i - (long)previousIndexOfPreviousStep);
                }
                else
                    memoryGameSequence.Add(0);

            }

            Console.WriteLine($"The last entry in the memory game is {memoryGameSequence[memoryGameSequence.Count - 2]}");
        }

        private static int? tryFindPreviousIndex(List<long> memoryGameSequence, long previousStep)
        {
            int? returnInt = null;

            for (int s = memoryGameSequence.Count - 2; s >= 0; s--)
            {
                if (memoryGameSequence[s] == previousStep)
                    return s;
            }

            return returnInt;
        }
    }
}