using System;
using UnityEngine;

namespace Ingame.Enemy.ECS
{
    [Serializable]
    public struct LocateTargetComponent
    {
        public Transform Target;
    }
}