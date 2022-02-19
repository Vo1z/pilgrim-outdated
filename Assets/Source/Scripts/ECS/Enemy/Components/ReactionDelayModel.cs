using System;
using Ingame.Enemy.Data;
using NaughtyAttributes;

namespace Ingame.Enemy
{      
    [Serializable]
    public struct ReactionDelayModel
    {
        [Expandable]
        public EnemyReactionTimeData ReactionTimeData;
    }
}