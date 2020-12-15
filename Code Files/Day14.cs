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

            ulong[] memory = new ulong[2 ^ 35];

            ulong[] initMemory = initalizationProgram(memory);
            ulong sum = sumAllMemory(initMemory);


        }

        private static ulong sumAllMemory(ulong[] initMemory)
        {
            throw new NotImplementedException();
        }

        private static ulong[] initalizationProgram(ulong[] memory)
        {
            throw new NotImplementedException();
        }
    }
}