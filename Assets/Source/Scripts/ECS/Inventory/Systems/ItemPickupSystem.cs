using Ingame.Interaction.Common;
using Ingame.Movement;
using Leopotam.Ecs;
using Support.Extensions;

namespace Ingame.Inventory
{
    public class ItemPickupSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ItemComponent, InteractiveTag, PerformInteractionTag> _pickupItemsFilter;

        public void Run()
        {
            foreach (var i in _pickupItemsFilter)
            {
                ref var itemEntity = ref _pickupItemsFilter.GetEntity(i);

                itemEntity.Get<InPlayersInventoryTag>();
                itemEntity.Del<PerformInteractionTag>();
                
                if(itemEntity.Has<TransformModel>())
                    itemEntity.Get<TransformModel>().transform.SetGameObjectInactive();
            }
        }
    }
}