using Leopotam.Ecs;

namespace Ingame.Inventory.Items
{
    public sealed class ItemInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        
        public void Init()
        {
            _world.NewEntity().Get<UpdateInventoryAppearanceEvent>();
        }
    }
}