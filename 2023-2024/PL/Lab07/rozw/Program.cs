namespace Lab07
{
    /*
     * Create Paragraph class that contains list of words.
     *      Constructor should take single string representing whole paragraph. Use appropriate method to split words.
     *      Indexer[int] should return lowercase word from i-th position.
     *      Indexer[Index] returns word from i-th position (without modifications).
     *      Indexer[Range] returns range of words indicated by the parameter.
     */
    internal class Paragraph
    {
        public List<string> Words { get; set; }

        public Paragraph(string paragraph)
        {
            this.Words = new List<string>(paragraph.Split(' '));
        }

        public string this[int index]
        {
            get { return this.Words[index].ToLower(); }
        }

        public string this[Index index]
        {
            get { return this.Words[index]; }
        }

        public List<string> this[Range range]
        {
            get
            {
                return this.Words.GetRange(range.Start.Value, range.End.Value - range.Start.Value);
            }
        }
    }

    /*
     * Create Stats class with static method that returns Dictionary that counts how many given word was present in whole text (not case sensitive).
     */
    internal class Stats
    {
        public static Dictionary<string, int> GetStats(List<Paragraph> paragraphs)
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();

            foreach (Paragraph paragraph in paragraphs)
            {
                for (int index = 0; index < paragraph.Words.Count; index++)
                {
                    if (!stats.TryAdd(paragraph.Words[index], 1))
                    {
                        stats[paragraph.Words[index]] += 1;
                    }
                }
            }

            return stats;
        }
    }

    internal class Program
    {
        static string[] GetWords(string filename)
        {
            List<string> words = new List<string>();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    words.Add(reader.ReadLine());
                }
            }

            return words.ToArray();
        }

        static void Main(string[] args)
        {
            /* Create list of paragraphs */
            List<Paragraph> paragraphs = new List<Paragraph>();

            /* Add new paragraphs based on GetWords("Text.txt") method */
            foreach (string paragraph in GetWords("Text.txt"))
                paragraphs.Add(new Paragraph(paragraph));

            /* Create Dicrionary and invoke GetStats method */
            Dictionary<string, int> wordStats = Stats.GetStats(paragraphs);

            /* Print all results using formatting (assume longest word is 15 characters long) */
            foreach (var wordStat in wordStats)
            {
                Console.WriteLine("{0,-15:N} - {1}", wordStat.Key, wordStat.Value);
            }
        }
    }
}
