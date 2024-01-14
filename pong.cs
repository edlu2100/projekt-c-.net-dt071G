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
        //Startar Pong-spelet
        public static void Start()
        {
            Console.Clear();
            
            //Visar huvudmenyn från GameMenu
            GameMenu.DisplayMenu();
            
            //Läser in användarens tangenttryckning för menyalternativ
            char option = Console.ReadKey(true).KeyChar;
            
            //Hanterar användarens val från menyn med hjälp av GameMenu.cs
            GameMenu.HandleMenuOption(option);
        }
    }
}
