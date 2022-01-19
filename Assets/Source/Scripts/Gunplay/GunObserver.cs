using System;
using UnityEngine;
using Zenject;

namespace Ingame.Guns
{
    public sealed class GunObserver : MonoBehaviour
    {
        [Inject] private GunData _gunData;
        [Inject] private GunSurfaceDetector _gunSurfaceDetector;
        
        public event Action OnTriggerPressed;
        public event Action ChangeMag;
        public event Action OnShutterDistorted;
        public event Action<FireMode> OnFireModeSwitched;

        public GunData GunData => _gunData;
        public GunSurfaceDetector GunSurfaceDetector => _gunSurfaceDetector;
        
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