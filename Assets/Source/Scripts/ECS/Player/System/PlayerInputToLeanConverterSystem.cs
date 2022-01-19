using Leopotam.Ecs;

namespace Ingame
{
    public sealed class PlayerInputToLeanConverterSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHudModel, RotationComponent> _playerHudFilter;

        public void Run()
        {
            foreach (var i in _playerHudFilter)
            {
                ref var hudRotator = ref _playerHudFilter.Get2(i);
                
                
            }
        }
    }
}