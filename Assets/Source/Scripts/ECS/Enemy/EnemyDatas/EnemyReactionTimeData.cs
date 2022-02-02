using UnityEngine;

namespace Ingame.Enemy.Data
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/ReactionTimeDelay", fileName = "EnemyReactionDelayData")]
    public class EnemyReactionTimeData: ScriptableObject
    {
        [Min(0)] 
        public float ReactionDelayFlat;

        [Min(0)]
        public float ReactionDelayRandom;
    }
}