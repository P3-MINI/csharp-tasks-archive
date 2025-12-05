using System;
using System.Collections.Generic;
using System.Linq;

namespace p3z
{
    class Program
    {

        static void Main(string[] args)
        {
            //!!! Można zmienić ścieżkę na katalog, w którym znajduje się Firma
            string basePath = @"Firma\";

            Console.WriteLine("\n----ETAP I----");
            //HR hr = new HR(basePath);
            //Pracownik p1 = hr.DeserializujPracownika(basePath + "Dzial Analiz\\pracownik1001.xml");
            //Console.WriteLine($"Zdeserializowano pracownika {p1.imie} {p1.nazwisko} na stanowisku {p1.stanowisko}");
            //Pracownik p2 = hr.DeserializujPracownika(basePath + "Dzial Jakosci\\pracownik5005.xml");
            //Console.WriteLine($"Zdeserializowano pracownika {p2.imie} {p2.nazwisko} na stanowisku {p2.stanowisko}");
            //Pracownik p3 = hr.DeserializujPracownika(basePath + "Dzial Analiz\\pracownik1001.xml");
            //Console.WriteLine($"Zdeserializowano pracownika {p3.imie} {p3.nazwisko} na stanowisku {p3.stanowisko}");

            Console.WriteLine("\n----ETAP II----");
            //hr.WypiszPracownikow();

            Console.WriteLine("\n----ETAP III----");
            //Pracownik p4 = new Pracownik(100, "Cyprian", "Cybulski", Stanowisko.Praktykant, 0, null);
            //hr.Zatrudnij(p4, "Dzial Jakosci");
            //Pracownik p5 = new Pracownik(200, "Zenon", "Zakrzewski", Stanowisko.Praktykant, 0, new Adres("Warszawa", "Chmielna", 10));
            //hr.Zatrudnij(p5, "Dzial HR");
            //Pracownik p6 = new Pracownik(300, "Siergiej", "Siwiec", Stanowisko.Specjalista, 0, new Adres("Kolno", "Polna", 4));
            //hr.Zatrudnij(p6, "Dzial Kierowcow");
            //Console.WriteLine("- Pracownicy po zatrudnieniach -");
            //hr.WypiszPracownikow();

            Console.WriteLine("\n----ETAP IV----");
            //hr.Zwolnij(2002);
            //hr.Zwolnij(100);
            //hr.Zwolnij(1);
            //Console.WriteLine("- Pracownicy po zwolnieniach -");
            //hr.WypiszPracownikow();

            Console.WriteLine("\n----ETAP V----");
            //hr.SerializujFirme();
            //List<Pracownik> pracownicy = hr.DeserializujFirme();
            //Console.WriteLine($"Firma liczy {pracownicy.Count} pracowników.");
            //Console.WriteLine($"Nazwisko czlonka zarzadu to {pracownicy.FirstOrDefault(x => x.stanowisko == Stanowisko.CzlonekZarzadu).nazwisko}");

            Console.WriteLine("\n----Koniec----\n");
        }
    }
}
