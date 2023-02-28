using System;

namespace Clicker.Localization
{
    public class KeyNotLocalizedException : Exception
    {
        public KeyNotLocalizedException(string key, object localizator)
            : base($"{key} is not localized in {localizator}") { }
    }
}