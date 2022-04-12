using Leopotam.Ecs;
using UnityEngine;


namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Logic/State/Detect", fileName = "DetectState")]
    public sealed class DetectState : StateBase
    {
        protected override void DeleteCurrentStateTag(ref EcsEntity entity)
        {
             
        }
    }
}