using UnityEngine;

namespace Ingame.Enemy
{
    public struct EnemyStateModel
    {
        [Header("Low Hp")]
        public bool IsLowHp;
        public bool IsDying;
        public bool IsDead;
        
        [Header("Ammo")] 
        public int MaxAmmo;
        public int CurrentAmmo;
        
        [Header("Detection")]
        public bool IsTargetDetected;

        public bool ShouldSearchForTarget;
        public bool HasDetectedNoises;
        public bool HasLostTarget;
        
        public Transform Target;
        public Vector3 Location;
        public Transform Cover;

    }
}