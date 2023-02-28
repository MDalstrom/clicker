using System.Collections.Generic;
using Clicker.DI;
using Clicker.Gameplay.Earning;
using Clicker.Gameplay.Income;
using Clicker.Gameplay.Upgrading;
using Clicker.Localization;
using Clicker.Models;
using Clicker.UI;
using Clicker.UI.Systems;
using Leopotam.EcsLite;
using UnityEngine;

namespace Clicker.Gameplay.Spawning
{
    public class SpawnSystem : InjectedSystem, IEcsInitSystem
    {
        [Inject] private readonly ILocalizator<string> _localizator;
        [Inject] private readonly IEnumerable<BusinessTemplate> _businesses;
        [Inject] private readonly BusinessView _businessViewPrefab;
        [Inject] private readonly PowerUpView _powerUpViewPrefab;
        [Inject] private readonly Transform _parent;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var incomePool = world.GetPool<IncomeComponent>();
            var earnPool = world.GetPool<EarnComponent>();
            var linkPool = world.GetPool<MonoLinkComponent<BusinessView>>();
            foreach (var business in _businesses)
            {
                var businessEntity = world.NewEntity();

                ref var earningComponent = ref earnPool.Add(businessEntity);
                earningComponent.TargetTime = business.Duration;

                ref var incomeComponent = ref incomePool.Add(businessEntity);
                
                var incomeValue = new Currency(business.BaseIncome);
                incomeComponent.BaseIncome = incomeValue;

                var upgradePriceValue = new Currency(business.BasePrice);
                incomeComponent.BasePrice = upgradePriceValue;
                
                ref var linkComponent = ref linkPool.Add(businessEntity);
                var businessView = Object.Instantiate(_businessViewPrefab, _parent);
                linkComponent.Instance = businessView;
                
                businessView.SetName(business.NameKey.Translate(_localizator));
                businessView.SetLevel(0);
                businessView.SetIncome(incomeValue);
                businessView.SetProgress(0);
                businessView.SetUpgradeButtonText(upgradePriceValue);

                var parent = businessView.Container;
                foreach (var powerUp in business.PowerUps)
                {
                    var powerUpEntity = world.NewEntity();

                    ref var powerUpComponent = ref world.GetPool<PowerUpComponent>().Add(powerUpEntity);
                    
                    powerUpComponent.BoundBusiness = businessEntity;
                    
                    powerUpComponent.Multiplier = powerUp.Multiplier;
                    
                    var priceValue = new Currency(powerUp.Price);
                    powerUpComponent.Price = new Currency(powerUp.Price);
                    
                    var powerUpView = Object.Instantiate(_powerUpViewPrefab, parent);
                    powerUpView.SetData(powerUp.NameKey, powerUp.Multiplier, priceValue);                   
                }
            }
        }
    }
}