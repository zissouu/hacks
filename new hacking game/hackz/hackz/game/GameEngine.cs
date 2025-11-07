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
            _missionManager = new MissionManager();
            _missionManager.LoadMissions("Data/missions.json");

            _parser = new CommandParser(_missionManager);
        }

        public void Start()
        {
            Console.Clear();
            PrintIntro();

            // Start missions after intro
            _missionManager.StartMissions();

            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine()?.Trim() ?? "";
                _parser.Parse(input);
            }
        }

        private void PrintIntro()
        {
            Utils.MatrixEffect(8, 30);
            Thread.Sleep(200);
            Utils.GlitchEffect("HACKZNET v3.7", 5, 30);
            Thread.Sleep(200);
            Utils.PrintRandomSystemAlert();
            Thread.Sleep(200);
            Utils.TypeLine("\n*** WELCOME TO HACKZNET ***", 25);
            Utils.TypeLine("You’re an intern at the world’s most chaotic hacktivist collective.", 20);
            Utils.TypeLine("Admin_404 has a series of missions for you.", 20);
            Utils.TypeLine("Your job? Leak corporate secrets, steal digital donuts, and expose absurdity.", 20);
            Thread.Sleep(400);
            Utils.PrintRandomJoke();
            Thread.Sleep(400);
            Utils.TypeLine("\nType 'help' to see available commands.", 15);
        }
    }
}
