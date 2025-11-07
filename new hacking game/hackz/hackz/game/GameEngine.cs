using System;
using System.Threading;
using hackz.Game;

namespace hackz.Game
{
    public class GameEngine
    {
        private readonly CommandParser _parser;
        private readonly MissionManager _missionManager;

        public GameEngine()
        {
            // Initialize the mission manager and load missions
            _missionManager = new MissionManager();
            _missionManager.LoadMissions("Data/missions.json");

            // Initialize the command parser
            _parser = new CommandParser(_missionManager);
        }

        public void Start()
        {
            Console.Clear();
            PrintIntro();

            // Main command loop
            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine()?.Trim() ?? "";
                _parser.Parse(input); // Use Parse() instead of Execute()
            }
        }

        private void PrintIntro()
        {
            Console.WriteLine("ACCESS GRANTED");
            Thread.Sleep(400);
            Console.WriteLine("Connection established.");
            Thread.Sleep(500);

            Console.WriteLine("\n*** WELCOME TO HACKZNET ***");
            Thread.Sleep(400);
            Console.WriteLine("You’re an intern at the world’s most chaotic hacktivist collective.");
            Thread.Sleep(400);
            Console.WriteLine("Admin_404 has a series of missions for you.");
            Thread.Sleep(400);
            Console.WriteLine("Your job? Leak corporate secrets, steal digital donuts, and expose absurdity.");
            Thread.Sleep(600);
            Console.WriteLine("\nType 'help' to see available commands.");
        }
    }
}
