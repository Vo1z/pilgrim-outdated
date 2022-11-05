using System;
using System.Reflection;
using Ingame.Gunplay;
using Ingame.Hud;
using Ingame.Interaction.Common;
using Ingame.Movement;
using Ingame.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Voody.UniLeo;
using Button = UnityEngine.UIElements.Button;

namespace Ingame.Tools
{
    public sealed class GunCreatorWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset gunCreatorVisualAsset;

        private readonly Type[] _commonComponents =
        {
            typeof(ConvertToEntity),
            typeof(EntityReference),
            typeof(EntityReferenceRequestProvider),
            typeof(TransformModelProvider),
            typeof(HudItemModelProvider),
            typeof(InteractiveTagProvider),
            typeof(HudItemRecoilComponentProvider)
        };

        private readonly Type[] _rifleComponentsTypes =
        {
            typeof(FirearmComponentProvider),
            typeof(HudItemInstabilityComponentProvider),
            typeof(TimerComponentProvider),
            typeof(RifleComponentProvider)
        };
        
        private Toggle _createBarrelOriginToggle;
        private Toggle _createSurfaceDetectorToggle;
        private RadioButton _rifleRadioButton;
        private RadioButton _dmrRadioButton;
        private Button _createGunButton;
        private Button _deleteComponentsButton;
        private Button _bringToPlayerButton;
        private Button _bakeHudPositionButton;

        [MenuItem("Tools/Pilgrim/Gun creator")]
        public static void ShowEditorWindow()
        {
            var window = GetWindow<GunCreatorWindow>();
            window.titleContent = new GUIContent("Gun Creator");
        }

        private void CreateGUI()
        {
            AccessField();
            ProcessField();
        }

        private void AccessField()
        {
            gunCreatorVisualAsset.CloneTree(rootVisualElement);
            
            _createBarrelOriginToggle = rootVisualElement.Q<Toggle>("CreateBarrelOrigin");
            _createSurfaceDetectorToggle = rootVisualElement.Q<Toggle>("CreateSurfaceDetector");
            _rifleRadioButton = rootVisualElement.Q<RadioButton>("Rifle");
            _dmrRadioButton = rootVisualElement.Q<RadioButton>("DMR");
            _createGunButton = rootVisualElement.Q<Button>("CreateButton");
            _deleteComponentsButton = rootVisualElement.Q<Button>("DeleteComponents");
            _bringToPlayerButton = rootVisualElement.Q<Button>("BringToPlayer");
            _bakeHudPositionButton = rootVisualElement.Q<Button>("BakeHudData");
        }

        private void ProcessField()
        {
            _createGunButton.clicked += OnCreateButtonClicked;
            _deleteComponentsButton.clicked += OnDeleteComponentsButtonClicked;
            _bringToPlayerButton.clicked += OnBringToPlayerButtonClicked;
            _bakeHudPositionButton.clicked += OnBakeHudDataButtonClicked;
        }

        private void OnCreateButtonClicked()
        {
            var currentGunType = _rifleRadioButton.value ? FirearmType.Rifle :
                                   _dmrRadioButton.value ? FirearmType.DMR :
                                   throw new ArgumentException("No gun type selected");

            var selectedGameObjects = Selection.gameObjects;

            foreach (var go in selectedGameObjects)
                AssembleGun(go, currentGunType);
        }

        private void OnDeleteComponentsButtonClicked()
        {
            var selectedGameObjects = Selection.gameObjects;

            foreach (var go in selectedGameObjects) 
                DeleteAllComponents(go);
        }

