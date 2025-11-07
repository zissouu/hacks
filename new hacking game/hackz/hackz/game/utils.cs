// hackz/hackz/game/Utils.cs
using System;
using System.Text;
using System.Threading;

namespace hackz.Game
{
    public static class Utils
    {
        private static readonly Random _rand = new();

        // Print text like a typewriter. Matches calls: Utils.TypeLine(text, delayMs)
        public static void TypeLine(string text, int delayMs = 15)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
        }

        // Matrix-style effect. Matches calls: Utils.MatrixEffect(lines, delayMs)
        public static void MatrixEffect(int lines = 10, int delayMs = 50)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            int width;
            try { width = Console.WindowWidth; }
            catch { width = 80; }

            ConsoleColor origColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            for (int i = 0; i < lines; i++)
            {
                var line = new char[width];
                for (int j = 0; j < width; j++)
                    line[j] = chars[_rand.Next(chars.Length)];

                Console.WriteLine(new string(line));
                Thread.Sleep(delayMs);
            }

            Console.ForegroundColor = origColor;
        }

        // Glitch effect. Matches calls: Utils.GlitchEffect(message, glitchCount, delayMs)
        public static void GlitchEffect(string message, int glitchCount = 3, int delayMs = 100)
        {
            string chars = "!@#$%^&*<>?/\\|~";
            int origLeft = 0;
            int origTop = 0;
            try
            {
                origLeft = Console.CursorLeft;
                origTop = Console.CursorTop;
            }
            catch { /* some terminals may not support cursor queries */ }

            for (int i = 0; i < glitchCount; i++)
            {
                var sb = new StringBuilder(message);
                int swaps = Math.Max(1, message.Length / 6);
                for (int s = 0; s < swaps; s++)
                {
                    int pos = _rand.Next(0, message.Length);
                    sb[pos] = chars[_rand.Next(chars.Length)];
                }

                Console.WriteLine(sb.ToString());
                Thread.Sleep(delayMs);

                try
                {
                    if (Console.CursorTop > 0) Console.SetCursorPosition(0, Console.CursorTop - 1);
                }
                catch { /* ignore */ }
            }

            Console.WriteLine(message); // final clean line
        }

        // Print a random fake system alert. Matches calls: Utils.PrintRandomSystemAlert()
        public static void PrintRandomSystemAlert()
        {
            string[] alerts =
            {
                "SYSTEM OVERLOAD WARNING",
                "Segmentation fault in your coffee machine driver",
                "CPU usage at 9000%... just kidding",
                "Unauthorized cat detected on network",
                "[!] Tracing route back to source..."
            };

            TypeLine(alerts[_rand.Next(alerts.Length)], 20);
        }

        // Hacking progress animation. Matches calls: Utils.RunHackingAnimation(durationMs, speed)
        public static void RunHackingAnimation(int durationMs = 1500, int speed = 50)
        {
            string[] frames = new string[]
            {
                "[=         ]",
                "[==        ]",
                "[===       ]",
                "[====      ]",
                "[=====     ]",
                "[======    ]",
                "[=======   ]",
                "[========  ]",
                "[========= ]",
                "[==========]"
            };

            int start = Environment.TickCount;
            while (Environment.TickCount - start < durationMs)
            {
                foreach (var frame in frames)
                {
                    Console.Write($"\r{frame}");
                    Thread.Sleep(speed);
                }
            }
            Console.WriteLine("\r[==========] Done!");
        }

        // Random hacker jokes. Matches calls: Utils.PrintRandomJoke()
        public static void PrintRandomJoke()
        {
            string[] jokes =
            {
                "Warning: Admin_404 is watching you sip your coffee.",
                "Tip: Brute force is only effective if you actually know the password.",
                "Reminder: Digital donuts contain zero calories. You’re welcome.",
                "System Alert: The office cat has gained root access again.",
                "404 Humor Not Found – but your mission is ready."
            };

            TypeLine(jokes[_rand.Next(jokes.Length)], 15);
        }
    }
}
