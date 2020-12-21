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

            List<long> memoryGameSequence = new List<long>();

            List<string> memoryGameSequenceRaw = InputString.Split(',').ToList();

            SortedList<long, HashSet<long>> seenLast = new SortedList<long, HashSet<long>>();

            foreach (string rawLine in memoryGameSequenceRaw)
            {
                memoryGameSequence.Add(long.Parse(rawLine));
                updateIndexDictionary(memoryGameSequence, seenLast);
            }

            for (int i = memoryGameSequence.Count() - 1; i <= (2020 - 1); i++) // 30000000
            {
                long previousStep = memoryGameSequence[i];

                int? previousIndexOfPreviousStep = null;

                previousIndexOfPreviousStep = tryFindPreviousIndex(memoryGameSequence.GetRange(0, memoryGameSequence.Count() - 1), seenLast, previousStep);
                // Console.WriteLine($"{i} | PrevStep:{previousStep} PrevIndx:{previousIndexOfPreviousStep}");

                if (previousIndexOfPreviousStep != null)
                {
                    long numToAdd = i - ((long)previousIndexOfPreviousStep);
                    // Console.WriteLine($"{i} | numToAdd {numToAdd}");
                    memoryGameSequence.Add(numToAdd);
                }
                else
                {
                    memoryGameSequence.Add(0);
                }

                // Update the last seen dictionary
                updateIndexDictionary(memoryGameSequence, seenLast);


            }

            perfStopwatch.Stop();

            Console.WriteLine($"perf:{perfStopwatch.Elapsed.TotalSeconds}");
            // Console.WriteLine(string.Join(',', memoryGameSequence));
            Console.WriteLine($"The last entry in the memory game is {memoryGameSequence[memoryGameSequence.Count - 2]}");
        }

        private static void solve(long[] startingSeq, long n)
        {
            Console.WriteLine("Running Solve");

            var history = new Dictionary<long, long[]>();

            long? last = null;

            for (long i = 0; i < startingSeq.Count(); i++)
            {
                long num = startingSeq[i];
                history.Add(num, new long[2]);
                history[num].Append(i);
                last = num;
            }

            long count = startingSeq.Count();
            while (count < n)
            {
                var positions = history.LastOrDefault();

                if (positions.Value.Length == 1)
                {
                    var zeroPositions = history.FirstOrDefault();

                }

            }
        }

        private static void updateIndexDictionary(List<long> passedSequence, SortedList<long, HashSet<long>> passedDictionary)
        {
            long justAdded = passedSequence.Last();

            if (passedDictionary.ContainsKey(justAdded))
            {
                passedDictionary[justAdded].Add(passedSequence.Count - 1);

                if (passedDictionary[justAdded].Count() > 2)
                {
                    var instance = passedDictionary[justAdded].First();
                    passedDictionary[justAdded].Remove(instance);
                }
            }
            else
            {
                passedDictionary.Add(justAdded, new HashSet<long> { passedSequence.Count - 1 });
            }
        }

        private static int? tryFindPreviousIndex(List<long> subSequence, SortedList<long, HashSet<long>> seenLast, long previousStep)
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