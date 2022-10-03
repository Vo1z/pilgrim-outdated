using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Enemy
{
    public struct EnemyStateModel
    {
        [Header("Low Hp")]
        public bool IsDying;
        public bool IsDead;
        
        [Header("Ammo")] 
        public int MaxAmmo;
        public int CurrentAmmo;
        
        [Header("Detection")]
        public bool IsTargetDetected;
        //public bool CanUseCamera;
        
        public bool ShouldSearchForTarget;
        public bool HasDetectedNoises;
        public bool HasLostTarget;
        
        //player
        public Transform Target;
        //noise position
        public Vector3? NoisePosition;
        public List<Vector3> LastRememberedNoises;
        
        public Vector3 Location;
        public Transform Cover;

    }
}