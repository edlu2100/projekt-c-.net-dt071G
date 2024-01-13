using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace projekt_csharp_net_dt071G
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("1. Välj spel:");
            Console.WriteLine("2. Se spelare:");
            Console.WriteLine("X. Avsluta");
            int inp = (int)Console.ReadKey(true).Key;
            switch (inp)
            {
                case '1':
                    Console.Clear();
                    Console.WriteLine("1. Pong");
                    Console.WriteLine("2. Hänga gubbe");
                    Console.WriteLine("3. Fyra i rad");
                    Console.WriteLine("X. Avsluta");
                    int game = (int)Console.ReadKey(true).Key;
                    switch (game)
                    {
                        case '1':
                            Pong.Start();
                            break;
                        case '2':
                            // Lägg till logik för Hänga gubbe
                            break;
                        case '3':
                            // Lägg till logik för Fyra i rad
                            break;
                        case 88:
                            Console.Clear();
                            Main(); // Starta om programmet om användaren väljer alternativet 'X'
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Ogiltigt val. Försök igen.");
                            Main();
                            break;
                    }
                    break;
                case '2':
                    Console.Clear();
                    // Loopa igenom och visa alla spelare
                    int i = 0;
                    foreach (Player player in Players.GetPlayers())
                    {
                        Console.WriteLine("[" + i++ + "] " + player.Name + " vinst " + CalculateWinLossRatio(player.Wins, player.Loses));
                    }
                    Console.WriteLine("Tryck på en tangent för att gå tillbaka...");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                    break;
                case 88:
                    Environment.Exit(0); // Avsluta programmet om användaren väljer alternativet 'X'
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    Main();
                    break;
            }
        }
         static string CalculateWinLossRatio(int? wins, int? loses)
        {
            if (loses != 0)
            {
                double ratio = ((double)wins / ((double)loses + (double)wins)) * 10;
                return $"{wins}/{loses} ({ratio:F2}%)";
            }
            else
            {
                // Handle the case where Loses is 0
                return $"{wins}/0 (100%)";
            }
        }
    }


    public static class Players
    {
        private static string filename = @"Players.json";
        private static List<Player> players = new List<Player>();

        static Players()
        {
            if (File.Exists(filename))
            {
                var jsonString = File.ReadAllText(filename);
                players = JsonSerializer.Deserialize<List<Player>>(jsonString);
            }
        }

        public static void AddPlayer(Player player)
        {
            // Kontrollera om spelarnamnet är unikt
            if (!players.Any(p => p.Name == player.Name))
            {
                players.Add(player);
                Marshal();
                Console.WriteLine($"Spelare {player.Name} har lagts till.");
            }
            else
            {
                Console.WriteLine($"Spelare med namnet {player.Name} finns redan. Välj ett unikt namn.");
            }
        }

        public static List<Player> GetPlayers()
        {
            return players;
        }

        private static void Marshal()
        {
            var jsonString = JsonSerializer.Serialize(players);
            File.WriteAllText(filename, jsonString);
        }
        public static void UpdatePlayerScore(string playerName, bool won)
    {
        Player? player = players.FirstOrDefault(p => p.Name == playerName);

        if (player != null)
        {
            if (won)
            {
                player.Wins++;
            }
            else
            {
                player.Loses++;
            }

            Marshal();
        }
        else
        {
            Console.WriteLine($"Spelaren {playerName} hittades inte.");
        }
    }
    }

    public class Player
    {
        public string? Name { get; set; }
        public int? Wins { get; set; }
        public int? Loses { get; set; }
    }
}
