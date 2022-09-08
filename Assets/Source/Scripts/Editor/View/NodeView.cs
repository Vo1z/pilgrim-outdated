using System;
using System.Collections;
using System.Collections.Generic;
using Ingame.Behaviour;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Node = Ingame.Behaviour.Node;

namespace Ingame.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> OnNodeSelected;
        public Node Node;
        public Port Input;
        public Port Output;
        
        public NodeView(Node node) : base("Assets/Source/Scripts/Editor/View/NodeView.uxml")
        {
            Node = node;
            this.title = node.name;
            this.viewDataKey = node.Guid;
            
            style.left = node.Position.x;
            style.top = node.Position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }
        
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
           
            Node.Position.x = newPos.xMin;
            Node.Position.y = newPos.yMin;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }

        private void CreateInputPorts()
        {
            if (Node is RootNode)
            {
                
            }
            
            if (Node is ActionNode)
            {
                Input = InstantiatePort(Orientation.Vertical, Direction.Input,Port.Capacity.Single,typeof(bool));
            }

            if (Node is CompositeNode)
            {

                Input = InstantiatePort(Orientation.Vertical, Direction.Input,Port.Capacity.Single,typeof(bool));
            }

            if (Node is DecoratorNode)
            {

                Input = InstantiatePort(Orientation.Vertical, Direction.Input,Port.Capacity.Single,typeof(bool));
            }
          
            if (Input !=null)
            {
                Input.portName = "".ToString();
                Input.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(Input);
            }
        }
        private void CreateOutputPorts()
        {
            if (Node is RootNode)
            {
                Output = InstantiatePort(Orientation.Vertical, Direction.Output,Port.Capacity.Single,typeof(bool));
            }
            
            if (Node is ActionNode)
            {
                //end
            }

            if (Node is CompositeNode)
            {
                Output = InstantiatePort(Orientation.Vertical, Direction.Output,Port.Capacity.Multi,typeof(bool));
            }

            if (Node is DecoratorNode)
            {
                Output = InstantiatePort(Orientation.Vertical, Direction.Output,Port.Capacity.Single,typeof(bool));
            }
            
            if (Output !=null)
            {
                Output.portName = "".ToString();
                Output.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(Output);
            }
        }
    }
}