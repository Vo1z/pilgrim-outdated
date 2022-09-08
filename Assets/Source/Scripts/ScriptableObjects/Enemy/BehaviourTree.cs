using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Ingame.Behaviour{
    [Serializable]
    [CreateAssetMenu(fileName = "BehaviourTree",menuName = "Behaviour/Tree")]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeField] private Node root;
        [SerializeField] private Node.State state = Node.State.Running;
        
        [SerializeField]
        [HideInInspector]
        public List<Node> Nodes  = new();
        public Node Root
        {
            get => root;
            set => root = value;
        }

        public Node.State State
        {
            get => state;
            set => state = value;
        }

        public Node.State Tick()
        {
            return state==Node.State.Running ? root.Tick() : state;
        }

        public Node CreateNode(Type typeOfNode)
        {
            var node = ScriptableObject.CreateInstance(typeOfNode) as Node;
            node.name = typeOfNode.Name;
            node.Guid = GUID.Generate().ToString();
            Nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node,this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void RemoveNode(Node node)
        {
            Nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            //decoration
            var decoratorNode = parent as DecoratorNode;
            if (decoratorNode)
            {
                decoratorNode.Child = child;
            }
            //root node
            var rootNode = parent as RootNode;
            if (rootNode)
            {
                rootNode.Child = child;
            }
            //composite
            var composite = parent as CompositeNode;
            if (composite)
            {
                composite.Children.Add(child);
            }
        }
        
        public void RemoveChild(Node parent, Node child)
        {
            //decoration node
            var decoratorNode = parent as DecoratorNode;
            if (decoratorNode)
            {
                decoratorNode.Child = null;
            }
            //root node
            var rootNode = parent as RootNode;
            if (rootNode)
            {
                rootNode.Child = null;
            }
            //composite node
            var composite = parent as CompositeNode;
            if (composite)
            {
                composite.Children.Remove(child);
            }
        }
        
        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new();
            //decoration node
            var decoratorNode = parent as DecoratorNode;
            if (decoratorNode && decoratorNode.Child != null)
            {
                children.Add(decoratorNode.Child);
            }
            //root node
            var rootNode = parent as RootNode;
            if (rootNode&& rootNode.Child!=null)
            {
                children.Add(rootNode.Child);
            }
            //composite node
            var composite = parent as CompositeNode;
            if (composite)
            {
                return composite.Children;
            }

            return children;
        }

        public BehaviourTree Clone()
        {
            var tree = Instantiate(this);
            tree.root = tree.root.Clone();
            return tree;

        }
    }
}