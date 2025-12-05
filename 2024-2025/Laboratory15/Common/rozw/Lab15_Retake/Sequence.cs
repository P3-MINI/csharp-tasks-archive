using System.Collections;

namespace Lab15_Retake
{
    // Points: 1
    // Create 'RandomSequence' class that implements 'IEnumerable' interface that will return random values from given range.
    // Create constructor that will get the instance of random class you should use for generating numbers and min/max values.
    public class RandomSequence : IEnumerable<int>
    {
        private readonly Random _randomEngine;
        private readonly int _minValue;
        private readonly int _maxValue;

        public RandomSequence(Random randomEngine, int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _randomEngine = randomEngine;
        }

        public IEnumerator<int> GetEnumerator()
        {
            while (true)
            {
                yield return _randomEngine.Next(_minValue, _maxValue);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
