using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace hackz.Game
{
    public class HackzMission
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<HackzMissionChoice> Choices { get; set; } = new();
        public int? NextMissionId { get; set; }
    }

    public class HackzMissionChoice
    {
        public string Option { get; set; } = string.Empty;
        public double SuccessChance { get; set; }
        public string SuccessText { get; set; } = string.Empty;
        public string FailText { get; set; } = string.Empty;
    }

    public class MissionManager
    {
        private List<HackzMission> _missions = new();
        private HackzMission? _currentMission;
        private readonly Random _rng = new();

        public void LoadMissions(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Mission file not found: {filePath}");
                    return;
                }

                string json = File.ReadAllText(filePath);
                _missions = JsonSerializer.Deserialize<List<HackzMission>>(json) ?? new List<HackzMission>();
                Console.WriteLine($"Loaded {_missions.Count} missions.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading missions: {ex.Message}");
            }
        }

        public void StartMissions()
        {
            if (_missions.Count == 0)
            {
                Console.WriteLine("No missions loaded.");
                return;
            }

            _currentMission = _missions[0];
            RunMission(_currentMission);
        }

        private void RunMission(HackzMission mission)
        {
            Console.Clear();
            PrintSlow($"[MISSION {mission.Id}] {mission.Title}\n", 20);
            PrintSlow($"{mission.Description}\n\n", 15);

            for (int i = 0; i < mission.Choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {mission.Choices[i].Option}");
            }

            Console.Write("\nChoose your exploit > ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choiceIndex) &&
                choiceIndex > 0 &&
                choiceIndex <= mission.Choices.Count)
            {
                var choice = mission.Choices[choiceIndex - 1];
                AttemptChoice(mission, choice);
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again...");
                Thread.Sleep(1000);
                RunMission(mission);
            }
        }

        private void AttemptChoice(HackzMission mission, HackzMissionChoice choice)
        {
            Console.WriteLine("\nRunning exploit...");
            Thread.Sleep(1000);

            bool success = _rng.NextDouble() < choice.SuccessChance;

            if (success)
                PrintSlow($"\nSUCCESS: {choice.SuccessText}\n", 15);
            else
                PrintSlow($"\nFAILURE: {choice.FailText}\n", 15);

            if (mission.NextMissionId != null)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();

                var nextMission = _missions.Find(m => m.Id == mission.NextMissionId);
                if (nextMission != null)
                {
                    _currentMission = nextMission;
                    RunMission(_currentMission);
                }
                else
                {
                    Console.WriteLine("Next mission not found.");
                }
            }
            else
            {
                PrintSlow("\n*** MISSION COMPLETE ***\n\nYou’ve reached the end of the current storyline.\n", 20);
                Console.WriteLine("Press Enter to return to the terminal...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void PrintSlow(string text, int delayMs = 25)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
        }
    }
}
