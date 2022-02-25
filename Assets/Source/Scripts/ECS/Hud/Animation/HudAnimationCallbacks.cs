using Ingame.Gunplay;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Ingame.Hud
{
    public sealed class HudAnimationCallbacks : MonoBehaviour
    {
        [Inject] private EcsWorld _ecsWorld;

        private void SendReloadCallback()
        {
            _ecsWorld.NewEntity().Get<ReloadPerformedCallbackEvent>();
        }
        
        private void SendShutterDistortionCallback()
        {
            _ecsWorld.NewEntity().Get<ShutterDistortionPerformedCallbackEvent>();
        }
    }
}