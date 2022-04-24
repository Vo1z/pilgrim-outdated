using System;
using UnityEngine;

namespace Ingame.Enemy 
{
    [Serializable]
    public struct LocateTargetComponent
    {
        [HideInInspector]
        public Transform Target;
        //.75f
        public float TargetHeadPositionAccordingToBody;
    }
}