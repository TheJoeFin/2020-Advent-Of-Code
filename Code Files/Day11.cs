using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public static class Day11
    {
        public const string TestString = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

        private const string dayNumber = "11";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");

            // Floor "."
            // open seat "L"
            // occupided seat "#"

            // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
            // If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
            // Otherwise, the seat's state does not change.

            // 
            // count number of occupied seats
            // Has number of occupied seats changed? if not, done return number
            // set changed state
            //  look at each position
            //  if is a seat
            //  count occupied neighbors
            //  change state based on neighbors

            char[,] AllSeats = ConvertStringsToArray(TestString);

            int steadyState = CycleSeating(AllSeats);
            Console.WriteLine($"Steady state number of people {steadyState}");
        }

        private static int CycleSeating(char[,] allSeats)
        {
            int numberOfOccupiedSeats = CountOccupiedSeats(allSeats);
            int newCycleCountOccupiedSeats = 0;

            while (numberOfOccupiedSeats != newCycleCountOccupiedSeats)
            {
                char[,] nextStep = TakeNextStep(allSeats);

                newCycleCountOccupiedSeats = CountOccupiedSeats(nextStep);
            }

            return numberOfOccupiedSeats;
        }

        private static char[,] TakeNextStep(char[,] allSeats)
        {
            throw new NotImplementedException();
        }

        private static int CountOccupiedSeats(char[,] allSeats)
        {
            int occupiedSeats = 0;

            for (int i = 0; i < allSeats.GetLength(1); i++)
            {
                for (int j = 0; j < allSeats.GetLength(0); j++)
                {
                    if (allSeats[j, i] == '#')
                        occupiedSeats++;
                }
            }

            return occupiedSeats;
        }

        private static char[,] ConvertStringsToArray(string passedString)
        {

            int width = 0;
            int height = 0;

            foreach (string line in passedString.ReadLines())
            {
                width = line.Length;
                height++;
            }

            char[,] returnArray = new char[width, height];

            int i = 0;
            int j = 0;

            foreach (string line in passedString.ReadLines())
            {
                foreach (char character in line)
                {
                    returnArray[i, j] = character;
                    i++;
                }
                i = 0;
                j++;
            }

            return returnArray;
        }
    }
}