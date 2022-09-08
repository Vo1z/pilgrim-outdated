using System.Collections;
using System.Collections.Generic;
using Ingame.Behaviour.Test;
using UnityEngine;

namespace Ingame.Behaviour
{
    public class BehaviourAgent : MonoBehaviour
    {
        [SerializeField] private BehaviourTree tree;
        
        // Start is called before the first frame update
        void Start()
        {
            this.tree = tree.Clone();
        }

        // Update is called once per frame
        void Update()
        {
            tree.Tick();
        }
    }
}