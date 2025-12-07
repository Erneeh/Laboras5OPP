using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._1
{

    class Matrica
    {
        const int CMaxEil = 10;
        const int CmaxSt = 100;
        private int[,] A;
        public int n { get; set; }
        public int m { get; set; }
        public Matrica()
        {
            n = 0;
            m = 0;
            A = new int[CMaxEil, CmaxSt];
        }
        public void Deti(int i, int j, int pirk)
        {
            A[i, j] = pirk;
        }

        public int ImtiReiksme(int i, int j)
        {
            return A[i, j];
        }
    }
    internal class Program
    {

        const string CFd = "..\\..\\Duomenys.txt";
        const string CFr = "..\\..\\Rezultatai.txt";
        static void Main(string[] args)
        {

            Matrica prekybosBaze = new Matrica();
            Skaityti(CFd, ref prekybosBaze);
            if (File.Exists(CFr))
            {
                File.Delete(CFr);
            }
            Spausdinti(CFr, prekybosBaze, "Pradiniai duomenys");
            // Uzduotis B1
            KasosNedirbo(CFr, prekybosBaze);


            int[] KasuSumos = new int[prekybosBaze.n];
            int[] DienuSumos = new int[prekybosBaze.m];
            int[] KasuVidurkiai = new int[prekybosBaze.n];

            KiekvienaKasaAptarnavo(prekybosBaze, KasuSumos);
            SpausdintiSumas(CFr, KasuSumos, prekybosBaze.n, "Kasos");

            KiekvienaDienaAptarnauta(prekybosBaze, DienuSumos);
            SpausdintiSumas(CFr, DienuSumos, prekybosBaze.m, "Dienos");

            //uzduotis B2

            KiekvienaKasaVidutiniskaiAptarnavo(prekybosBaze, KasuVidurkiai);
            SpausdintiSumas(CFr, KasuVidurkiai, prekybosBaze.n, "VidKasos");


            Console.WriteLine("Programa baige darba!");
        }

        static void Skaityti(string fd, ref Matrica prekybosBaze)
        {
            int nn, mm, skaic;
            string line;
            using (StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                line = reader.ReadLine();
                mm = int.Parse(line);
                prekybosBaze.n = nn;
                prekybosBaze.m = mm;
                for (int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    for (int j = 0; j < mm; j++)
                    {
                        skaic = int.Parse(parts[j]);
                        prekybosBaze.Deti(i, j, skaic);
                    }
                }
            }
        }

        static void Spausdinti(string fv, Matrica prekybosBaze, string antraste)
        {
            int MaxKasosSuma;
            int MinDienosSuma;
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraste);
                fr.WriteLine();
                fr.WriteLine(" Kasu kiekis {0}", prekybosBaze.n);
                fr.WriteLine(" Darbo dienu kiekis {0}", prekybosBaze.m);
                fr.WriteLine(" Aptarnautu klientu kiekiai");
                for (int i = 0; i < prekybosBaze.n; i++)
                {
                    for (int j = 0; j < prekybosBaze.m; j++)
                    {
                        fr.Write("{0,4:d}", prekybosBaze.ImtiReiksme(i, j));
                    }
                    fr.WriteLine();
                }
                fr.WriteLine();
                fr.WriteLine(" Rezultatai");
                fr.WriteLine();
                fr.WriteLine(" Viso aptarnauta: {0} klientų.",
                VisoAptarnauta(prekybosBaze));
                // Uzduotis A1
                fr.WriteLine(" A1) Vidutiniskai per diena aptarnauta: {0} klientų.",
                VidutisniskaiKasaPerDiena(prekybosBaze));

                fr.WriteLine(" Daugiausia pirkėjų aptarnavo (kasa): {0} - {1} pirkejus",
                KasosNumerisMaxPirkeju(prekybosBaze, out MaxKasosSuma), MaxKasosSuma);

                // Uzduotis A2
                fr.WriteLine(" A2) Mažiausiai pirkeju aptarnauta (diena): {0} - {1} pirkejai",
                DienosNumerisMinPirkeju(prekybosBaze, out MinDienosSuma), MinDienosSuma);


            }
        }

        static int VisoAptarnauta(Matrica A)
        {
            int suma = 0;
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    suma += A.ImtiReiksme(i, j);
                }
            }
            return suma;
        }

        //Uzduotis A1
        static int VidutisniskaiKasaPerDiena(Matrica A)
        {
            int vidutinis = 0;
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    vidutinis += A.ImtiReiksme(i, j);
                }
            }

            return vidutinis / A.m;
        }

        // Uzduotis B1
        static void KasosNedirbo(string CFr, Matrica A)
        {
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine();
                fr.WriteLine(" B1) Kasos, kurios nedirbo: ");
                for (int i = 0; i < A.n; i++)
                {
                    int kiekNedirbo = 0;
                    for (int j = 0; j < A.m; j++)
                    {
                        if (A.ImtiReiksme(i, j) == 0)
                        {
                            kiekNedirbo++;
                        }

                    }
                    if (kiekNedirbo == 0)
                    {
                        continue;
                    }
                    else if (kiekNedirbo == 1)
                    {
                        fr.WriteLine("  {0} kasa nedirbo {1} diena.", i + 1, kiekNedirbo);
                    }

                    else
                    {
                        fr.WriteLine("  {0} kasa nedirbo {1} dienas.", i + 1, kiekNedirbo);
                    }
                }



            }
        }

        static void KiekvienaKasaAptarnavo(Matrica A, int[] Sumos)
        {
            for (int i = 0; i < A.n; i++)
            {
                int suma = 0;
                for (int j = 0; j < A.m; j++)
                    suma = suma + A.ImtiReiksme(i, j);
                Sumos[i] = suma;
            }
        }

        static void KiekvienaDienaAptarnauta(Matrica A, int[] Sumos)
        {
            for (int j = 0; j < A.m; j++)
            {
                int suma = 0;
                for (int i = 0; i < A.n; i++)
                    suma = suma + A.ImtiReiksme(i, j);
                Sumos[j] = suma;
            }
        }

        //static void KiekvienaKasaAptarnavo(string CFr, Matrica A)
        //{
        //    using (var fr = File.AppendText(CFr))
        //    {
        //        for (int i = 0; i < A.n; i++)
        //        {
        //            int suma = 0;
        //            for (int j = 0; j < A.m; j++)
        //            {
        //                suma += A.ImtiReiksme(i, j);
        //            }
        //            fr.WriteLine(" Kasa nr. {0} aptarnavo {1} klientu.", i + 1, suma);
        //        }
        //    }
        //}


        //static void KiekvienaDienaAptarnauta(string CFr, Matrica A)
        //{
        //    using (var fr = File.AppendText(CFr))
        //    {
        //        fr.WriteLine();
        //        for (int j = 0; j < A.m; j++)
        //        {
        //            int suma = 0;
        //            for(int i = 0; i < A.n; i++)
        //            {
        //                suma += A.ImtiReiksme(i, j);
        //            }
        //            fr.WriteLine(" Diena nr. {0}: aptarnauta klientu - {1}.", j + 1, suma);
        //        }
        //    }
        //}

        // Uzduotis B2
        static void KiekvienaKasaVidutiniskaiAptarnavo(Matrica A, int[] Sumos)
        {
            KiekvienaKasaAptarnavo(A, Sumos);
            for(int i = 0; i < A.n; i++)
            {
                Sumos[i] = (int)Math.Round((double)Sumos[i] / A.m);
            }
        }

        static void SpausdintiSumas(string CFr, int[] Sumos, int n, string pav)
        {
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine();
                for(int i = 0; i < n; i++)
                {
                    if (pav == "Dienos")
                    {
                        fr.WriteLine(" Diena nr. {0}: aptarnauta klientu - {1}.", i + 1, Sumos[i]);
                    }
                    else if (pav == "Kasos")
                    {
                        fr.WriteLine(" Kasa nr. {0} aptarnavo {1} klientu.", i + 1, Sumos[i]);
                    }
                    // Uzduotis B2
                    else
                    {
                        fr.WriteLine(" B2) Kasa nr. {0} vidutiniskai per diena aptarnavo {1} klientus.", i + 1, Sumos[i]);
                    }
                }
            }
        }

        static int KasosNumerisMaxPirkeju(Matrica A, out int max)
        {
            max = 0;
            int nr = 0;
            for (int i = 0; i < A.n; i++)
            {
                int suma = 0;
                for(int j = 0; j < A.m; j++)
                    suma += A.ImtiReiksme(i, j);
                if (suma > max)
                {
                    max = suma;
                    nr = i + 1;
                }
            }

            return nr;
        }
        // Uzduotis A2
        static int DienosNumerisMinPirkeju(Matrica A, out int min)
        {
            min = 0;
            for(int i = 0; i < A.n; i++)
            {
                min += A.ImtiReiksme(i, 0);
            }
            int nr = 0;
            for (int j = 0; j < A.m; j++)
            {
                int suma = 0;
                for (int i = 0; i < A.n; i++)
                {
                    suma += A.ImtiReiksme(i, j);
                }

                
                if (suma < min)
                {
                    min = suma;
                    nr = j + 1;
                }
            }

            return nr;
        }

    }
}
