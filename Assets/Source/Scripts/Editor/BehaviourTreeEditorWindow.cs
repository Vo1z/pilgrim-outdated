using System;
using Ingame.Behaviour;
using PlasticGui.WorkspaceWindow.CodeReview;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.ProBuilder.MeshOperations;

namespace Ingame.Editor
{
    
    public class BehaviourTreeEditorWindow : EditorWindow
    {
        private BehaviourTreePanelView _treePanelView;
        private BehaviourPanelInspectorView _insepctorPanelView;
        
        
        [MenuItem("Editor/Behaviour/BehaviourTreeEditorWindow")]
        public static void Init()
        {
            BehaviourTreeEditorWindow wnd = GetWindow<BehaviourTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BehaviourTreeEditorWindow");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            /*VisualElement label = new Label("Hello World! From C#");
            root.Add(label);*/

            // Import UXML
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/Source/Scripts/Editor/BehaviourTreeEditorWindow.uxml");
            visualTree.CloneTree(root);
            //VisualElement labelFromUXML = visualTree.Instantiate();
            //root.Add(labelFromUXML);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/Scripts/Editor/BehaviourTreeEditorWindow.uss");
            //VisualElement labelWithStyle = new Label("Hello World! With Style");
            //labelWithStyle.styleSheets.Add(styleSheet);
            root.styleSheets.Add(styleSheet);

            _treePanelView = root.Q<BehaviourTreePanelView>();
            _insepctorPanelView = root.Q<BehaviourPanelInspectorView>();
            _treePanelView.OnNodeSelected = OnNodeSelectionChange;
            
            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            var tree = Selection.activeObject as BehaviourTree;
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                _treePanelView.PopulateView(tree);
              
            }
        }

        private void OnNodeSelectionChange(NodeView nodeView)
        {
            _insepctorPanelView.UpdateSelection(nodeView);
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int id, int line)
        {
            if (Selection.activeObject is BehaviourTree )
            {
                Init();
                return true;
            }

            return false;
        }

        private void OnDestroy()
        {
            _treePanelView.Tree.Nodes.ForEach(EditorUtility.SetDirty);
            EditorUtility.SetDirty(_treePanelView.Tree);
            AssetDatabase.SaveAssets();
        }
    }
}