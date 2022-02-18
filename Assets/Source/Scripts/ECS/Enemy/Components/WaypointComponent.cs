using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Enemy 
{
    [Serializable]
    public struct WaypointComponent
    {
        public List<Transform> Waypoints;
        [HideInInspector] public int Index;
    }
}
