using System;
using UnityEngine;
using Zenject;

namespace Ingame.Guns
{
    public sealed class GunObserver : MonoBehaviour
    {
        [Inject] private GunStatsData _gunStatsData;
        
        public event Action OnTriggerPressed;
        public event Action ChangeMag;
        public event Action OnShutterDistorted;
        public event Action<FireMode> OnFireModeSwitched;

        public GunStatsData GunStatsData => _gunStatsData;
        
        public void ChangeMagazine()
        {
            ChangeMag?.Invoke();
        }

        public void PressTrigger()
        {
            OnTriggerPressed?.Invoke();
        }

        public void DistortTheShutter()
        {
            OnShutterDistorted?.Invoke();
        }

        public void SwitchFireMode(FireMode fireMode)
        {
            OnFireModeSwitched?.Invoke(fireMode);
        }
    }
}