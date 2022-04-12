using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Enemy
{
    public sealed class ReloadSystem: IEcsRunSystem
    {
        private EcsFilter<RelaodCallbackRequest,ShootingModel> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var reload = ref _filter.Get1(i);
                ref var shootingModel = ref _filter.Get2(i);

                if (reload.TimeLeft<shootingModel.ShootingData.ReloadTime)
                {
                    reload.TimeLeft += Time.deltaTime;
                    continue;
                }
                shootingModel.CurrentAmountOfAmmunition = shootingModel.ShootingData.MaxAmountOfAmmunition;
                
            }
        }
    }
}