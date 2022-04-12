 
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo;
using Zenject;

namespace Ingame.Enemy.Provider
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class EnemyMovementComponentProvider : MonoProvider<EnemyMovementComponent>
    {
        [Inject]
        private void Construct()
        {
            value = new EnemyMovementComponent()
            {
                EnemyMovementData = value.EnemyMovementData,
                Waypoint = value.Waypoint,
                NavMeshAgent = this.GetComponent<NavMeshAgent>()
            };
        }
    }
}
