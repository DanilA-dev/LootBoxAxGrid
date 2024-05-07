using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ListExtensions
    {
        private static Random _rng;

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            if(_rng == null)
                _rng = new Random();

            var count = list.Count;
            while (count > 0)
            {
                count--;
                var index = _rng.Next(count + 1);
                T value = list[index];
                list[index] = list[count];
                list[count] = value;
            }
            
            return list;
        }

        public static T GetRandomElement<T>(this IList<T> enumerable)
        {
            if(_rng == null)
                _rng = new Random();

            int randIndex = _rng.Next(0, enumerable.Count());
            return enumerable[randIndex];
        }
    }
}