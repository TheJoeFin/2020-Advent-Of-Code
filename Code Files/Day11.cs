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

            char[,] AllSeats = ConvertStringsToArray(inputString);

            int steadyState = CycleSeating(AllSeats);
            Console.WriteLine($"Steady state number of people {steadyState}");
        }

        private static int CycleSeating(char[,] allSeats)
        {
            int numberOfOccupiedSeats = CountOccupiedSeats(allSeats);
            int newCycleCountOccupiedSeats = 0;

            do
            {
                char[,] nextStep = TakeNextStep(allSeats);
                newCycleCountOccupiedSeats = CountOccupiedSeats(nextStep);
                numberOfOccupiedSeats = CountOccupiedSeats(allSeats);
                allSeats = (char[,])nextStep.Clone();
            }
            while (numberOfOccupiedSeats != newCycleCountOccupiedSeats);

            return numberOfOccupiedSeats;
        }

        private static char[,] TakeNextStep(char[,] allSeats)
        {
            char[,] returnArray = (char[,])allSeats.Clone();

            int maxWidth = allSeats.GetLength(0);
            int maxHeight = allSeats.GetLength(1);

            for (int i = 0; i < maxHeight; i++)
            {
                for (int j = 0; j < maxWidth; j++)
                {
                    char seat = allSeats[j, i];

                    if (seat == 'L' || seat == '#')
                    {
                        int occupidedNeighbors = 0;
                        // 8 neighbors
                        // 1) i + 1 | j - 1
                        if ((i + 1) < maxHeight && (j - 1) >= 0 && allSeats[j - 1, i + 1] == '#')
                            occupidedNeighbors++;
                        // 2) i + 0 | j - 1
                        if ((i + 0) < maxHeight && (j - 1) >= 0 && allSeats[j - 1, i + 0] == '#')
                            occupidedNeighbors++;
                        // 3) i - 1 | j - 1
                        if ((i - 1) >= 0 && (j - 1) >= 0 && allSeats[j - 1, i - 1] == '#')
                            occupidedNeighbors++;
                        // 4) i - 1 | j + 0
                        if ((i - 1) >= 0 && (j - 0) >= 0 && allSeats[j + 0, i - 1] == '#')
                            occupidedNeighbors++;
                        // 5) i - 1 | j + 1
                        if ((i - 1) >= 0 && (j + 1) < maxWidth && allSeats[j + 1, i - 1] == '#')
                            occupidedNeighbors++;
                        // 6) i - 0 | j + 1
                        if ((i - 0) >= 0 && (j + 1) < maxWidth && allSeats[j + 1, i - 0] == '#')
                            occupidedNeighbors++;
                        // 7) i + 1 | j + 1
                        if ((i + 1) < maxHeight && (j + 1) < maxWidth && allSeats[j + 1, i + 1] == '#')
                            occupidedNeighbors++;
                        // 8) i + 1 | j + 0
                        if ((i + 1) < maxHeight && (j - 0) >= 0 && allSeats[j + 0, i + 1] == '#')
                            occupidedNeighbors++;

                        if (occupidedNeighbors >= 4 && seat == '#')
                            returnArray[j, i] = 'L';

                        if (occupidedNeighbors == 0 && seat == 'L')
                            returnArray[j, i] = '#';

                    }
                }
            }

            return returnArray;
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