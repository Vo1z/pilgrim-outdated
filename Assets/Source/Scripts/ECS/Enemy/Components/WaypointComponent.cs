using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Enemy.ECS
{
    [Serializable]
    public struct WaypointComponent
    {
        public List<Transform> Waypoints;
        [HideInInspector] public int Index;
    }
}
