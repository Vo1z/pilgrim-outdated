using Leopotam.Ecs;

namespace Ingame
{
    public sealed class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotatorComponent, TransformModel> _rotationRequestFilter;
        
        public void Run()
        {
            foreach (var i in _rotationRequestFilter)
            {
                ref var rotatorComp = ref _rotationRequestFilter.Get1(i);
                ref var transformModel = ref _rotationRequestFilter.Get2(i);

                transformModel.transform.localRotation = rotatorComp.rotation;
            }
        }
    }
}