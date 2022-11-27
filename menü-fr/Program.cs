using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

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

        static string[] dataarray = { " ", "1] Specifikáció", "2] Algoritmus/Pszeudo kód", "3] Mintaprogram", "4] Vissza", "5] Kilépés" };
        static string[] dataarray_minta = { "", "1] Adatok beírásból", "2] Adatok fileból", "3] Vissza", "4] Kilépés" };
        static string[] specalg = { "Specifikáció: ", "Algoritmus: " };
        static string[] mintak = { "Maximumkiválasztás mintaprogram", "Megszámolás mintaprogram", "Metszet mintaprogram", "Kiválogatás mintaprogram", "Egyszerű cserés rendezés mintaprogram", "Javított beillesztéses rendezés mintaprogram", "Lineáris keresés mintaprogram", "Bináris (logaritmikus) keresés mintaprogram" };
        static string[] exitargs = { "exit", "vissza", "back", "x", "X", "Exit", "Vissza", "EXIT", "VISSZA", "Back", "BACK" };
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
        static void arrowNav(Action[] methods_in, string[] menudata)
        {
            Console.CursorVisible = false;
            int valasztott = 1;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, valasztott);
            Console.Write(menudata[valasztott]);

            while (true)
            {
                Console.SetCursorPosition(0, valasztott);
                var valasztas = Console.ReadKey(false).Key;

                switch (valasztas)
                {
                    case ConsoleKey.UpArrow:
                        if (valasztott > 1)
                        {
                            valasztott -= 1;
                        }

                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(0, valasztott);
                        Console.Write("{0}", menudata[valasztott]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(0, valasztott + 1);
                        Console.Write("{0}", menudata[valasztott + 1]);
                        break;

                    case ConsoleKey.DownArrow:
                        if (valasztott < menudata.Length - 1)
                        {
                            valasztott += 1;
                        }
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(0, valasztott);
                        Console.Write("{0}", menudata[valasztott]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(0, valasztott - 1);
                        Console.Write("{0}", menudata[valasztott - 1]);
                        break;
                    case ConsoleKey.Escape:
                        Console.ResetColor();
                        Exit();
                        break;
                    case ConsoleKey.Enter:
                        Console.ResetColor();
                        methods_in[valasztott - 1]();
                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(0, 4);


            }
        }
        static void Menu()
        {
            Console.Clear();

            Console.WriteLine("Menü");
            Console.WriteLine("1] Elemi programozástételek\n2] Összett programozástételek\n3] Rendezések\n4] Keresések\n5] Kilépés");
            string[] menudata = { "Menü", "1] Elemi Programozástételek", "2] Összett programozástételek", "3] Rendezések", "4] Keresések", "5] Kilépés" };
            Action[] funcs = { ElemiAlmenu, OsszetettAlmenu, RendezesekAlmenu, KeresesekAlmenu, Exit };
            arrowNav(funcs, menudata);

        }
        static void InvalidInput()
        {
            Console.WriteLine("A bemenő adatok formátuma nem megfelelő!");
            System.Threading.Thread.Sleep(1000);

        }
        static void InvalidURL(Action action)
        {
            Console.WriteLine("A megadott elérési útvonal nem megfelelő!");
            System.Threading.Thread.Sleep(1000);
            action();
        }
        static void FileInputUsermanagement(Action felsobbmenu,Action onmaga,string url)
        {
            if (exitargs.Contains(url))
            {
                felsobbmenu();
            }
            try
            {
                StreamReader streamReader_test = new StreamReader(url);
            }
            catch (Exception)
            {
                InvalidURL(onmaga);
                throw;
            }
        }
        static void ElemiAlmenu()
        {
            Console.Clear();
            Console.WriteLine("Elemi programozásételek");
            Console.WriteLine("1] Maximumkiválasztás\n2] Megszámolás\n3] Vissza\n4] Kilépés");
            string[] menudata = { "Elemi programozásételek", "1] Maximumkiválasztás", "2] Megszámolás", "3] Vissza", "4] Kilépés" };
            Action[] funcs = { Maximumkivalasztas, Megszamolas, Menu, Exit };
            arrowNav(funcs, menudata);
        }
        static void Maximumkivalasztas()
        {
            Console.Clear();
            string a = "Maximumkiválasztás";
            Console.WriteLine(a);
            printData("m");

            dataarray[0] = a;

            Action[] funcs = { Maxspec, Maxalg, MaxMinta, ElemiAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Maxspec() {
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[0, 0]);
            Console.ReadKey();
            Maximumkivalasztas();
        }
        static void Maxalg() {
            Console.Clear();
            Console.WriteLine(specalg[1]);
            Console.WriteLine(data[0, 1]);
            Console.ReadKey();
            Maximumkivalasztas();
        }
        static void MaxMinta()
        {
            Console.Clear();
            string a = mintak[0];
            Console.WriteLine(a);
            printData("n");

            dataarray_minta[0] = a;
            Action[] funcs = { Maxbeir, Maxfilebol, Maximumkivalasztas, Exit };
            arrowNav(funcs, dataarray_minta);
        }
        static void Maxbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mintak[0]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írjon be számokat (szóközzel elválasztva) és a program megkeresi a legnagyobbat");
            string[] tomb = Console.ReadLine().Split(' ');
            int[] X = new int[tomb.Length];
            try
            {
            for(int i = 0; i < tomb.Length; i++)
                {
                    X[i] = Convert.ToInt32(tomb[i]);
                }
            }
            catch (Exception)
            {
                InvalidInput();
                Maxbeir();
                throw;
            }
            FuncMaxKiv(X, X.Length);
            Console.ReadKey();
            MaxMinta();
        }
        static void Maxfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mintak[0]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg az adathalmaz elérési útvonalát!");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(MaxMinta, Maxfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfile = file.ReadToEnd().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] X_fromfile = new int[tomb_fromfile.Length];
            for(int i = 0; i < tomb_fromfile.Length; i++)
            {
                X_fromfile[i] = Convert.ToInt32(tomb_fromfile[i]);
            }
            FuncMaxKiv(X_fromfile, X_fromfile.Length);
            file.Close();
            Console.ReadKey();
            MaxMinta();
        }
        static void FuncMaxKiv(int[] X, int N)
        {
            int MAX = 0;
            for(int I = 1; I < N; I++)
            {
                if(X[MAX] < X[I])
                {
                    MAX = I;
                }
            }
            Console.WriteLine("A legnagyobb elem: {0}", X[MAX]);
        }
        static void Megszamolas() {
            Console.Clear();
            string a = "Megszámolás";
            Console.WriteLine(a);
            printData("m");


            dataarray[0] = a;
            Action[] funcs = { Megszspec, Megszalg, MegszamolMinta, ElemiAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Megszspec() {
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[1, 0]);
            Console.ReadKey();
            Megszamolas();
        }
        static void Megszalg() {
            Console.Clear();
            Console.WriteLine(specalg[1]);
            Console.WriteLine(data[1, 1]);
            Console.ReadKey();
            Megszamolas();
        }
        static void MegszamolMinta() {
            Console.Clear();
            string a = mintak[1];
            Console.WriteLine(a);
            printData("n");
            dataarray_minta[0] = a;
            Action[] funcs = { Megszamolbeir, Megszamolfilebol, Megszamolas, Exit };
            arrowNav(funcs, dataarray_minta);

        }
        static void Megszamolbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(mintak[1]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írjon be adatokat (szóközökkel elválasztva)");
            string[] X = Console.ReadLine().Split(' ');
            Console.WriteLine("Íjron be egy adatot, a program megszámolja, hogy hányszor szerepel az előbbi adathalmazban");
            string keresett = Console.ReadLine();
            FuncMegszamol(X, X.Length, keresett);
            Console.ReadKey();
            MaxMinta();
        }
        static void Megszamolfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(mintak[1]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg az adathalmaz elérési útvonalát!");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(MegszamolMinta, Megszamolfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfile = file.ReadToEnd().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Íjron be egy adatot, a program megszámolja, hogy hányszor szerepel az előbbi adathalmazban");
            string file_keresett = Console.ReadLine();
            FuncMegszamol(tomb_fromfile, tomb_fromfile.Length, file_keresett);
            file.Close();
            Console.ReadKey();
            MegszamolMinta();
        }
        static void FuncMegszamol(string[] X, int N, string keresett) {
            int DB = 0;
            for(int i = 0; i < N; i++)
            {
                if(X[i] == keresett)
                {
                    DB++;
                }
            }
            Console.WriteLine("A keresett elem ennyiszer szerepel: {0}", DB);
        }
        static void OsszetettAlmenu()
        {
            Console.Clear();

            Console.WriteLine("Összetett programozásételek");
            Console.WriteLine("1] Metszet\n2] Kiválogatás\n3] Vissza\n4] Kilépés");
            string[] menudata = { "Összetett programozásételek", "1] Metszet", "2] Kiválogatás", "3] Vissza", "4] Kilépés" };
            Action[] funcs = { Metszet, Kivalogatas, Menu, Exit };
            arrowNav(funcs, menudata);
        }
        static void Metszet() {
            Console.Clear();
            string a = "Metszet";
            Console.WriteLine(a);
            printData("m");

            dataarray[0] = a;
            Action[] funcs = { Metszspec, Metszalg, MetszetMinta, OsszetettAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Metszspec() {
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[2, 0]);
            Console.ReadKey();
            Metszet();
        }
        static void Metszalg() {
            Console.Clear();
            Console.WriteLine(specalg[1]);
            Console.WriteLine(data[2, 1]);
            Console.ReadKey();
            Metszet();
        }
        static void MetszetMinta()
        {
            Console.Clear();
            string a = mintak[2];
            Console.WriteLine(a);
            printData("n");
            dataarray_minta[0] = a;
            Action[] funcs = { Metszetbeir, Metszetfilebol, Metszet, Exit };
            arrowNav(funcs, dataarray_minta);
        }
        static void Metszetbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mintak[2]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írja be az egyik tömb elemeit (szóközökkel elválasztva)");
            string[] X_beker = Console.ReadLine().Split(' ');
            int[] X = new int[X_beker.Length];
            try
            {
                for (int i = 0; i < X_beker.Length; i++)
                {
                    X[i] = Convert.ToInt32(X_beker[i]);
                }
            }
            catch (Exception)
            {
                InvalidInput();
                Metszetbeir();
                throw;
            }
            
            Console.WriteLine("Írja be a másik tömb elemeit (szóközökkel elválasztva)");
            string[] Y_beker = Console.ReadLine().Split(' ');
            int[] Y = new int[Y_beker.Length];
            try
            {
                for (int i = 0; i < Y_beker.Length; i++)
                {
                    Y[i] = Convert.ToInt32(Y_beker[i]);
                }
            }
            catch (Exception)
            {
                InvalidInput();
                Metszetbeir();
                throw;
            }
            
            FuncMetszet(X.Length, X, Y.Length, Y);
            Console.ReadKey();
            MetszetMinta();
        }
        static void Metszetfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mintak[2]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg az adathalmaz elérési útvonalát! (két halmaz elemei két sorban, szóközzel elválasztva)");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(MetszetMinta, Metszetfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] X_fromfile = new int[tomb_fromfileX.Length];
            for(int i = 0; i < tomb_fromfileX.Length; i++)
            {
                X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
            }
            string[] tomb_fromfileY = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] Y_fromfile = new int[tomb_fromfileY.Length];
            for(int i = 0; i < tomb_fromfileY.Length; i++)
            {
                Y_fromfile[i] = Convert.ToInt32(tomb_fromfileY[i]);
            }
            FuncMetszet(X_fromfile.Length, X_fromfile, Y_fromfile.Length, Y_fromfile);
            file.Close();
            Console.ReadKey();
            MetszetMinta();
        }
        static void FuncMetszet(int N, int[] X, int M, int[] Y)
        {
            int db = 0;
            int[] Z = new int[N];
            for(int i = 0; i < N; i++)
            {
                int j = 0;
                while(j < M && X[i] != Y[j])
                {
                    j++;

                }
                if(j < M)
                {
                    Z[db] = X[i];
                    db++;
                }
            }
            Console.WriteLine("A metszet elemei:");
            for(int i = 0; i < db; i++)
            {
                Console.Write("{0}, ", Z[i]);
            }
            Console.WriteLine();
        }
        static void Kivalogatas() {
            Console.Clear();
            string a = "Kiválogatás";
            Console.WriteLine(a);
            printData("m");

            dataarray[0] = a;
            Action[] funcs = { Kivspec, Kivalg, KivalogatasMinta, OsszetettAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Kivspec() {
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[3, 0]);
            Console.ReadKey();
            Kivalogatas();
        }
        static void Kivalg() {
            Console.Clear();
            Console.WriteLine(specalg[1]);
            Console.WriteLine(data[3, 1]);
            Console.ReadKey();
            Kivalogatas();
        }
        static void KivalogatasMinta()
        {
            Console.Clear();
            string a = mintak[3];
            Console.WriteLine(a);
            printData("n");
            dataarray_minta[0] = a;
            Action[] funcs = { Kivalogatasbeir, Kivalogatasfilebol, Kivalogatas, Exit };
            arrowNav(funcs, dataarray_minta);
        }
        static void Kivalogatasbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(mintak[3]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írjon be számokat (szóközökkel elválasztva), a program kiválogatja a 10-nél nagyobbakat");
            string[] X_beker = Console.ReadLine().Split(' ');
            int[] X = new int[X_beker.Length];
            for(int i = 0; i < X_beker.Length; i++)
            {
                X[i] = Convert.ToInt32(X_beker[i]);
            }
            FuncKivalogatas(X.Length, X);
            Console.ReadKey();
            MetszetMinta();
        }
        static void Kivalogatasfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(mintak[3]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg az adathalmaz elérési útvonalát, a program kiválogatja a 10-nél nagyobbakat! (számok szóközökkel elválasztva)");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(KivalogatasMinta, Kivalogatasfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] X_fromfile = new int[tomb_fromfileX.Length];
            for(int i = 0; i < tomb_fromfileX.Length; i++)
            {
                X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
            }
            FuncKivalogatas(X_fromfile.Length, X_fromfile);
            file.Close();
            Console.ReadKey();
            KivalogatasMinta();
        }
        static void FuncKivalogatas(int N, int[] X)
        {
            int db = 0;
            int[] Y = new int[N];
            for(int i = 0; i < N; i++)
            {
                if(X[i] > 10)
                {
                    Y[db] = X[i];
                    db++;
                }
            }
            Console.WriteLine("10-nél nagyobb elemek: ");
            for(int i = 0; i < db; i++)
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
            string[] menudata = { "Rendezések", "1] Egyszerű cserés rendezés", "2] Javított Beillesztéses rendezés", "3] Vissza", "4] Kilépés" };
            Action[] funcs = { Egyszerucseres, Beillesztesesrendezes, Menu, Exit };
            arrowNav(funcs, menudata);
        }
        static void Egyszerucseres() {
            Console.Clear();
            string a = "Egyszerű cserés rendezés";
            Console.WriteLine(a);
            printData("m");
            dataarray[0] = a;
            Action[] funcs = { Egyszspec, Egyszalg, EgyszerucseresMinta, RendezesekAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Egyszspec() {
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[5, 0]);
            Console.ReadKey();
            Egyszerucseres();
        }
        static void Egyszalg() {
            Console.Clear();
            Console.WriteLine(specalg[1]);
            Console.WriteLine(data[5, 1]);
            Console.ReadKey();
            Egyszerucseres();
        }
        static void EgyszerucseresMinta()
        {
            Console.Clear();
            string a = mintak[4];
            Console.WriteLine(a);
            printData("n");
            dataarray_minta[0] = a;
            Action[] funcs = { Egyszerucseresbeir, Egyszerucseresfilebol, Egyszerucseres, Exit };
            arrowNav(funcs, dataarray_minta);

        }
        static void Egyszerucseresbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(mintak[4]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írjon be számokat (szóközökkel elválasztva), a program rendezi ezeket");
            string[] X_beker = Console.ReadLine().Split(' ');
            int[] X = new int[X_beker.Length];
            for(int i = 0; i < X_beker.Length; i++)
            {
                X[i] = Convert.ToInt32(X_beker[i]);
            }
            FuncEgyszerucseres(X.Length, X);
            Console.ReadKey();
            MetszetMinta();
        }
        static void Egyszerucseresfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(mintak[4]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg az adathalmaz elérési útvonalát, a program kiválogatja a 10-nél nagyobbakat! (számok szóközökkel elválasztva)");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(EgyszerucseresMinta, Egyszerucseresfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] X_fromfile = new int[tomb_fromfileX.Length];
            for(int i = 0; i < tomb_fromfileX.Length; i++)
            {
                X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
            }
            FuncEgyszerucseres(X_fromfile.Length, X_fromfile);
            file.Close();
            Console.ReadKey();
            EgyszerucseresMinta();
        }
        static void FuncEgyszerucseres(int N, int[] X)
        {
            DateTime start = DateTime.Now;

            for(int i = 0; i < N - 1; i++)
            {
                for(int j = i + 1; j < N; j++)
                {
                    if(X[i] > X[j])
                    {
                        int cs = X[i];
                        X[i] = X[j];
                        X[j] = cs;
                    }
                }
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);

            Console.WriteLine("A rendezett számsor: ");
            for(int i = 0; i < N; i++)
            {
                Console.Write("{0}, ", X[i]);

            }
            Console.WriteLine("\nA rendezés {0} miliszekundum alatt ment végbe", ts.TotalMilliseconds);
            Console.WriteLine();
        }
        static void Beillesztesesrendezes() {
            Console.Clear();
            string a = "Javított beillesztéses rendezés";
            Console.WriteLine(a);
            printData("m");
            dataarray[0] = a;
            Action[] funcs = { Beillesztesspec, Beillesztesalg, BeillesztesesrendezesMinta, RendezesekAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Beillesztesspec() {
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[4, 0]);
            Console.ReadKey();
            Beillesztesesrendezes();
        }
        static void Beillesztesalg() {
            Console.Clear();
            Console.WriteLine(specalg[1]);
            Console.WriteLine(data[4, 1]);
            Console.ReadKey();
            Beillesztesesrendezes();
        }
        static void BeillesztesesrendezesMinta()
        {
            Console.Clear();
            string a = mintak[5];
            Console.WriteLine(a);
            printData("n");
            dataarray_minta[0] = a;
            Action[] funcs = { Beillesztesesbeir, Beillesztesesfilebol, Beillesztesesrendezes, Exit };
            arrowNav(funcs, dataarray_minta);

        }
        static void Beillesztesesbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mintak[5]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írjon be számokat (szóközökkel elválasztva), a program rendezi ezeket");
            string[] X_beker = Console.ReadLine().Split(' ');
            int[] X = new int[X_beker.Length];
            for(int i = 0; i < X_beker.Length; i++)
            {
                X[i] = Convert.ToInt32(X_beker[i]);
            }
            //
            FuncBeillesztesesrendezes(X.Length, X);
            Console.ReadKey();
            BeillesztesesrendezesMinta();
        }
        static void Beillesztesesfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mintak[5]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg az adathalmaz elérési útvonalát, a program kiválogatja a 10-nél nagyobbakat! (számok szóközökkel elválasztva)");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(BeillesztesesrendezesMinta, Beillesztesesfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] X_fromfile = new int[tomb_fromfileX.Length];
            for(int i = 0; i < tomb_fromfileX.Length; i++)
            {
                X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
            }

            //
            FuncBeillesztesesrendezes(X_fromfile.Length, X_fromfile);
            file.Close();
            Console.ReadKey();
            BeillesztesesrendezesMinta();
        }
        static void FuncBeillesztesesrendezes(int N, int[] X)
        {
            DateTime start = DateTime.Now;
            for(int i = 1; i < N; i++)
            {
                int j = i - 1;
                int y = X[i];
                while(j > 0 && X[j] > y)
                {
                    X[j + 1] = X[j];
                    j--;
                }
                X[j + 1] = y;

            }
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);
            Console.WriteLine("A rendezett számsor: ");
            for(int i = 0; i < N; i++)
            {
                Console.Write("{0}, ", X[i]);
            }
            Console.WriteLine("\nA renezés {0} miliszekundum alatt ment végbe", ts.TotalMilliseconds);
            Console.WriteLine();
        }
        static void KeresesekAlmenu()
        {
            Console.Clear();
            Console.WriteLine("Keresések");
            Console.WriteLine("1] Lineáris keresés rendezett adathalmazban\n2] Bináris(Logaritmikus) keresés\n3] Vissza\n4] Kilépés");
            string[] menudata = { "Keresések", "1] Lineáris keresés rendezett adathalmazban", "2] Bináris(Logaritmikus) keresés", "3] Vissza", "4] Kilépés" };
            Action[] funcs = { Lineariskereses, Binariskereses, Menu, Exit };
            arrowNav(funcs, menudata);
        }
        static void Lineariskereses() {
            Console.Clear();
            string a = "Lineáris keresés rendezett sorozatban";
            Console.WriteLine(a);
            printData("m");
            dataarray[0] = a;
            Action[] funcs = { Linearisspec, Linearisalg, LineariskeresesMinta, KeresesekAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Linearisspec() {
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[6, 0]);
            Console.ReadKey();
            Lineariskereses();
        }
        static void Linearisalg() {
            Console.Clear();
            Console.WriteLine(specalg[1]);
            Console.WriteLine(data[6, 1]);
            Console.ReadKey();
            Lineariskereses();
        }
        static void LineariskeresesMinta()
        {
            Console.Clear();
            string a = mintak[6];
            Console.WriteLine(a);
            printData("n");
            dataarray_minta[0] = a;
            Action[] funcs = { Linearisbeir, Linearisfilebol, Lineariskereses, Exit };
            arrowNav(funcs, dataarray_minta);
        }
        static void Linearisbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(mintak[6]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írjon be számokat rendezett sorozatban (szóközökkel elválasztva)!");
            string[] X_beker = Console.ReadLine().Split(' ');
            int[] X = new int[X_beker.Length];
            for(int i = 0; i < X_beker.Length; i++)
            {
                X[i] = Convert.ToInt32(X_beker[i]);
            }
            Console.WriteLine("Írjon be egy számot, a program megkeresei a sorozatban");
            int keresett = Convert.ToInt32(Console.ReadLine());
            //
            FuncLineariskereses(X.Length, X, keresett);
            Console.ReadKey();
            LineariskeresesMinta();
        }
        static void Linearisfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(mintak[6]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg a rendezett adathalmaz elérési útvonalát!");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(LineariskeresesMinta, Linearisfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] X_fromfile = new int[tomb_fromfileX.Length];
            for(int i = 0; i < tomb_fromfileX.Length; i++)
            {
                X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
            }
            Console.WriteLine("Írjon be egy számot, a program megkeresei a sorozatban");
            int keresett_file = Convert.ToInt32(Console.ReadLine());

            FuncLineariskereses(X_fromfile.Length, X_fromfile, keresett_file);
            file.Close();
            Console.ReadKey();
            LineariskeresesMinta();
        }
		static void FuncLineariskereses(int N, int[] X, int Y)
		{
            DateTime start = DateTime.Now;
            
            int i = 0;
			int sorsz = -1;
			while (i < N && X[i] < Y)
			{
				i++;
			}
			bool van = (i < N && X[i] == Y);
            DateTime end = DateTime.Now;
            if(van)
            {
                sorsz = i;
                TimeSpan ts = (end - start);
                Console.WriteLine("A keresett elem a {0}. helyen van", sorsz + 1);
                Console.WriteLine("\nA keresés {0} miliszekundum alatt ment végbe", ts.TotalMilliseconds);
            } else
            {
                Console.WriteLine("A keresett elem nincs a sorozatban");    
            }
            
		}
		static void Binariskereses() {
			Console.Clear();
            string a = "Bináris (logaritmikus) keresés";
			Console.WriteLine(a);
            printData("m");
            dataarray[0] = a;
            Action[] funcs = { Binarisspec, Binarisalg, BinariskeresesMinta, KeresesekAlmenu, Exit };
            arrowNav(funcs, dataarray);
        }
        static void Binarisspec(){
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[7, 0]);
            Console.ReadKey();
            Binariskereses();
        }
        static void Binarisalg(){
            Console.Clear();
            Console.WriteLine(specalg[0]);
            Console.WriteLine(data[7, 1]);
			Console.ReadKey();
			Binariskereses();
    }
        static void BinariskeresesMinta() {
            Console.Clear();
            string a = mintak[7];
            Console.WriteLine(a);
            printData("n");
            dataarray_minta[0] = a;
            Action[] funcs = { Binarisbeir, Binarisfilebol, Binariskereses, Exit };
            arrowNav(funcs, dataarray_minta);
        }
        static void Binarisbeir() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(mintak[7]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Írjon be számokat rendezett sorozatban (szóközökkel elválasztva)!");
            string[] X_beker = Console.ReadLine().Split(' ');
            int[] X = new int[X_beker.Length];
            for(int i = 0; i < X_beker.Length; i++)
            {
                X[i] = Convert.ToInt32(X_beker[i]);
            }
            Console.WriteLine("Írjon be egy számot, a program megkeresei a sorozatban");
            int keresett = Convert.ToInt32(Console.ReadLine());
            //
            FuncBinariskereses(X.Length, X, keresett);
            Console.ReadKey();
            BinariskeresesMinta();
        }
        static void Binarisfilebol() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(mintak[7]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Adja meg a rendezett adathalmaz elérési útvonalát!");
            Console.WriteLine("A 'vissza' beírásával léphet ki!");
            string url = Console.ReadLine();
            FileInputUsermanagement(BinariskeresesMinta, Binarisfilebol, url);
            StreamReader file = new StreamReader(url);
            string[] tomb_fromfileX = file.ReadLine().Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] X_fromfile = new int[tomb_fromfileX.Length];
            for(int i = 0; i < tomb_fromfileX.Length; i++)
            {
                X_fromfile[i] = Convert.ToInt32(tomb_fromfileX[i]);
            }
            Console.WriteLine("Írjon be egy számot, a program megkeresei a sorozatban");
            int keresett_file = Convert.ToInt32(Console.ReadLine());

            FuncBinariskereses(X_fromfile.Length, X_fromfile, keresett_file);
            file.Close();
            Console.ReadKey();
            BinariskeresesMinta();
        }
        static void FuncBinariskereses(int N, int[] X, int Y) {
            DateTime start = DateTime.Now;
        
            int e = 0, u = N-1, k, sorsz;
            do
            {
                k = ((e + u) / 2);
                if(Y < X[k])
                {
                    u = k - 1;
                } else if(Y > X[k])
                {
                    e = k + 1;
                }
            } while(e <= u && X[k] != Y);

            bool van = (e <= u);
            DateTime end = DateTime.Now;
            if(van)
            {
                sorsz = k;
                Console.WriteLine("A keresett elem a {0}. helyen van", sorsz + 1);
                TimeSpan ts = (end - start);
                Console.WriteLine("\nA keresés {0} miliszekundum alatt ment végbe", ts.TotalMilliseconds);
            } else
            {
                Console.WriteLine("A keresett elem nincs a sorozatban");
            }
            
            
        }
		static void Exit()
		{
			Console.Clear();
            using(StringReader reader = new StringReader(@"


─▄█▀▀║░▄█▀▄║▄█▀▄║██▀▄║─
─██║▀█║██║█║██║█║██║█║─
─▀███▀║▀██▀║▀██▀║███▀║─
───────────────────────
───▐█▀▄─ ▀▄─▄▀ █▀▀──█───
───▐█▀▀▄ ──█── █▀▀──▀───
───▐█▄▄▀ ──▀── ▀▀▀──▄───"))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if(line != null)
                    {
                        Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                        Console.WriteLine(line);
                    }
                } while(line != null);
            }
            
            
            System.Threading.Thread.Sleep(1000);
			System.Environment.Exit(0);
		}


		static void Main(string[] args)
		{
			LaunchArguments();
			Menu();
            
        }
		//  dokumentáció, 
	}
}
