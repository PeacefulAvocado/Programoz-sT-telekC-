using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace menü_fr
{
    class Program
    {
        static string[,] data =
            {
            { "Bemenet: \n\tN:Egész, X:Tömb [1..N:H], H rendezett elemtípus (∃<,<= rendezési relációk)\nKimenet: \n\tMAX: Egész\nElőfeltétel: \n\tN>=1\nUtófeltétel: \n\t1≤MAX≤N és ∀i(1≤i≤N): X[i]≤X[MAX]", "Maximumkiválasztás (N, X, MAX):\n\tMAX:= 1\n\tCiklus I = 2 - től N - ig\n\t\tHa X[MAX] < X[I] akkor\n\t\t\tMAX:= I\n\tCiklus vége\nEljárás vége." },//maxkivalasztas| specifikacio,algoritmus 
			{"Bemenet: \n\tN: Egész, X: Tömb [1..N:H], T:H→Logikai\nKimenet: \n\tDB: Egész\nElőfeltétel: \n\tN>=0\nUtófeltétel: \n\tDB = SZUM(i = 1..N; T(X[i])) 1","Megszámolás (N,X,DB):\n\tDB:= 0\n\tCiklus I = 1 - től N - ig\n\t\tHa T(X[I]) akkor \n\t\t\tDB:= DB + 1\n\tCiklus vége\nEljárás vége" },//megszamolas | specifikacio,algoritmus
			{"Bemenet: \n\tN, M: Egész, X: Tömb[1..N:Valami], Y: Tömb[1..M:Valami]\nKimenet: \n\tDb: Egész, Z: Tömb[1..min(N, M):Valami]\nElőfeltétel: \n\tN>=0 és M≥0 és HalmazE(X) és HalmazE(Y)\nUtófeltétel:\n\tDb = SZUM(i = 1..N; X[i]∈Y) 1  és\n∀i(1≤i≤Db): (Z[i]∈X és Z[i]∈Y)  és\nHalmazE(Z)","Metszet(N,X,M,Y,Db,Z):\n\t[kiválogatás:]\n\tDb:= 0\n\tCiklus I = 1 - től N - ig\n\t\t[eldöntés:]\n\t\tJ:= 1\n\t\tCiklus amíg J≤M és X[I]≠Y[J]\n\t\t\tJ:= J + 1\n\t\tCiklus vége\n\t\tHa J≤M akkor \n\t\t\tDb:= Db + 1; Z[Db]:= X[I]\n\tCiklus vége\nEljárás vége." }, //metszet 
			{"Bemenet:\n\tN: Egész, X: Tömb[1..N:Valami]\nKimenet:\n\tDb: Egész, Y: Tömb[1..N:Egész]\nElőfeltétel:\n\tN>=0\nUtófeltétel:\n\tDb = SZUM(i = 1..N; T(X[i])) 1  és\n\t∀i(1≤i≤Db): T(X[Y[i]])  és\n\tY⊆(1, 2, ...,N)","Kiválogatás(N,X,Db,Y):\n\tDb:= 0\n\tCiklus I = 1 - től N - ig\n\t\tHa T(X[I]) akkor \n\t\t\tDb:= Db + 1; Y[Db]:= I\n\tCiklus vége\nEljárás vége." }, // kivalogatas
			{"Bemenet:\n\tN: Egész, X: Tömb[1..N:Valami]\nKimenet:\n\tX: Tömb[1..N:Valami]\nElőfeltétel:\n\tN>=0 és RendezettHalmazE(Valami)\nUtófeltétel:\n\tRendezettE(X’) és X’∈Permutáció(X)","Rendezés(N,X):\n\tCiklus I = 2 - től N - ig\n\t\tJ:= I - 1; Y:= X[I]\n\t\tCiklus amíg J > 0 és X[J]> Y\n\t\t\tX[J + 1]:= X[J]; J:= J - 1\n\t\tCiklus vége\n\t\tX[J + 1]:= Y\n\tCiklus vége\nEljárás vége." }, //beillesztésesrendezés
			{"Bemenet:\n\tN: Egész, X: Tömb[1..N:Valami]\nKimenet:\n\tX: Tömb[1..N:Valami]\nElőfeltétel:\n\tN>=0 és RendezettHalmazE(Valami)\nUtófeltétel:\n\tRendezettE(X’) és X’∈Permutáció(X)","Rendezés(N,X):\n\tCiklus I = 1 - től N - 1 - ig\n\t\tCiklus J = I + 1 - től N - ig\n\t\t\tHa X[I] > X[J] akkor \n\t\t\t\tCsere(X[I], X[J])\n\t\tCiklus vége\n\tCiklus vége\nEljárás vége." },//egyszerücserésrendezés
			{"Bemenet:\n\tN: Egész, X: Tömb[1..N:Valami], Y: Valami,\n\tT: Valami→Logikai\nKimenet:\n\tVAN: Logikai, SORSZ: Egész\nElőfeltétel:\n\tN>=0 és RendezettE(X)\nUtófeltétel:\n\tVAN ≡ (∃i(1≤i≤N): X[i] = Y) és\n\tVAN → 1≤SORSZ≤N és X[SORSZ] = Y és\n\t∀i(1≤i < SORSZ): X[i] < Y","Keresés(N,X,Y,VAN,SORSZ):\n\tI:= 1\n\tCiklus amíg I≤N és X[I] < Y\n\t\tI:= I + 1\n\tCiklus vége\n\tVAN:= (I≤N) és X[I] = Y\n\tHa VAN akkor \n\t\tSORSZ:= I\nEljárás vége." },//Lineáris keresés rendezett halmazban
			{"Bemenet:\n\tN: Egész, X: Tömb[1..N:Valami], Y: Valami,\n\tT: Valami→Logikai\nKimenet:\n\tVAN: Logikai, SORSZ: Egész\nElőfeltétel:\n\tN>=0 és RendezettE(X)\nUtófeltétel:\n\tVAN ≡ (∃i(1≤i≤N): X[i] = Y) és\n\tVAN → 1≤SORSZ≤N és X[SORSZ] = Y és\n\t∀i(1≤i < SORSZ):X[i]≤Y és ∀i(SORSZ < i≤N): X[i]≥Y","Keresés(N,X,Y,VAN,SORSZ):\n\tE:= 1; U:= N\n\tCiklus\n\t\tK:=[(E + U) / 2]  (E + U felének egész értéke)\n\t\tElágazás\n\t\t\tY < X[K] esetén U:= K - 1\n\t\t\tY > X[K] esetén E:= K + 1\n\t\tElágazás vége\n\tamíg E≤U és X[K] ? Y\n\tCiklus vége\n\tVAN:= (E≤U)\n\tHa VAN akkor SORSZ:= K\nEljárás vége." } // bináris keresés (logaritmikus)
			}; //data[x,y] x=tételek specifikációja, y=algoritmusa
        static void printData(string a)
        {
            if (a == "m")//m = menu
            {
                Console.WriteLine("1] Specifikáció");
                Console.WriteLine("2] Algoritmus/Pszeudo kód");
                Console.WriteLine("3] Mintaprogram");
                Console.WriteLine("4] Vissza");
                Console.WriteLine("5] Kilépés");
            }
            if (a == "n")//n = minta
            {
                Console.WriteLine("1] Adatok beírásból");
                Console.WriteLine("2] Adatok fileból");
                Console.WriteLine("3] Vissza");
                Console.WriteLine("4] Kilépés");
            }
        }
        static void LaunchArguments()
        {
            Console.SetWindowSize(128, 32);
            Console.Title = "Programozástételek";
            UTF8Encoding uTF8 = new UTF8Encoding();
        }

        static void arrowNav(Action[] methods_in)
        {
            int valasztott = 1;
            
            while (true)
            {
                var valasztas = Console.ReadKey(false).Key;
                switch (valasztas)
                {
                    case ConsoleKey.UpArrow:
                        if (valasztott > 1)
                        {
                            valasztott -= 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (valasztott < methods_in.Length)
                        {
                            valasztott += 1;
                        }
                        break;
                    case ConsoleKey.Escape:
                        Exit();
                        break;
                    case ConsoleKey.Enter:
                        methods_in[valasztott - 1]();
                        break;
                    default:
                        break;
                }
            }
        }
		static void Menu()
		{
			Console.Clear();
			Console.WriteLine("SZia üdvözöllek a programban!");
			Console.WriteLine("Menü \n1] Elemi Progtételek\n2] Összett progtételek\n3] Rendezések\n4] Keresések\n5] Kilépés");
            Action[] funcs = { ElemiAlmenu, OsszetettAlmenu, RendezesekAlmenu, KeresesekAlmenu,Exit };
            arrowNav(funcs);
            
		}
		static void InvalidInput()
		{
			Console.WriteLine("Invalid input");
			System.Threading.Thread.Sleep(1000);

		}
		static void ElemiAlmenu()
		{
			Console.Clear();
			Console.WriteLine("Elemi programozásételek");
			Console.WriteLine("1] Maximumkiválasztás\n2] Megszámolás\n3] Vissza\n4] Kilépés");
            Action[] funcs = { Maximumkivalasztas, Megszamolas, Menu, Exit };
            arrowNav(funcs);
        }
		static void Maximumkivalasztas()
		{
			Console.Clear();
			Console.WriteLine("Maximumkiválasztás");
            printData("m");
			string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[0, 0]);
					Console.ReadKey();
					Maximumkivalasztas();
					break;
				case "2":
					Console.WriteLine(data[0, 1]);
					Console.ReadKey();
					Maximumkivalasztas();
					break;
				case "3":
					MaxMinta();
					break;
				case "4":
					ElemiAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Maximumkivalasztas();
					break;
			}
		}
		static void MaxMinta()
		{
			Console.Clear();
			Console.WriteLine("Maximumkiválasztás mintaprogram");
            printData("n");
            string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine("Írjon be számokat és a program megkeresi a legnagyobbat(számokat szóközzel válassza el)");
					string[] tomb = Console.ReadLine().Split(' ');
					int[] X = new int[tomb.Length];
					for(int i = 0; i < tomb.Length; i++)
					{
						X[i] = Convert.ToInt32(tomb[i]);
					}
					Console.WriteLine("A legnagyobb elem: {0}", FuncMaxKiv(X,X.Length));
					Console.ReadKey();
					MaxMinta();
					break;
				case "2":
					Console.WriteLine("Adja meg az adathalmaz elérési útvonalát!");
					string url = Console.ReadLine();
					StreamReader file = new StreamReader(url);
					string[] tomb_fromfile = file.ReadToEnd().Split(new char[] {' ','\n' },StringSplitOptions.RemoveEmptyEntries);
					int[] X_fromfile = new int[tomb_fromfile.Length];
					for(int i = 0; i < tomb_fromfile.Length; i++)
					{
						X_fromfile[i] = Convert.ToInt32(tomb_fromfile[i]);
					}
					Console.WriteLine("A legnagyobb elem: {0}", FuncMaxKiv(X_fromfile, X_fromfile.Length));
					file.Close();
					Console.ReadKey();
					MaxMinta();
					break;
				case "3":
					Maximumkivalasztas();
					break;
				case "4":
					Exit();
					break;
				default:
					InvalidInput();
					MaxMinta();
					break;
			}
		}
		static int FuncMaxKiv(int[] X, int N)
		{
			int MAX = 0;
			for(int I = 1; I < N; I++)
			{
				if(X[MAX] < X[I])
				{
					MAX = I;
				}
			}
			return X[MAX];
		}
		static void Megszamolas() {
			Console.Clear();
			Console.WriteLine("Megszámolás");
            printData("m");
            string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[1,0]);
					Console.ReadKey();
					Megszamolas();
					break;
				case "2":
					Console.WriteLine(data[1,1]);
					Console.ReadKey();
					Megszamolas();
					break;
				case "3":
					MegszamolMinta();
					break;
				case "4":
					ElemiAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Megszamolas();
					break;
			}
		}
		static void MegszamolMinta() {
			Console.Clear();
			Console.WriteLine("Megszámolás mintaprogram");
            printData("n");
			string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine("Írjon be adatokat (szóközökkel elválasztva)");
					string[] X = Console.ReadLine().Split(' ');
					Console.WriteLine("Íjron be egy adatot, a program megszámolja, hogy hányszor szerepel az előbbi adathalmazban");
					string keresett = Console.ReadLine();
					
					Console.WriteLine("A keresett elem ennyiszer szerepel: {0}", FuncMegszamol(X, X.Length, keresett));
					Console.ReadKey();
					MaxMinta();
					break;
				case "2":
					Console.WriteLine("Adja meg az adathalmaz elérési útvonalát!");
					string url = Console.ReadLine();
					StreamReader file = new StreamReader(url);
					string[] tomb_fromfile = file.ReadToEnd().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					Console.WriteLine("Íjron be egy adatot, a program megszámolja, hogy hányszor szerepel az előbbi adathalmazban");
					string file_keresett = Console.ReadLine();
					Console.WriteLine("A keresett elem ennyiszer szerepel: {0}", FuncMegszamol(tomb_fromfile, tomb_fromfile.Length, file_keresett));
					file.Close();
					Console.ReadKey();
					MegszamolMinta();
					break;
				case "3":
					Maximumkivalasztas();
					break;
				case "4":
					Exit();
					break;
				default:
					InvalidInput();
					MaxMinta();
					break;
			}

		}
		static int FuncMegszamol(string[] X, int N, string keresett) {
			int DB = 0;
			for(int i = 0; i < N; i++)
			{
				if(X[i] == keresett)
				{
					DB++;
				}
			}
			return DB;
		}
		static void OsszetettAlmenu()
		{
			Console.Clear();
			Console.WriteLine("Összetett programozásételek");
			Console.WriteLine("1] Metszet\n2] Kiválogatás\n3] Vissza\n4] Kilépés");
            Action[] funcs = { Metszet,Kivalogatas,Menu,Exit};
            arrowNav(funcs);
        }
		static void Metszet() {
			Console.Clear();
			Console.WriteLine("Metszet");
            printData("m");
            string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[2,0]);
					Console.ReadKey();
					Metszet();
					break;
				case "2":
					Console.WriteLine(data[2,1]);
					Console.ReadKey();
					Metszet();
					break;
				case "3":
					MetszetMinta();
					break;
				case "4":
					OsszetettAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Metszet();
					break;
			}
		}
		static void MetszetMinta()
		{
			Console.Clear();
			Console.WriteLine("Metszet mintaprogram");
			Console.WriteLine("1] Adatok beírásból");
			Console.WriteLine("2] Adatok fileból");
			Console.WriteLine("3] Vissza");
			Console.WriteLine("4] Kilépés");
			string tovabb = Console.ReadLine().Trim(' ');
			switch (tovabb)
			{
				case "1":
					Console.WriteLine("Írja be az egyik tömb elemeit (szóközökkel elválasztva)");
					string[] X_beker = Console.ReadLine().Split(' ');
					int[] X = new int[X_beker.Length];
					for (int i = 0; i < X_beker.Length; i++)
					{
						X[i] = Convert.ToInt32(X_beker[i]);
					}
					//
					Console.WriteLine("Írja be a másik tömb elemeit (szóközökkel elválasztva)");
					string[] Y_beker = Console.ReadLine().Split(' ');
					int[] Y = new int[Y_beker.Length];
					for (int i = 0; i < Y_beker.Length; i++)
					{
						Y[i] = Convert.ToInt32(Y_beker[i]);
					}
					//
					FuncMetszet(X.Length, X, Y.Length, Y);
					Console.ReadKey();
					MetszetMinta();
					break;
				case "2":
					Console.WriteLine("Adja meg az adathalmaz elérési útvonalát! (két halmaz elemei két sorban, szóközzel elválasztva)");
					string url = Console.ReadLine();
					StreamReader file = new StreamReader(url);
					string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					int[] X_fromfile = new int[tomb_fromfileX.Length];
					for (int i = 0; i < tomb_fromfileX.Length; i++)
					{
						X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
					}

					//
					string[] tomb_fromfileY = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					int[] Y_fromfile = new int[tomb_fromfileY.Length];
					for (int i = 0; i < tomb_fromfileY.Length; i++)
					{
						Y_fromfile[i] = Convert.ToInt32(tomb_fromfileY[i]);
					}
					//
					FuncMetszet(X_fromfile.Length, X_fromfile, Y_fromfile.Length, Y_fromfile);
					file.Close();
					Console.ReadKey();
					MetszetMinta();
					break;
				case "3":
					Metszet();
					break;
				case "4":
					Exit();
					break;
				default:
					InvalidInput();
					MetszetMinta();
					break;
			}
		}
		static void FuncMetszet(int N, int[] X, int M, int[] Y)
		{
			int db = 0;
			int[] Z = new int[N];
			for (int i = 0; i < N; i++)
			{
				int j = 0;
				while (j < M && X[i] != Y[j])
				{
					j++;

				}
				if (j < M)
				{
					Z[db] = X[i];
					db++;
				}
			}
			Console.WriteLine("A metszet elemei:");
			for (int i = 0; i < db; i++)
			{
				Console.Write("{0}, ", Z[i]);
			}
			Console.WriteLine();
		}

		static void Kivalogatas() {
			Console.Clear();
			Console.WriteLine("Kiválogatás");
            printData("m");
            string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[3,0]);
					Console.ReadKey();
					Kivalogatas();
					break;
				case "2":
					Console.WriteLine(data[3,1]);
					Console.ReadKey();
					Kivalogatas();
					break;
				case "3":
					KivalogatasMinta();
					break;
				case "4":
					OsszetettAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Kivalogatas();
					break;
			}
		}
		static void KivalogatasMinta()
		{
			Console.Clear();
			Console.WriteLine("Kiválogatás mintaprogram");
			Console.WriteLine("1] Adatok beírásból");
			Console.WriteLine("2] Adatok fileból");
			Console.WriteLine("3] Vissza");
			Console.WriteLine("4] Kilépés");
			string tovabb = Console.ReadLine().Trim(' ');
			switch (tovabb)
			{
				case "1":
					Console.WriteLine("Írjon be számokat (szóközökkel elválasztva), a program kiválogatja a 10-nél nagyobbakat");
					string[] X_beker = Console.ReadLine().Split(' ');
					int[] X = new int[X_beker.Length];
					for (int i = 0; i < X_beker.Length; i++)
					{
						X[i] = Convert.ToInt32(X_beker[i]);
					}
					//
					FuncKivalogatas(X.Length, X);
					Console.ReadKey();
					MetszetMinta();
					break;
				case "2":
					Console.WriteLine("Adja meg az adathalmaz elérési útvonalát, a program kiválogatja a 10-nél nagyobbakat! (számok szóközökkel elválasztva)");
					string url = Console.ReadLine();
					StreamReader file = new StreamReader(url);
					string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					int[] X_fromfile = new int[tomb_fromfileX.Length];
					for (int i = 0; i < tomb_fromfileX.Length; i++)
					{
						X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
					}

					//
					FuncKivalogatas(X_fromfile.Length, X_fromfile);
					file.Close();
					Console.ReadKey();
					KivalogatasMinta();
					break;
				case "3":
					Kivalogatas();
					break;
				case "4":
					Exit();
					break;
				default:
					InvalidInput();
					KivalogatasMinta();
					break;
			}
		}
		static void FuncKivalogatas(int N, int[] X)
		{
			int db = 0;
			int[] Y = new int[N];
			for (int i = 0; i < N; i++)
			{
				if (X[i] > 10)
				{
					Y[db] = X[i];
					db++;
				}
			}
			Console.WriteLine("10-nél nagyobb elemek: ");
			for (int i = 0; i < db; i++)
			{
				Console.Write("{0}, ", Y[i]);
			}
			Console.WriteLine();

		}
		static void RendezesekAlmenu()
		{
			Console.Clear();
			Console.WriteLine("Rendezések");
			Console.WriteLine("1] Egyszerű cserés rendezés\n2] Javított Beillesztéses rendezés\n3] Vissza\n4] Kilépés");
            Action[] funcs = {Egyszerucseres,Beillesztesesrendezes,Menu,Exit };
            arrowNav(funcs);
        }
		static void Egyszerucseres() {
			Console.Clear();
			Console.WriteLine("Egyszerű cserés rendezés");
            printData("m");
            string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[5,0]);
					Console.ReadKey();
					Egyszerucseres();
					break;
				case "2":
					Console.WriteLine(data[5,1]);
					Console.ReadKey();
					Egyszerucseres();
					break;
				case "3":
					EgyszerucseresMinta();
					break;
				case "4":
					RendezesekAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Egyszerucseres();
					break;
			}
		}
		static void EgyszerucseresMinta()
		{
			Console.Clear();
			Console.WriteLine("Egyszerű cserés rendezés mintaprogram");
			Console.WriteLine("1] Adatok beírásból");
			Console.WriteLine("2] Adatok fileból");
			Console.WriteLine("3] Vissza");
			Console.WriteLine("4] Kilépés");
			string tovabb = Console.ReadLine().Trim(' ');
			switch (tovabb)
			{
				case "1":
					Console.WriteLine("Írjon be számokat (szóközökkel elválasztva), a program rendezi ezeket");
					string[] X_beker = Console.ReadLine().Split(' ');
					int[] X = new int[X_beker.Length];
					for (int i = 0; i < X_beker.Length; i++)
					{
						X[i] = Convert.ToInt32(X_beker[i]);
					}
					//
					FuncEgyszerucseres(X.Length, X);
					Console.ReadKey();
					MetszetMinta();
					break;
				case "2":
					Console.WriteLine("Adja meg az adathalmaz elérési útvonalát, a program kiválogatja a 10-nél nagyobbakat! (számok szóközökkel elválasztva)");
					string url = Console.ReadLine();
					StreamReader file = new StreamReader(url);
					string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					int[] X_fromfile = new int[tomb_fromfileX.Length];
					for (int i = 0; i < tomb_fromfileX.Length; i++)
					{
						X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
					}

					//
					FuncEgyszerucseres(X_fromfile.Length, X_fromfile);
					file.Close();
					Console.ReadKey();
					EgyszerucseresMinta();
					break;
				case "3":
					Egyszerucseres();
					break;
				case "4":
					Exit();
					break;
				default:
					InvalidInput();
					EgyszerucseresMinta();
					break;
			}

		}
		static void FuncEgyszerucseres(int N, int[] X)
		{
			for (int i = 0; i < N - 1; i++)
			{
				for (int j = i + 1; j < N; j++)
				{
					if (X[i] > X[j])
					{
						int cs = X[i];
						X[i] = X[j];
						X[j] = cs;
					}
				}
			}
			Console.WriteLine("A rendezett számsor: ");
			for (int i = 0; i < N; i++)
			{
				Console.Write("{0}, ", X[i]);
			}
			Console.WriteLine();
		}

		static void Beillesztesesrendezes() {
			Console.Clear();
			Console.WriteLine("Javított beillesztéses rendezés");
            printData("m");
            string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[4,0]);
					Console.ReadKey();
					Beillesztesesrendezes();
					break;
				case "2":
					Console.WriteLine(data[4,1]);
					Console.ReadKey();
					Beillesztesesrendezes();
					break;
				case "3":
					BeillesztesesrendezesMinta();
					break;
				case "4":
					RendezesekAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Beillesztesesrendezes();
					break;
			}
		}
		static void BeillesztesesrendezesMinta()
		{
			Console.Clear();
			Console.WriteLine("javított beillesztéses rendezés mintaprogram");
			Console.WriteLine("1] Adatok beírásból");
			Console.WriteLine("2] Adatok fileból");
			Console.WriteLine("3] Vissza");
			Console.WriteLine("4] Kilépés");
			string tovabb = Console.ReadLine().Trim(' ');
			switch (tovabb)
			{
				case "1":
					Console.WriteLine("Írjon be számokat (szóközökkel elválasztva), a program rendezi ezeket");
					string[] X_beker = Console.ReadLine().Split(' ');
					int[] X = new int[X_beker.Length];
					for (int i = 0; i < X_beker.Length; i++)
					{
						X[i] = Convert.ToInt32(X_beker[i]);
					}
					//
					FuncBeillesztesesrendezes(X.Length, X);
					Console.ReadKey();
					BeillesztesesrendezesMinta();
					break;
				case "2":
					Console.WriteLine("Adja meg az adathalmaz elérési útvonalát, a program kiválogatja a 10-nél nagyobbakat! (számok szóközökkel elválasztva)");
					string url = Console.ReadLine();
					StreamReader file = new StreamReader(url);
					string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					int[] X_fromfile = new int[tomb_fromfileX.Length];
					for (int i = 0; i < tomb_fromfileX.Length; i++)
					{
						X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
					}

					//
					FuncBeillesztesesrendezes(X_fromfile.Length, X_fromfile);
					file.Close();
					Console.ReadKey();
					BeillesztesesrendezesMinta();
					break;
				case "3":
					Beillesztesesrendezes();
					break;
				case "4":
					Exit();
					break;
				default:
					InvalidInput();
					BeillesztesesrendezesMinta();
					break;
			}

		}
		static void FuncBeillesztesesrendezes(int N, int[] X)
		{
			for (int i = 1; i < N; i++)
			{
				int j = i - 1;
				int y = X[i];
				while (j > 0 && X[j] > y)
				{
					X[j + 1] = X[j];
					j--;
				}
				X[j + 1] = y;

			}
			Console.WriteLine("A rendezett számsor: ");
			for (int i = 0; i < N; i++)
			{
				Console.Write("{0}, ", X[i]);
			}
			Console.WriteLine();
		}
		static void KeresesekAlmenu()
		{
			Console.Clear();
			Console.WriteLine("Keresések");
			Console.WriteLine("1] Lineáris keresés rendezett adathalmazban\n2] Bináris(Logaritmikus) keresés\n3] Vissza\n4] Kilépés");
            Action[] funcs = { Lineariskereses,Binariskereses,Menu,Exit};
            arrowNav(funcs);
        }
		static void Lineariskereses() {
			Console.Clear();
			Console.WriteLine("Lineáris keresés rendezett sorozatban");
            printData("m");
			string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[6,0]);
					Console.ReadKey();
					Lineariskereses();
					break;
				case "2":
					Console.WriteLine(data[6,1]);
					Console.ReadKey();
					Lineariskereses();
					break;
				case "3":
					LineariskeresesMinta();
					break;
				case "4":
					KeresesekAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Lineariskereses();
					break;
			}
		}
		static void LineariskeresesMinta()
		{
			Console.Clear();
			Console.WriteLine("Lineáris keresés mintaprogram");
			Console.WriteLine("1] Adatok beírásból");
			Console.WriteLine("2] Adatok fileból");
			Console.WriteLine("3] Vissza");
			Console.WriteLine("4] Kilépés");
			string tovabb = Console.ReadLine().Trim(' ');
			switch (tovabb)
			{
				case "1":
					Console.WriteLine("Írjon be számokat sorozatban (szóközökkel elválasztva)!");
					string[] X_beker = Console.ReadLine().Split(' ');
					int[] X = new int[X_beker.Length];
					for (int i = 0; i < X_beker.Length; i++)
					{
						X[i] = Convert.ToInt32(X_beker[i]);
					}
					Console.WriteLine("Írjon be egy számot, a program megkeresei a sorozatban");
					int keresett = Convert.ToInt32(Console.ReadLine());
					//
					FuncLineariskereses(X.Length, X, keresett);
					Console.ReadKey();
					LineariskeresesMinta();
					break;
				case "2":
				//nincs kész
				/*
				Console.WriteLine("Adja meg a rendezett adathalmaz elérési útvonalát!");
				string url = Console.ReadLine();
				StreamReader file = new StreamReader(url);
				string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				int[] X_fromfile = new int[tomb_fromfileX.Length];
				for(int i = 0; i < tomb_fromfileX.Length; i++) {
					X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
				}

				//
				//FuncLineariskereses(X_fromfile.Length, X_fromfile);
				file.Close();
				Console.ReadKey();
				LineariskeresesMinta();
				break;
				*/
				case "3":
					Lineariskereses();
					break;
				case "4":
					Exit();
					break;
				default:
					InvalidInput();
					LineariskeresesMinta();
					break;
			}
		}
		static void FuncLineariskereses(int N, int[] X, int Y)
		{
			int i = 0;
			int sorsz = -1;
			while (i < N && X[i] < Y)
			{
				i++;
			}
			bool van = (i <= N && X[i] == Y);
			if (van)
			{
				sorsz = i;
			}
			Console.WriteLine("A keresett elem a {0}. helyen van", sorsz + 1);
		}
		static void Binariskereses() {
			Console.Clear();
			Console.WriteLine("Bináris (logaritmikus) rendezés");
            printData("m");
            string tovabb = Console.ReadLine().Trim(' ');
			switch(tovabb)
			{
				case "1":
					Console.WriteLine(data[7,0]);
					Console.ReadKey();
					Binariskereses();
					break;
				case "2":
					Console.WriteLine(data[7,1]);
					Console.ReadKey();
					Binariskereses();
					break;
				case "3":
					break;
				case "4":
					RendezesekAlmenu();
					break;
				case "5":
					Exit();
					break;
				default:
					InvalidInput();
					Binariskereses();
					break;
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
			LaunchArguments();
			Menu();
			
			
		}
	}
}
