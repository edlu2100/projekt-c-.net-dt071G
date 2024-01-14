// PongRules.cs

using System;

namespace projekt_csharp_net_dt071G
{
    public static class PongRules
    {
        //Skriver ut regler för pong
        public static void PrintPongRules()
        {
            Console.WriteLine("Spelets Mål:");
            Console.WriteLine("Målet med Pong är att få fler poäng än motståndaren genom att skjuta bollen förbi deras racket.");

            Console.WriteLine("\nSpelets Element:");
            Console.WriteLine("Boll: En liten boll som studsar fram och tillbaka på skärmen.");
            Console.WriteLine("Racket: Två vertikala paddlar (representeras av symbolen | )");

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
        }
    }
}
