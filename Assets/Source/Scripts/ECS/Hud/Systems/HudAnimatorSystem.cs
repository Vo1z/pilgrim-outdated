using Leopotam.Ecs;

namespace Ingame
{
    public sealed class HudAnimatorSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, InHandsTag> _itemModelFilter;

        public void Run()
        {
            foreach (var i in _itemModelFilter)
            {
                ref var hudItemModel = ref _itemModelFilter.Get1(i);
                var animator = hudItemModel.itemAnimator;
                
                animator.SetBool("IsAiming", hudItemModel.isAiming);
            }
        }
    }
}