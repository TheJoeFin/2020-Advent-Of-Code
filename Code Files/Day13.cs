using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public static class Day13
    {
        public const string TestString = @"939
7,13,x,x,59,x,31,19";

        private const string dayNumber = "13";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");

            // Day 13: Shuttle Search
            var inputArray = inputString.Split('\n');

            int earliestTimeStampToDepart = int.Parse(inputArray[0]);
            string busDepartaures = inputArray[1];

            int answer = FindAnswer(earliestTimeStampToDepart, busDepartaures);
            Console.WriteLine($"Answer: {answer}");

        }

        private static int FindAnswer(int earliestTimeStampToDepart, string busDepartaures)
        {
            (int busID, int time) eariliestAvailableBusNumberAndTime = findFirstBus(earliestTimeStampToDepart, busDepartaures);

            int waitingTime = eariliestAvailableBusNumberAndTime.time - earliestTimeStampToDepart;

            return waitingTime * eariliestAvailableBusNumberAndTime.busID;
        }

        private static (int busID, int time) findFirstBus(int earliestTimeStampToDepart, string busDepartaures)
        {
            (int returnBusID, int returnTime) returnTuple = (0, 0);

            List<int> busIDList = new List<int>();
            List<string> rawPassedList = busDepartaures.Split(',').ToList();

            foreach (string rawString in rawPassedList)
            {
                if (rawString != "x")
                    busIDList.Add(int.Parse(rawString));
            }

            int timeLookingForBus = earliestTimeStampToDepart;
            bool foundAMatch = false;
            while (foundAMatch == false)
            {
                foreach (int busID in busIDList)
                {
                    if (timeLookingForBus % busID == 0)
                    {
                        returnTuple.returnBusID = busID;
                        returnTuple.returnTime = timeLookingForBus;
                        foundAMatch = true;
                    }
                }
                timeLookingForBus++;
            }

            return returnTuple;
        }
    }
}