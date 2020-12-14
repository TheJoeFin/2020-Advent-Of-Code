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
            var inputArray = TestString6.Split('\n');

            int earliestTimeStampToDepart = int.Parse(inputArray[0]);
            string busDepartaures = inputArray[1];

            int answer = FindAnswer(earliestTimeStampToDepart, busDepartaures);
            ulong answerPart2 = findCompactBusChain(busDepartaures);

            Console.WriteLine($"Answer: {answerPart2}");

        }

        private static ulong findCompactBusChain(string busDepartaures)
        {
            List<string> listOfDepartaures = busDepartaures.Split(',').ToList();

            ulong firstbusID = ulong.Parse(listOfDepartaures.First());

            ulong[] foundSteps = new ulong[listOfDepartaures.Count()];
            foundSteps.Append(firstbusID);

            ulong startTimeOfContinuousChain = 0;
            bool continuousChain = false;
            do
            {
                continuousChain = true;

                if (foundSteps.Count() > 0)
                    startTimeOfContinuousChain += LCM(foundSteps);
                else
                    startTimeOfContinuousChain += firstbusID;

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
                        foundSteps.Append(startTimeOfContinuousChain);
                    }
                }
            }
            while (continuousChain == false);

            return startTimeOfContinuousChain;
        }

        static ulong LCM(ulong[] numbers)
        {
            return numbers.Aggregate(lcm);
        }
        static ulong lcm(ulong a, ulong b)
        {
            return (ulong)(Math.Abs((decimal)(a * b)) / GCD(a, b));
        }
        static ulong GCD(ulong a, ulong b)
        {
            return b == 0 ? a : GCD(b, a % b);
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