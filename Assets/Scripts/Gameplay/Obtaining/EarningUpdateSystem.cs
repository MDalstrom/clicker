using Clicker.UI;
using Clicker.UI.Systems;
using Leopotam.EcsLite;

namespace Clicker.Gameplay.Earning
{
    public class EarningUpdateSystem : IEcsPostRunSystem
    {
        public void PostRun(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<EarnComponent>().Inc<MonoLinkComponent<BusinessView>>().End();
            var earnPool = world.GetPool<EarnComponent>();
            var monoPool = world.GetPool<MonoLinkComponent<BusinessView>>();
            foreach (var entity in filter)
            {
                var earnComponent = earnPool.Get(entity);
                var delta = earnComponent.ElapsedTime / earnComponent.TargetTime;

                var view = monoPool.Get(entity).Instance;
                view.SetProgress(delta);
            }
        }
    }
}