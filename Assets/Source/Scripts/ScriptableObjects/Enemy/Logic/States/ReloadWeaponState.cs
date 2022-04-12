using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/ReloadWeapon", fileName = " ReloadWeapon")]
    public sealed class ReloadWeaponState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
            if (entity.Has<RelaodCallbackRequest>())
            {
                entity.Del<RelaodCallbackRequest>();
            }
        }

        protected override bool IsNotBlocked(ref EcsEntity entity)
        {
            ref var shooting = ref entity.Get<ShootingModel>();
            return shooting.CurrentAmountOfAmmunition > 0;
        }
    }
}