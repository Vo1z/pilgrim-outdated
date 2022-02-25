using Ingame.Hud;
using Leopotam.Ecs;

namespace Ingame.Gunplay
{
    public sealed class GunDistortTheShutterCallbackReceiverSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, InHandsTag, AwaitingShutterDistortionTag> _gunsFilter;
        private readonly EcsFilter<ShutterDistortionPerformedCallbackEvent> _shutterDistortionCallbackFilter;

        public void Run()
        {
            if (_shutterDistortionCallbackFilter.IsEmpty())
                return;

            foreach (var i in _gunsFilter)
            {
                ref var gunEntity = ref _gunsFilter.GetEntity(i);

                gunEntity.Del<AwaitingShutterDistortionTag>();

                if (gunEntity.Has<BulletIsInShutterTag>()) 
                    gunEntity.Del<BulletIsInShutterTag>();

                if (gunEntity.Has<GunMagazineComponent>())
                {
                    ref var gunMagazine = ref gunEntity.Get<GunMagazineComponent>();

                    if (gunMagazine.amountOfBullets > 0)
                    {
                        gunMagazine.amountOfBullets--;
                        gunEntity.Get<BulletIsInShutterTag>();
                    }
                }
            }
        }
    }
}