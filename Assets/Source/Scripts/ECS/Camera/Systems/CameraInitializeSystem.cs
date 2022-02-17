using Leopotam.Ecs;

namespace Ingame.CameraWork
{
    public class CameraInitializeSystem : IEcsInitSystem
    {
        private readonly EcsFilter<CameraModel, MainCameraTag> _mainCameraFilter;

        public void Init()
        {
            foreach (var i in _mainCameraFilter)
            {
                ref var cameraEntity = ref _mainCameraFilter.GetEntity(i);

                cameraEntity.Get<CameraBobbingComponent>();
            }
        }
    }
}