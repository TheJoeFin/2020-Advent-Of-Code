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

            List<string> memoryGameSequenceRaw = TestString7.Split(',').ToList();

            Dictionary<long, long> seenLast = new Dictionary<long, long>();

            foreach (string rawLine in memoryGameSequenceRaw)
            {
                memoryGameSequence.Add(long.Parse(rawLine));
            }

            for (int i = memoryGameSequence.Count() - 1; i <= (10 - 1); i++) // 30000000
            {
                long previousStep = memoryGameSequence[i];

                int? previousIndexOfPreviousStep = null;

                if (seenLast.ContainsKey(previousStep))
                {
                    previousIndexOfPreviousStep = (int?)seenLast[previousStep];
                    Console.WriteLine($"{i} | seenLast contains {previousStep} at {previousIndexOfPreviousStep}");
                }
                else
                    previousIndexOfPreviousStep = tryFindPreviousIndex(memoryGameSequence, previousStep);

                Console.WriteLine($"{i} | PrevStep:{previousStep} PrevIndx:{previousIndexOfPreviousStep}");

                if (previousIndexOfPreviousStep != null)
                {
                    long numToAdd = i - (long)previousIndexOfPreviousStep;

                    Console.WriteLine($"{i} | numToAdd {numToAdd}");

                    memoryGameSequence.Add(numToAdd);
                    if (seenLast.ContainsKey(numToAdd))
                        seenLast[numToAdd] = (i);
                    else
                        seenLast.Add(numToAdd, i);
                    // }
                }
                else
                {
                    seenLast[0] = i;
                    memoryGameSequence.Add(0);
                }

            }

            Console.WriteLine(string.Join(',', memoryGameSequence));
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