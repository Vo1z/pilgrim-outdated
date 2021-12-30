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

            hudLocalRotationX -= yRotation;
            hudLocalRotationX = Mathf.Clamp(hudLocalRotationX, -90, 90);

            transform.Rotate(Vector3.up * xRotation);
            _playerHUD.transform.localRotation = Quaternion.Euler(hudLocalRotationX, 0, 0);
        }
    }
}