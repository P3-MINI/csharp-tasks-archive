using System.IO;

namespace Lab13
{
    class RankSerializers
    {
        // ETAP 2
        public static MemoryStream SerializeBinary(Ranking ranking)
        {
            // E2 - zaimplementowac
            return null;
        }
        public static Ranking DeserializeBinary(MemoryStream stream)
        {
            // E2 - zaimplementowac
            return null;
        }

        // ETAP 3
        public static void SerializeSOAP(Ranking ranking, string path)
        {
            // E3 - zaimplementowac
        }
        public static Ranking DeserializeSOAP(string path)
        {
            // E3 - zaimplementowac
            return null;
        }

        // ETAP 5 - serializacja do pliku tekstowego w formacie podanym w treści zadania
        public static void SerializeOwn(Ranking ranking, Stream stream)
        {
            // E5 - zaimplementowac
            // do wygenerowania tekstu do zapisu należy wywołać jedynie metodę ranking.ToText()
        }
        public static Ranking DeserializeOwn(Stream stream)
        {
            // E5 - zaimplementowac
            // należy użyć metody Ranking.FromText();
            return null;
        }
    }
}
