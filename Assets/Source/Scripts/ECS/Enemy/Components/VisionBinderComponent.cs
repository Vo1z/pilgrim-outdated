using System;
 
using UnityEngine;

namespace Ingame.Enemy
{
    [Serializable]
    public struct VisionBinderComponent
    {
        //must be an entity, have on Trigger Enter/Exit and VisionModel
        public SphereCollider ShortRange;
        public SphereCollider FarRange;
    }
}