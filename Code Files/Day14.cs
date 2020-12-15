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
        public const string TestString = @"";

        private const string dayNumber = "14";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");

            // Docking Data

            Dictionary<ulong, ulong> initMemory = initalizationProgram(inputString);
            // ulong sum = sumAllMemory(initMemory);



        }

        private static ulong sumAllMemory(Dictionary<ulong, ulong> initMemory)
        {
            ulong runningSum = 0;

            foreach (ulong itemInMemory in initMemory.Values)
            {
                runningSum += itemInMemory;
            }

            return runningSum;
        }

        private static Dictionary<ulong, ulong> initalizationProgram(string bootProgram)
        {
            Dictionary<ulong, ulong> initedmemory = new Dictionary<ulong, ulong>();

            string mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            foreach (string rawLine in bootProgram.ReadLines())
            {
                if (rawLine.StartsWith("mask = "))
                {
                    mask = rawLine.Substring(7, 36);
                }
                else
                {
                    int indexOfEqualSign = rawLine.IndexOf('=');
                    // int indexOfFirstBracket = rawLine.IndexOf('[');
                    int indexOfLastBracket = rawLine.IndexOf(']');

                    ulong memoryLocation = ulong.Parse(rawLine.Substring(4, indexOfLastBracket - 4));

                    string stringNum = rawLine.Substring(indexOfEqualSign + 2, rawLine.Length - (indexOfEqualSign + 2));
                    ulong lineNumber = ulong.Parse(stringNum);

                    ulong maskedNumber = maskNumber(lineNumber, mask);

                    initedmemory.Add(memoryLocation, maskedNumber);
                }
            }

            return initedmemory;
        }
    }
}