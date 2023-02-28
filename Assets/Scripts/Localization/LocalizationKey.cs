using System;
using UnityEngine;

namespace Clicker.Localization
{
    [Serializable]
    public struct LocalizationKey<T>
    {
        [SerializeField] private string _key;

        public T Translate(ILocalizator<T> localizator)
        {
            return localizator.GetTranslation(_key);
        }
    }
}