using UnityEngine;
using Zenject;

namespace Ingame.Player.HUD
{
    public class CameraRotator : MonoBehaviour
    {
        [Inject] private PlayerData _playerData;
        [Inject] private PlayerRotator _playerRotator;
        
        private Vector3 _initialPosition;
        private Vector3 _initialLocalPosition;
        private Quaternion _initialLocalRotation;

        private Vector3 _positionOnThePreviousFrame;

        private void Awake()
        {
            _initialPosition = transform.position;
            _initialLocalRotation = transform.localRotation;
            _initialLocalPosition = transform.localPosition;
            _positionOnThePreviousFrame = transform.position;
            
            _playerRotator.OnRotationPerformed += RotateCamera;
        }

        private void OnDestroy()
        {
            _playerRotator.OnRotationPerformed -= RotateCamera;
        }

        private void Update()
        {
            // var deltaMovement = Vector3.Distance(_positionOnThePreviousFrame, transform.position);
            // //todo remove hardcod
            // var cameraOffset = Mathf.Cos(Vector3.Distance(_positionOnThePreviousFrame, transform.position));
            // transform.localPosition += Vector3.up * cameraOffset;
            //
            // _positionOnThePreviousFrame = transform.position;
        }

        private void RotateCamera(Vector2 deltaRotationInput)
        {
            var deltaInputInAngle = deltaRotationInput * PlayerInputReceiver.ANGLE_FOR_ONE_SCREEN_PIXEL;
            deltaInputInAngle.x = Mathf.Clamp(deltaInputInAngle.x, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            deltaInputInAngle.y = Mathf.Clamp(deltaInputInAngle.y, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            
            var xRotationAngle = _playerData.RotationAngleMultiplierX * deltaInputInAngle.y;
            xRotationAngle = -Mathf.Clamp(xRotationAngle, _playerData.MinMaxRotationAngleX.x, _playerData.MinMaxRotationAngleX.y);
           
            var yRotationAngle = _playerData.RotationAngleMultiplierY * deltaInputInAngle.x;
            yRotationAngle = Mathf.Clamp(yRotationAngle, _playerData.MinMaxRotationAngleY.x, _playerData.MinMaxRotationAngleY.y);
            
            var targetRotation = _initialLocalRotation *
                                 Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up);
            var rotationOffset = _playerData.CameraRotationSpeed * Time.deltaTime;
            
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationOffset);
        }
    }
}