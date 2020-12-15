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
        public const string TestString1 = @"939
7,13,x,x,59,x,31,19";

        public const string TestString2 = @"3417
17,x,13,19";

        public const string TestString3 = @"754018
67,7,59,61";

        public const string TestString4 = @"779210
67,x,7,59,61";

        public const string TestString5 = @"1261476
67,7,x,59,61";

        public const string TestString6 = @"1202161486
1789,37,47,1889";

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
            ulong answerPart2 = findCompactBusChain(busDepartaures);

            Console.WriteLine($"Answer: {answerPart2}");

        }

        private static ulong findCompactBusChain(string busDepartaures)
        {
            // step at the first bus id to find the first match
            // then when you find the second match
            // multiply the first id and the second id to get your new step size
            // check for a match for the next row
            // when fail looking for a match on that row
            // step by the product of all matches plus the current position until the next step is found


            List<string> listOfDepartaures = busDepartaures.Split(',').ToList();

            ulong stepSize = ulong.Parse(listOfDepartaures.First());

            List<ulong> stepCollection = new List<ulong>();
            stepCollection.Add(stepSize);

            ulong startTimeOfContinuousChain = 0;
            bool continuousChain = false;
            do
            {
                continuousChain = true;

                startTimeOfContinuousChain += stepSize;

                for (int i = 0; i < listOfDepartaures.Count(); i++)
                {
                    if (listOfDepartaures[i] == "x")
                        continue;

                    ulong busID = ulong.Parse(listOfDepartaures[i]);

                    if ((startTimeOfContinuousChain + (ulong)i) % busID != 0)
                    {
                        continuousChain = false;
                        break;
                    }
                    else
                    {
                        if (stepCollection.Contains(busID) == false)
                        {
                            stepCollection.Add(busID);
                            stepSize *= busID;
                            Console.WriteLine($"Added {busID} to {stepSize}");
                        }
                    }
                }
            }
            while (continuousChain == false);

            return startTimeOfContinuousChain;
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