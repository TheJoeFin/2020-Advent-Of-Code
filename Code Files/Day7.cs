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
            Console.WriteLine($"Running {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day7Input.txt");

            List<string> inputStringList = inputString.ReadLines().ToList();

            HashSet<string> inputMatches = new HashSet<string>();
            inputMatches.Add("shiny gold");

            HashSet<string> simpleMatches = simpleTextMatch(inputStringList, inputMatches);
            int simpleMatchNumber = simpleMatches.Count();
            // subtract 1 because "shiny gold" was the first entry
            simpleMatchNumber--;

            List<Bag> bags = ConvertToBags(inputString);

            foreach (Bag bag in bags)
            {
                // Console.WriteLine(bag.ToString());
            }

            int numberOfBagsContainingGold = lookForBags("shiny gold", bags);

            //Console.WriteLine($"Number of bags holding shiny Gold {numberOfBagsContainingGold}");
            Console.WriteLine($"Simple text match {simpleMatchNumber}");
        }

        private static HashSet<string> simpleTextMatch(List<string> listOfStrings, HashSet<string> matches)
        {
            HashSet<string> returnHashSet = new HashSet<string>();

            foreach (var item in matches)
            {
                returnHashSet.Add(item);
            }

            // How many in the list hold the target (not  counting the target)
            foreach (string hashString in matches)
            {
                foreach (string line in listOfStrings)
                {
                    string desc = String.Join(" ", line.Trim().Split().Take(2));

                    if (line.Contains(hashString) && desc != hashString)
                        returnHashSet.Add(desc);
                }

            }

            if (returnHashSet.Count() != matches.Count())
            {
                returnHashSet = simpleTextMatch(listOfStrings, returnHashSet);
            }

            return returnHashSet;
        }

        private static int lookForBags(string v, List<Bag> bags)
        {
            int matches = 0;

            foreach (Bag bag in bags)
            {
                if (containsMatch(v, bag, bags))
                    matches++;
            }

            return matches;
        }

        private static bool containsMatch(string v, Bag bag, List<Bag> bags)
        {
            bool doesContainMatch = false;

            foreach ((int num, string desc) in bag.Contents)
            {
                if (num > 0)
                {
                    if (desc == v)
                        doesContainMatch = true;
                    else
                    {
                        Bag bagR = bags.Where(b => b.Appearance == desc).FirstOrDefault();

                        if (bagR != null)
                            doesContainMatch = containsMatch(bagR.Appearance, bagR, bags);
                    }
                }
            }

            return doesContainMatch;
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