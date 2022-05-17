using Leopotam.Ecs;
using Support.Extensions;

namespace Ingame.Inventory
{
    public sealed class UpdateMagazineFullOfAmmoMarkSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MagazineComponent> _magazineFilter;
        private readonly EcsFilter<UpdateMagazineAppearanceEvent> _updateMagazineAppearanceEventFilter;

        public void Run()
        {
            if(_updateMagazineAppearanceEventFilter.IsEmpty())
                return;
            
            foreach (var i in _magazineFilter)
            {
                ref var magazineComp = ref _magazineFilter.Get1(i);
                
                if(magazineComp.fullLoadedMagIdentifier == null)
                    continue;
                
                if(magazineComp.magazineData.MaxAmountOfAmmoInMagazine == magazineComp.currentAmountOfAmmoInMagazine)
                    magazineComp.fullLoadedMagIdentifier.SetGameObjectActive();
                else
                    magazineComp.fullLoadedMagIdentifier.SetGameObjectInactive();
            }
        }
    }
}