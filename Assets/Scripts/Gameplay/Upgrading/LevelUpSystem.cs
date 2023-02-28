using System;
using Clicker.DI;
using Clicker.Gameplay.Base;
using Clicker.Gameplay.Income;
using Clicker.UI;
using Clicker.UI.Systems;
using Leopotam.EcsLite;
using UnityEngine.Events;

namespace Clicker.Gameplay.Upgrading
{
    public sealed class LevelUpSystem : EventEmitSystem<MonoLinkComponent<BusinessView>>
    {
        [Inject] private IWallet _wallet;
        
        protected override IDisposable Subscribe(MonoLinkComponent<BusinessView> component, UnityAction callback)
        {
            return component.Instance.SubscribeOnLevelUpgrade(callback);
        }

        protected override void OnTriggered(MonoLinkComponent<BusinessView> component, EcsPackedEntityWithWorld packedEntity)
        {
            if (!packedEntity.Unpack(out var world, out var entity))
                return;

            ref var incomeComponent = ref world.GetPool<IncomeComponent>().Get(entity);
            var price = incomeComponent.Price;
            if (_wallet.TryPurchase(price))
            {
                incomeComponent.Level++;

                var view = component.Instance;
                view.SetIncome(incomeComponent.Income);
                view.SetLevel(incomeComponent.Level);
                view.SetUpgradeButtonText(incomeComponent.Price);
            }
        }
    }
}