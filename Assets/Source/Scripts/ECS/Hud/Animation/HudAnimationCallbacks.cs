using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Ingame
{
    public sealed class HudAnimationCallbacks : MonoBehaviour
    {
        [Inject] private EcsWorld _ecsWorld;

        private void SendReloadCallback()
        {
            print("bbbbbbbbbbbbb");
            _ecsWorld.NewEntity().Get<ReloadPerformedCallbackEvent>();
        }
        
        private void SendShutterDistortionCallback()
        {
            print("aaaaaaaaaaaaaa");
            _ecsWorld.NewEntity().Get<ShutterDistortionPerformedCallbackEvent>();
        }
    }
}