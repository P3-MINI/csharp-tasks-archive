using System.Text;

namespace Lab15_Retake;

// Points: 2.0
// Create extension method for 'FileStream' called 'WriteMangled' that given text and random sequence will mangle the text before outputting it to the file.
// Write some characters of original text, after that write some random characters from randomSequence.
// In order to know how many characters to write (from both text and sequence) create random type variable with seed 1234.
// Repeat until text runs out of characters, save the file.
public static class FileExtension
{
    public static async void WriteMangled(this FileStream stream, string[] text, RandomSequence randomSequence)
    {
        Random random = new Random(1234);

        List<Task<string>> mangleTask = new List<Task<string>>();

        foreach (string singleText in text)
        {
            mangleTask.Add(Task.Run(() =>
            {
                StringBuilder stringBuilder = new StringBuilder();

                int charactersToWrite = singleText.Length;

                while (charactersToWrite > 0)
                {
                    int firstRandomValue = random.Next(10);
                    int secondRandomValue = random.Next(10);

                    int currentCharactersToWrite = Math.Min(firstRandomValue, charactersToWrite);
                    int randomCharactersToWrite = secondRandomValue;

                    // Write text characters.
                    for (int i = 0; i < currentCharactersToWrite; i++)
                        stringBuilder.Append(singleText[singleText.Length - charactersToWrite + i]);

                    // Write random characters.
                    foreach (int randomValue in randomSequence)
                    {
                        if (randomCharactersToWrite-- < 0)
                            break;

                        stringBuilder.Append(randomValue);
                    }
                    charactersToWrite -= currentCharactersToWrite;
                }

                return stringBuilder.ToString();
            }));
        }

        string[] manglesStrings = await Task.WhenAll(mangleTask);
        
        // ContinueWith is not going to work since we need to be on the same thread where FileStream was created.
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (string manglesString in manglesStrings)
                {
                    writer.WriteLine(manglesString);
                }
            }
        }
    }
}
