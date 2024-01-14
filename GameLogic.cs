using System;
using System.Linq;
using System.Threading;

namespace projekt_csharp_net_dt071G
{
    //Allt bakom Pong-spelet
    public static class GameLogic
    {
        //Startar Pong-spelet med player1 och player2 som valts
        public static void StartGame(string player1, string player2)
        {
            //Döljer cursor som ritar bollen
            Console.CursorVisible = false;
            Console.Clear();

            //Planens utformning samt tecken
            //Planens längd och bredd
            const int fieldLength = 60, fieldWidth = 15;
            //Tecken som representerar planen
            const char fieldTile = '#';
            //Ritar planen efter längd samt valt tecken
            string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));

            //Racket
            //Längd på spelarrack
            const int racketLength = fieldWidth / 4;
            //Tecken som representerar spelarracket
            const char racketTile = '|';
            //Startar med racket i mitten
            int leftRacketHeight = 5;
            //Startar med racket i mitten
            int rightRacketHeight = 5;

            //Bollens rörelse och tecken
            //Tecken som representerar bollen
            const char ballTile = 'O';
            //X och Y-koordinat för bollen
            int ballX = fieldLength / 2;
            int ballY = fieldWidth / 2;

            //Rörelseriktning för bollen (nedåt)
            bool isBallGoingDown = true;
            //Rörelseriktning för bollen (höger) (false-vänster)
            bool isBallGoingRight = true;

            //Scoreboard
            //Poäng för vänster spelare
            int leftPlayerPoints = 0;
            //Poäng för höger spelare
            int rightPlayerPoints = 0;

            //X-koordinat för poängtavlan
            int scoreboardX = fieldLength / 2 - 12;
            //Y-koordinat för poängtavlan
            int scoreboardY = fieldWidth + 3;

            // Visa scoreboard från start
            Console.SetCursorPosition(scoreboardX, scoreboardY);
            Console.WriteLine($"{player1} - {leftPlayerPoints} | {rightPlayerPoints} - {player2}");


            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(line);

                Console.SetCursorPosition(0, fieldWidth);
                Console.WriteLine(line);

                //Rita ut vänster och höger racket
                for (int i = 0; i < racketLength; i++)
                {
                    Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                    Console.WriteLine(racketTile);
                    Console.SetCursorPosition(fieldLength - 1, i + 1 + rightRacketHeight);
                    Console.WriteLine(racketTile);
                }

                //Hantera bollens rörelse
                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(ballTile);
                    //Hastighet på bollen
                    Thread.Sleep(100);

                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(' ');

                    //Hur bollen ska studsa vid kontakt i vägg
                    if (isBallGoingDown)
                    {
                        ballY++;
                    }
                    else
                    {
                        ballY--;
                    }

                    //Hur bollen ska studsa vid kontakt i racket
                    if (isBallGoingRight)
                    {
                        ballX++;
                    }
                    else
                    {
                        ballX--;
                    }

                    //Ändra rörelseriktning om bollen når översta eller nedersta kanten av fältet
                    if (ballY == 1 || ballY == fieldWidth - 1)
                    {
                        isBallGoingDown = !isBallGoingDown;
                    }

                    //Hantera träff med vänster racket
                    if (ballX == 1)
                    {
                        if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + racketLength)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        //Ger poäng om racket inte är på rätt plats
                        else
                        {
                            //Vänster spelare får poäng och bollen återställs till mitten
                            rightPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{player1} - {leftPlayerPoints} | {rightPlayerPoints} - {player2}");

                            //Gå till resultatet om höger spelare når 5 poäng
                            if (rightPlayerPoints == 5)
                            {
                                goto outer;
                            }
                        }
                    }

                    //Hantera träff med höger racket
                    if (ballX == fieldLength - 2)
                    {
                        if (ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + racketLength)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        //Ger poäng om racket inte är på rätt plats
                        else
                        {
                            //Vänster spelare får poäng och bollen återställs till mitten
                            leftPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{player1} - {leftPlayerPoints} | {rightPlayerPoints} - {player2}");

                            //Gå till resultatet om vänster spelare når 5 poäng
                            if (leftPlayerPoints == 5)
                            {
                                goto outer;
                            }
                        }
                    }
                }

                //Hantera rörelser för racketarna baserat på användarens tangenttryckningar
                switch (Console.ReadKey().Key)
                {
                    //Uppåtpil
                    case ConsoleKey.UpArrow:
                        if (rightRacketHeight > 0)
                        {
                            rightRacketHeight--;
                        }
                        break;
                    //Neråtpil
                    case ConsoleKey.DownArrow:
                        if (rightRacketHeight < fieldWidth - racketLength - 1)
                        {
                            rightRacketHeight++;
                        }
                        break;
                    //W ger upp
                    case ConsoleKey.W:
                        if (leftRacketHeight > 0)
                        {
                            leftRacketHeight--;
                        }
                        break;
                    //S ger neråt
                    case ConsoleKey.S:
                        if (leftRacketHeight < fieldWidth - racketLength - 1)
                        {
                            leftRacketHeight++;
                        }
                        break;
                }

                //Uppdatera bollen, inte blir en skugga efter
                for (int i = 1; i < fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(' ');
                    Console.SetCursorPosition(fieldLength - 1, i);
                    Console.WriteLine(' ');
                }
            }
            //När en spelare når 5poäng hoppa hit
            outer:;

            //Visa vinnaren och uppdatera spelarnas poäng
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            //Skriver ut vem som vinner
            if (rightPlayerPoints == 5)
            {
                Console.WriteLine($"\nGrattis {player2}!" );
                Console.WriteLine($"\nNi vann med " + rightPlayerPoints+ " / " + leftPlayerPoints);
            }
            else
            {
                Console.WriteLine($"\nGrattis {player1}!" );
                Console.WriteLine($"\nNi vann med " + leftPlayerPoints+ " / " + rightPlayerPoints);
            }

            //Listar ut vem som vann
            string? winner = rightPlayerPoints == 5 ? player2 : player1;
            string? loser = rightPlayerPoints == 5 ? player1 : player2;

            //Uppdatera spelarnas poäng
            Players.UpdatePlayerScore(winner, true);
            Players.UpdatePlayerScore(loser, false);
            
            //Vänta på tangenttryckning och gå tillbaka till spelmenyn
            Console.WriteLine("\nTryck på en tangent för att gå tillbaka till spelmenyn...");
            Console.ReadKey();
            Pong.Start();
        }
    }
}
