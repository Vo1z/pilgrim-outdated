﻿using Ingame.Behaviour;
using UnityEngine;

namespace Ingame.Enemy
{
    public class IsCoverNearbyNode : ActionNode
    {
        [SerializeField] 
        private  TypeOfCover type;
        protected override void ActOnStart()
        {
             
        }

        protected override void ActOnStop()
        {
            
        }

        protected override State ActOnTick()
        {
            return State.Failure;
        }
    }
}