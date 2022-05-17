using System;
using System.Collections.Generic;
using Ingame.Data.Gunplay;
using Ingame.Inventory;
using Leopotam.Ecs;
using ModestTree;

namespace Ingame.Gunplay
{
    public sealed class AssignNextMagazineThatWillBeUsedInReloadingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MagazineComponent, MagazineIsInInventoryTag> _magazinesInInventoryFilter;
        private readonly EcsFilter<UpdateMagazineAppearanceEvent> _updateMagazinesEventFilter;

        public void Run()
        {
            if(_updateMagazinesEventFilter.IsEmpty())
                return;

            var ammoTypeToMaxNumberOfBulletsInMagazine = GetMaximumAmountOfBulletsInInventoryMagazinesForEachAmmoType();
            
            if(ammoTypeToMaxNumberOfBulletsInMagazine.IsEmpty())
                return;
            
            foreach (var i in _magazinesInInventoryFilter)
            {
                ref var magazineEntity = ref _magazinesInInventoryFilter.GetEntity(i);
                ref var magazineComp = ref _magazinesInInventoryFilter.Get1(i);
                var magazineAmmoType = magazineComp.magazineData.AmmoType;

                if(!ammoTypeToMaxNumberOfBulletsInMagazine.ContainsKey(magazineAmmoType))
                    continue;
                
                if (magazineComp.currentAmountOfAmmoInMagazine == ammoTypeToMaxNumberOfBulletsInMagazine[magazineAmmoType])
                    continue;
                
                magazineEntity.Get<MagazineNextToUseInReloadTag>();
                ammoTypeToMaxNumberOfBulletsInMagazine.Remove(magazineAmmoType);
            }
        }

        private Dictionary<AmmoType, int> GetMaximumAmountOfBulletsInInventoryMagazinesForEachAmmoType()
        {
            var ammoTypes = (AmmoType[]) Enum.GetValues(typeof(AmmoType));
            var ammoTypeToMaxNumberOfBulletsInMagazine = new Dictionary<AmmoType, int>(ammoTypes.Length);

            foreach (var i in _magazinesInInventoryFilter)
            {
                ref var magazineComp = ref _magazinesInInventoryFilter.Get1(i);
                var magazineAmmoType = magazineComp.magazineData.AmmoType;
                
                if(ammoTypeToMaxNumberOfBulletsInMagazine[magazineAmmoType] < magazineComp.currentAmountOfAmmoInMagazine)
                    ammoTypeToMaxNumberOfBulletsInMagazine[magazineAmmoType] = magazineComp.currentAmountOfAmmoInMagazine;
            }

            return ammoTypeToMaxNumberOfBulletsInMagazine;
        }
    }
}