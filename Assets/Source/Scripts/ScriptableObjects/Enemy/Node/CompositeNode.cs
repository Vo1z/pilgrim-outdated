using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Behaviour
{
    [Serializable]
    public abstract class CompositeNode : Node
    {
        [SerializeField]
        public List<Node> Children = new();
        
        public override Node Clone()
        {
            var node = Instantiate(this);
            node.Children = Children.ConvertAll(e=>e.Clone());
            return node;
        }
    }
}