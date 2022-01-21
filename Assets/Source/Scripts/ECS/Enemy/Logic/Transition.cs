using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ingame.Enemy.Logic{
    [System.Serializable]
    public class Transition
    {
        public DecisionBase Decision;
        public StateBase State;
    }
}