
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;     // add reference System.Runtime.Serialization.Formatters.Soap
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;                         // add reference System.Xml
using System.Runtime.Serialization;                     // add reference System.Runtime.Serialization

[Serializable] public struct Point  // public is required by xml serialization
    {
    public double m;
    public double x;
    public double y;
    public double z;
    }

class Test
    {

#if DEBUG
    const int Size = 5;
#else
    const int Size = 50000;
#endif

    public static void Main()
        {
        int i;
        FileStream fs;
        long time;

        // generating array of points
        Random r = new Random();
        Point[] tab = new Point[Size];
        Console.WriteLine();
        Console.WriteLine("Generated points");
        for ( i=0 ; i<tab.Length ; ++i )
            {
            tab[i].m = r.NextDouble()*10.0;
            tab[i].x = (r.NextDouble()-0.5)*20.0;
            tab[i].y = (r.NextDouble()-0.5)*20.0;
            tab[i].z = (r.NextDouble()-0.5)*20.0;
#if DEBUG
            Console.WriteLine("Point no {0,2}:  m ={1,6:F3} ,  x ={2,7:F3} ,  y ={3,7:F3} ,  z ={4,7:F3}",
                               i, tab[i].m, tab[i].x, tab[i].y, tab[i].z);
#endif
            }

        // "manual" writing array of points to file (as text)
        time = DateTime.Now.Ticks;
        StreamWriter sw = new StreamWriter("text_data.dat");  // stream opened in Create mode
        sw.WriteLine(tab.Length);
        for ( i=0 ; i<tab.Length ; ++i )
            sw.WriteLine("{0},{1},{2},{3}", tab[i].m, tab[i].x, tab[i].y, tab[i].z);
        sw.Close();

        // reading array of points (saved as text) from file
        StreamReader sr = new StreamReader("text_data.dat");  // stream opened in Open mode
        Point[] tab1 = new Point[int.Parse(sr.ReadLine())];
        string[] buf;
        Console.WriteLine();
        Console.WriteLine("Points read from text file");
        for ( i=0 ; i<tab1.Length ; ++i )
            {
            buf = sr.ReadLine().Split(',');
            tab1[i].m = double.Parse(buf[0]);
            tab1[i].x = double.Parse(buf[1]);
            tab1[i].y = double.Parse(buf[2]);
            tab1[i].z = double.Parse(buf[3]);
#if DEBUG
            Console.WriteLine("Point no {0,2}:  m ={1,6:F3} ,  x ={2,7:F3} ,  y ={3,7:F3} ,  z ={4,7:F3}",
                               i, tab1[i].m, tab1[i].x, tab1[i].y, tab1[i].z);
#endif
            }
        sr.Close();
        time = DateTime.Now.Ticks - time;
#if !DEBUG
        Console.WriteLine(time);
#endif

        // "manual" writing array of points to file (binary)
        time = DateTime.Now.Ticks;
        fs = new FileStream("binary_data.dat",FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(tab.Length);
        for ( i=0 ; i<tab.Length ; ++i )
            {
            bw.Write(tab[i].m);
            bw.Write(tab[i].x);
            bw.Write(tab[i].y);
            bw.Write(tab[i].z);
            }
        bw.Close();
        fs.Close();

        // reading array of points (binary saved) from file
        fs = new FileStream("binary_data.dat",FileMode.Open);
        BinaryReader br = new BinaryReader(fs);
        Point[] tab2 = new Point[br.ReadInt32()];
        Console.WriteLine();
        Console.WriteLine("Points read from binary file");
        for ( i=0 ; i<tab2.Length ; ++i )
            {
            tab2[i].m = br.ReadDouble();
            tab2[i].x = br.ReadDouble();
            tab2[i].y = br.ReadDouble();
            tab2[i].z = br.ReadDouble();
#if DEBUG
            Console.WriteLine("Point no {0,2}:  m ={1,6:F3} ,  x ={2,7:F3} ,  y ={3,7:F3} ,  z ={4,7:F3}",
                               i, tab2[i].m, tab2[i].x, tab2[i].y, tab2[i].z);
#endif
            }
        br.Close();
        fs.Close();
        time = DateTime.Now.Ticks - time;
#if !DEBUG
        Console.WriteLine(time);
#endif

        // soap serialization of array of points
        time = DateTime.Now.Ticks;
        fs = new FileStream("soap_serialization.dat",FileMode.Create);
        SoapFormatter sf = new SoapFormatter();
        sf.Serialize(fs,tab);
        fs.Close();

        // soap deserialization of array of points
        fs = new FileStream("soap_serialization.dat",FileMode.Open);
        Point[] tab3 = (Point[])sf.Deserialize(fs);
        fs.Close();
        Console.WriteLine();
        Console.WriteLine("Soap serialization");
#if DEBUG
        for ( i=0 ; i<tab3.Length ; ++i )
            Console.WriteLine("Point no {0,2}:  m ={1,6:F3} ,  x ={2,7:F3} ,  y ={3,7:F3} ,  z ={4,7:F3}",
                               i, tab3[i].m, tab3[i].x, tab3[i].y, tab3[i].z);
#endif
        time = DateTime.Now.Ticks - time;
#if !DEBUG
        Console.WriteLine(time);
#endif

        // binary serialization of array of points
        time = DateTime.Now.Ticks;
        fs = new FileStream("binary_serialization.dat",FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs,tab);
        fs.Close();

        // binary deserialization of array of points
        fs = new FileStream("binary_serialization.dat",FileMode.Open);
        Point[] tab4 = (Point[])bf.Deserialize(fs);
        fs.Close();
        Console.WriteLine();
        Console.WriteLine("Binary serialization");
#if DEBUG
        for ( i=0 ; i<tab4.Length ; ++i )
            Console.WriteLine("Point no {0,2}:  m ={1,6:F3} ,  x ={2,7:F3} ,  y ={3,7:F3} ,  z ={4,7:F3}",
                               i, tab4[i].m, tab4[i].x, tab4[i].y, tab4[i].z);
#endif
        time = DateTime.Now.Ticks - time;
#if !DEBUG
        Console.WriteLine(time);
#endif

        // xml serialization of array of points
        time = DateTime.Now.Ticks;
        fs = new FileStream("xml_serialization.dat",FileMode.Create);
        XmlSerializer xs = new XmlSerializer(typeof(Point[]));
        xs.Serialize(fs,tab);
        fs.Close();

        // xml deserialization of array of points
        fs = new FileStream("xml_serialization.dat",FileMode.Open);
        Point[] tab5 = (Point[])xs.Deserialize(fs);
        fs.Close();
        Console.WriteLine();
        Console.WriteLine("Xml serialization");
#if DEBUG
        for ( i=0 ; i<tab5.Length ; ++i )
            Console.WriteLine("Point no {0,2}:  m ={1,6:F3} ,  x ={2,7:F3} ,  y ={3,7:F3} ,  z ={4,7:F3}",
                               i, tab5[i].m, tab5[i].x, tab5[i].y, tab5[i].z);
#endif
        time = DateTime.Now.Ticks - time;
#if !DEBUG
        Console.WriteLine(time);
#endif

        // data contract serialization of array of points
        time = DateTime.Now.Ticks;
        fs = new FileStream("data_contract_serialization.dat",FileMode.Create);
        DataContractSerializer dcs = new DataContractSerializer(typeof(Point[]));
        dcs.WriteObject(fs,tab);
        fs.Close();

        // data contract deserialization of array of points
        fs = new FileStream("data_contract_serialization.dat",FileMode.Open);
        Point[] tab6 = (Point[])dcs.ReadObject(fs);
        fs.Close();
        Console.WriteLine();
        Console.WriteLine("Data contract serialization");
#if DEBUG
        for ( i=0 ; i<tab6.Length ; ++i )
            Console.WriteLine("Point no {0,2}:  m ={1,6:F3} ,  x ={2,7:F3} ,  y ={3,7:F3} ,  z ={4,7:F3}",
                               i, tab6[i].m, tab6[i].x, tab6[i].y, tab6[i].z);
#endif
        time = DateTime.Now.Ticks - time;
#if !DEBUG
        Console.WriteLine(time);
#endif

        Console.WriteLine();
        }

    }
