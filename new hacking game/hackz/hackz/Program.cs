using System;
using System.Threading;
using hackz.Game;

namespace hackz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine("ACCESS GRANTED");
            Console.WriteLine("Connection established.\n");
            Thread.Sleep(500);

            Console.WriteLine(">> Initializing HackzNet v3.7...");
            Thread.Sleep(700);
            Console.WriteLine(">> Bypassing corporate firewalls... Done.");
            Thread.Sleep(600);
            Console.WriteLine(">> Spoofing your mom’s Netflix account... Success.");
            Thread.Sleep(800);

            Console.WriteLine("\n*** WELCOME TO HACKZNET ***");
            Console.WriteLine("Type 'missions' to begin your first assignment.");

            var missionManager = new MissionManager();
            missionManager.LoadMissions("Data/missions.json");

            var parser = new CommandParser(missionManager);

            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine()?.Trim() ?? "";
                parser.Parse(input);
            }
        }
    }
}
