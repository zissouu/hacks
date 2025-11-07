using System;
using hackz.Game;

namespace hackz.Game
{
    public class CommandParser
    {
        private readonly MissionManager _missionManager;

        public CommandParser(MissionManager missionManager)
        {
            _missionManager = missionManager;
        }

        public void Parse(string input)
        {
            switch (input.Trim().ToLower())
            {
                case "missions":
                    _missionManager.StartMissions();
                    break;

                case "help":
                    Console.WriteLine("Available commands: missions, exit, help");
                    break;

                case "exit":
                    Console.WriteLine("Disconnecting... Stay neon.");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'help' for options.");
                    break;
            }
        }
    }
}
