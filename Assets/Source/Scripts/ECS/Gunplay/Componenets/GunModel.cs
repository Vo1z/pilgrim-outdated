using System;
using Ingame.Data.Gunplay;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Gunplay
{
    [Serializable]
    public struct GunModel
    {
        [BoxGroup("Data")] 
        public Vector3 localPositionInsideHud;
        [BoxGroup("Data")]
        public Quaternion localRotationInsideHud;
        [BoxGroup("Data")]
        public GunData gunData;
        [BoxGroup("References"), Required]
        public Transform barrelTransform;
        [BoxGroup("References"), Required]
        public Transform handsTransform;
        [BoxGroup("References"), Required]
        public Transform lootDataTransform;
    }
}