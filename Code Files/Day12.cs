using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public class Ship
    {
        public Point Position { get; set; } = new Point(0, 0);

        public string Heading { get; set; } = "E";

        public void Act(string order)
        {
            string Action = order.Substring(0, 1);
            int magnitude = int.Parse(order.Substring(1, order.Length - 1));

            if (Action == "R" || Action == "L")
                Turn(Action, magnitude);
            else
                Move(Action, magnitude);
        }

        private List<string> ordinalDirections = new List<string> { "N", "E", "S", "W" };
        private void Turn(string direction, int degrees)
        {
            int turnTimes = degrees / 90;

            int startingPosition = ordinalDirections.IndexOf(Heading);

            if (direction == "R")
            {
                for (int r = 1; r <= turnTimes; r++)
                {
                    int newIndex = startingPosition + r;
                    if (newIndex > 3)
                        newIndex = 0;
                    Heading = ordinalDirections[newIndex];
                }
            }

            if (direction == "L")
            {
                for (int l = 1; l <= turnTimes; l++)
                {
                    int newIndex = startingPosition - l;
                    if (newIndex < 0)
                        newIndex = 3;
                    Heading = ordinalDirections[newIndex];
                }
            }
        }

        private void Move(string direction, int magnitude)
        {

        }
    }

    public static class Day12
    {
        public const string TestString = @"F10
N3
F7
R90
F11";

        private const string dayNumber = "12";


        public static void Run()
        {
            Console.WriteLine($"Running Day {dayNumber} {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day{dayNumber}Input.txt");


            // Action N means to move north by the given value.
            // Action S means to move south by the given value.
            // Action E means to move east by the given value.
            // Action W means to move west by the given value.
            // Action L means to turn left the given number of degrees.
            // Action R means to turn right the given number of degrees.
            // Action F means to move forward by the given value in the direction the ship is currently facing.

            // test string Manhattan distance = 25

            Ship myShip = new Ship();
        }
    }
}