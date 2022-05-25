using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Utils
{
    public sealed class UpdateSettingsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UpdateSettingsRequest> _updateSettingsRequestFilter; 

        public void Run()
        {
            foreach (var i in _updateSettingsRequestFilter)
            {
                ref var updateSettingsReq = ref _updateSettingsRequestFilter.Get1(i);

                if (updateSettingsReq.isCursorAvailable)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}