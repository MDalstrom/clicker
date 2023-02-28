using System;
using System.Linq;
using Clicker.DI;
using Clicker.Localization;
using Clicker.Models;
using Clicker.UI;
using UnityEngine;

namespace Clicker
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LocalizationBuilder _localizationBuilder;
        [SerializeField] private BusinessTemplate[] _businessFactories;
        [SerializeField] private BusinessView _businessView;
        [SerializeField] private Transform _businessesContainer;
        [SerializeField] private PowerUpView _powerUpView;
        
        private EcsStarter _ecsStarter;
        
        private void Awake()
        {
            var provider = new Provider();

            var wallet = new Wallet();
            provider.Add(wallet);
            
            var localizator = _localizationBuilder.Build();
            provider.Add(localizator);

            var businesses = _businessFactories.Select(Instantiate).ToArray();            
            provider.Add(businesses);
            provider.Add(_businessView);
            provider.Add(_powerUpView);
            provider.Add(_businessesContainer);
            
            _ecsStarter = new EcsStarter(provider);
        }

        private void OnDestroy()
        {
            _ecsStarter.Dispose();
        }

        private void Update() => _ecsStarter.Update();
        private void FixedUpdate() => _ecsStarter.FixedUpdate();
    }
}