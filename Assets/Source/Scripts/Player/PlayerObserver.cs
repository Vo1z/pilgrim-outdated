using System;
using Ingame.Guns;
using UnityEngine;

namespace Ingame.Player
{
    public sealed class PlayerObserver : MonoBehaviour
    {
        //todo remove hardcode;
        [SerializeField] private Gun gun;
        
        public event Action<Gun> OnGunInHandsTaken;

        private void Start()
        {
            TakeGunInHands(gun);
        }

        public void TakeGunInHands(Gun gun)
        {
            OnGunInHandsTaken?.Invoke(gun);
        }
    }
}