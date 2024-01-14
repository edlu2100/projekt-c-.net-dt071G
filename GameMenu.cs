using System;

namespace projekt_csharp_net_dt071G
{
    // Visa och hantera huvudmenyn
    public static class GameMenu
    {
        // Visar huvudmenyalternativen
        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("PONG");
            Console.WriteLine("1. Starta spel");
            Console.WriteLine("2. Regler");
            Console.WriteLine("X. Tillbaka");
        }

        // Hantera användarinput från huvudmenyn
        public static void HandleMenuOption(char option)
        {
            switch (option)
            {
                // Starta spel genom att välja spelare
                case '1':
                    PlayerSelectionMenu();
                    break;

                case '2':
                    // Visar Pong-regler
                    Console.Clear();
                    PongRules.PrintPongRules();
                    Console.WriteLine("\nTryck på en tangent för att gå tillbaka");
                    Console.ReadKey(); // Väntar på tangenttryckning
                    Pong.Start();
                    break;

                case 'X':
                case 'x':
                    // Gå tillbaka till huvudmenyn
                    Program.Main();
                    break;

                default:
                    // Visar felmeddelande om ogiltig input
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    Console.ReadKey();
                    Pong.Start();
                    break;
            }
        }

        // Meny för spelarval vid start av spel
        private static void PlayerSelectionMenu()
        {
            Console.Clear();
            Console.WriteLine("Spelare:");

            // Skriver ut befintliga spelare
            foreach (Player player in Players.GetPlayers())
            {
                Console.WriteLine(player.Name);
            }

            // Val om man vill lägga till ny eller använda befintlig
            Console.WriteLine("\nVal för spelare 1:");
            Console.WriteLine("1. Lägg till ny spelare:");
            Console.WriteLine("2. Välj spelare:");
            Console.WriteLine("X. Tillbaka");

            char addChoose = Console.ReadKey(true).KeyChar;
            switch (addChoose)
            {
                // Lägg till en ny spelare
                case '1':
                    PlayerSelection.AddPlayer();
                    PlayerSelectionMenu();
                    break;

                // Välj en befintlig spelare för spelare 1
                case '2':
                    string? player1 = PlayerSelection.SelectPlayer("Skriv in namn på önskad spelare för spelare 1");
                    if (player1 != null)
                    {
                        // Gå till val för spelare 2
                        PlayerSelectionMenuForPlayer2(player1);
                    }
                    break;

                // Gå tillbaka till huvudmenyn
                case 'X':
                case 'x':
                    Pong.Start();
                    break;

                // Visar felmeddelande för ogiltig input
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    Console.ReadKey();
                    PlayerSelectionMenu();
                    break;
            }
        }

        // Meny för spelarval för spelare 2
        private static void PlayerSelectionMenuForPlayer2(string player1)
        {
            Console.WriteLine("\nVal för spelare 2:");
            Console.WriteLine("1. Lägg till ny spelare:");
            Console.WriteLine("2. Välj spelare:");
            Console.WriteLine("X. Tillbaka");

            char addChoose2 = Console.ReadKey(true).KeyChar;
            switch (addChoose2)
            {
                // Lägg till en ny spelare
                case '1':
                    PlayerSelection.AddPlayer();
                    PlayerSelectionMenuForPlayer2(player1);
                    break;

                // Välj en befintlig spelare för spelare 2 och starta spelet
                case '2':
                    string? player2 = PlayerSelection.SelectPlayer("Skriv in namn på önskad spelare för spelare 2");
                    if (player2 != null)
                    {
                        GameLogic.StartGame(player1, player2);
                    }
                    break;

                // Gå tillbaka till menyn för spelarval för spelare 1
                case 'X':
                case 'x':
                    PlayerSelectionMenu();
                    break;

                // Visar felmeddelande för ogiltig input
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    Console.ReadKey();
                    PlayerSelectionMenuForPlayer2(player1);
                    break;
            }
        }
    }
}
