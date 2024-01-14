using System;
using System.Linq;

namespace projekt_csharp_net_dt071G
{
    //Hanterar urval och tillägg av spelare
    public static class PlayerSelection
    {
        //Vid val av befintlig spelare i GameMenu.cs
        public static string? SelectPlayer(string prompt)
        {
            Console.WriteLine(prompt);
            string? selectedPlayer = Console.ReadLine();
            //Hämtar spelare från den befintliga listan av spelare
            Player? player = Players.GetPlayers().FirstOrDefault(p => p.Name == selectedPlayer);

            if (player != null)
            {
                //Returnerar namnet på den valda spelaren
                return selectedPlayer;
            }
            else
            {
                //Meddelar att den valda spelaren inte hittades och returnerar null
                Console.WriteLine($"Spelare {selectedPlayer} hittades inte.");
                return null;
            }
        }

        //Lägger till en ny spelare med ett unikt namn
        public static void AddPlayer()
        {
            Console.Write("Namn på ny spelare (måste vara unikt):");
            string? playerName = Console.ReadLine();

            //Skapar en ny spelare med noll vinster och förluster
            Player player = new Player { Name = playerName, Wins = 0, Loses = 0 };

            //Lägger till den nya spelaren i spelarlistan
            Players.AddPlayer(player);
        }
    }
}
