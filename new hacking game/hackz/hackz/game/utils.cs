using System;
using System.Threading;

namespace hackz.Game
{
    public static class Utils
    {
        // Typewriter effect for text output
        public static void TypeText(string text, int delay = 25, bool newline = true)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            if (newline) Console.WriteLine();
        }

        // Simulate a loading bar
        public static void LoadingBar(string message, int duration = 1500)
        {
            Console.Write(message);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(duration / 5);
                Console.Write(".");
            }
            Console.WriteLine(" done!");
        }

        // ASCII logo intro
        public static void AsciiIntro()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
██╗  ██╗ █████╗  ██████╗██╗  ██╗███████╗
██║  ██║██╔══██╗██╔════╝██║ ██╔╝██╔════╝
███████║███████║██║     █████╔╝ ███████╗
██╔══██║██╔══██║██║     ██╔═██╗ ╚════██║
██║  ██║██║  ██║╚██████╗██║  ██╗███████║
╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝
");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        // Simulate 90s modem dial-up and connection boot (no sound)
        public static void ModemConnect()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            TypeText("Initializing 56k modem driver...", 30);
            Thread.Sleep(500);

            TypeText("Dialing BBS: DARKNET_404", 40);
            LoadingBar("", 2000);

            TypeText("Authenticating handshake...");
            Thread.Sleep(1000);
            GlitchFlicker("ACCESS GRANTED");
            Console.ResetColor();
        }

        // Glitch flicker animation
        public static void GlitchFlicker(string message, int flickers = 6)
        {
            for (int i = 0; i < flickers; i++)
            {
                Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.Green : ConsoleColor.DarkGreen;
                Console.WriteLine(message);
                Thread.Sleep(70);
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Fake diagnostics scroll
        public static void BootDiagnostics()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string[] lines = new string[]
            {
                "[SYS] BIOS version 2.04.3 loaded",
                "[RAM] 32MB memory verified",
                "[NET] Ethernet adapter detected",
                "[NET] IP assigned: 192.168.13.37",
                "[SEC] Firewall bypass module: OK",
                "[SYS] Launching hackz OS kernel..."
            };

            foreach (string line in lines)
            {
                TypeText(line, 30);
                Thread.Sleep(150);
            }

            Console.ResetColor();
            Thread.Sleep(700);
            Console.Clear();
        }
    }
}
