using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menü_fr
{
	class Program
	{
		static void Menu()
		{
			Console.Clear();
			Console.WriteLine("SZia üdvözöllek a programban!");
			Console.WriteLine("Válasszon: \na.) Elemi Progtételek\nb.) Összett progtételek\nc.) Rendezések\nd.) Keresések");
			Console.Write("[a,b,c,d]: ");
			string valasztas = Console.ReadLine().Trim(' ');
			switch(valasztas)
			{
				case "a":
					ElemiAlmenu();
					break;
				case "b":
					break;
				case "c":
					break;
				case "d":
					break;
				default:
					Console.WriteLine("Invalid input");
					System.Threading.Thread.Sleep(1000);
					Menu();
					break;
			}
		}

		static void ElemiAlmenu()
		{
			Console.Clear();
			Console.WriteLine("Elemi programozásételek");
			Console.WriteLine("a.) Maximumkiválasztás\nb.) Megszámolás");
			Console.Write("[a,b]: ");
			string almenu = Console.ReadLine().Trim(' ');

			if(almenu == "a")
			{

			}
			else if(almenu == "b")
			{

			}
			else if(almenu == "x")
			{
				Exit();
			}
			else if(almenu == "e")
			{
				Menu();
			}
			else
			{
				ElemiAlmenu();
			}
		}

		static void Exit()
		{
			Console.Clear();
			Console.WriteLine("Szia!");
			System.Threading.Thread.Sleep(1000);
			System.Environment.Exit(0);
		}


		static void Main(string[] args)
		{
			 Menu();
			
			
		}
	}
}
