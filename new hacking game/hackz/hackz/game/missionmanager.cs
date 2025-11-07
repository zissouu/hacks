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
        public int? NextMissionOnSuccess { get; set; }
        public int? NextMissionOnFailure { get; set; }
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
            Utils.MatrixEffect(6, 25);
            Utils.GlitchEffect($"[MISSION {mission.Id}] {mission.Title}", 4, 30);
            Utils.TypeLine($"{mission.Description}\n", 15);
            Utils.PrintRandomSystemAlert();
            Console.WriteLine();

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
            Utils.TypeLine("\nRunning exploit...", 20);
            Thread.Sleep(400);

            // Run dynamic ASCII hacking animation
            Utils.RunHackingAnimation(1500, 50);

            bool success = _rng.NextDouble() < choice.SuccessChance;

            if (success)
                Utils.TypeLine($"\nSUCCESS: {choice.SuccessText}\n", 15);
            else
                Utils.TypeLine($"\nFAILURE: {choice.FailText}\n", 15);

            // Determine next mission based on success/failure
            int? nextId = success ? mission.NextMissionOnSuccess : mission.NextMissionOnFailure;

            if (nextId != null)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();

                var nextMission = _missions.Find(m => m.Id == nextId);
                if (nextMission != null)
                {
                    _currentMission = nextMission;
                    RunMission(_currentMission);
                }
                else
                {
                    Utils.TypeLine("Next mission not found. Returning to terminal...", 20);
                    Console.ReadLine();
                }
            }
            else
            {
                Utils.TypeLine("\n*** MISSION COMPLETE ***\nYou’ve reached the end of this storyline.\n", 20);
                Utils.PrintRandomJoke();
                Console.WriteLine("Press Enter to return to the terminal...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
