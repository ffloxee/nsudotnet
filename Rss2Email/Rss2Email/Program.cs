using System;
using System.Threading;

namespace RSS2Email
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread thread = new Thread(ThreadLauncher);
            thread.Start();

        }

        public static void ThreadLauncher()
        {
            Sender sender = new Sender();
            while (true)
            {
                
                {
                    sender.SendData();
                }
               
                {
                    Console.WriteLine("--------------------------------");
                }
                Thread.Sleep(10000);
            }
        }
    }
}
