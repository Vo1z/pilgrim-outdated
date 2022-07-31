using Ingame.Interaction.Common;
using Ingame.Movement;
using Leopotam.Ecs;
using Support.Extensions;
using UnityEngine;

namespace Ingame.Inventory
{
    public sealed class DropMagazineSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MagazineComponent, TransformModel, RigidbodyModel, ColliderModel, MagazineIsInInventoryTag, PerformLongInteractionTag> _magazineToDropFilter;

        public void Run()
        {
            foreach (var i in _magazineToDropFilter)
            {
                ref var magazineEntity = ref _magazineToDropFilter.GetEntity(i);
                var magazineTransform = _magazineToDropFilter.Get2(i).transform;
                var magazineRigidbody = _magazineToDropFilter.Get3(i).rigidbody;
                var magazineCollider = _magazineToDropFilter.Get4(i).collider;
                int defaultLayer = LayerMask.NameToLayer("Default");
                
                magazineEntity.Del<PerformLongInteractionTag>();
                magazineEntity.Del<MagazineIsInInventoryTag>();

                magazineTransform.SetParent(null);
                magazineTransform.gameObject.layer = defaultLayer;
                magazineTransform.gameObject.SetLayerToAllChildren(defaultLayer);

                magazineRigidbody.isKinematic = false;
                magazineRigidbody.useGravity = true;
                magazineCollider.isTrigger = false;
            }
        }
    }
}