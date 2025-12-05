//#define NAIVE
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace solution
{
    public class PasswordRecoveryTool
    {
        private static IEnumerable<string> GenerateWords(IEnumerable<char> characters, int length)
        {
            if (length > 0)
            {
                foreach (char c in characters)
                {
                    foreach (string suffix in GenerateWords(characters, length - 1))
                    {
                        yield return c + suffix;
                    }
                }
            }
            else
            {
                yield return string.Empty;
            }
        }

        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string RecoverPasswordSequential(string passwordHash, int passwordLength, string alphabet, string hashAlgorithmName = "md5")
        {
            var hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName);
            string foundPassword = null;
            foreach (string password in GenerateWords(alphabet, passwordLength))
            {
                string computedPasswordHash = GetHash(hashAlgorithm, password);
                if (passwordHash == computedPasswordHash)
                {
                    foundPassword = password;
                }
            }
            return foundPassword;
        }

        public static string RecoverPasswordParallel(string passwordHash, int passwordLength, string alphabet, string hashAlgorithmName = "md5")
        {
            string foundPassword = null;
#if NAIVE
            Parallel.ForEach(GenerateWords(alphabet, passwordLength), (password, loopState) =>
            {
                var hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName);

                string computedPasswordHash = GetHash(hashAlgorithm, password);
                if (passwordHash == computedPasswordHash)
                {
                    foundPassword = password;
                    loopState.Stop();
                }
            });

#else
            // wersja powyżej (naiwne przekształcenie foreach w Parallel.ForEacha) nie powinna przejść testów hard

            // wersja poniżej powinna przejść testy hard

            Parallel.ForEach(alphabet, (firstLetter, loopState) =>
            {
                var hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName);
                foreach (string subPassword in GenerateWords(alphabet, passwordLength - 1))
                {
                    string computedPasswordHash = GetHash(hashAlgorithm, firstLetter + subPassword);
                    if (passwordHash == computedPasswordHash)
                    {
                        foundPassword = firstLetter + subPassword;
                        loopState.Stop();
                    }
                }
            });
#endif
            return foundPassword;
        }
    }
}
