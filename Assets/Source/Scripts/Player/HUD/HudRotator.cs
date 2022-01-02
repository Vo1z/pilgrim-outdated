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
            if(_gun == null || _gunTransform == null)
                return;

            var deltaInputInAngle = deltaRotationInput * ANGLE_FOR_ONE_SCREEN_PIXEL;
            var rotationAngle = GunStatsData.MAX_ROTATION_ANGLE * Mathf.Clamp(deltaInputInAngle.x, -1, 1);
            var rotationOffset = _gun.GunStatsData.RotationSpeed * Time.deltaTime;
            var targetRotation = _initialGunLocalRotation * Quaternion.AngleAxis(rotationAngle, Vector3.forward);
            
            _gunTransform.localRotation = Quaternion.Slerp(_gunTransform.localRotation, targetRotation, rotationOffset);
        }

        private void PlaceGunInHands(Gun gun)
        {
            _gun = gun;
            _gunTransform = _gun.transform;
            _gunTransform.parent = _playerHUD.transform;
            _initialGunLocalRotation = _gunTransform.localRotation;
            
            _playerInputReceiver.OnRotationDeltaInputReceived += RotateGun;
        }

        private void ReleaseGunFromHands()
        {
            _gunTransform.parent = null;
            _gun = null;
            _gunTransform = null;
            
            _playerInputReceiver.OnRotationDeltaInputReceived -= RotateGun;
        }
    }
}