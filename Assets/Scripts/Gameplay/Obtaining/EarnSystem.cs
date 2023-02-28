using Leopotam.EcsLite;

namespace Clicker.Gameplay.Earning
{
    public class EarnSystem : IEcsRunSystem
    {
        private readonly TimeMeasurer _timeDeltaGetter;
        
        public delegate float TimeMeasurer();

        /// <summary>
        /// Updating <see cref="EarnComponent"/>'s time 
        /// </summary>
        /// <param name="timeDeltaGetter">delegate calculating time delta between calls</param>
        public EarnSystem(TimeMeasurer timeDeltaGetter)
        {
            _timeDeltaGetter = timeDeltaGetter;
        }

        public void Run(IEcsSystems systems)
        {
            var timeDelta = _timeDeltaGetter.Invoke();
            var world = systems.GetWorld();
            var pool = world.GetPool<EarnComponent>();
            var filter = world.Filter<EarnComponent>().End();
            foreach (var entity in filter)
            {
                ref var component = ref pool.Get(entity);
                component.ElapsedTime += timeDelta;
            }
        }
    }
}