using Leopotam.Ecs;

namespace Ingame
{
    public sealed class HudGunAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, GunModel, InHandsTag> _gunItemModelFilter;
        private readonly EcsFilter<HudReloadAnimationTriggerEvent> _hudReloadAnimationEventFilter;
        private readonly EcsFilter<HudDistortTheShutterAnimationTriggerEvent> _hudDistortTheShutterAnimationEventFilter;

        public void Run()
        {
            foreach (var i in _gunItemModelFilter)
            {
                ref var hudItemEntity = ref _gunItemModelFilter.GetEntity(i);
                ref var hudItemModel = ref _gunItemModelFilter.Get1(i);
                var animator = hudItemModel.itemAnimator;
                
                animator.SetBool("IsAiming", hudItemEntity.Has<HudIsAimingTag>());

                if (!_hudReloadAnimationEventFilter.IsEmpty())
                {
                    animator.ResetTrigger("Reload");
                    animator.SetTrigger("Reload");
                }

                if (!_hudDistortTheShutterAnimationEventFilter.IsEmpty())
                {
                    animator.ResetTrigger("DistortTheShutter");
                    animator.SetTrigger("DistortTheShutter");
                }
            }
        }
    }
}