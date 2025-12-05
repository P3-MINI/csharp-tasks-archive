using System;

namespace Lab7a
{
    public class UniqueVector
    {
        private int[] tab;

        public override string ToString()
        {
            string result = "[";
            result += string.Join(';', tab);
            result += "]";

            return result;
        }
    }
}