        private void OnBringToPlayerButtonClicked()
        {
            var playerItemsGo = FindObjectOfType<HudPlayerItemContainerComponentProvider>();
            var selectedGo = Selection.gameObjects[0];
            
            if(playerItemsGo == null || selectedGo == null)
                return;
            
            selectedGo.transform.SetParent(playerItemsGo.transform);

            if (selectedGo.TryGetComponent(out HudItemModelProvider hudItemModelProvider))
            {
                var localPos = (Vector3) typeof(HudItemModelProvider)
                    .GetField("localPositionInHud", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(hudItemModelProvider)!;
                var localRotation = (Quaternion) typeof(HudItemModelProvider)
                    .GetField("localRotationInHud", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(hudItemModelProvider)!;

                selectedGo.transform.localPosition = localPos;
                selectedGo.transform.localRotation = localRotation;
                
                UnityEngine.Debug.Log($"[Gun creator] GameObject is placed in player's hands");
                
                return;
            }
            
            selectedGo.transform.localPosition = Vector3.zero;
            
            UnityEngine.Debug.Log($"[Gun creator] {nameof(HudItemModelProvider)} is missing, placing to the Vector3.zero");
        }

        private void OnBakeHudDataButtonClicked()
        {
            var selectedGo = Selection.gameObjects[0];
            
            if(selectedGo == null)
                return;

            if (!selectedGo.TryGetComponent(out HudItemModelProvider hudItemModelProvider))
                UnityEngine.Debug.LogWarning($"Target object does not have {typeof(HudItemModelProvider)}");
            
            typeof(HudItemModelProvider)
                .GetField("localPositionInHud", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(hudItemModelProvider, selectedGo.transform.localPosition);
            typeof(HudItemModelProvider)
                .GetField("localRotationInHud", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(hudItemModelProvider, selectedGo.transform.localRotation);
            
            UnityEngine.Debug.Log($"[Gun creator] HUD item data was backed");
        }

        private void AssembleGun(GameObject go, FirearmType firearmType)
        {
            if(go == null)
                return;
            
            //Common components
            foreach (var componentType in _commonComponents)
            {
                if(go.TryGetComponent(componentType, out Component _))
                    continue;

                go.AddComponent(componentType);
            }
            
            //Rifle components
            if (firearmType == FirearmType.Rifle)
            {
                foreach (var componentType in _rifleComponentsTypes)
                {
                    if(go.TryGetComponent(componentType, out Component _))
                        continue;

                    go.AddComponent(componentType);
                }
                
                if(_createBarrelOriginToggle.value)
                    CreateBarrelOrigin(go);

                if (_createSurfaceDetectorToggle.value)
                    CreateSurfaceDetector(go);
                
                UnityEngine.Debug.Log($"[Gun creator] {firearmType} was created");
                
                return;
            }

            //DMR components
            if (firearmType == FirearmType.DMR)
            {
                
            }
        }

        private void CreateBarrelOrigin(GameObject go)
        {
            if(go == null)
                return;
            
            var barrelTransformGameObject = new GameObject("Transform - Barrel origin");
            barrelTransformGameObject.transform.SetParent(go.transform);
            barrelTransformGameObject.transform.localPosition = Vector3.zero;

            var components = go.GetComponents<Component>();

            foreach (var component in components)
            {
                if(component is not FirearmComponentProvider)
                    continue;

                typeof(FirearmComponentProvider)
                    .GetField("barrelOrigin", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(component, barrelTransformGameObject.transform);
            }
            
            UnityEngine.Debug.Log("[Gun creator] Barrel origin was created");
        }

        private void CreateSurfaceDetector(GameObject go)
        {
            if(go == null)
                return;
            
            var surfaceDetectorGo = new GameObject("SurfaceDetector - Gun surface detector", typeof(SurfaceDetector));
            var surfaceDetector = surfaceDetectorGo.GetComponent<SurfaceDetector>();
            SurfaceDetectorModelProvider targetSurfaceDetectorModelPrvider;
            
            surfaceDetectorGo.transform.SetParent(go.transform);
            surfaceDetectorGo.transform.localPosition = Vector3.zero;
            
            if (!go.TryGetComponent(out targetSurfaceDetectorModelPrvider))
                targetSurfaceDetectorModelPrvider = go.AddComponent<SurfaceDetectorModelProvider>();

            typeof(SurfaceDetectorModelProvider)
                .GetField("surfaceDetector", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(targetSurfaceDetectorModelPrvider, surfaceDetector);
            
            UnityEngine.Debug.Log($"[Gun creator] {typeof(SurfaceDetector)} was added");
        }

        private void DeleteAllComponents(GameObject go)
        {
            if(go == null)
                return;
            
            var components = go.GetComponents<Component>();

            foreach (var component in components) 
                DestroyImmediate(component);
            
            UnityEngine.Debug.Log($"[Gun creator] components were deleted from {go.name}");
        }
    }
    
    internal enum FirearmType
    {
        Rifle,
        DMR,
    }
}