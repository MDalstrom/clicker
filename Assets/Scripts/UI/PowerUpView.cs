using System;
using Clicker.DI;
using Clicker.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Clicker.UI
{
    public class PowerUpView : MonoBehaviour
    {
        [Inject] private ILocalizator<string> _localizator;

        [SerializeField] private LocalizationKey<string> _incomeKey;
        [SerializeField] private LocalizationKey<string> _priceKey;
        [SerializeField] private LocalizationKey<string> _boughtKey;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button _purchasingButton;

        private LocalizationKey<string> _nameKey;

        public void SetData(LocalizationKey<string> nameKey, float multiplier, Currency price)
        {
            _nameKey = nameKey;
            var nameValue = _nameKey.Translate(_localizator);
            var incomeHeader = _incomeKey.Translate(_localizator);
            var multiplierValue = Mathf.RoundToInt(multiplier * 100).ToString();
            var priceHeader = _priceKey.Translate(_localizator);
            var priceValue = price.ToString();
            _label.text = $"\"{nameValue}\"\n{incomeHeader}: + {multiplierValue}%\n{priceHeader}: {priceValue}";
        }

        public void MarkAsBought(float multiplier)
        {
            var nameValue = _nameKey.Translate(_localizator);
            var incomeHeader = _incomeKey.Translate(_localizator);
            var multiplierValue = Mathf.RoundToInt(multiplier * 100).ToString();
            var boughtStatus = _boughtKey.Translate(_localizator);
            _label.text = $"\"{nameValue}\"\n{incomeHeader}: + {multiplierValue}%\n{boughtStatus}";
        }

        public IDisposable SubscribeOnPurchasing(UnityAction callback)
        {
            return _purchasingButton.SubscribeOnClick(callback);
        }
    }
}