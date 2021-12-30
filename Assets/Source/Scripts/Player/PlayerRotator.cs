using UnityEngine;
using Zenject;

namespace Ingame.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        [Inject] private PlayerInputReceiver _playerInputReceiver;
        [Inject] private PlayerData _playerData;
        [Inject] private PlayerHUD _playerHUD; 
    
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
            var yRotation = direction.y * _playerData.Sensitivity;
            var xRotation = direction.x * _playerData.Sensitivity;

            transform.Rotate(Vector3.up * xRotation * Time.deltaTime);
            _playerHUD.transform.Rotate(Vector3.right * yRotation * Time.deltaTime);
        }
    }
}