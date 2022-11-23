using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace overwrite_text
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Választás:1");
            Console.WriteLine("Választás:2");
            Console.WriteLine("Választás:3");
            arrowNav();
        }
        static void arrowNav()
        {
            int i = 0;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, i);
            Console.Write("Valasztas:{0}",i+1);
            while (true)
            {
                
                Console.SetCursorPosition(0, i);
                
                var valasztas = Console.ReadKey(false).Key;
                switch (valasztas)
                {
                    case ConsoleKey.DownArrow:
                        if (i != 2)
                        {
                            i++;
                        }
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(0, i);
                        Console.Write("Valasztas:{0}", i + 1);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(0, i-1);
                        Console.Write("Valasztas:{0}", i);

                        break;
                    case ConsoleKey.UpArrow:
                        if (i != 0)
                        {
                            i--;
                        }
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(0, i);
                        Console.Write("Valasztas:{0}", i + 1);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(0, i+1);
                        Console.Write("Valasztas:{0}", i);

                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(0, 4);
                Console.WriteLine(i);
                
            }
            
        }
    }
}
