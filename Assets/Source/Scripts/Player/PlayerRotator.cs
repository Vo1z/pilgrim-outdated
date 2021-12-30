using Support.Extensions;
using UnityEngine;
using Zenject;

namespace Ingame.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        [Inject] private PlayerInputReceiver _playerInputReceiver;
        [Inject] private PlayerData _playerData;
        [Inject] private PlayerHUD _playerHUD;

        private float hudRotationX = 0f;
        
        private void Awake()
        {
            _playerInputReceiver.OnRotationDeltaInputReceived += Rotate;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.OnRotationDeltaInputReceived -= Rotate;
        }

        private void Rotate(Vector2 direction)
        {
            var yRotation = direction.y * _playerData.Sensitivity * Time.deltaTime;
            var xRotation = direction.x * _playerData.Sensitivity * Time.deltaTime;

            hudRotationX -= yRotation;
            hudRotationX = Mathf.Clamp(hudRotationX, -90, 90);

            transform.Rotate(Vector3.up * xRotation);
            _playerHUD.transform.localRotation = Quaternion.Euler(hudRotationX, 0, 0);
        }
    }
}