using System;
using Ingame.Enemy.Data;
using UnityEngine;

namespace Ingame.Enemy.ECS
{      
    [Serializable]
    public struct ReactionDelayModel
    {
        public EnemyReactionTimeData ReactionTimeData;
    }
}