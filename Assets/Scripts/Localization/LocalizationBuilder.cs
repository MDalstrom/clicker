using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clicker.Localization
{
    [CreateAssetMenu(menuName = "Localization/ScriptableBuilder", fileName = "LocalizationTable")]
    public class LocalizationBuilder : ScriptableObject
    {
        [SerializeField] private Pair<string>[] _pairs;
        
        public ILocalizator<string> Build()
        {
            var keyValuePairs = _pairs.Select(x => x.ToKeyValuePair());
            return new LocalizationDictionary<string>(keyValuePairs);
        }
        
#if UNITY_EDITOR
        public IEnumerable<string> Keys => _pairs.Select(x => x.Key);
#endif
        
        [Serializable]
        private struct Pair<T>
        {
            [SerializeField] private string _key;
            [SerializeField] private T _translation;

#if UNITY_EDITOR
            public string Key => _key;
#endif
            
            public KeyValuePair<string, T> ToKeyValuePair()
            {
                return new KeyValuePair<string, T>(_key, _translation);
            }
        }
    }
}