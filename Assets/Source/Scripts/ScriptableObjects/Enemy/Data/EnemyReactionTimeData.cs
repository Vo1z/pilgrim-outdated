using UnityEngine;

namespace Ingame.Enemy.Data
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Data/ReactionTimeDelay", fileName = "EnemyReactionDelayData")]
    public class EnemyReactionTimeData: ScriptableObject
    {
        [SerializeField][Min(0)] 
        private float reactionDelayFlat;

        [SerializeField][Min(0)]
        private float reactionDelayRandom;
        
        public float ReactionDelayFlat => reactionDelayFlat;
        public float ReactionDelayRandom => reactionDelayRandom;
    }
}