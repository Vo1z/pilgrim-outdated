using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Enemy
{
    public struct EnemyStateModel
    {
        //hp
        [Header("Low Hp")]
        public bool IsDying;
        public bool IsDead;
        
        //attack
        [Header("Ammo")] 
        public int MaxAmmo;
        public int CurrentAmmo;
        
        //detection of player
        [Header("Detection")]
        public bool IsTargetDetected;
        public Transform Target;
        
        //special detections
        public bool ShouldSearchForTarget;
        public bool HasDetectedNoises;
        public bool HasLostTarget;
        
        public Vector3? NoisePosition;
        public List<Vector3> LastRememberedNoises;
        
        public Vector3 Location;
        
        //Covers
        public HashSet<Transform> Covers;
        public HashSet<Transform> UndefinedCovers;
        
        //Transparent Covers
        public HashSet<Transform> TransparentCovers;
        public HashSet<Transform> UndefinedTransparentCovers;
        
        
        public Transform Cover;

    }
}