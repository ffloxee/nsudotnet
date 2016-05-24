using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class Program
    {

        static void Main(string[] args)
        {
            DateTime date = new DateTime();
            DateTime today = DateTime.Today;

            Console.WriteLine("Please, enter date:");
            String stringDate = Console.ReadLine();

            if (!DateTime.TryParse(stringDate, out date))
            {
                Console.WriteLine("This is a wrong date format.\nPress any key...");
                Console.Read();
                Environment.Exit(0);
            }

            Console.ForegroundColor = ConsoleColor.White;
            // исправлен "хардкод" с днями недели
            int dayOfWeek = (int)date.DayOfWeek;
            DateTime day = date.AddDays(1 - dayOfWeek);

            for (int i = 0; i < 7; i++)
            {
                Console.Write("   {0}\t", day.ToString("ddd"));
                day = day.AddDays(1);
            }
            Console.WriteLine();

            int dayInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            DateTime firstDate = new DateTime(date.Year, date.Month, 1);
            int count = (int)firstDate.DayOfWeek;
            if (count == 0)
            {
                count = 7;
            }

            for (int i = 1; i < count; i++)
            {
                Console.Write("\t");
            }

            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                if ((i == today.Day) && (today.Month == date.Month) && (today.Year == date.Year))
                {
                    Console.BackgroundColor = ConsoleColor.Gray;

                }
                else
                {
                    if (i == date.Day)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;

                    }
                }
                Console.Write("   {0}\t", i);
                switch (count)
                {
                    case 5:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }
                    case 6:
                        {
                            dayInMonth--;
                            break;
                        }
                    case 7:
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\n");
                            dayInMonth--;
                            count = 0;
                            break;
                        }
                }
                count++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nThe number of weekdays {0}", dayInMonth);
            Console.Read();

        }
    }
}
