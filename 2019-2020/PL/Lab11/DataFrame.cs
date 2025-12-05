using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace pl_lab11
{

    public static class DataFrame
    {
        public static DataFrame<T> FromCsv<T>(string filepath, Func<Dictionary<string, string>, T> mapper)
        {
            var csvFileInfo = CsvFile.Read(filepath);
            if (csvFileInfo == null)
            {
                return null;
            }
            var fileData = csvFileInfo.Data;

            var data = new T[csvFileInfo.RowsNumber];

            var row = new Dictionary<string, string>();

            for (int i = 0; i < csvFileInfo.RowsNumber; i++)
            {
                foreach (string header in csvFileInfo.Headers)
                {
                    row[header] = fileData[header][i];
                }
                data[i] = mapper(row);
            }
            return new DataFrame<T>(data);
        }

        public static DataFrame<T> FromXml<T>(string filepath)
        {
            var serializer = new XmlSerializer(typeof(DataFrame<T>));
            using (var sw = new FileStream(filepath, FileMode.Open))
            {
                return (DataFrame<T>)serializer.Deserialize(sw);
            }
        }

        public static DataFrame<T> FromBin<T>(string filepath)
        {
            var serializer = new BinaryFormatter();
            //var serializer = new SoapFormatter();
            using (var sw = new FileStream(filepath, FileMode.Open))
            {
                return (DataFrame<T>)serializer.Deserialize(sw);
            }
        }

    }

    [Serializable]
    public class DataFrame<T>
    {
        public T[] Data;

        public T this[int index] => Data[index];

        public DataFrame(IEnumerable<T> data)
        {
            var list = new List<T>();
            foreach (var element in data)
            {
                list.Add(element);
            }
            Data = list.ToArray();
        }

        private DataFrame() { }


        public void ToXml(string filepath)
        {
            var xmlSerializer = new XmlSerializer(typeof(DataFrame<T>));

            using (var sw = new FileStream(filepath, FileMode.Create))
            {
                xmlSerializer.Serialize(sw, this);
            }
        }

        public void ToBin(string filepath)
        {
            var serializer = new BinaryFormatter();
            using (var sw = new FileStream(filepath, FileMode.Create))
            {
                serializer.Serialize(sw, this);
            }
        }
    }
}
