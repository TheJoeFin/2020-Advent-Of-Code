using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

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

        private const string InputString = @"6,3,15,13,1,0"; // 700 then 51358


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            // Rambunctious Recitation

            Stopwatch perfStopwatch = new Stopwatch();
            perfStopwatch.Start();

            List<int> memoryGameSequence = new List<int>();

            List<string> memoryGameSequenceRaw = InputString.Split(',').ToList();

            SortedList<int, int> seenLast = new SortedList<int, int>();

            foreach (string rawLine in memoryGameSequenceRaw)
            {
                memoryGameSequence.Add(int.Parse(rawLine));
                updateIndexDictionary(memoryGameSequence, seenLast);
            }

            int lastValue = memoryGameSequence.LastOrDefault();
            int itt = memoryGameSequence.Count();

            do
            {
                Console.WriteLine($"ITT:{itt} lastValue:{lastValue}");
                if (seenLast.ContainsKey(lastValue))
                {
                    lastValue = itt - seenLast[lastValue];
                }
                else
                {
                    lastValue = 0;
                }

                // Update the seen dictionary 
                if (seenLast.ContainsKey(lastValue))
                {
                    seenLast[lastValue] = (itt);
                }
                else
                {
                    seenLast.Add(lastValue, itt);
                }

                itt++;
            }
            while (itt < 20 + 1);

            // for (int i = memoryGameSequence.Count() - 1; i <= (202000 - 1); i++) // 30000000
            // {
            //     int previousStep = memoryGameSequence[i];

            //     int? previousIndexOfPreviousStep = null;

            //     previousIndexOfPreviousStep = tryFindPreviousIndex(memoryGameSequence.GetRange(0, memoryGameSequence.Count() - 1), seenLast, previousStep);
            //     // Console.WriteLine($"{i} | PrevStep:{previousStep} PrevIndx:{previousIndexOfPreviousStep}");

            //     if (previousIndexOfPreviousStep != null)
            //     {
            //         int numToAdd = i - ((int)previousIndexOfPreviousStep);
            //         // Console.WriteLine($"{i} | numToAdd {numToAdd}");
            //         memoryGameSequence.Add(numToAdd);
            //     }
            //     else
            //     {
            //         memoryGameSequence.Add(0);
            //     }

            //     // Update the last seen dictionary
            //     updateIndexDictionary(memoryGameSequence, seenLast);


            // }

            perfStopwatch.Stop();

            Console.WriteLine($"perf:{perfStopwatch.Elapsed.TotalSeconds}");
            // Console.WriteLine(string.Join(',', memoryGameSequence));
            Console.WriteLine($"The last entry in the memory game is {lastValue}");
        }

        private static void solve(int[] startingSeq, int n)
        {
            // making a C# version of this JS solution:
            // https://github.com/azablan/advent-of-code-2020/blob/main/walkthrough/d15/solution.js

            Console.WriteLine("Running Solve");

            var history = new Dictionary<int, int[]>();

            int? last = null;

            for (int i = 0; i < startingSeq.Count(); i++)
            {
                int num = startingSeq[i];
                history.Add(num, new int[2]);
                history[num].Append(i);
                last = num;
            }

            int count = startingSeq.Count();
            while (count < n)
            {
                var positions = history.LastOrDefault();

                if (positions.Value.Length == 1)
                {
                    var zeroPositions = history.FirstOrDefault();

                }

            }
        }

        private static void updateIndexDictionary(List<int> passedSequence, SortedList<int, int> passedDictionary)
        {
            int justAdded = passedSequence.Last();

            if (passedDictionary.ContainsKey(justAdded))
            {
                passedDictionary[justAdded] = (passedSequence.Count - 1);
            }
            else
            {
                passedDictionary.Add(justAdded, passedSequence.Count - 1);
            }
        }

        private static int? tryFindPreviousIndex(List<int> subSequence, SortedList<int, HashSet<int>> seenLast, int previousStep)
        {
            int? returnInt = null;

            if (seenLast.ContainsKey(previousStep) && seenLast[previousStep].Count > 1)
            {
                returnInt = (int?)seenLast[previousStep].FirstOrDefault();
                //seenLast[previousStep] = subSequence.Count();
                return returnInt;
            }

            return returnInt;
        }
    }
}