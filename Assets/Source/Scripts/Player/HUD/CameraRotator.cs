using UnityEngine;
using Zenject;

namespace Ingame.Player.HUD
{
    public class CameraRotator : MonoBehaviour
    {
        [Inject] private PlayerData _playerData;
        [Inject] private PlayerInputReceiver _playerInputReceiver;

        private Vector3 _initialPosition;
        private Quaternion _initialLocalRotation;
        
        private Vector3 _positionOnThePreviousFrame;
        
        private void Awake()
        {
            _initialPosition = transform.position;
            _initialLocalRotation = transform.localRotation;
            _positionOnThePreviousFrame = transform.position;

            _playerInputReceiver.OnRotationDeltaInputReceived += RotateCamera;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.OnRotationDeltaInputReceived -= RotateCamera;
        }

        private void Update()
        {
            // var deltaMovement = Vector3.Distance(_initialPosition, transform.position);
            // //todo remove hardcode
            // var deltaAngle = Quaternion.AngleAxis(Mathf.Sin(deltaMovement) * 2f, Vector3.forward);
            // transform.localRotation = Quaternion.Slerp(transform.localRotation, _initialLocalRotation * deltaAngle, 10f * Time.deltaTime);
            
            // _positionOnThePreviousFrame = transform.position;
        }

        private void RotateCamera(Vector2 deltaRotationInput)
        {
            var deltaInputInAngle = deltaRotationInput * PlayerInputReceiver.ANGLE_FOR_ONE_SCREEN_PIXEL;
            
            var xRotationAngle = _playerData.RotationAngleMultiplierX * Mathf.Clamp(deltaInputInAngle.y, -1, 1);
            xRotationAngle = Mathf.Clamp(xRotationAngle, _playerData.MinMaxRotationAngleX.x, _playerData.MinMaxRotationAngleX.y);
           
            var yRotationAngle = _playerData.RotationAngleMultiplierY * Mathf.Clamp(deltaInputInAngle.x, -1, 1);
            yRotationAngle = Mathf.Clamp(yRotationAngle, _playerData.MinMaxRotationAngleY.x, _playerData.MinMaxRotationAngleY.y);
            
            var targetRotation = _initialLocalRotation *
                                 Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up);
            var rotationOffset = _playerData.CameraRotationSpeed * Time.deltaTime;
            
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationOffset);
        }
    }
}