using System;
using Clicker.DI;
using Clicker.Localization;
using Clicker.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Clicker.UI
{
    public class BusinessView : MonoBehaviour
    {
        [Inject] private readonly ILocalizator<string> _localizator;

        [Header("Name")]
        [SerializeField] private TMP_Text _nameText;
        [Header("Progress Bar")]
        [SerializeField] private Transform _progressBar;
        [Header("Level")]
        [SerializeField] private LocalizationKey<string> _levelHeaderKey;
        [SerializeField] private TMP_Text _levelText;
        [Header("Income")]
        [SerializeField] private LocalizationKey<string> _incomeHeaderKey;
        [SerializeField] private TMP_Text _incomeText;
        [Header("Upgrade")]
        [SerializeField] private LocalizationKey<string> _levelUpLabelKey;
        [SerializeField] private LocalizationKey<string> _levelUpPriceKey;
        [SerializeField] private TMP_Text _levelUpText;
        [SerializeField] private Button _levelUpButton;
        [Header("Power Ups")]
        [SerializeField] private Transform _container;

        public Transform Container => _container;
        
        public void SetName(string nameValue)
        {
            _nameText.text = $"\"{nameValue}\"";
        }
        
        public void SetLevel(int level)
        {
            var header = _levelHeaderKey.Translate(_localizator);
            var value = level.ToString();
            _levelText.text = $"{header}\n{value}";
        }

        public void SetIncome(Currency income)
        {
            var header = _incomeHeaderKey.Translate(_localizator);
            var value = income.ToString();
            _incomeText.text = $"{header}\n{value}";
        }

        public void SetProgress(float progress)
        {
            _progressBar.localScale = new Vector3(progress, 0, 0);
        }
        
        public void SetUpgradeButtonText(Currency upgradePrice)
        {
            var header = _levelUpLabelKey.Translate(_localizator);
            var postHeader = _levelUpPriceKey.Translate(_localizator);
            var value = upgradePrice.ToString();
            _levelUpText.text = $"{header}\n{postHeader}: {value}";
        }

        public IDisposable SubscribeOnLevelUpgrade(UnityAction callback)
        {
            return _levelUpButton.SubscribeOnClick(callback);
        }
    }
}