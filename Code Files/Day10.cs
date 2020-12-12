using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public class Adaptor
    {
        public int Joltage { get; set; }

        public Adaptor(int seedInt)
        {
            Joltage = seedInt;
        }

        public List<int> CompatibleAdaptors = new List<int>();

        public override string ToString()
        {
            return Joltage.ToString();
        }
    }

    public static class Day10
    {
        public const string TestString = @"1
4
5
6
7
10
11
12
15
16
19";

        public const string TestString2 = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
0
10
3";

        private const string dayNumber = "10";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");

            List<int> rawList = new List<int>();
            foreach (string line in inputString.ReadLines())
            {
                int.TryParse(line, out int lineInt);
                rawList.Add(lineInt);
            }
            List<int> sortedList = rawList.OrderBy(x => x).ToList();
            int deviceJoltage = sortedList.Last() + 3;

            // Begin solving the problem

            Console.WriteLine("beginning to sort into adaptors");
            List<Adaptor> adaptors = ConvertIntsToAdaptors(sortedList);
            Console.WriteLine("organize into adaptors");

            Console.WriteLine("beginning to check chains");
            ulong answers = CountBranches(0, sortedList, new Dictionary<int, ulong>());

            Console.WriteLine($"Answer: {answers.ToString()}");
            // answer for test 2: 19208

        }

        private static ulong CountBranches(int Position, List<int> IntListSorted, Dictionary<int, ulong> cache)
        {
            if (cache.ContainsKey(Position))
                return cache[Position];

            int largestIndex = IntListSorted.Count() - 1;

            if (Position == largestIndex)
                return 1;

            ulong totalBranches = 0;

            for (int i = 1; i < 4; i++)
            {
                if ((Position + i) <= largestIndex && (IntListSorted[Position + i] - IntListSorted[Position]) <= 3)
                    totalBranches += CountBranches(Position + i, IntListSorted, cache);
            }

            cache.Add(Position, totalBranches);

            return totalBranches;
        }

        private static int ConvertAdaptorsTo(List<Adaptor> adaptors, int deviceJoltage)
        {
            int total = 0;

            Adaptor firstAdaptor = adaptors.FirstOrDefault();

            if (firstAdaptor != null)
            {
                List<Adaptor> chainSoFar = new List<Adaptor>();
                chainSoFar.Add(firstAdaptor);

                Dictionary<int, int> cache = new Dictionary<int, int>();

                foreach (int adaptor in firstAdaptor.CompatibleAdaptors)
                {
                    List<Adaptor> chainCopy = new List<Adaptor>(chainSoFar);

                    total += ContinueTheChain(adaptor, chainCopy, adaptors, total, deviceJoltage, cache);
                }
            }

            return total;
        }

        private static int ContinueTheChain(int adaptorJoltage, List<Adaptor> chainCopy, List<Adaptor> adaptors, int total, int max, Dictionary<int, int> cache)
        {
            Adaptor adaptor = adaptors.Where(a => a.Joltage == adaptorJoltage).FirstOrDefault();
            chainCopy.Add(adaptor);



            if (adaptor != null)
            {
                foreach (int compatJoltage in adaptor.CompatibleAdaptors)
                {
                    List<Adaptor> newCopy = new List<Adaptor>(chainCopy);

                    if (cache.ContainsKey(compatJoltage))
                    {
                        Console.WriteLine($"Found {compatJoltage} in the cache for {cache[compatJoltage]}");
                        total += cache[compatJoltage] + 1;
                        cache.Add(adaptorJoltage, total);
                        return total;
                    }
                    if (compatJoltage + 3 >= max)
                    {
                        Console.WriteLine($"Found the end  with {adaptorJoltage}");
                        cache.Add(compatJoltage, 1);
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine($"Continuing the chain for {compatJoltage}");
                        total += ContinueTheChain(compatJoltage, newCopy, adaptors, total, max, cache);
                    }

                }
            }

            return total;
        }

        private static List<Adaptor> ConvertIntsToAdaptors(List<int> sortedList)
        {
            List<Adaptor> returnAdaptors = new List<Adaptor>();

            foreach (int intList in sortedList)
            {
                Adaptor adaptor = new Adaptor(intList);

                for (int i = 1; i < 4; i++)
                {
                    int possibleAdaptors = adaptor.Joltage + i;

                    if (sortedList.Contains(possibleAdaptors))
                        adaptor.CompatibleAdaptors.Add(possibleAdaptors);

                }

                returnAdaptors.Add(adaptor);
            }

            return returnAdaptors;
        }

        public static string convertBinaryStringToIntListString(string binaryString, int max)
        {
            List<int> convertedList = new List<int> { 0 };

            int lastNumber = 0;
            int newNumber = 0;

            for (int i = 0; i < binaryString.Length; i++)
            {
                if (binaryString[i] == '0')
                    newNumber = lastNumber += 1;
                else
                    newNumber = lastNumber += 3;

                if (newNumber <= max)
                    convertedList.Add(newNumber);
                lastNumber = newNumber;

            }

            string returnString = String.Join(',', convertedList.ToArray());

            return returnString;
        }
    }
}