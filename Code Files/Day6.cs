using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{

    public class BoardingGroup
    {
        public HashSet<char> YesAnswers { get; set; } = new HashSet<char>();
        public List<string> ListOfPersonAnswers { get; set; } = new List<string>();
        public int AllYesAnswers
        {
            get
            {
                int numberOfYes = 0;

                foreach (char yesAns in YesAnswers)
                {
                    bool YesToAll = true;

                    foreach (string personAnswer in ListOfPersonAnswers)
                    {
                        if (personAnswer.Contains(yesAns) == false)
                            YesToAll = false;
                    }

                    if (YesToAll)
                        numberOfYes++;
                }

                return numberOfYes;
            }
        }
    }

    public static class Day6
    {
        private const string testString = @"
abc

a
b
c

ab
ac

a
a
a
a

b
            ";

        public static IEnumerable<string> ReadLines(this string s)
        {
            string line;
            using (var sr = new StringReader(s))
                while ((line = sr.ReadLine()) != null)
                    yield return line;
        }

        public static void Run()
        {
            string inputString = File.ReadAllText($"Input Files/Day6Input.txt");

            // List<string> inputList = ConvertStringToListOfStrings(inputString);

            // int total = 0;

            List<BoardingGroup> EveryoneOnThePlane = new List<BoardingGroup>();

            BoardingGroup newBoardingGroup = new BoardingGroup();

            int totalCommonYes = 0;

            foreach (string singleLine in inputString.ReadLines())
            {
                string cleanLine = singleLine.Trim();

                // HashSet<char> uniqueLetters = new HashSet<char>(customsAnswers.Trim());
                // int countYes = uniqueLetters.Count();
                // Console.WriteLine($"Number of Yes: {countYes} - {customsAnswers}");

                if (cleanLine != string.Empty)
                {
                    newBoardingGroup.ListOfPersonAnswers.Add(cleanLine);

                    foreach (char cleanStringChar in cleanLine.ToCharArray())
                    {
                        newBoardingGroup.YesAnswers.Add(cleanStringChar);
                    }
                }

                if (cleanLine == string.Empty && newBoardingGroup.ListOfPersonAnswers.Count() > 0)
                {
                    totalCommonYes += newBoardingGroup.AllYesAnswers;
                    EveryoneOnThePlane.Add(newBoardingGroup);

                    newBoardingGroup = new BoardingGroup();
                }

                // total += countYes;
            }

            if (newBoardingGroup.ListOfPersonAnswers.Count() > 0)
            {
                totalCommonYes += newBoardingGroup.AllYesAnswers;
            }

            Console.WriteLine($"Total: {totalCommonYes}");
        }

        private static List<string> ConvertStringToListOfStrings(string input)
        {
            List<string> returnList = new List<string>();

            string stringLine = "";

            foreach (string singleRead in input.ReadLines())
            {

                if (string.IsNullOrWhiteSpace(singleRead) == false)
                {
                    stringLine += singleRead;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(stringLine) == false)
                        returnList.Add(stringLine);

                    stringLine = string.Empty;
                }
            }

            if (string.IsNullOrWhiteSpace(stringLine) == false)
                returnList.Add(stringLine);

            return returnList;
        }

    }
}