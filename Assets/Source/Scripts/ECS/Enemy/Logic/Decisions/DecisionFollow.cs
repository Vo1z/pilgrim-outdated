using System.Collections;
using System.Collections.Generic;
using Ingame.Enemy.Logic;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Decision/Follow", fileName = "EnemyDecisionFollow")]
    public class DecisionFollow : DecisionBase
    {
        public override bool Decide()
        {
            return false;
        }
    }
}
