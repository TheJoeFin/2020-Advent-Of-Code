using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public class TicketInformation
    {
        public List<int> MyTicket { get; set; }

        public List<List<int>> NearbyTickets { get; set; }

        public int Rules { get; set; }

    }

    public static class Day16
    {
        public const string TestString = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

        private const string dayNumber = "16";

        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");

            // Parse inputs

            string[] inputArray = TestString.Split("\r\n\r");

            string rawRules = inputArray[0].Trim();
            string rawYourTicket = inputArray[1].Trim();
            string rawNearbyTickets = inputArray[2].Trim();

            List<int[]> rules = new List<int[]>();
            List<int> yourTicket = new List<int>();
            List<int> nearByTickets = new List<int>();

            foreach (string rulesLine in rawRules.ReadLines())
            {
                string[] rawLineArray = rulesLine.Split();

                string[] Rule1stringArr = rawLineArray[1].Split('-');
                string[] Rule2stringArr = rawLineArray[3].Split('-');

                int[] rule1 = new int[2];
                rule1[0] = int.Parse(Rule1stringArr[0]);
                rule1[1] = int.Parse(Rule1stringArr[1]);
                rules.Add(rule1);

                int[] rule2 = new int[2];
                rule2[0] = int.Parse(Rule2stringArr[0]);
                rule2[1] = int.Parse(Rule2stringArr[1]);
                rules.Add(rule2);
            }

            string[] rawYourNumbers = rawYourTicket.Split('\n').Last().Split(',');
            foreach (string rawNumb in rawYourNumbers)
                yourTicket.Add(int.Parse(rawNumb));

            foreach (string rawNearbyLine in rawNearbyTickets.ReadLines())
            {
                if (rawNearbyLine.Contains("tickets"))
                    continue;

                string[] rawNumbers = rawNearbyLine.Split(',');

                foreach (string rawNum in rawNumbers)
                    nearByTickets.Add(int.Parse(rawNum));
            }


            // Start evaulating rules
            List<int> invalidTickets = new List<int>();



        }
    }
}