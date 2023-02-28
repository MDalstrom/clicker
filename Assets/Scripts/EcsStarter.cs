using System;
using System.Collections.Generic;
using Clicker.DI;
using Clicker.Gameplay.Earning;
using Clicker.Gameplay.Spawning;
using Clicker.Gameplay.Upgrading;
using Clicker.Models;
using Clicker.UI;
using Leopotam.EcsLite;
using UnityEngine;

namespace Clicker
{
    public class EcsStarter : IDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _updateSystems;
        private readonly EcsSystems _fixedUpdateSystems;
        
        public EcsStarter(IProvider provider)
        {
            _world = new EcsWorld();

            var initSystems = new EcsSystems(_world, provider);
            initSystems
                .Add(new SpawnSystem())
                .Add(new LevelUpSystem())
                .Add(new PowerUpSystem())
                .Init();
            
            _updateSystems = new EcsSystems(_world, provider);
            _updateSystems
                .Init();

            _fixedUpdateSystems = new EcsSystems(_world, provider);
            _fixedUpdateSystems
                .Add(new EarnSystem(() => Time.fixedDeltaTime))
                .Add(new ObtainSystem())
                .Add(new EarningUpdateSystem())
                .Init();
        }

        public void Update()
        {
            _updateSystems.Run();
        }
        
        public void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }

        public void Dispose()
        {
            _world.Destroy();
            _updateSystems.Destroy();
            _fixedUpdateSystems.Destroy();
        }
    }

    [Serializable]
    public struct LevelWrapper<T>
    {
        [SerializeField] private T[] _levels;

        public int Level { get; private set; }
        public bool IsMax => Level == _levels.Length - 1;
        public T Instance => _levels[Level];

        public void LevelUp()
        {
            if (IsMax)
                throw new InvalidOperationException("Level is already maximum");

            Level++;
        }
    }
    
    
}