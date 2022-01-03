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
        
        private Quaternion _initialGunLocalRotation;
        private Transform _gunTransform;
        private GunObserver _gunObserver;
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
            if (_gunObserver == null || _gunTransform == null || _gunStats == null)
                return;

            var deltaInputInAngle = deltaRotationInput * PlayerInputReceiver.ANGLE_FOR_ONE_SCREEN_PIXEL;
            
            var xRotationAngle = _gunStats.RotationAngleMultiplierX * Mathf.Clamp(deltaInputInAngle.y, -1, 1);
            xRotationAngle = Mathf.Clamp(xRotationAngle, _gunStats.MinMaxRotationAngleX.x, _gunStats.MinMaxRotationAngleX.y);
            xRotationAngle *= _gunStats.InverseRotationX;

            var yRotationAngle = _gunStats.RotationAngleMultiplierY * Mathf.Clamp(deltaInputInAngle.x, -1, 1);
            yRotationAngle = Mathf.Clamp(yRotationAngle, _gunStats.MinMaxRotationAngleY.x, _gunStats.MinMaxRotationAngleY.y);
            yRotationAngle *= _gunStats.InverseRotationY;
            
            var zRotationAngle = _gunStats.RotationAngleMultiplierZ * Mathf.Clamp(deltaInputInAngle.x, -1, 1);
            zRotationAngle = Mathf.Clamp(zRotationAngle, _gunStats.MinMaxRotationAngleZ.x, _gunStats.MinMaxRotationAngleZ.y);
            zRotationAngle *= _gunStats.InverseRotationZ;
            
            var rotationOffset = _gunStats.RotationSpeed * Time.deltaTime;
            var targetRotation = _initialGunLocalRotation *
                                 Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up) *
                                 Quaternion.AngleAxis(zRotationAngle, Vector3.forward);

            _gunTransform.localRotation = Quaternion.Slerp(_gunTransform.localRotation, targetRotation, rotationOffset);
        }

        private void PlaceGunInHands(GunObserver gunObserver)
        {
            _gunObserver = gunObserver;
            _gunStats = _gunObserver.GunStatsData;
            _gunTransform = _gunObserver.transform;
            _gunTransform.parent = _playerHUD.transform;
            _initialGunLocalRotation = _gunTransform.localRotation;
            
            _playerInputReceiver.OnRotationDeltaInputReceived += RotateGun;
        }

        private void ReleaseGunFromHands()
        {
            _gunTransform.parent = null;
            _gunObserver = null;
            _gunStats = null;
            _gunTransform = null;
            
            _playerInputReceiver.OnRotationDeltaInputReceived -= RotateGun;
        }
    }
}