using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public class Bag
    {
        public string Appearance { get; set; } = "";

        public List<(int bagNumber, string bagDescription)> Contents { get; set; } = new List<(int bagNumber, string bagDescription)>();

        public override string ToString()
        {
            return $"{Appearance} bag contains {Contents}";
        }
    }


    public static class Day7
    {
        public const string TestString =
            @"
            light red bags contain 1 bright white bag, 2 muted yellow bags.
            dark orange bags contain 3 bright white bags, 4 muted yellow bags.
            bright white bags contain 1 shiny gold bag.
            muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
            shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
            dark olive bags contain 3 faded blue bags, 4 dotted black bags.
            vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
            faded blue bags contain no other bags.
            dotted black bags contain no other bags.";

        public static void Run()
        {
            Console.WriteLine("Running");

            string inputString = File.ReadAllText($"Input Files/Day7Input.txt");

            List<Bag> bags = ConvertToBags(TestString);

            foreach (Bag bag in bags)
            {
                Console.WriteLine(bag.ToString());
            }
        }

        public static List<Bag> ConvertToBags(string listOfBags)
        {
            List<Bag> returnList = new List<Bag>();

            foreach (string bagDescription in listOfBags.ReadLines())
            {
                if (string.IsNullOrWhiteSpace(bagDescription))
                    continue;

                string[] wordArray = bagDescription.Trim().Split();

                Bag newBag = new Bag();
                newBag.Appearance = $"{wordArray[0]} {wordArray[1]}";
                string contentsString = "";
                for (int i = 4; i < (wordArray.Count() - 1); i++)
                {
                    contentsString += $"{wordArray[i]} ";
                }

                string[] bagsStrings = contentsString.Split(',');

                foreach (string bagString in bagsStrings)
                {
                    string[] bagDetails = bagString.Split();
                    int bagQty = 0;
                    int.TryParse(bagDetails[0], out bagQty);
                    newBag.Contents.Add((bagQty, $"{bagDetails[1]} {bagDetails[2]}"));
                }

                returnList.Add(newBag);
            }

            return returnList;
        }
    }
}