using Ingame.Input;
using Ingame.Inventory;
using Leopotam.Ecs;

namespace Ingame.Hud
{
    public sealed class HudItemSlotChooseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HudItemModel, InInventoryTag> _hudItemsFilter;

        private readonly EcsFilter<InteractWithFirstSlotInputEvent> _interactWithFirstSlotEventFilter;
        private readonly EcsFilter<InteractWithSecondSlotInputEvent> _interactWithSecondSlotEventFilter;
        private readonly EcsFilter<OpenInventoryInputEvent> _openInventoryInputEventFilter;

        public void Run()
        {
            bool isInteractedWithFirstSlot = !_interactWithFirstSlotEventFilter.IsEmpty();
            bool isInteractedWithSecondSlot = !_interactWithSecondSlotEventFilter.IsEmpty();
            bool isInteractedWithInventory = !_openInventoryInputEventFilter.IsEmpty(); 
            
            if(!isInteractedWithFirstSlot && !isInteractedWithSecondSlot && !isInteractedWithInventory)
                return;
            
            foreach (var i in _hudItemsFilter)
            {
                ref var hudItemEntity = ref _hudItemsFilter.GetEntity(i);
                
                if (isInteractedWithFirstSlot && hudItemEntity.Has<FirstHudItemSlotTag>())
                {
                    hudItemEntity.Get<HudIsInHandsTag>();
                    continue;
                }
                
                if (isInteractedWithSecondSlot && hudItemEntity.Has<SecondHudItemSlotTag>())
                {
                    hudItemEntity.Get<HudIsInHandsTag>();
                    continue;
                }

                if (isInteractedWithInventory && hudItemEntity.Has<BackpackModel>())
                    hudItemEntity.Get<HudIsInHandsTag>();
                else
                    hudItemEntity.Del<HudIsInHandsTag>();
            }
        }
    }
}