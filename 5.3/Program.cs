using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._3
{


    class Asmuo
    {
        private string vardas;
        private double pinigai;

        public Asmuo(string vardas, double pinigai)
        {
            this.vardas = vardas;
            this.pinigai = pinigai;
        }

        public string ImtiVarda() { return vardas; }
        public double ImtiPinigus() { return pinigai; }
    }

    class Matrica
    {
        const int CMaxEil = 100;
        const int CmaxSt = 7;
        private Asmuo[,] A;
        public int n { get; set; }
        public int m { get; set; }

        public Matrica()
        {
            n = 0;
            m = 0;
            A = new Asmuo[CMaxEil, CmaxSt];
        }
        public void Deti(int i, int j, Asmuo asmuo)
        {
            A[i, j] = asmuo;
        }

        public Asmuo ImtiReiksme(int i, int j)
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
            Matrica seimosIslaidos = new Matrica();
            Skaityti(CFd, ref seimosIslaidos);
            if (File.Exists(CFr))
                File.Delete(CFr);



            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine();
                fr.WriteLine("Antradienių bendros išlaidos {0,5:c2}.",
                IslaidosSavaitesDienaX(seimosIslaidos, 2));
                fr.WriteLine("Šeštadienių bendros išlaidos {0,5:c2}.",
                IslaidosSavaitesDienaX(seimosIslaidos, 6));
                fr.WriteLine();
                int savaite, diena;
                Asmuo a;
                DienaMaxIslaidos(seimosIslaidos, out savaite, out diena);
                fr.Write("Daugiausia išleista {0} sav. {1} dieną.", savaite, diena);
                a = seimosIslaidos.ImtiReiksme(savaite - 1, diena - 1);
                fr.WriteLine(" Pinigus išleido {0}: {1,5:c2}.", a.ImtiVarda(),
                a.ImtiPinigus());

                int savaiteMin;
                double savaiteMinSuma;
                SavaiteMinIslaidos(seimosIslaidos, out savaiteMin, out savaiteMinSuma);
                fr.Write("Mažiausiai išleista {0} sav.", savaiteMin);
                fr.Write("Isleista: {0}", savaiteMinSuma);

                fr.WriteLine();
            }



            Spausdinti(CFr, seimosIslaidos, "Pradiniai duomenys");


            Console.WriteLine("Pradiniai duomenys isspausdinti faile: {0}\n", CFr);

            double[] Islaidos = new double[seimosIslaidos.n];
            IslaidosSavaitemis(seimosIslaidos, Islaidos);
            SpausdintiIslaidosSavaitemis1(CFr, Islaidos, seimosIslaidos.n);
            Console.WriteLine(seimosIslaidos.m);
            SpausdintiIslaidosDienomis(seimosIslaidos, CFr, seimosIslaidos.m);

            Console.WriteLine("Programa baige darba");
        }

        static void Skaityti(string fd, ref Matrica seimosIslaidos)
        {
            int nn, mm;
            double pinigai;
            string line, vardas;
            Asmuo asmuo;
            using (StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                line = reader.ReadLine();
                mm = int.Parse(line);
                seimosIslaidos.n = nn;
                seimosIslaidos.m = mm;
                for (int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    for (int j = 0; j < mm; j++)
                    {
                        vardas = parts[2 * j];
                        pinigai = double.Parse(parts[2 * j + 1]);
                        asmuo = new Asmuo(vardas, pinigai);
                        seimosIslaidos.Deti(i, j, asmuo);
                    }
                }
            }
        }

        static void Spausdinti(string fv, Matrica seimosIslaidos, string antraste)
        {
            Asmuo asmuo;
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraste);
                fr.WriteLine();
                fr.WriteLine("Savaiciu kiekis {0}", seimosIslaidos.n);
                fr.WriteLine("Nariu skaicius {0}", seimosIslaidos.m);
                fr.WriteLine();

                for (int j = 0; j < seimosIslaidos.m; j++)
                {
                    if (j == 0)
                    {
                        fr.Write("|{0}-dienis     |", j + 1);
                    }
                    else
                    {
                        fr.Write("{0}-dienis      |", j + 1);

                    }
                }

                fr.WriteLine("\n|---------------------------------------------" +
                    "----------------------------------------------------------|");


                for (int i = 0; i < seimosIslaidos.n; i++)
                {
                    for (int j = 0; j < seimosIslaidos.m; j++)
                    {
                        asmuo = seimosIslaidos.ImtiReiksme(i, j);
                        if (j == 0)
                        {
                            fr.Write("|");
                            fr.Write("{0} {1,6:f2} |", asmuo.ImtiVarda(), asmuo.ImtiPinigus());
                        }
                        else
                        {
                            fr.Write("{0} {1,6:f2}  |", asmuo.ImtiVarda(), asmuo.ImtiPinigus());

                        }
                    }
                    fr.WriteLine();
                }

                fr.WriteLine();
                fr.WriteLine("Rezultatai");
                fr.WriteLine();
                fr.WriteLine("Viso isleista: {0,5:c2}.", VisosIslaidos(seimosIslaidos));
                fr.WriteLine();
                fr.WriteLine("Viso dienu be islaidu: {0}.", KeliasDienosNebuvoIslaidu(seimosIslaidos));
                fr.WriteLine();
                fr.WriteLine("Vyro islaidos: {0,5:c2}.", VisosIslaidosPagalAsmeni(seimosIslaidos, "vyras"));
                fr.WriteLine();
                fr.WriteLine("Zmonos islaidos: {0,5:c2}.", VisosIslaidosPagalAsmeni(seimosIslaidos, "žmona"));



            }
        }

        static decimal VisosIslaidos(Matrica A)
        {
            Asmuo asmuo;
            double suma = 0;
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    asmuo = A.ImtiReiksme(i, j);
                    suma += asmuo.ImtiPinigus();
                }
            }
            return (decimal)suma;
        }

        static int KeliasDienosNebuvoIslaidu(Matrica A)
        {
            Asmuo asmuo;
            int kiekDienu = 0;
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    asmuo = A.ImtiReiksme(i, j);
                    if (asmuo.ImtiPinigus() == 0)
                    {
                        kiekDienu++;
                    }
                }
            }
            return kiekDienu;
        }

        static double VisosIslaidosPagalAsmeni(Matrica A, string narys)
        {
            Asmuo asmuo;
            double suma = 0;
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    if (A.ImtiReiksme(i, j).ImtiVarda() == narys)
                    {
                        asmuo = A.ImtiReiksme(i, j);
                        suma += asmuo.ImtiPinigus();
                    }
                }
            }
            return suma;
        }

        static void IslaidosSavaitemis(string fv, ref Matrica A)
        {
            using (var fr = File.AppendText(fv))
            {
                for (int i = 0; i < A.n; i++)
                {
                    double suma = 0;
                    for (int j = 0; j < A.m; j++)
                    {
                        suma += A.ImtiReiksme(i, j).ImtiPinigus();
                    }
                    fr.WriteLine("Savaites nr. {0} islaidos {1,5:c2}.", i + 1, (decimal)suma);
                }
            }
        }

        static void IslaidosSavaitemis(Matrica A, double[] Islaidos)
        {
            for (int i = 0; i < A.n; i++)
            {
                double suma = 0;
                for (int j = 0; j < A.m; j++)
                {
                    Asmuo x = A.ImtiReiksme(i, j);
                    suma += x.ImtiPinigus();
                }
                Islaidos[i] = suma;
            }
        }

        static void SpausdintiIslaidosSavaitemis1(string Cfr, double[] Islaidos, int n)
        {
            using (var fr = File.AppendText(CFr))
            {
                for (int i = 0; i < n; i++)
                {
                    fr.WriteLine("Savaites nr. {0} islaidos {1,5:c2}.", i + 1, (decimal)Islaidos[i]);
                }
            }
        }

        static decimal IslaidosSavaitesDienaX(Matrica A, int nr)
        {
            double suma = 0;
            for (int i = 0; i < A.n; i++)
            {
                Asmuo x = A.ImtiReiksme(i, nr - 1);
                suma = suma + x.ImtiPinigus();
            }
            return (decimal)suma;
        }

        static void SpausdintiIslaidosDienomis(Matrica A, string CFr, int m)
        {
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine("\n|---------------------------------------------" +
                   "----------------------------------------------------------|");
                for (int j = 0; j < m; j++)
                {

                    if (j == 0)
                    {
                        fr.Write("|{0}-dienis     |", j+1);
                    }
                    else
                    {
                        fr.Write("{0}-dienis      |", j+1);

                    }
                }

                fr.WriteLine("\n|---------------------------------------------" +
                    "----------------------------------------------------------|");

                for (int j = 0; j < m; j++)
                {

                    if (j == 0)
                    {
                        fr.Write("|");
                        fr.Write("{0,-8:c2}     |", IslaidosSavaitesDienaX(A, j + 1));
                    }
                    else
                    {
                        fr.Write("{0,-9:c2}     |", IslaidosSavaitesDienaX(A, j + 1));

                    }
                }
                fr.WriteLine("\n|---------------------------------------------" +
                   "----------------------------------------------------------|");

                fr.WriteLine();
            }


        }
    

        static void DienaMaxIslaidos(Matrica A, out int eilNr, out int stNr)
        {
            eilNr = -1;
            stNr = -1;
            double max = 0;
            for (int i = 0; i < A.n; i++)
            {
                for(int j = 0; j < A.m; j++)
                {
                    double x = A.ImtiReiksme(i, j).ImtiPinigus();
                    if(x > max)
                    {
                        max = x;
                        eilNr = i + 1;
                        stNr = j + 1;
                    }
                }
            }
        }

        static void SavaiteMinIslaidos(Matrica A, out int eilNr, out double min)
        {
            double[] islaidos = new double[A.n];
            IslaidosSavaitemis(A, islaidos);
            eilNr = 0;
            min = islaidos[0];

            for(int i = 0; i < A.n; i++)
            {
                if (islaidos[i] < min)
                {
                    min = islaidos[i];
                    eilNr++;

                }
            }
            eilNr++;
        }
            
    }
}
