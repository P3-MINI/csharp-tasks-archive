using System.Collections.Generic;
using System.IO;

namespace pl_lab11
{
    public class CsvFileInfo
    {
        public CsvFileInfo(string fullDirectoryPath, string fileName, string[] headers, int rowsNumber, Dictionary<string, string[]> data)
        {
            FullDirectoryPath = fullDirectoryPath;
            FileName = fileName;
            Headers = headers;
            RowsNumber = rowsNumber;
            Data = data;
        }
        public string FullDirectoryPath { get; }
        public string FileName { get; }
        public string[] Headers { get; }
        public int RowsNumber { get; }
        public Dictionary<string, string[]> Data { get; }
    }

    public static class CsvFile
    {
        public static CsvFileInfo Read(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return null;
            }
            
            using (var sr = new StreamReader(filepath))
            {
                string[] headers = sr.ReadLine().Split(',');

                for (int i = 0; i < headers.Length; i++)
                {
                    headers[i] = NormalizeInput(headers[i]);
                }

                var columns = new List<string>[headers.Length];
                for (int i = 0; i < headers.Length; i++)
                {
                    columns[i] = new List<string>();
                }

                while (!sr.EndOfStream)
                {
                    string[] row = sr.ReadLine().Split(',');
                    for (int i = 0; i < headers.Length; i++)
                    {
                        columns[i].Add(NormalizeInput(row[i]));
                    }
                }

                var result = new Dictionary<string, string[]>();

                for (int i = 0; i < headers.Length; i++)
                {
                    result.Add(headers[i], columns[i].ToArray());
                }

                return new CsvFileInfo(
                    Path.GetFullPath(filepath),
                    Path.GetFileName(filepath),
                    headers,
                    columns[0].Count,
                    result
                );
            }
        }

        private static string NormalizeInput(string rawValue)
        {
            if (!string.IsNullOrEmpty(rawValue))
            {
                if (rawValue.StartsWith("\"") && rawValue.EndsWith("\""))
                {
                    return rawValue.Substring(1, rawValue.Length - 2);
                }
            }
            return rawValue;
        }

    }
}
