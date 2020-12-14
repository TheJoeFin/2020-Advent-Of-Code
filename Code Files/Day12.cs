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

        public int ManhattanDirection
        {
            get
            {
                return Math.Abs(Position.X) + Math.Abs(Position.Y);
            }
        }

        public Point Waypoint { get; set; } = new Point(10, 1);

        public string Heading { get; set; } = "E";

        public void Act(string order, bool Verbose = false)
        {
            string Action = order.Substring(0, 1);
            int magnitude = int.Parse(order.Substring(1, order.Length - 1));

            if (Action == "R" || Action == "L")
                Turn(Action, magnitude, Verbose);
            else
                Move(Action, magnitude, Verbose);
        }

        private List<string> ordinalDirections = new List<string> { "N", "E", "S", "W" };
        private void Turn(string direction, int degrees, bool verbose = false)
        {
            int turnTimes = degrees / 90;

            int startingPosition = ordinalDirections.IndexOf(Heading);

            if (direction == "R")
            {
                for (int r = 1; r <= turnTimes; r++)
                {
                    Waypoint = new Point(Waypoint.Y, -Waypoint.X);
                }
            }

            if (direction == "L")
            {
                for (int l = 1; l <= turnTimes; l++)
                {
                    Waypoint = new Point(-Waypoint.Y, Waypoint.X);
                }
            }
        }

        private void Move(string direction, int magnitude, bool Verbose = false)
        {
            string translatedDirection = "";

            if (direction == "F")
                MoveShip();
            else
                MoveWaypoint(string direction, int magnitude);

        }

        private void MoveWaypoint(string direction, int magnitude)
        {
            switch (direction)
            {
                case "N":
                    Waypoint = new Point(Waypoint.X, Waypoint.Y + magnitude);
                    break;
                case "E":
                    Waypoint = new Point(Waypoint.X + magnitude, Waypoint.Y);
                    break;
                case "S":
                    Waypoint = new Point(Waypoint.X, Waypoint.Y - magnitude);
                    break;
                case "W":
                    Waypoint = new Point(Waypoint.X - magnitude, Waypoint.Y);
                    break;
                default:
                    break;
            }
        }

        private void MoveShip()
        {
            Position = new Point(Position.X + Waypoint.X, Position.Y + Waypoint.Y);
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

            foreach (string line in TestString.ReadLines())
            {
                myShip.Act(line, true);
            }

            Console.WriteLine($"Manhattan Direction is {myShip.ManhattanDirection}");
            // 2795 is false
        }
    }
}