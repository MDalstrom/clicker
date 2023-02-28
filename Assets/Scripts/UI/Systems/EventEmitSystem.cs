using System;
using System.Collections.Generic;
using Clicker.DI;
using Leopotam.EcsLite;
using UnityEngine.Events;

namespace Clicker.Gameplay.Base
{
    public abstract class EventEmitSystem<T> : InjectedSystem, IEcsInitSystem, IEcsDestroySystem
        where T : struct
    {
        private readonly List<IDisposable> _subscriptions = new();
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<T>().End();
            var pool = world.GetPool<T>();

            foreach (var entity in filter)
            {
                var component = pool.Get(entity);
                var packedEntity = world.PackEntityWithWorld(entity);
                var subscription = Subscribe(component, () => OnTriggered(component, packedEntity));
                _subscriptions.Add(subscription);   
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();
        }
        
        protected abstract IDisposable Subscribe(T component, UnityAction callback);
        protected abstract void OnTriggered(T component, EcsPackedEntityWithWorld packedEntity);
    }
}