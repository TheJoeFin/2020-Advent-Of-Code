using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace _2020_Advent_Of_Code
{
    public record AirplaneRow
    {
        public int RowID { get; set; } = 0;

        public int[] Seats { get; set; } = { 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    public class Day5
    {
        public static void Run()
        {
            string inputString = File.ReadAllText($"Input Files\\{MethodBase.GetCurrentMethod().DeclaringType.ToString().Split('.').LastOrDefault()}Input.txt");

            Console.WriteLine("hello from day 5");

            List<string> inputList = inputString.Split(Environment.NewLine.FirstOrDefault()).ToList();

            Console.WriteLine($"number of inputs {inputList.Count()}");

            // string testString = "BBFFBBFRLL";

            int largestSeatID = 0;
            int largetstRow = 0;
            int largestColumn = 0;

            List<AirplaneRow> AllSeats = new List<AirplaneRow>();

            foreach (string ticketString in inputList)
            {
                string trimmedString = ticketString.Trim();

                int splitPoint = 0;
                foreach (char character in trimmedString)
                {
                    if (character == 'F' || character == 'B')
                        splitPoint++;
                }

                string[] stringArr = { trimmedString.Substring(0, splitPoint), trimmedString.Substring(splitPoint, (trimmedString.Length - splitPoint)) };
                stringArr[0] = stringArr[0].Replace('F', '0');
                stringArr[0] = stringArr[0].Replace('B', '1');

                stringArr[1] = stringArr[1].Replace('L', '0');
                stringArr[1] = stringArr[1].Replace('R', '1');

                int rowInt = 0;
                int columnInt = 0;
                try
                {
                    rowInt = Convert.ToInt32(stringArr[0], 2);
                    columnInt = Convert.ToInt32(stringArr[1], 2);
                }
                catch (System.Exception)
                {
                    Console.WriteLine($"Error with string {ticketString} array {stringArr[0]},{stringArr[1]}");
                }
                int seatID = (rowInt * 8) + columnInt;

                if (seatID > largestSeatID)
                    largestSeatID = seatID;

                if (columnInt > largestColumn)
                    largestColumn = columnInt;

                if (rowInt > largetstRow)
                    largetstRow = rowInt;

                if (AllSeats.Where<AirplaneRow>(r => r.RowID == rowInt).Count() == 0)
                {
                    AirplaneRow newRow = new AirplaneRow();
                    newRow.RowID = rowInt;
                    AllSeats.Add(newRow);
                }

                AirplaneRow currentRow = AllSeats.Where<AirplaneRow>(r => r.RowID == rowInt).FirstOrDefault();

                currentRow.Seats[columnInt] = 1;
            }

            List<AirplaneRow> orderedSeats = AllSeats.OrderBy(x => x.RowID).ToList();

            foreach (AirplaneRow ar in orderedSeats)
            {
                string seatString = "";
                foreach (int seat in ar.Seats)
                {
                    seatString += seat.ToString();
                }

                Console.WriteLine($"{ar.RowID}: {seatString}");
            }

            Console.WriteLine($"Largest Seat ID is {largestSeatID}");
            Console.WriteLine($"Largest Row is {largetstRow}");
            Console.WriteLine($"Largest Column is {largestColumn}");
        }
    }
}