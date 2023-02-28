using System;
using System.Collections.Generic;
using Clicker.DI;
using Clicker.Localization;
using UnityEngine;

namespace Clicker.Models
{
    [CreateAssetMenu(menuName = "Gameplay/Business", fileName = "New Business")]
    public class BusinessTemplate : ScriptableObject
    {
        [SerializeField] private LocalizationKey<string> _nameKey;
        [SerializeField] private int _income;
        [SerializeField] private float _duration;
        [SerializeField] private int _basePrice;
        [SerializeField] private PowerUpTemplate[] _powerUps;
        
        public LocalizationKey<string> NameKey => _nameKey;
        public int BaseIncome => _income;
        public float Duration => _duration;
        public int BasePrice => _basePrice;
        public IEnumerable<PowerUpTemplate> PowerUps => _powerUps;
    }
}