using System;
using Ingame.Guns;
using UnityEngine;

namespace Ingame.Player
{
    public sealed class PlayerObserver : MonoBehaviour
    {
        //todo remove hardcode;
        [SerializeField] private GunObserver gunObserver;
        
        public event Action<GunObserver> OnGunInHandsTaken;

        private void Start()
        {
            TakeGunInHands(gunObserver);
        }

        public void TakeGunInHands(GunObserver gunObserver)
        {
            OnGunInHandsTaken?.Invoke(gunObserver);
        }
    }
}