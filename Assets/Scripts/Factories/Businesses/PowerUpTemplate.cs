using System;
using Clicker.Localization;
using UnityEngine;

namespace Clicker.Models
{
    [Serializable]
    public class PowerUpTemplate
    {
        [SerializeField] private LocalizationKey<string> _nameKey;
        [SerializeField] private float _multiplier;
        [SerializeField] private int _price;

        public LocalizationKey<string> NameKey => _nameKey;
        public float Multiplier => _multiplier;
        public int Price => _price;
    }
}