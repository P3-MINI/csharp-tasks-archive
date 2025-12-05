using System;

namespace Lab7b
{
    public class UniqueSet
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
