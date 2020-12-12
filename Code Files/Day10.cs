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
    }

    public static class Day10
    {
        public const string TestString = @"16
10
15
5
1
11
7
19
6
12
4";

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
10
3";

        private const string dayNumber = "10";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");

            List<int> rawList = new List<int>();
            foreach (string line in TestString.ReadLines())
            {
                int.TryParse(line, out int lineInt);
                rawList.Add(lineInt);
            }
            List<int> sortedList = rawList.OrderBy(x => x).ToList();
            int deviceJoltage = sortedList.Last() + 3;

            int numberOfOnes = 1;
            int numberOfThrees = 1;

            for (int i = 0; i < sortedList.Count() - 1; i++)
            {
                int diff = sortedList[i + 1] - sortedList[i];

                if (diff == 1)
                    numberOfOnes++;

                if (diff == 3)
                    numberOfThrees++;
            }

            List<string> correctStrings = new List<string>();

            // int loopNumbers

            for (int i = 0; i < 4095; i++)
            {
                string output = Convert.ToString(i, 2).PadLeft((sortedList.Count() + 1), '0');

                int countingUp = 0;
                bool inTheList = true;
                for (int j = 0; j < output.Length; j++)
                {
                    switch (output[j])
                    {
                        case '0':
                            countingUp += 1;
                            break;
                        case '1':
                            countingUp += 3;

                            break;
                        default:
                            break;
                    }

                    // if (countingUp > 16)
                    //     Console.WriteLine($"Joltage of: {countingUp} with path: {output}");

                    if (countingUp == deviceJoltage)
                    {
                        correctStrings.Add(output);
                        j = 1000;
                    }

                    if (countingUp > deviceJoltage)
                        j = 1000;

                    if (sortedList.Contains(countingUp) == false)
                    {
                        inTheList = false;
                        j = 1000;
                    }
                }
            }

            // Console.WriteLine($"Number of ones {numberOfOnes}");
            // Console.WriteLine($"Number of threes {numberOfThrees}");

            // foreach (string binaryString in correctStrings)
            // {
            //     Console.WriteLine(convertBinaryStringToIntListString(binaryString, deviceJoltage));
            // }

            List<Adaptor> adaptors = ConvertIntsToAdaptors(sortedList);

            List<List<int>> answers = ConvertAdaptorsTo(adaptors, deviceJoltage);


            Console.WriteLine($"Answer, count of correctStrings: {correctStrings.Count()}");

        }

        private static List<List<int>> ConvertAdaptorsTo(List<Adaptor> adaptors, int deviceJoltage)
        {
            List<List<int>> returnList = new List<List<int>>();

            foreach (Adaptor adaptor in adaptors)
            {

            }

            return returnList;
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

        private static List<List<int>> PlugInAllOfTheseAdaptors(List<int> sortedList)
        {
            List<List<int>> allArrangments = new List<List<int>>();

            List<int>

            return allArrangments;
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