using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public static class Day8
    {
        public const string TestString = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

        public static void Run()
        {
            Console.WriteLine($"Running Day 8 {DateTime.Now.ToShortTimeString()}");

            string inputString = File.ReadAllText($"Input Files/Day8Input.txt");

            int accOutput = Boot(inputString);

            Console.WriteLine($"Accumulator Value {accOutput}");
        }

        private static int Boot(string programAsString)
        {
            int accumulator = 0;

            List<string> program = new List<string>();

            foreach (string programLine in programAsString.ReadLines())
            {
                program.Add(programLine);
            }

            int headPosition = 0;

            List<int> sequence = new List<int>();

            bool notInf = true;
            while (notInf)
            {
                if (sequence.Contains(headPosition))
                {
                    accumulator = 5;
                    Console.WriteLine("Inf Loop");
                    notInf = false;
                    break;
                }

                sequence.Add(headPosition);

                if (headPosition < 0)
                {
                    Console.WriteLine($"BAD Head position: {headPosition}");
                    notInf = false;
                    break;
                }


                string[] dividedLineString = program[headPosition].Split();
                (string command, int value) line = (dividedLineString[0], int.Parse(dividedLineString[1]));

                switch (line.command)
                {
                    case "nop":
                        headPosition++;
                        break;
                    case "acc":
                        accumulator += line.value;
                        headPosition++;
                        break;
                    case "jmp":
                        headPosition += line.value;
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"{program[headPosition]} : acc: {accumulator}");

            }

            return accumulator;
        }
    }
}