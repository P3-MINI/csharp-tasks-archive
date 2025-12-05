using System;
using System.Collections.Generic;

namespace p3z
{

    class Program
    {
        static void Main(string[] args)
        {
            Samochod s1 = new Samochod() { rejestracja = "LU 16120", miasto = "Lublin",
                dataPierwszejRejestracji = new DateTime(2010, 10, 10), dataOstatniegoPrzegladu = new DateTime(2018, 12, 12) };

            Samochod s2 = new Samochod() { rejestracja = "BZA 33RF", miasto = "Zambrów",
                dataPierwszejRejestracji = new DateTime(2010, 10, 01), dataOstatniegoPrzegladu = new DateTime(2016, 01, 11) };

            Samochod s3 = new Samochod() { rejestracja = "WN 8883H", miasto = "Warszawa",
                dataPierwszejRejestracji = new DateTime(2018, 12, 01), dataOstatniegoPrzegladu = new DateTime(2018, 12, 02) };

            Samochod s4 = new Samochod() { rejestracja = "WRA 92BH", miasto = "Radom",
                dataPierwszejRejestracji = new DateTime(2017, 10, 01), dataOstatniegoPrzegladu = new DateTime(2016, 02, 11) };

            Samochod s5 = new Samochod() { rejestracja = "BI 806LP", miasto = "Białystok",
                dataPierwszejRejestracji = new DateTime(2010, 10, 01), dataOstatniegoPrzegladu = new DateTime(2018, 02, 01) };

            Samochod s6 = new Samochod() { rejestracja = "WB 138OP", miasto = "Warszawa",
                dataPierwszejRejestracji = new DateTime(2012, 01, 01), dataOstatniegoPrzegladu = new DateTime(2012, 01, 01) };

            Samochod s7 = new Samochod() { rejestracja = "WBR 77LX", miasto = "Białobrzegi",
                dataPierwszejRejestracji = new DateTime(2018, 01, 01), dataOstatniegoPrzegladu = new DateTime(2018, 02, 01) };

            Samochod s8 = new Samochod() { rejestracja = "WOS 55ML", miasto = "Ostrołęka",
                dataPierwszejRejestracji = new DateTime(2017, 05, 01), dataOstatniegoPrzegladu = new DateTime(2017, 08, 01) };

            Samochod s9 = new Samochod() { rejestracja = "WPI 07BR", miasto = "Piaseczno",
                dataPierwszejRejestracji = new DateTime(2016, 10, 01), dataOstatniegoPrzegladu = new DateTime(2017, 05, 11) };

            Samochod s10 = new Samochod() { rejestracja = "WPL 67CL", miasto = "Płock",
                dataPierwszejRejestracji = new DateTime(2000, 10, 10), dataOstatniegoPrzegladu = new DateTime(2017, 05, 05) };

            List<Samochod> samochody = new List<Samochod>() { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 };

            // ================ ETAP I ================
            Console.WriteLine("*** ETAP I ***");
            //WydzialKomunikacji wk = new WydzialKomunikacji();
            //wk.SerializujSamochod(s1, wk.name + "\\testS1.soap");
            //Samochod s1Copy = wk.DeserializujSamochod(wk.name + "\\testS1.soap");

            //if (s1.rejestracja == s1Copy.rejestracja
            //    && s1.dataPierwszejRejestracji == s1Copy.dataPierwszejRejestracji
            //    && s1.dataOstatniegoPrzegladu == s1Copy.dataOstatniegoPrzegladu)
            //    Console.WriteLine("Serializacja OK");
            //else
            //    Console.WriteLine("Błąd serializacji");

            // ================ ETAP II ================
            Console.WriteLine("*** ETAP II ***");
            //wk.SerializujSamochod(s2, wk.name + "\\testS2.soap");
            //Samochod s2Copy = wk.DeserializujSamochod(wk.name + "\\testS2.soap");
            //wk.SerializujSamochod(s3, wk.name + "\\testS3.soap");
            //Samochod s3Copy = wk.DeserializujSamochod(wk.name + "\\testS3.soap");
            //if (s2.miasto == s2Copy.miasto && s3.miasto == s3Copy.miasto)
            //    Console.WriteLine("ZnajdzMiasto OK");
            //else
            //    Console.WriteLine("Błąd ZnajdzMiasto");

            // ================ ETAP III ================
            Console.WriteLine("*** ETAP III ***");
            //foreach (Samochod s in samochody)
            //    wk.DodajSamochod(s);
            //wk.DodajSamochod(s1);

            // ================ ETAP IV ================
            Console.WriteLine("*** ETAP IV ***");
            //wk.UsunSamochod("BI 806LP");
            //wk.UsunSamochod("WPI 07BR");
            //wk.UsunSamochod("WPL 67CL");
            //wk.UsunSamochod("WBR 77LX");
            //wk.UsunSamochod("LU 16120");
            //wk.UsunSamochod("WPL 67CL");
            //wk.UsunSamochod("BZA 04CV");
            //wk.UsunSamochod("NGI 89DF");

            // ================ ETAP V ================
            Console.WriteLine("*** ETAP V ***");
            //int ile = wk.IleDoPrzegladu();
            //Console.WriteLine("Liczba samochodów do przeglądu: " + ile);

            Console.WriteLine("*** KONIEC ***");
        }
    }
}
