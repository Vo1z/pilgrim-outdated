
using Ingame.Enemy;
using Ingame.Enemy.Logic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/Decision/Reload", fileName = "ReloadDecision")]
    public sealed class ReloadWeaponDecision : DecisionBase
    {
        public override bool Decide(ref EcsEntity entity)
        {
            if (!entity.Has<ShootingModel>())
            {
                return false;
            }

           ref var shooting =  ref entity.Get<ShootingModel>();
           if (shooting.CurrentAmountOfAmmunition<=0)
           {
               if (!entity.Has<RelaodCallbackRequest>())
               {
                   entity.Get<RelaodCallbackRequest>();
               }
               return true;
           }
           return false;
        }
    }
}