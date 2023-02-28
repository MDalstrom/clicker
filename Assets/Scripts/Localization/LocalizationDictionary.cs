using System.Collections.Generic;

namespace Clicker.Localization
{
    public class LocalizationDictionary<T> : ILocalizator<T>
    {
        private readonly Dictionary<string, T> _dictionary;

        public LocalizationDictionary(IEnumerable<KeyValuePair<string, T>> pairs)
        {
            _dictionary = new Dictionary<string, T>(pairs);
        }

        public T GetTranslation(string key)
        {
            if (_dictionary.TryGetValue(key, out var result))
                return result;

            throw new KeyNotLocalizedException(key, this);
        }
    }
}