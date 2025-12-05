namespace Lab07
{
    /*
     * Create Paragraph class that contains list of words.
     *      Constructor should take single string representing whole paragraph. Use appropriate method to split words.
     *      Indexer[int] should return lowercase word from i-th position.
     *      Indexer[Index] returns word from i-th position (without modifications).
     *      Indexer[Range] returns range of words indicated by the parameter.
     */

    /*
     * Create Stats class with static method that returns Dictionary that counts how many given word was present in whole text (not case sensitive).
     */

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

            /* Add new paragraphs based on GetWords("Text.txt") method */

            /* Create Dicrionary and invoke GetStats method */

            /* Print all results using formatting (assume longest word is 15 characters long) */
        }
    }
}
