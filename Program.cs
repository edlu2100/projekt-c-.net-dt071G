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
        //Huvudmetoden för programmet
        static public void Main()
        {
            Console.Clear();

            //Visar huvudmenyn
            Console.WriteLine("1. Välj spel:");
            Console.WriteLine("2. Se spelare:");
            Console.WriteLine("X. Avsluta");

            //Läser in användarens tangenttryckning för huvudmenyn
            int inp = (int)Console.ReadKey(true).Key;

            //Hanterar användarens val
            switch (inp)
            {
                case '1':
                    //Undermeny för att välja ett spel
                    Console.Clear();
                    Console.WriteLine("1. Pong");
                    Console.WriteLine("2. Fyra i rad");
                    Console.WriteLine("X. Tillbaka");

                    //Läser in användarens tangenttryckning för spelmenyn
                    int game = (int)Console.ReadKey(true).Key;

                    //Hanterar användarens val i spelmenyn
                    switch (game)
                    {
                        //Pong-spelet som är skapat
                        case '1':
                            Pong.Start(); //Startar Pong-spelet
                            break;

                        //Fyra i rad (finns inte men hade planer på att skapa)
                        case '2':
                            Console.Clear();
                            Console.WriteLine("Under utveckling, kommer i framtiden...");
                            //Tyck tangen för att gå till Main
                            Console.WriteLine("\nTryck på en tangent för att gå tillbaka...");
                            Console.ReadKey();
                            Main();
                            break;

                        //Starta om programmet om användaren väljer alternativet 'X'
                        case 88:
                            Console.Clear();
                            Main(); 
                            break;
                        //om något annat än valen tycks i
                        default:
                            Console.Clear();
                            Console.WriteLine("Ogiltigt val. Försök igen.");
                            Main();
                            break;
                    }
                    break;

                //Visar spelarstatistik
                case '2':
                    Console.Clear();
                    int i = 0;
                    //Loopar igenom spelare och skriver ut statistik
                    foreach (Player player in Players.GetPlayers())
                    {
                        Console.WriteLine("[" + i++ + "] " + player.Name + ": " + CalculateWinLossRatio(player.Wins, player.Loses));
                    }
                    //Går tillbaka vid knapptyckning
                    Console.WriteLine("Tryck på en tangent för att gå tillbaka...");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                    break;

                //Avslutar programmet om användaren väljer alternativet 'X'
                case 88:
                    Environment.Exit(0); 
                    break;
                //Om något annat trycks
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    Main();
                    break;
            }
        }

        //Beräknar vinst-förlustförhållande och returnerar en sträng
        static string CalculateWinLossRatio(int? wins, int? loses)
        {
            if (loses != 0)
            {
                //Antal spelade matcher
                double gamesPlayed = (double)loses + (double)wins;
                //Får ett värde under 1
                double ratio = ((double)wins / gamesPlayed);
                //Skriver om till procent
                double percent = ratio * 100;
                //Retunerar sträng som sedans skrivs ut
                return $"Antal vinster: {wins}, Antal förluster: {loses} (Vinstprocent: {percent:F2}%)";
            }
            else
            {
                //Om inga förluster finns
                return $"{wins}/0 (100%)";
            }
        }
    }
}
