using Leopotam.Ecs;
using Support.Extensions;

namespace Ingame.Hud
{
    public sealed class HudGunAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, InHandsTag> _gunItemModelFilter;
        private readonly EcsFilter<HudReloadAnimationTriggerEvent> _hudReloadAnimationEventFilter;
        private readonly EcsFilter<HudDistortTheShutterAnimationTriggerEvent> _hudDistortTheShutterAnimationEventFilter;

        public void Run()
        {
            foreach (var i in _gunItemModelFilter)
            {
                ref var hudItemEntity = ref _gunItemModelFilter.GetEntity(i);
                ref var hudItemModel = ref _gunItemModelFilter.Get1(i);
                var animator = hudItemModel.itemAnimator;
                
                if (hudItemEntity.Has<HudIsVisibleTag>()) 
                    animator.SetGameObjectActive();

                if(animator == null)
                    continue;
                
                animator.SetBool("IsAiming", hudItemEntity.Has<HudIsAimingTag>());
                animator.SetBool("IsVisible", hudItemEntity.Has<HudIsVisibleTag>());

                if (!_hudReloadAnimationEventFilter.IsEmpty() && hudItemEntity.Has<HudIsVisibleTag>())
                {
                    animator.ResetTrigger("Reload");
                    animator.SetTrigger("Reload");
                }

                if (!_hudDistortTheShutterAnimationEventFilter.IsEmpty() && hudItemEntity.Has<HudIsVisibleTag>())
                {
                    animator.ResetTrigger("DistortTheShutter");
                    animator.SetTrigger("DistortTheShutter");
                }
            }
        }
    }
}