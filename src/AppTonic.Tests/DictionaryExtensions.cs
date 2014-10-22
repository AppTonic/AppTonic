using System.Collections.Generic;

namespace AppTonic.Tests
{
    public static class DictionaryExtensions
    {
        public static void Increment<T>(this Dictionary<T, int> dictionary, T key)
        {
            int count;
            dictionary.TryGetValue(key, out count);
            dictionary[key] = count + 1;
        }
    }
}