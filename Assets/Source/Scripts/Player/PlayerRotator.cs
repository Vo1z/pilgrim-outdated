using System;
using Ingame.Player.HUD;
using UnityEngine;
using Zenject;

namespace Ingame.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        [Inject] private PlayerInputReceiver _playerInputReceiver;
        [Inject] private PlayerData _playerData;
        [Inject] private PlayerHUD _playerHUD;

        private float hudLocalRotationX = 0f;

        public event Action<Vector2> OnRotationPerformed; 

        private void Awake()
        {
            _playerInputReceiver.OnRotationDeltaInputReceived += Rotate;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.OnRotationDeltaInputReceived -= Rotate;
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Rotate(Vector2 direction)
        {
            var yRotation = direction.y * _playerData.Sensitivity * Time.deltaTime;
            var xRotation = direction.x * _playerData.Sensitivity * Time.deltaTime;

            hudLocalRotationX -= yRotation;
            hudLocalRotationX = Mathf.Clamp(hudLocalRotationX, -90, 90);

            transform.Rotate(Vector3.up * xRotation);
            _playerHUD.transform.localRotation = Quaternion.Euler(hudLocalRotationX, 0, 0);
            
            OnRotationPerformed?.Invoke(direction);
        }
    }
}