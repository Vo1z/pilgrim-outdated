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
        public GunData gunData;
        [BoxGroup("References"), Required()]
        public Transform barrelTransform;
    }
}