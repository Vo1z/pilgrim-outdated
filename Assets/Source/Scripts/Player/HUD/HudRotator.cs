using Ingame.Guns;
using UnityEngine;
using Zenject;

namespace Ingame.Player.HUD
{
    public sealed class HudRotator : MonoBehaviour
    {
        [Inject] private PlayerObserver _playerObserver;
        [Inject] private PlayerInputReceiver _playerInputReceiver;
        [Inject] private PlayerHUD _playerHUD;

        private const float ANGLE_FOR_ONE_SCREEN_PIXEL = .1f;
        
        private Quaternion _initialGunLocalRotation;
        private Transform _gunTransform;
        private Gun _gun;
        private GunStatsData _gunStats;

        private void Awake()
        {
            _playerObserver.OnGunInHandsTaken += PlaceGunInHands;
        }

        private void OnDestroy()
        {
            _playerObserver.OnGunInHandsTaken -= PlaceGunInHands;
        }

        private void RotateGun(Vector2 deltaRotationInput)
        {
            if (_gun == null || _gunTransform == null || _gunStats == null)
                return;

            var deltaInputInAngle = deltaRotationInput * ANGLE_FOR_ONE_SCREEN_PIXEL;
            
            var xRotationAngle = _gunStats.RotationAngleMultiplierX * Mathf.Clamp(deltaInputInAngle.y, -1, 1);
            xRotationAngle = Mathf.Clamp(xRotationAngle, -_gunStats.MaxRotationAngleX, _gunStats.MaxRotationAngleX);
            xRotationAngle *= _gunStats.InverseRotationX;

            var yRotationAngle = _gunStats.RotationAngleMultiplierY * Mathf.Clamp(deltaInputInAngle.x, -1, 1);
            yRotationAngle = Mathf.Clamp(yRotationAngle, -_gunStats.MaxRotationAngleY, _gunStats.MaxRotationAngleY);
            yRotationAngle *= _gunStats.InverseRotationY;
            
            var zRotationAngle = _gunStats.RotationAngleMultiplierZ * Mathf.Clamp(deltaInputInAngle.x, -1, 1);
            zRotationAngle = Mathf.Clamp(zRotationAngle, -_gunStats.MaxRotationAngleZ, _gunStats.MaxRotationAngleZ);
            zRotationAngle *= _gunStats.InverseRotationZ;
            
            var rotationOffset = _gunStats.RotationSpeed * Time.deltaTime;
            var targetRotation = _initialGunLocalRotation *
                                 Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up) *
                                 Quaternion.AngleAxis(zRotationAngle, Vector3.forward);

            _gunTransform.localRotation = Quaternion.Slerp(_gunTransform.localRotation, targetRotation, rotationOffset);
        }

        private void PlaceGunInHands(Gun gun)
        {
            _gun = gun;
            _gunStats = _gun.GunStatsData;
            _gunTransform = _gun.transform;
            _gunTransform.parent = _playerHUD.transform;
            _initialGunLocalRotation = _gunTransform.localRotation;
            
            _playerInputReceiver.OnRotationDeltaInputReceived += RotateGun;
        }

        private void ReleaseGunFromHands()
        {
            _gunTransform.parent = null;
            _gun = null;
            _gunStats = null;
            _gunTransform = null;
            
            _playerInputReceiver.OnRotationDeltaInputReceived -= RotateGun;
        }
    }
}