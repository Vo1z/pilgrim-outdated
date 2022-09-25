using Ingame.Behaviour;
using Leopotam.Ecs;
using UnityEngine;
using  Ingame.Support;
namespace Ingame.Enemy
{
    public sealed class RagdollActionNode : ActionNode
    {
        private Animator _animator;

        protected override void ActOnStart()
        {
            _animator = Entity.Get<AnimatorModel>().Animator;
            _animator.Play("RAGDOLL");
        }

        protected override void ActOnStop()
        {
             
        }

        protected override State ActOnTick()
        {
            //is dying
            _animator.Play("RAGDOLL");
            var animationState = _animator.IsAnimationPlaying("RAGDOLL");
            //animation has ended
            if (!animationState)
            {
                //is already dead
                ref var enemyStateModel = ref Entity.Get<EnemyStateModel>();
                enemyStateModel.IsDead = true;
                enemyStateModel.IsDying = false;
                return State.Success;
            }
            return State.Running;
        }
    }
}