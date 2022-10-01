using Ingame.Animation;
using Leopotam.Ecs;
using Support.Extensions;

namespace Ingame.Hud
{
    public sealed class HudGunAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, AnimatorModel, InInventoryTag> _gunItemModelFilter;
        private readonly EcsFilter<HudReloadAnimationTriggerEvent> _hudReloadAnimationEventFilter;
        private readonly EcsFilter<HudDistortTheShutterAnimationTriggerEvent> _hudDistortTheShutterAnimationEventFilter;

        public void Run()
        {
            foreach (var i in _gunItemModelFilter)
            {
                ref var hudItemEntity = ref _gunItemModelFilter.GetEntity(i);
                ref var hudItemModel = ref _gunItemModelFilter.Get2(i);
                var animator = hudItemModel.animator;

                if (hudItemEntity.Has<HudIsInHandsTag>()) 
                    animator.SetGameObjectActive();

                if(animator == null)
                    continue;
                
                animator.SetBool("IsAiming", hudItemEntity.Has<HudIsAimingTag>());
                animator.SetBool("IsVisible", hudItemEntity.Has<HudIsInHandsTag>());

                if (!_hudReloadAnimationEventFilter.IsEmpty() && hudItemEntity.Has<HudIsInHandsTag>())
                {
                    animator.ResetTrigger("Reload");
                    animator.SetTrigger("Reload");
                }

                if (!_hudDistortTheShutterAnimationEventFilter.IsEmpty() && hudItemEntity.Has<HudIsInHandsTag>())
                {
                    animator.ResetTrigger("DistortTheShutter");
                    animator.SetTrigger("DistortTheShutter");
                }
            }
        }
    }
}