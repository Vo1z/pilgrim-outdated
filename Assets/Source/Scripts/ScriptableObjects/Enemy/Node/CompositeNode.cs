using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Behaviour
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector]
        public List<Node> Children { get; private set; } = new();
        
        public override Node Clone()
        {
            var node = Instantiate(this);
            node.Children = Children.ConvertAll(e=>e.Clone());
            return node;
        }
    }
}