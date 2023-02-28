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
    public class PowerUpSystem : EventEmitSystem<MonoLinkComponent<PowerUpView>>
    {
        [Inject] private IWallet _wallet;
        
        protected override IDisposable Subscribe(MonoLinkComponent<PowerUpView> component, UnityAction callback)
        {
            return component.Instance.SubscribeOnPurchasing(callback);
        }

        protected override void OnTriggered(MonoLinkComponent<PowerUpView> component, EcsPackedEntityWithWorld packedEntity)
        {
            if (!packedEntity.Unpack(out var world, out var entity))
                return;

            ref var powerUpComponent = ref world.GetPool<PowerUpComponent>().Get(entity);
            if (powerUpComponent.IsPurchased)
                return;
            
            if (!_wallet.TryPurchase(powerUpComponent.Price))
                return;

            ref var incomeComponent = ref world.GetPool<IncomeComponent>().Get(powerUpComponent.BoundBusiness);
            incomeComponent.Multiplier += powerUpComponent.Multiplier;
            powerUpComponent.IsPurchased = true;
            
            component.Instance.MarkAsBought(incomeComponent.Multiplier);
        }
    }
}