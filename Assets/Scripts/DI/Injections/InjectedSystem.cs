using Leopotam.EcsLite;

namespace Clicker.DI
{
    public abstract class InjectedSystem : IEcsPreInitSystem
    {
        public void PreInit(IEcsSystems systems)
        {
            var provider = systems.GetShared<IProvider>();
            provider.InjectFields(this);
        }
    }
}