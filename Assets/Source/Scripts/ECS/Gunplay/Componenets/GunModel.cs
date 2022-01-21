using System;
using Ingame.Guns;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame
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