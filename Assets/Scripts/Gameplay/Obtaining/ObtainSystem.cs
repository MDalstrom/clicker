using Clicker.DI;
using Clicker.Gameplay.Income;
using Leopotam.EcsLite;

namespace Clicker.Gameplay.Earning
{
    /// <summary>
    /// Compares <see cref="EarnComponent"/>'s elapsed time with target and accrue the <see cref="IncomeComponent"/>'s income to shared <see cref="IWallet"/> if elapsed
    /// </summary>
    public class ObtainSystem : IEcsRunSystem
    {
        [Inject] private readonly IWallet _wallet;
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var earningPool = world.GetPool<EarnComponent>();
            var obtainingPool = world.GetPool<IncomeComponent>();
            var filter = world.Filter<EarnComponent>().Inc<IncomeComponent>().End();
            foreach (var entity in filter)
            {
                ref var earningComponent = ref earningPool.Get(entity);
                if (earningComponent.ElapsedTime >= earningComponent.TargetTime)
                {
                    var incomeComponent = obtainingPool.Get(entity);
                    _wallet.Amount.Add(incomeComponent.Income);

                    earningComponent.ElapsedTime = 0;
                }
            }
        }
    }
}