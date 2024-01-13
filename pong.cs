using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace projekt_csharp_net_dt071G
{
    public class Pong
    {
        private static string? player1;
        private static string? player2;

        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("1. Starta spel");
            Console.WriteLine("2. Regler");
            Console.WriteLine("X. Avsluta");
            int inp = (int)Console.ReadKey(true).Key;
            switch (inp)
            {
                case '1':
                    Console.Clear();
                    // Loopa igenom och visa alla spelare
                    Console.WriteLine("Spelare:");
                    foreach (Player player in Players.GetPlayers())
                    {
                        Console.WriteLine(player.Name);
                    }
                    Console.WriteLine("\nVal för spelare 1:");
                    Console.WriteLine("1. Lägg till ny spelare:");
                    Console.WriteLine("2. Välj spelare:");
                    Console.WriteLine("X. Tillbaka");

                    int addChoose = (int)Console.ReadKey(true).Key;
                    switch (addChoose)
                    {
                        case '1':
                            // Skriv in spelarnas namn
                            Console.Write("Namn på ny spelare (måste vara unikt):");
                            string? playerName = Console.ReadLine();

                            // Skapa spelarobjekt med namnen
                            Player player = new Player { Name = playerName, Wins= 0, Loses= 0 };

                            // Lägg till spelarna i listan (antag att detta är en lista av Player-objekt)
                            Players.AddPlayer(player);
                            player1 = playerName;
                            Console.Clear();

                            //Spelare 2
                            Console.WriteLine("\nVal för spelare 2:");
                            Console.WriteLine("1. Lägg till ny spelare:");
                            Console.WriteLine("2. Välj spelare:");
                            Console.WriteLine("X. Tillbaka");
                            int addChoose2 = (int)Console.ReadKey(true).Key;
                            switch (addChoose2)
                            {
                                case '1':
                                    // Skriv in spelarnas namn
                                    Console.Write("\nNamn på ny spelare (måste vara unikt):");
                                    string? playerName2 = Console.ReadLine();

                                    // Skapa spelarobjekt med namnen
                                    Player players2 = new Player { Name = playerName2, Wins= 0, Loses= 0 };

                                    // Lägg till spelarna i listan (antag att detta är en lista av Player-objekt)
                                    Players.AddPlayer(players2);
                                    player2 = playerName2;
                                    break;

                                case '2':
                                    Console.WriteLine("\nSkriv in namn på önskad spelare");
                                    string? selectedPlayerName2 = Console.ReadLine();

                                    // Kontrollera om spelaren finns i listan
                                    Player? selectedPlayer2 = Players.GetPlayers().FirstOrDefault(p => p.Name == selectedPlayerName2);
                                    if (selectedPlayer2 != null)
                                    {
                                        player2 = selectedPlayerName2;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Spelare {selectedPlayerName2} hittades inte.");
                                    }
                                    
                                    break;
                                case 88:
                                    Start();
                                    break;
                                default:
                                    Console.WriteLine("Ogiltigt val. Försök igen.");
                                    Console.ReadKey();
                                    Start();
                                    break;
                            }

                            break;

                        case '2':
                            Console.WriteLine("\nSkriv in namn på önskad spelare");
                            string? selectedPlayerName = Console.ReadLine();

                            // Kontrollera om spelaren finns i listan
                            Player? selectedPlayer = Players.GetPlayers().FirstOrDefault(p => p.Name == selectedPlayerName);
                            if (selectedPlayer != null)
                            {
                                player1 = selectedPlayerName;
                            }
                            else
                            {
                                Console.WriteLine($"Spelare {selectedPlayerName} hittades inte.");
                            }

                            //Spelare 2
                            Console.WriteLine("\nVal för spelare 2:");
                            Console.WriteLine("1. Lägg till ny spelare:");
                            Console.WriteLine("2. Välj spelare:");
                            Console.WriteLine("(X). Tillbaka");
                            int addChose22 = (int)Console.ReadKey(true).Key;
                            switch (addChose22)
                            {
                                case '1':
                                    // Skriv in spelarnas namn
                                    Console.Write("\nNamn på ny spelare (måste vara unikt):");
                                    string? playerName22 = Console.ReadLine();

                                    // Skapa spelarobjekt med namnen
                                    Player players2 = new Player { Name = playerName22, Wins= 0, Loses= 0 };

                                    // Lägg till spelarna i listan (antag att detta är en lista av Player-objekt)
                                    Players.AddPlayer(players2);
                                    player2 = playerName22;
                                    break;

                                case '2':
                                    Console.WriteLine("\nSkriv in namn på önskad spelare");
                                    string? selectedPlayerName2 = Console.ReadLine();

                                    // Kontrollera om spelaren finns i listan
                                    Player? selectedPlayer2 = Players.GetPlayers().FirstOrDefault(p => p.Name == selectedPlayerName2);
                                    if (selectedPlayer2 != null)
                                    {
                                        player2 = selectedPlayerName2;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Spelare {selectedPlayerName2} hittades inte.");
                                    }
                                    break;
                                case 88:
                                    Start();
                                    break;
                                default:
                                    Console.WriteLine("Ogiltigt val. Försök igen.");
                                    Console.ReadKey();
                                    Start();
                                    break;
                            }
                            
                            break;
                        case 88:
                            Start();
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val. Försök igen.");
                            break;
                    }
                    break;

                case '2':
                    Console.Clear();
                    PrintPongRules();
                    break;

                case 88:
                    Environment.Exit(0); // Avsluta programmet om användaren väljer alternativet 'X'
                    break;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    Console.ReadKey();
                    Start();
                    break;
            }
            Console.WriteLine("Tryck på en tangent för att starta spelet...");
            Console.ReadKey();
            StartGame();
        }

        static void StartGame()
        {
            Console.Clear();
            const int fieldLenght = 60, fieldWidth = 15;
            const char fieldTile = '#';
            string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLenght));

            const int racketLenght = fieldWidth / 4;
            const char racketTile = '|';

            int leftRacketHeight = 0;
            int rightRacketHeight = 0;

            int ballX = fieldLenght / 2;
            int ballY = fieldWidth / 2;
            const char ballTile = 'O';

            bool isBallGoingDown = true;
            bool isBallGoingRight = true;

            int leftPlayerPoints = 0;
            int rightPlayerPoints = 0;

            int scoreboardX = fieldLenght / 2 - 12;
            int scoreboardY = fieldWidth + 3;

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(line);

                Console.SetCursorPosition(0, fieldWidth);
                Console.WriteLine(line);

                for (int i = 0; i < racketLenght; i++)
                {
                    Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                    Console.WriteLine(racketTile);
                    Console.SetCursorPosition(fieldLenght - 1, i + 1 + rightRacketHeight);
                    Console.WriteLine(racketTile);
                }

                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(ballTile);
                    Thread.Sleep(100);

                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(' ');

                    if (isBallGoingDown)
                    {
                        ballY++;
                    }
                    else
                    {
                        ballY--;
                    }
                    if (isBallGoingRight)
                    {
                        ballX++;
                    }
                    else
                    {
                        ballX--;
                    }

                    if (ballY == 1 || ballY == fieldWidth - 1)
                    {
                        isBallGoingDown = !isBallGoingDown;
                    }

                    if (ballX == 1)
                    {
                        if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + racketLenght)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else
                        {
                            rightPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLenght / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            System.Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");

                            if (rightPlayerPoints == 5)
                            {
                                goto outer;
                            }
                        }
                    }
                    if (ballX == fieldLenght - 2)
                    {
                        if (ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + racketLenght)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else
                        {
                            leftPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLenght / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{player1} - {leftPlayerPoints} | {rightPlayerPoints} - {player2}");

                            if (leftPlayerPoints == 5)
                            {
                                goto outer;
                            }
                        }
                    }
                }
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (rightRacketHeight > 0)
                        {
                            rightRacketHeight--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (rightRacketHeight < fieldWidth - racketLenght - 1)
                        {
                            rightRacketHeight++;
                        }
                        break;
                    case ConsoleKey.W:
                        if (leftRacketHeight > 0)
                        {
                            leftRacketHeight--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (leftRacketHeight < fieldWidth - racketLenght - 1)
                        {
                            leftRacketHeight++;
                        }
                        break;
                }
                for (int i = 1; i < fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(' ');
                    Console.SetCursorPosition(fieldLenght - 1, i);
                    Console.WriteLine(' ');
                }
            }
            outer:;

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            if (rightPlayerPoints == 5)
            {
                Console.WriteLine($"{player2} vann!");
            }
            else
            {
                Console.WriteLine($"{player1} vann!");
            }

            string? winner = rightPlayerPoints == 5 ? player2 : player1;
            string? loser = rightPlayerPoints == 5 ? player1 : player2;

            // Update player scores
            Players.UpdatePlayerScore(winner, true);
            Players.UpdatePlayerScore(loser, false);


            Console.WriteLine("Tryck på en tangent för att gå tillbaka till spelmenyn...");
            Console.ReadKey();
            Start();
        }

        static void PrintPongRules()
        {
            Console.WriteLine("Spelets Mål:");
            Console.WriteLine("Målet med Pong är att få fler poäng än motståndaren genom att skjuta bollen förbi deras racket.");

            Console.WriteLine("\nSpelets Element:");
            Console.WriteLine("Boll: En liten boll som studsar fram och tillbaka på skärmen.");
            Console.WriteLine("Racket: Två vertikala paddlar (representeras av symbolen |");

            Console.WriteLine("\nStart av Spelet:");
            Console.WriteLine("Bollen placeras i mitten av skärmen.");
            Console.WriteLine("Spelarna startar från sina respektive sidor av skärmen med sina rackets.");

            Console.WriteLine("\nSpelets Framsteg:");
            Console.WriteLine("Spelarna slår bollen med sina rackets och försöker skjuta bollen förbi motståndarens racket.");
            Console.WriteLine("Om bollen passerar en spelares racket och går ut genom motståndarens sida av skärmen, får den andra spelaren poäng.");

            Console.WriteLine("\nPoängräkning:");
            Console.WriteLine("En spelare får ett poäng varje gång bollen går förbi motståndarens racket och går ut genom deras sida av skärmen.");
            Console.WriteLine("Poängen visas vanligtvis längst upp eller längst ner på skärmen.");

            Console.WriteLine("\nBollens Rörelse:");
            Console.WriteLine("Bollen studsar från sidorna och toppen/botten av skärmen.");
            Console.WriteLine("Om bollen träffar en spelares racket ändrar den riktning och fortsätter i motsatt riktning.");

            Console.WriteLine("\nSpelarens Racket:");
            Console.WriteLine("Spelaren kontrollerar sitt racket vertikalt och kan flytta upp eller ner för att träffa bollen.");
            Console.WriteLine("Racketen måste träffa bollen för att hålla spelet igång.");
            Console.WriteLine("Spelare 1 (vänster) använder W för upp och S för ned.");
            Console.WriteLine("Spelare 2 (höger) använder uppåtpil för upp och neråtpil för ned.");

            Console.WriteLine("\nGame Over:");
            Console.WriteLine("Spelet fortsätter tills en spelare når 5 poäng");
            Console.WriteLine("När spelet är över, visas vinnaren och spelarna återgår till startsidan.");

            Console.WriteLine("\nTryck på en tangent för att gå tillbaka");
            Console.ReadKey(); // Vänta på tangenttryckning
            Start(); //Gå tillbaka
        }
    }
}
