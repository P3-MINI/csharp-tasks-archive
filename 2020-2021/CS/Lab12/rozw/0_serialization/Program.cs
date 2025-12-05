#define STAGE_IO
#define STAGE_STREAM
#define STAGE_BIN
#define STAGE_XML
#define STAGE_STREAM_CHAINING
#define STAGE_LIMITATIONS_BIN
#define STAGE_LIMITATIONS_XML
#define STAGE_DATA_CONTRACT
#define STAGE_JSON

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace _0_serialization
{

    [Serializable]
    public class Parent
    {
        public int Id { get; set; }
        // dodajemy, jak pokażemy, że XmlSerializer sobie nie radzi
        [XmlIgnore]
        public List<Child> Children;
    }

    [Serializable]
    public class Child
    {
        public Parent Parent { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"[CHILD {Id}] - {Parent.Id}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
#if STAGE_IO
            // 0. Display current working directory

            Console.WriteLine($"Current working directory is {Directory.GetCurrentDirectory()}");
            Console.WriteLine();

            // 1. Get all source files within project directory
            // For each file display: file name, size, and parent diretory name
            // Use DirectoryInfo class and EnumerateFiles method (EnumerateFiles is an instance method of DirectoryInfo class)

            Console.WriteLine("Source files in project directory:");
            Console.WriteLine();

            var di = new DirectoryInfo($"{Directory.GetCurrentDirectory()}../../../../");
            foreach (var file in di.EnumerateFiles("*.cs"))
            {
                
                Console.WriteLine($"{file.Directory.Name} {file.Name.PadLeft(20)} | {file.Length} B");
            }

            Console.WriteLine();
#endif
            // 2. Create List of persons based on data in persons.csv (file will be copied to the same directory where exe is during building)
            // Use StreamReader class, string.Split and Convert.ToDateTime methods
            // Display ten first persons from file (there is already prepared ToString method)

            var persons = new List<Person>();

            using (var sr = new StreamReader("./persons.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] parts = sr.ReadLine().Split(',');
                    if (parts.Length > 0)
                    {
                        persons.Add(new Person(parts[0], parts[1])
                        {
                            Country = parts[2],
                            DateOfBirth = Convert.ToDateTime(parts[3])
                        });
                    }
                }
            }

#if STAGE_STREAM

            Console.WriteLine("Persons from file");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(persons[i]);
            }

            Console.WriteLine("...");
            Console.WriteLine();
#endif

            // 3. Binary serialization 
            // Serialize persons into binary file "persons.bin"
            // Deserialize persons from created binary file into another list
            // For serialization use BinaryFormatter (System.Runtime.Serialization.Formatters.Binary)
            // Add proper class attributes to make it work
            // Compare persons across two lists (use CompareLists extension method on List<Person>)

            // BinaryFormatter is insecure and can't be made secure. For more information, see the BinaryFormatter security guide.
            // https://docs.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide

#if STAGE_BIN
            Console.WriteLine("\nBinary serialization\n");

            var bin_serializer = new BinaryFormatter();

            using (var fs = new FileStream("./persons.bin", FileMode.Create))
            {
                bin_serializer.Serialize(fs, persons);
            }

            using (var fs = new FileStream("./persons.bin", FileMode.Open))
            {
                var persons_bin = (List<Person>)bin_serializer.Deserialize(fs);
                persons.CompareLists(persons_bin);
            }
#endif
            // 4. Xml serialization 
            // Serialize persons into binary file "persons.xml"
            // Deserialize persons from created binary file into another list
            // For serialization use XmlSerializer (System.Xml.Serialization)
            // Add necessary changes to Person class to make it work
            // Compare persons across two lists (use CompareLists extension method on List<Person>)

#if STAGE_XML
            Console.WriteLine("\nXml serialization\n");

            var xml_serializer = new XmlSerializer(typeof(List<Person>));

            using (var fs = new FileStream("./persons.xml", FileMode.Create))
            {
                xml_serializer.Serialize(fs, persons);
            }

            using (var fs = new FileStream("./persons.xml", FileMode.Open))
            {
                var persons_xml = (List<Person>)xml_serializer.Deserialize(fs);
                persons.CompareLists(persons_xml);
            }

#endif

            // 5. Chaining streams
            // Use GZipStream and StreamReader to read first 2 lines of "./compressed.txt.gzip" file

#if STAGE_STREAM_CHAINING

            //using (var fs = new FileStream("./compressed.txt", FileMode.Open))
            //{
            //    using (var fs2 = new FileStream("./compressed.txt.gzip", FileMode.Create))
            //    {
            //        using (var gzip = new GZipStream(fs2, CompressionMode.Compress))
            //        {
            //            fs.CopyTo(gzip);
            //        }
            //    }
            //}


            using (var fs = new FileStream("./compressed.txt.gzip", FileMode.Open))
            {
                using (var gzip = new GZipStream(fs, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(gzip))
                    {
                        Console.WriteLine(reader.ReadLine());
                        Console.WriteLine(reader.ReadLine());
                    }
                }
            }

#endif
            // 6* try to serialize following object using binary and xml serializer 

            Console.WriteLine("Serialization common limitations");

            var parent = new Parent()
            {
                Id = 2
            };
            parent.Children = new List<Child>
            {
                new Child {Id = 10, Parent = parent},
                new Child {Id = 20, Parent = parent},
                new Child {Id = 30, Parent = parent},
                new Child {Id = 40, Parent = parent},
            };

            static void CheckForReferences(List<Child> children, string serializationType)
            {
                Console.WriteLine($"[{serializationType}] Deserialized children: ");
                foreach (var child in children)
                {
                    Console.WriteLine(child);
                }
                Console.WriteLine($"[{serializationType}] Changing id of parent on first child to 42");
                children[0].Parent.Id = 42;
                Console.WriteLine($"[{serializationType}] Deserialized points: ");
                foreach (var child in children)
                {
                    Console.WriteLine(child);
                }
            };


#if STAGE_LIMITATIONS_BIN

            // serialize and deserialize 'parent' instance using binary formatter
            // check if references are preserved

            using (var fs = new FileStream("./parent.bin", FileMode.Create))
            {
                bin_serializer.Serialize(fs, parent);
            }

            using (var fs = new FileStream("./parent.bin", FileMode.Open))
            {
                var parent_des = bin_serializer.Deserialize(fs) as Parent;
                CheckForReferences(parent_des.Children, "BIN");
            }

#endif

#if STAGE_LIMITATIONS_XML

            // try to do the same with xmlSerializer
            // XmlSerializer throws exception as it not able to handle circular references

            Console.WriteLine();
            try
            {
                using (var fs = new FileStream("./parent.xml", FileMode.Create))
                {
                    var parent_xml_serializer = new XmlSerializer(typeof(Parent));
                    parent_xml_serializer.Serialize(fs, parent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization failed - {ex?.Message}");
            }

            // Add [XmlIgnore] to Points property of Shape class and 
            // serialize children only 
            using (var fs = new FileStream("./children.xml", FileMode.Create))
            {
                var children_xml_serializer = new XmlSerializer(typeof(List<Child>));
                children_xml_serializer.Serialize(fs, parent.Children);
            }

            // deserialize children, check if parent reference is preserved

            using (var fs = new FileStream("./children.xml", FileMode.Open))
            {
                var children_xml_serializer = new XmlSerializer(typeof(List<Child>));
                var children = children_xml_serializer.Deserialize(fs) as List<Child>;

                CheckForReferences(children, "XML");
            }
#endif

#if STAGE_DATA_CONTRACT
            Console.WriteLine();
            Console.WriteLine("Data contract");

            using (var fs = new FileStream("./points.xml.data", FileMode.Create))
            {
                using (var xtw = new XmlTextWriter(fs, System.Text.Encoding.UTF8))
                {
                    new DataContractSerializer(typeof(Parent), new DataContractSerializerSettings { PreserveObjectReferences = true })
                        .WriteObject(xtw, parent);
                }
            }

            using (var fs = new FileStream("./points.xml.data", FileMode.Open))
            {
                using (var xtr = new XmlTextReader(fs))
                {
                    var parent_dc = new DataContractSerializer(typeof(Parent)).ReadObject(xtr) as Parent;
                    CheckForReferences(parent_dc.Children, "DATA CONTRACT");
                }
            }
#endif

            // we will use JSON serializer from System.Text.JSON (introduced in dotnet core by msft)
            // but it's worth noting that there is also Newtonsoft.JSON that was introduced during 'era' of dotnet framework

#if STAGE_JSON
            var json = JsonSerializer.Serialize(persons, new JsonSerializerOptions { 
                WriteIndented = true
            });

            Console.WriteLine(json);

            var persons_from_json = JsonSerializer.Deserialize<List<Person>>(json);
            var persons_from_json2 = JsonSerializer.Deserialize<Person[]>(json);

            // we can just deserialize any valid json to JsonDocument and inspect it as general document
            var json_document_1 = JsonSerializer.Deserialize<JsonDocument>(@"
{
    ""FirstName"": ""George"",
    ""LastName"": ""Williams"",
    ""DateOfBirth"": ""2014-04-24T00:00:00"",
    ""Country"": ""China""
}
            ");

            Console.WriteLine(json_document_1.RootElement.GetProperty("Country").GetString());
            Console.WriteLine(json_document_1.RootElement.GetProperty("DateOfBirth").GetString());
#endif



            // https://owasp.org/www-project-top-ten/2017/A8_2017-Insecure_Deserialization
            // https://github.com/pwntester/ysoserial.net
        }

        public static void WriteColor(string message, ConsoleColor color = ConsoleColor.Red)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = old;
        }
    }

}
