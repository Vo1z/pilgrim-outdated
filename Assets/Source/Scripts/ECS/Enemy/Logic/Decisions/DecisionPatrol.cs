using System.Collections;
using System.Collections.Generic;
using Ingame.Enemy.Logic;
using UnityEngine;

namespace Ingame.Enemy.Logic
{
    [CreateAssetMenu(menuName = "Ingame/Enemy/Decision/Patrol", fileName = "EnemyDecisionPatrol")]
    public class DecisionPatrol : DecisionBase
    {

        
        public override bool Decide()
        {

            return true;
        }
    }
}
