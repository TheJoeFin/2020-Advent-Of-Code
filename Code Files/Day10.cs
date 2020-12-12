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

            Console.WriteLine("beginning to sort into adaptors");
            List<Adaptor> adaptors = ConvertIntsToAdaptors(sortedList);
            Console.WriteLine("sorted into adaptors");

            Console.WriteLine("beginning to check chains");
            List<string> answers = ConvertAdaptorsTo(adaptors, deviceJoltage);

            Console.WriteLine($"Answer, count of correctStrings: {answers.Count()}");

        }

        private static List<string> ConvertAdaptorsTo(List<Adaptor> adaptors, int deviceJoltage)
        {
            List<string> returnList = new List<string>();

            Adaptor firstAdaptor = adaptors.FirstOrDefault();

            if (firstAdaptor != null)
            {
                List<Adaptor> chainSoFar = new List<Adaptor>();
                chainSoFar.Add(firstAdaptor);

                foreach (int adaptor in firstAdaptor.CompatibleAdaptors)
                {
                    List<Adaptor> chainCopy = new List<Adaptor>(chainSoFar);

                    ContinueTheChain(adaptor, chainCopy, adaptors, returnList, deviceJoltage);
                }
            }

            return returnList;
        }

        private static void ContinueTheChain(int adaptorJoltage, List<Adaptor> chainCopy, List<Adaptor> adaptors, List<string> returnList, int max)
        {
            Adaptor adaptor = adaptors.Where(a => a.Joltage == adaptorJoltage).FirstOrDefault();
            chainCopy.Add(adaptor);

            if (adaptorJoltage + 3 >= max)
            {
                returnList.Add(string.Join(',', chainCopy));
                chainCopy.Clear();
                return;
            }

            if (adaptor != null)
            {
                foreach (int compatJoltage in adaptor.CompatibleAdaptors)
                {
                    List<Adaptor> newCopy = new List<Adaptor>(chainCopy);

                    ContinueTheChain(compatJoltage, newCopy, adaptors, returnList, max);
                }
            }
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