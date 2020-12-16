using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public static class Day14
    {
        public const string TestString = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

        public const string TestString2 = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";

        private const string dayNumber = "14";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");

            // Docking Data

            Dictionary<ulong, ulong> initMemory = initalizationProgram(inputString);
            ulong sum = sumAllMemory(initMemory);

            Console.WriteLine($"The sum of all numbers in memory is {sum}");

        }

        private static ulong sumAllMemory(Dictionary<ulong, ulong> initMemory)
        {
            Console.WriteLine("Summing Memory");

            ulong runningSum = 0;

            foreach (ulong itemInMemory in initMemory.Values)
            {
                runningSum += itemInMemory;
            }

            return runningSum;
        }

        private static Dictionary<ulong, ulong> initalizationProgram(string bootProgram)
        {
            Console.WriteLine("Initializing Program");
            Dictionary<ulong, ulong> initedmemory = new Dictionary<ulong, ulong>();

            string mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            int numberOfMaskLoops = 0;
            foreach (string rawLine in bootProgram.ReadLines())
            {
                if (rawLine.StartsWith("mask = "))
                {
                    mask = rawLine.Substring(7, 36);
                    numberOfMaskLoops = countLoopsInMask(mask);
                }
                else
                {
                    int indexOfEqualSign = rawLine.IndexOf('=');
                    // int indexOfFirstBracket = rawLine.IndexOf('[');
                    int indexOfLastBracket = rawLine.IndexOf(']');


                    ulong memoryLocation = ulong.Parse(rawLine.Substring(4, indexOfLastBracket - 4));

                    List<ulong> memLocList = createMemLocList(memoryLocation, mask);

                    string stringNum = rawLine.Substring(indexOfEqualSign + 2, rawLine.Length - (indexOfEqualSign + 2));
                    ulong lineNumber = ulong.Parse(stringNum);

                    // ulong maskedNumber = maskNumber(lineNumber, mask);

                    foreach (ulong memLoc in memLocList)
                    {

                        if (initedmemory.ContainsKey(memLoc))
                            initedmemory[memLoc] = lineNumber;
                        else
                            initedmemory.Add(memLoc, lineNumber);
                    }
                }
            }

            return initedmemory;
        }

        private static List<ulong> createMemLocList(ulong memoryLocation, string mask)
        {
            List<ulong> returnList = new List<ulong>();

            char[] maskArray = mask.ToCharArray();
            int countofX = maskArray.Where(c => c == 'X').ToArray().Count();

            List<int> addressOfEveryX = new List<int>();

            for (int i = 0; i < mask.Length; i++)
            {
                if (maskArray[i] == 'X')
                {
                    addressOfEveryX.Add(i);
                }
            }

            List<int> addressOfEvery1 = new List<int>();

            for (int i = 0; i < mask.Length; i++)
            {
                if (maskArray[i] == '1')
                {
                    addressOfEvery1.Add(i);
                }
            }

            for (int j = 0; j < (int)Math.Pow(2, countofX); j++)
            {
                string binString = Convert.ToString(j, 2).PadLeft(countofX, '0');

                char[] addressAsBinArray = Convert.ToString((long)memoryLocation, 2).PadLeft(36, '0').ToCharArray();

                // Mask the X
                for (int k = binString.Length - 1; k >= 0; k--)
                {
                    addressAsBinArray[addressOfEveryX[k]] = binString[k];
                }

                // Mask the 1
                for (int l = 0; l < addressOfEvery1.Count(); l++)
                {
                    addressAsBinArray[addressOfEvery1[l]] = '1';
                }

                ulong newlyMaskedAddress = Convert.ToUInt64(string.Join("", addressAsBinArray), 2);

                returnList.Add(newlyMaskedAddress);
            }

            return returnList;
        }

        private static int countLoopsInMask(string mask)
        {
            char[] maskArray = mask.ToCharArray();

            char[] xArray = maskArray.Where(c => c == 'X').ToArray();

            int countOfX = xArray.Count();

            return (int)Math.Pow(2, countOfX);
        }

        private static ulong maskNumber(ulong lineNumber, string mask)
        {
            ulong returnMaskedNumber = 0;

            char[] lineString = Convert.ToString((int)lineNumber, 2).PadLeft(36, '0').ToArray();

            for (int i = 35; i >= 0; i--)
            {
                if (mask[i] == 'X')
                    continue;

                lineString[i] = mask[i];
            }

            string binaryString = string.Join("", lineString);
            // Console.WriteLine(binaryString.ToString());
            returnMaskedNumber = Convert.ToUInt64(binaryString, 2);

            return returnMaskedNumber;
        }
    }
}