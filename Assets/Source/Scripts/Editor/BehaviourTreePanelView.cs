using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingame.Behaviour;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Node = Ingame.Behaviour.Node;

namespace Ingame.Editor
{
    public class BehaviourTreePanelView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreePanelView,GraphView.UxmlTraits>{}

        public Action<NodeView> OnNodeSelected;
        private BehaviourTree _tree;

        public BehaviourTree Tree => _tree;
        public BehaviourTreePanelView()
        {
            Insert(0,new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/Scripts/Editor/BehaviourTreeEditorWindow.uss");
                this.styleSheets.Add(styleSheet);
        }
        
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
           // base.BuildContextualMenu(evt);
           var typesAction = TypeCache.GetTypesDerivedFrom<ActionNode>();
           var typesComposite = TypeCache.GetTypesDerivedFrom<CompositeNode>();
           var typesDecorator = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
           
           AddOptionAction(evt,typesAction);
           AddOptionAction(evt,typesComposite);
           AddOptionAction(evt,typesDecorator);
        
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports
                .Where(e=> e.direction != startPort.direction && e.node != startPort.node)
                .ToList();
        }

        private void AddOptionAction(ContextualMenuPopulateEvent evt,TypeCache.TypeCollection collection)
        {
            foreach (var t in collection)
            {
                evt.menu.AppendAction($"[{t.BaseType.Name}] {t.Name}",(e)=>CreateNode(t));
            }
        }
        private NodeView FindNode(Node node)
        {
            return GetNodeByGuid(node.Guid) as NodeView;
        }
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
        {
            //remove nodes and theirs connections
            if (graphviewchange.elementsToRemove !=null)
            {
                graphviewchange.elementsToRemove.ForEach(e=>
                {
                    var node = e as NodeView;
                    if (node!=null)
                    {
                        _tree.RemoveNode(node.Node);
                        EditorUtility.SetDirty(_tree);
                        AssetDatabase.SaveAssets();
                    }

                    var edge = e as Edge;
                    if (edge != null)
                    {
                        var parent = edge.output.node as NodeView;
                        var child = edge.input.node as NodeView;
                        _tree.RemoveChild(parent.Node,child.Node);
                    }
                });                
            }
            //create connections between graphs
            if (graphviewchange.edgesToCreate != null)
            {
                graphviewchange.edgesToCreate.ForEach(e =>
                {
                    var parent = e.output.node as NodeView;
                    var child = e.input.node as NodeView;
                    _tree.AddChild(parent.Node,child.Node);
                });
            }
            return graphviewchange;
        }

   
        public void PopulateView(BehaviourTree tree)
        {
            this._tree = tree;
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
            tree.Nodes.ForEach(CreateNodeView);

            //create root
            if (_tree.Root == null)
            {
                _tree.Root = tree.CreateNode(typeof(RootNode)) as RootNode;
            }
            //create nodes and edges
            _tree.Nodes.ForEach(e =>
            {
                var children = _tree.GetChildren(e);
                children.ForEach(child =>
                {
                    var c = FindNode(child);
                    var p = FindNode(e);

                    var edge = p.Output.ConnectTo(c.Input);
                    AddElement(edge);
                });
            });
        }
        
        public void CreateNodeView(Node node)
        {
            var view = new NodeView(node);
            view.OnNodeSelected = OnNodeSelected;
            this.AddElement(view);
        }
        
        public void CreateNode(System.Type type)
        {
            var node = _tree.CreateNode(type);
            CreateNodeView(node);
        }
    }
}