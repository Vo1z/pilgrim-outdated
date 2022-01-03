using System;
using Ingame.Guns;
using UnityEngine;
using Zenject;

namespace Ingame.Player.HUD
{
    public sealed class HudRotator : MonoBehaviour
    {
        [Inject] private PlayerObserver _playerObserver;
        [Inject] private PlayerMovementController _playerMovementController;
        [Inject] private PlayerRotator _playerRotator;
        [Inject] private PlayerHUD _playerHUD;
        [Inject(Id = "Hands")] private Transform _hands;
        
        private Quaternion _initialGunLocalRotation;
        private Transform _gunTransform;
        private GunObserver _gunObserver;
        private GunStatsData _gunStats;

        private Vector2 _deltaRotation; 
        private Vector3 _deltaMovement; 

        private void Awake()
        {
            _playerObserver.OnGunInHandsTaken += PlaceGunInHands;
        }

        private void OnDestroy()
        {
            _playerObserver.OnGunInHandsTaken -= PlaceGunInHands;
        }

        private void Update()
        {
            if (_gunObserver == null || _gunTransform == null || _gunStats == null)
                return;
            
            var deltaInputInAngle = _deltaRotation * PlayerInputReceiver.ANGLE_FOR_ONE_SCREEN_PIXEL;
            deltaInputInAngle.x = Mathf.Clamp(deltaInputInAngle.x, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            deltaInputInAngle.y = Mathf.Clamp(deltaInputInAngle.y, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            
            var xRotationAngle = _gunStats.RotationAngleMultiplierX * deltaInputInAngle.y;
            xRotationAngle = Mathf.Clamp(xRotationAngle, _gunStats.MinMaxRotationAngleX.x, _gunStats.MinMaxRotationAngleX.y);
            xRotationAngle *= _gunStats.InverseRotationX;

            var yRotationAngle = _gunStats.RotationAngleMultiplierY * deltaInputInAngle.x;
            yRotationAngle = Mathf.Clamp(yRotationAngle, _gunStats.MinMaxRotationAngleY.x, _gunStats.MinMaxRotationAngleY.y);
            yRotationAngle *= _gunStats.InverseRotationY;

            var zRotationAngle = _gunStats.RotationAngleMultiplierZ * deltaInputInAngle.x;
            zRotationAngle = Mathf.Clamp(zRotationAngle, _gunStats.MinMaxRotationAngleZ.x, _gunStats.MinMaxRotationAngleZ.y);
            zRotationAngle *= _gunStats.InverseRotationZ;
            
            var rotationOffset = _gunStats.RotationSpeed * Time.deltaTime;
            var targetRotation = _initialGunLocalRotation *
                                 Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up) *
                                 Quaternion.AngleAxis(zRotationAngle, Vector3.forward);
            _gunTransform.localRotation = Quaternion.Slerp(_gunTransform.localRotation, targetRotation, rotationOffset);
        }

        private void RotateGunDueMovement(Vector3 deltaMovement)
        {
            _deltaMovement = deltaMovement;
        }

        private void RotateGunDueRotation(Vector2 deltaRotationInput)
        {
            _deltaRotation = deltaRotationInput;
        }

        private void PlaceGunInHands(GunObserver gunObserver)
        {
            _gunObserver = gunObserver;
            _gunStats = _gunObserver.GunStatsData;
            _gunTransform = _gunObserver.transform;
            _gunTransform.parent = _playerHUD.transform;
            _initialGunLocalRotation = _gunTransform.localRotation;
            _hands.parent = _gunTransform;
            
            _playerMovementController.OnMovementPerformed += RotateGunDueMovement;
            _playerRotator.OnRotationPerformed += RotateGunDueRotation;
        }

        private void ReleaseGunFromHands()
        {
            _gunTransform.parent = null;
            _gunObserver = null;
            _gunStats = null;
            _gunTransform = null;
            _hands.parent = _playerHUD.transform;
            
            _playerMovementController.OnMovementPerformed -= RotateGunDueMovement;
            _playerRotator.OnRotationPerformed -= RotateGunDueRotation;
        }
    }
}