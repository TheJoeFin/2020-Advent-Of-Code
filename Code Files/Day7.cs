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

        public Bag()
        {

        }

        public Bag(string stringLine)
        {

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

        public const string Part2TestString =
        @"shiny gold bags contain 2 dark red bags.
          dark red bags contain 2 dark orange bags.
          dark orange bags contain 2 dark yellow bags.
          dark yellow bags contain 2 dark green bags.
          dark green bags contain 2 dark blue bags.
          dark blue bags contain 2 dark violet bags.
          dark violet bags contain no other bags.";

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

            //Console.WriteLine($"Number of bags holding shiny Gold {numberOfBagsContainingGold}");
            Console.WriteLine($"Simple text match {simpleMatchNumber}");

            List<Bag> bags = ConvertToBags(inputString);
            int countShinyGoldContents = NumberOfBagsInShinyGold(bags);
            Console.WriteLine($"Number of bags inside Shiny Gold {countShinyGoldContents}");


        }

        private static int NumberOfBagsInShinyGold(List<Bag> bags)
        {
            int shinyContentsCount = 0;

            Bag shinyGold = bags.Where(b => b.Appearance == "shiny gold").FirstOrDefault();

            foreach ((int qty, string desc) pair in shinyGold.Contents)
            {
                shinyContentsCount += pair.qty;

                if (pair.qty > 0)
                {
                    int subContents = BagContents(pair.desc, bags);

                    if (subContents > 0)
                        shinyContentsCount += (pair.qty * subContents);
                    else
                        shinyContentsCount += pair.qty;
                }

            }

            return shinyContentsCount;
        }

        private static int BagContents(string desc, List<Bag> bags)
        {
            int contentsNumber = 0;

            Bag bag = bags.Where(b => b.Appearance == desc).FirstOrDefault();

            if (bag != null)
            {
                if (bag.Contents.Count() > 0)
                {
                    foreach ((int qty, string desc) pair in bag.Contents)
                    {
                        int subContents = BagContents(pair.desc, bags);

                        if (subContents > 0)
                        {
                            contentsNumber += pair.qty;
                            contentsNumber += (pair.qty * subContents);
                        }
                        else
                            contentsNumber += pair.qty;
                    }
                }
            }

            return contentsNumber;
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
                    string[] bagDetails = bagString.Trim().Split();
                    int bagQty = 0;
                    int.TryParse(bagDetails[0], out bagQty);

                    if (bagQty > 0)
                        newBag.Contents.Add((bagQty, $"{bagDetails[1]} {bagDetails[2]}"));

                }

                returnList.Add(newBag);
            }

            return returnList;
        }
    }
}