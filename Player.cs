using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace projekt_csharp_net_dt071G
{
    //Hanterar lägg till, uppdatera och hämta spelarinformation
    public static class Players
    {
        //Filnamn för JSON-filen som lagrar spelarinformation
        private static string filename = @"Players.json";

        //Lista för att lagra information om spelare
        private static List<Player> players = new List<Player>();

        //Konstruktor som körs vid initiering av Players-klassen
        static Players()
        {
            //Läs in spelare från JSON-filen om den existerar
            if (File.Exists(filename))
            {
                var jsonString = File.ReadAllText(filename);
                //Deserialisera listan från JSON-format
                players = JsonSerializer.Deserialize<List<Player>>(jsonString);
            }
        }

        //Lägger till en ny spelare om spelarens namn inte redan finns
        public static void AddPlayer(Player player)
        {
            //Kontrollera om spelarens namn är unikt
            if (!players.Any(p => p.Name == player.Name))
            {
                players.Add(player);
                //Spara ändringar till filen
                Marshal(); 
                Console.WriteLine($"Spelare {player.Name} har lagts till.");
            }
            else
            {
                Console.WriteLine($"En spelare med namnet {player.Name} finns redan. Välj ett unikt namn.");
            }
        }

        //Returnerar en lista med alla spelare
        public static List<Player> GetPlayers()
        {
            return players;
        }

        //Sparar spelarinformation till filen
        private static void Marshal()
        {
            var jsonString = JsonSerializer.Serialize(players);
            File.WriteAllText(filename, jsonString);
        }

        //Uppdaterar resultatet för en spelare baserat på om de vann eller förlorade
        public static void UpdatePlayerScore(string playerName, bool won)
        {
            //Kolla om spelaren finns
            Player? player = players.FirstOrDefault(p => p.Name == playerName);

            if (player != null)
            {
                //Uppdatera antal vinster eller förluster beroende på resultatet
                if (won)
                {
                    player.Wins++;
                }
                else
                {
                    player.Loses++;
                }

                //Spara ändringar till filen
                Marshal(); 
            }
            else
            {
                Console.WriteLine($"Spelaren {playerName} hittades inte.");
            }
        }
    }

    //Representerar en spelare med namn, antal vinster och antal förluster
    public class Player
    {
        public string? Name { get; set; }
        public int? Wins { get; set; }
        public int? Loses { get; set; }
    }
}
