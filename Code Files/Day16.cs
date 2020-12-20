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


        }
    }
}