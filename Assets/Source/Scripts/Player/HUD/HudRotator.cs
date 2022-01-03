using System.Threading.Tasks;
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

        private async void Update()
        {
            if (_gunObserver == null || _gunTransform == null || _gunStats == null)
                return;

            var hudRotationDutToDeltaMovementTask = Task.Run(GetHudRotationDueToDeltaMovement);
            var hudRotationDutToDeltaRotationTask = Task.Run(GetHudRotationDueToDeltaRotation);

            var rotationOffset = _gunStats.RotationSpeed * Time.deltaTime;
            var targetRotation = _initialGunLocalRotation * 
                                 await hudRotationDutToDeltaMovementTask *
                                 await hudRotationDutToDeltaRotationTask;
            
            _gunTransform.localRotation = Quaternion.Slerp(_gunTransform.localRotation, targetRotation, rotationOffset);
        }

        private Quaternion GetHudRotationDueToDeltaMovement()
        {
            var deltaMovementInAngle = _deltaMovement * PlayerInputReceiver.ANGLE_FOR_ONE_SCREEN_PIXEL;
            deltaMovementInAngle.x = Mathf.Clamp(deltaMovementInAngle.x, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            deltaMovementInAngle.y = Mathf.Clamp(deltaMovementInAngle.y, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            // deltaMovementInAngle.z = Mathf.Clamp(deltaMovementInAngle.z, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);

            if (Mathf.Abs(deltaMovementInAngle.y) < 2f * PlayerInputReceiver.ANGLE_FOR_ONE_SCREEN_PIXEL)
                deltaMovementInAngle.y = 0;
            
            var xMovementAngle = _gunStats.RotationMovementAngleMultiplierX * deltaMovementInAngle.y;
            xMovementAngle = Mathf.Clamp(xMovementAngle, _gunStats.MinMaxRotationMovementAngleX.x, _gunStats.MinMaxRotationMovementAngleX.y);
            xMovementAngle *= _gunStats.InverseRotationMovementX;
            
            var zMovementAngle = _gunStats.RotationMovementAngleMultiplierZ * deltaMovementInAngle.x;
            zMovementAngle = Mathf.Clamp(zMovementAngle, _gunStats.MinMaxRotationMovementAngleZ.x, _gunStats.MinMaxRotationMovementAngleZ.y);
            zMovementAngle *= _gunStats.InverseRotationMovementZ;
            
            // var yMovementAngle = 10 * deltaMovementInAngle.z;
            // yMovementAngle = Mathf.Clamp(yMovementAngle, -10, 10);
            // yMovementAngle *= -1;

            var resultRotation = Quaternion.AngleAxis(xMovementAngle, Vector3.right) 
                                        * Quaternion.AngleAxis(zMovementAngle, Vector3.forward);
                                        // * Quaternion.AngleAxis(zMovementAngle, Vector3.forward);

            return resultRotation;
        }

        private Quaternion GetHudRotationDueToDeltaRotation()
        {
            var deltaRotationInputInAngle = _deltaRotation * PlayerInputReceiver.ANGLE_FOR_ONE_SCREEN_PIXEL;
            deltaRotationInputInAngle.x = Mathf.Clamp(deltaRotationInputInAngle.x, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            deltaRotationInputInAngle.y = Mathf.Clamp(deltaRotationInputInAngle.y, -PlayerInputReceiver.INPUT_ANGLE_VARIETY, PlayerInputReceiver.INPUT_ANGLE_VARIETY);
            
            var xRotationAngle = _gunStats.RotationAngleMultiplierX * deltaRotationInputInAngle.y;
            xRotationAngle = Mathf.Clamp(xRotationAngle, _gunStats.MinMaxRotationAngleX.x, _gunStats.MinMaxRotationAngleX.y);
            xRotationAngle *= _gunStats.InverseRotationX;

            var yRotationAngle = _gunStats.RotationAngleMultiplierY * deltaRotationInputInAngle.x;
            yRotationAngle = Mathf.Clamp(yRotationAngle, _gunStats.MinMaxRotationAngleY.x, _gunStats.MinMaxRotationAngleY.y);
            yRotationAngle *= _gunStats.InverseRotationY;

            var zRotationAngle = _gunStats.RotationAngleMultiplierZ * deltaRotationInputInAngle.x;
            zRotationAngle = Mathf.Clamp(zRotationAngle, _gunStats.MinMaxRotationAngleZ.x, _gunStats.MinMaxRotationAngleZ.y);
            zRotationAngle *= _gunStats.InverseRotationZ;

            var resultRotation = Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up) *
                                 Quaternion.AngleAxis(zRotationAngle, Vector3.forward);

            return resultRotation;
        }

        private void SetDeltaMovement(Vector3 deltaMovement)
        {
            _deltaMovement = deltaMovement;
        }

        private void SetDeltaRotation(Vector2 deltaRotationInput)
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
            
            _playerMovementController.OnMovementPerformed += SetDeltaMovement;
            _playerRotator.OnRotationPerformed += SetDeltaRotation;
        }

        private void ReleaseGunFromHands()
        {
            _gunTransform.parent = null;
            _gunObserver = null;
            _gunStats = null;
            _gunTransform = null;
            _hands.parent = _playerHUD.transform;
            
            _playerMovementController.OnMovementPerformed -= SetDeltaMovement;
            _playerRotator.OnRotationPerformed -= SetDeltaRotation;
        }
    }
}