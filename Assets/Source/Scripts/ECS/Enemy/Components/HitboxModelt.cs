using System;
using Ingame.Enemy.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Enemy
{
    [Serializable]
    public struct HitboxModel
    {
        [HideInInspector]
        public CapsuleCollider Hitbox;
        [Expandable]
        public EnemyHitboxData HitboxData;
    }
}