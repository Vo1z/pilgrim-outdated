using System;
using UnityEngine;

namespace Ingame.Enemy.Debug
{
    [Serializable]
    public struct DebugStateVisualiserModel
    {
        public MeshRenderer MeshRenderer;
        public Material Attack;
        public Material Flee;
        public Material Follow;
        public Material Patrol;
        public Material FocusedAttack;
        public Material Hide;
        public Material FindCover;
    }
}