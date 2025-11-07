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

            // Matrix-style flicker intro
            Utils.MatrixEffect(6, 30);
            Thread.Sleep(200);

            Utils.GlitchEffect("HACKZNET v3.7 BOOT", 6, 25);
            Thread.Sleep(200);

            Utils.TypeLine("Kernel patching...  ok", 18);
            Thread.Sleep(120);
            Utils.TypeLine("Dial-up handshake: ~~~ ~~~ ~~~", 18);
            Thread.Sleep(120);
            Utils.TypeLine("Modem noise: bzzzzzt-eeeeep...", 18);
            Thread.Sleep(300);

            // RootKitten (mentor) flavour
            Utils.TypeLine("\nA message appears in pulsing green:", 20);
            Utils.GlitchEffect("<RootKitten@the-grid>", 4, 40);
            Utils.TypeLine("\"Hey. Welcome to the Grid, recruit.\"", 22);
            Utils.TypeLine("\"We don't do world-ending hacks on the first day. Start small: don't set anything on fire (physically).\"", 18);
            Thread.Sleep(200);

            Utils.PrintRandomJoke();
            Thread.Sleep(300);

            Utils.TypeLine("\nType 'help' if you're lost. Type 'missions' to begin your first assignment.", 16);
            Console.WriteLine();

            // Load missions & start command loop (bypass GameEngine intro to avoid duplication)
            var missionManager = new MissionManager();
            missionManager.LoadMissions("Data/missions.json");

            var parser = new CommandParser(missionManager);

            // Command loop
            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine()?.Trim() ?? "";
                parser.Parse(input);
            }
        }
    }
}
