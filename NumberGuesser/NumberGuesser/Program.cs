using System;

namespace NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            Random random = new Random();
            int number = random.Next(100);
            int retryCounter = 0;
            int angryCounter = 0;
            String answer;
            int intAnswer = 0;
            String[] memoryArray = new String[1000];

            Console.WriteLine("Hello. Do you want play with me? It's my game and my rules.\nWrite your name:");
            String userName = Console.ReadLine();
            String[] angryString = new String[4] {
                String.Format("{0}, you're a real rattlebrain. That's just funk.\n", userName),
                String.Format("{0}, that's as easy as apple pie. What's the deal?\n", userName),
                String.Format("{0}, you're so soft. That's funky thing.\n", userName),
                String.Format("{0}, don't be such a knockhead.\n", userName) };
            Console.WriteLine("Okay, {0}, try guess my number. It's in [0..100].\nAnd your choice:", userName);

            do
            {
                if (angryCounter == 4)
                {
                    Console.Write(angryString[random.Next(4)]);
                    angryCounter = 0;
                }

                answer = Console.ReadLine();

                if (answer.Equals("q"))
                {
                    Console.WriteLine("You're leaving. That's no problem of mine.\nPress any key...");
                    Console.Read();
                    Environment.Exit(0);

                }

                bool checkAnswer = int.TryParse(answer, out intAnswer);

                if (checkAnswer)
                {

                    angryCounter++;
                    retryCounter++;

                    if (number > intAnswer)
                    {
                        Console.WriteLine("Nice try. My number is greater. Try again.");
                        memoryArray[retryCounter] = String.Format("{0}, greater", answer);
                    }
                    if (number < intAnswer)
                    {
                        Console.WriteLine("Mistake. My number is smaller. Try again.");
                        memoryArray[retryCounter] = String.Format("{0}, smaller", answer);
                    }

                }
                else
                {
                    Console.WriteLine("It's not a number! Don't joke with me. Try again.");
                    continue;
                }

            }
            while (number != intAnswer);

            int time = (int)DateTime.Now.Subtract(start).TotalMinutes;

            Console.WriteLine("Well. You coped in {0} steps:", retryCounter);
            for (int i = 0; i < retryCounter; i++)
            {
                Console.WriteLine("{0}", memoryArray[i]);
            }
            Console.WriteLine("{0} , this is my number. You're smarter than I thought.", number);
            Console.WriteLine("\nYou spent it {0} minutes of your miserable life.\nPress any key...", time);
            Console.Read();
        }
    }
}
