using Ingame.Hud;
using Leopotam.Ecs;
using Support;
using Support.Extensions;

namespace Ingame.Animation
{
    public sealed class HudItemAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, AnimatorModel, AvailableAnimationsComponent, InInventoryTag> _gunItemModelFilter;
        private readonly EcsFilter<HudReloadAnimationTriggerEvent> _hudReloadAnimationEventFilter;
        private readonly EcsFilter<HudDistortTheShutterAnimationTriggerEvent> _hudDistortTheShutterAnimationEventFilter;

        public void Run()
        {
            foreach (var i in _gunItemModelFilter)
            {
                ref var hudItemEntity = ref _gunItemModelFilter.GetEntity(i);
                ref var animatorModel = ref _gunItemModelFilter.Get2(i);
                var itemAvailableAnimations = _gunItemModelFilter.Get3(i); 
                var animator = animatorModel.animator;

                if (hudItemEntity.Has<HudIsInHandsTag>()) 
                    animator.SetGameObjectActive();

                if (animator == null)
                {
                    TemplateUtils.SafeDebug($"animator is null inside {nameof(HudItemAnimationSystem)}");
                    continue;
                }
                
                if (itemAvailableAnimations.HasAnimation(AnimationType.Aim))
                    animator.SetBool("IsAiming", hudItemEntity.Has<HudIsAimingTag>());

                if (itemAvailableAnimations.HasAnimation(AnimationType.Reload))
                {
                    if (!_hudReloadAnimationEventFilter.IsEmpty() && hudItemEntity.Has<HudIsInHandsTag>())
                    {
                        animator.ResetTrigger("Reload");
                        animator.SetTrigger("Reload");
                    }
                }

                if (itemAvailableAnimations.HasAnimation(AnimationType.DistortTheShutter))
                {
                    if (!_hudDistortTheShutterAnimationEventFilter.IsEmpty() && hudItemEntity.Has<HudIsInHandsTag>())
                    {
                        animator.ResetTrigger("DistortTheShutter");
                        animator.SetTrigger("DistortTheShutter");
                    }
                }

                if (itemAvailableAnimations.HasAnimation(AnimationType.ShutterDelay))
                {
                    if (!_hudDistortTheShutterAnimationEventFilter.IsEmpty() && hudItemEntity.Has<HudIsInHandsTag>())
                    {
                        animator.ResetTrigger("DistortTheShutter");
                        animator.SetTrigger("DistortTheShutter");
                    }
                }
            }
        }
    }
}