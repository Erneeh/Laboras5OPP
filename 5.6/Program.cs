using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._6
{

    class Mokinys
    {
        private string pav, vard;
        private int klas, laikas;
        private double vid;

        public Mokinys()
        {
            pav = "";
            vard = "";
            klas = 0;
            laikas = 0;
            vid = 0.0;
        }

        public Mokinys(string pav, string vard, int klas, int laikas, double vid)
        {
            this.pav = pav;
            this.vard = vard;
            this.klas = klas;
            this.laikas = laikas;
            this.vid = vid;
        }

        public void Deti(string pav, string vard, int klas, double vid)
        {
            this.pav = pav;
            this.vard = vard;
            this.klas = klas;
            this.vid = vid;
        }

        public void DėtiLaiką(int laik) { laikas = laik; }
        /** Grąžina mokinio pavardę */
        public string ImtiPav() { return pav; }
        /** Grąžina mokinio vardą */
        public string ImtiVard() { return vard; }
        /** Grąžina mokinio klasę */
        public int ImtiKlas() { return klas; }
        /** Grąžina mokinio vidurkį */
        public double ImtiVid() { return vid; }
        /** Grąžina mokinio laiką, praleistą internete */
        public int ImtiLaiką() { return laikas; }
        //------------------------------------------------

        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, -15} {1, -10} {2,2:d} {3, 6:f2}",
            pav, vard, klas, laikas);
            return eilute;
        }

        public static bool operator <=(Mokinys pirmas, Mokinys antras)
        {
            return pirmas.klas < antras.klas ||
            pirmas.klas == antras.klas && pirmas.laikas < antras.laikas;
        }
        //-----------------------------------------------------------
        public static bool operator >=(Mokinys pirmas, Mokinys antras)
        {
            return pirmas.klas > antras.klas ||
            pirmas.klas == antras.klas && pirmas.laikas > antras.laikas;
        }

    }
    class Mokykla
    {
        const int CMaxMk = 1000;
        public int n { get; set; }
        private Mokinys[] Mokiniai;

        public Mokykla()
        {
            n = 0;
            Mokiniai = new Mokinys[CMaxMk];
        }

        public Mokinys Imti(int nr)
        {
            Mokinys ob1 = new Mokinys(Mokiniai[nr].ImtiPav(), Mokiniai[nr].ImtiVard(),
                Mokiniai[nr].ImtiKlas(), Mokiniai[nr].ImtiLaiką(),
                Mokiniai[nr].ImtiVid());

            return ob1;
        }

        public void Deti(Mokinys ob)
        {
            Mokinys ob1 = new Mokinys(ob.ImtiPav(), ob.ImtiVard(), ob.ImtiKlas(),
                ob.ImtiLaiką(), ob.ImtiVid());
            Mokiniai[n++] = ob1;
        }

        public void PakeistiMokini(int nr, Mokinys mok)
        {
            Mokinys ob1 = new Mokinys(mok.ImtiPav(), mok.ImtiVard(), mok.ImtiKlas(),
                mok.ImtiLaiką(), mok.ImtiVid());
            Mokiniai[nr] = ob1;
        }


    }


    class Matrica
    {
        const int CMaxMk = 1000;
        const int CMaxDn = 30;
        public int n { get; set; }
        public int m { get; set; }
        private int[,] WWW;
        public Matrica()
        {
            n = 0;
            m = 0;
            WWW = new int[CMaxMk, CMaxDn];
        }

        public void DetiWWW(int i, int j, int r) { WWW[i, j] = r; }
        public int ImtiWWW(int i, int j) { return WWW[i, j]; }

        public void SukeistiEilutesWWW(int nr1, int nr2)
        {
        for(int j = 0; j  < m ; j++)
            {
                int d = WWW[nr1, j];
                WWW[nr1, j] = WWW[nr2, j];
                WWW[nr2, j] = d;
            }
        }
    }

    internal class Program
    {
        const string CFd = "..\\..\\Duomenys.txt";
        const string CFd1 = "..\\..\\Duomenys1.txt";
        const string CFr = "..\\..\\Rezultatai.txt";
        static void Main(string[] args)
        {
            Mokykla mokykl = new Mokykla(); // mokyklos mokinių duomenys
            Matrica mokykla = new Matrica();
            SkaitytiMok(CFd, ref mokykl);
            SkaitytiLaik(CFd1, ref mokykla);
            if (File.Exists(CFr))
                File.Delete(CFr);
            using (var fr = File.CreateText(CFr))
            {
                fr.WriteLine(" Pradiniai duomenys");
                fr.WriteLine();
                fr.WriteLine("Mokinių kiekis {0}", mokykl.n);
                fr.WriteLine("Dienų kiekis {0}", mokykla.m);
                fr.WriteLine();
            }
            Spausdinti(CFr, mokykl, " Mokyklos mokiniai (laikai = 0)");
            SpausdintiLaik(CFr, mokykla, "Mokinių laikai, praleisti internete");
            using (var fr = File.AppendText(CFr))
        {
                fr.WriteLine();
                fr.WriteLine(" Rezultatai");
                fr.WriteLine();
            }
            PapildytiMokiniųDuomenis(mokykl, mokykla);
            Spausdinti(CFr, mokykl, " Mokyklos mokiniai (papildyta, laikai != 0)");
        RikiuotiMinMax(mokykl, mokykla);
            Spausdinti(CFr, mokykl, " Mokyklos mokiniai (surikiuoti)");
            SpausdintiLaik(CFr, mokykla,
            "Mokinių laikai, praleisti internete (po rikiavimo)");
            using (var fr = File.AppendText(CFr))
            {
                int klasė;
                Console.WriteLine("Užrašykite klasę (1-12): ");
                klasė = int.Parse(Console.ReadLine());
                fr.WriteLine();
                if (VidLaikasKl(mokykl, klasė) != 0)
                    fr.WriteLine("{0} klasės mokiniai internete vidutiniškai praleido "
                    + "{1,6:f2} minučių.", klasė, VidLaikasKl(mokykl,
                   klasė));
                else
                    fr.WriteLine("{0} klasės mokinių sąraše nėra.", klasė);
            }
            Console.WriteLine("Pradiniai duomenys išspausdinti faile: {0}", CFr);
            Console.WriteLine("Programa baigė darbą!");
        }

        static void SkaitytiMok(string fd, ref Mokykla mokykl)
        {
            string pav, vard;
            int klas, nn;
            double vid;
            string line;
            using(StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts = line.Split(';');
                nn = int.Parse(parts[0]);
                for(int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    pav = parts[0];
                    vard = parts[1];
                    klas = int.Parse(parts[2]);
                    vid = double.Parse(parts[3]);
                    Mokinys mok;
                    mok = new Mokinys();
                    mok.Deti(pav, vard, klas, vid);
                    mokykl.Deti(mok);
                }

            }
        }

        static void SkaitytiLaik(string fd, ref Matrica mokykla)
        {
            int laikas, nn, mm;
            string line;
            using (StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                line = reader.ReadLine();
                mm = int.Parse(line);
                mokykla.m = mm;
                mokykla.n = nn;
                for (int i = 0; i < mokykla.n; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(' ');
                    for (int j = 0; j < mokykla.m; j++)
                    {
                        laikas = int.Parse(parts[j]);
                        mokykla.DetiWWW(i, j, laikas);
                    }
                }
            }
        }

        //------------------------------------------------------------
        static void Spausdinti(string fv, Mokykla mokykl, string antraštė)
        {
            using (var fr = File.AppendText(fv))
            {
                string bruksnys = new string('-', 46);
                fr.WriteLine(antraštė);
                fr.WriteLine();
                fr.WriteLine(bruksnys);
                fr.WriteLine(" Nr. Pavardė Vardas Klasė Laikas ");
                fr.WriteLine(bruksnys);
                for (int i = 0; i < mokykl.n; i++)
                    fr.WriteLine(" {0}. {1} ", i + 1, mokykl.Imti(i).ToString());
                fr.WriteLine(bruksnys);
                fr.WriteLine();
            }
        }

        static void SpausdintiLaik(string fv, Matrica mokykla, string koment)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("{0} per {1} dienas.", koment, mokykla.m);
                fr.WriteLine();
                for (int i = 0; i < mokykla.n; i++)
                {
                    fr.Write("{0,4:d}. ", i + 1);
                    for (int j = 0; j < mokykla.m; j++)
                        fr.Write("{0,3:d} ", mokykla.ImtiWWW(i, j));
                    fr.WriteLine();
                }
            }
        }


        static double VidLaikasKl(Mokykla mokykl, int klasė)
        {
            double suma = 0;
            int kiek = 0;
            for (int i = 0; i < mokykl.n; i++)
                if (mokykl.Imti(i).ImtiKlas() == klasė)
                {
                    kiek++;
                    suma += mokykl.Imti(i).ImtiLaiką();
                }
            if (kiek != 0)
                return suma / kiek;
            else
                return 0;
        }

        static void PapildytiMokiniųDuomenis(Mokykla mokykl, Matrica mokykla)
        {
            int suma;
            Mokinys mok;
            for (int i = 0; i < mokykl.n; i++)
            {
                suma = 0;
                for (int j = 0; j < mokykla.m; j++)
                    suma = suma + mokykla.ImtiWWW(i, j);
                mok = mokykl.Imti(i);
                mok.DėtiLaiką(suma);
                mokykl.PakeistiMokini(i, mok);
            }
        }

        static void RikiuotiMinMax(Mokykla mokykl, Matrica mokykla)
        {
            Mokinys mok;
            for (int i = 0; i < mokykl.n - 1; i++)
            {
                int minnr = i;
                for (int j = i + 1; j < mokykl.n; j++)
                    if (mokykl.Imti(j) <= mokykl.Imti(minnr))
                        minnr = j;
                mok = mokykl.Imti(i);
                // pakeitimai konteineriuose mokykl ir mokykla
                mokykl.PakeistiMokini(i, mokykl.Imti(minnr));
                mokykl.PakeistiMokini(minnr, mok);
                mokykla.SukeistiEilutesWWW(i, minnr);
            }
        }




    }
}
