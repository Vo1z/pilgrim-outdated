using System.Threading.Tasks;
using Ingame.Guns;
using UnityEngine;
using Zenject;

namespace Ingame.PlayerLegacy.HUD
{
    public sealed class HudGunMover : MonoBehaviour
    {
        [Inject] private PlayerObserver _playerObserver;
        [Inject] private PlayerMover _playerMover;
        [Inject(Id = "Hands")] private Transform _hands;

        private const float GUN_CLIPPING_MOVEMENT_SPEED = 4f;

        private Quaternion _initialGunLocalRotation;
        private Vector3 _initialGunLocalPosition;
        private Transform _gunTransform;
        private GunObserver _gunObserver;
        private GunSurfaceDetector _gunSurfaceDetector;
        private GunData _gun;
        
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
            if (_gunObserver == null || _gunTransform == null || _gun == null)
                return;

            var hudRotationDutToDeltaMovementTask = Task.Run(GetHudRotationDueToDeltaMovement);
            var hudRotationDutToDeltaRotationTask = Task.Run(GetHudRotationDueToDeltaRotation);
            
            MoveGunDueToSurfaceInteraction();

            var rotationOffset = _gun.RotationSpeed * Time.fixedDeltaTime;
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
            
            var xMovementAngle = _gun.RotationMovementAngleMultiplierX * deltaMovementInAngle.y;
            xMovementAngle = Mathf.Clamp(xMovementAngle, _gun.MinMaxRotationMovementAngleX.x, _gun.MinMaxRotationMovementAngleX.y);
            xMovementAngle *= _gun.InverseRotationMovementX;
            
            var zMovementAngle = _gun.RotationMovementAngleMultiplierZ * deltaMovementInAngle.x;
            zMovementAngle = Mathf.Clamp(zMovementAngle, _gun.MinMaxRotationMovementAngleZ.x, _gun.MinMaxRotationMovementAngleZ.y);
            zMovementAngle *= _gun.InverseRotationMovementZ;
            
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
            
            var xRotationAngle = _gun.RotationAngleMultiplierX * deltaRotationInputInAngle.y;
            xRotationAngle = Mathf.Clamp(xRotationAngle, _gun.MinMaxRotationAngleX.x, _gun.MinMaxRotationAngleX.y);
            xRotationAngle *= _gun.InverseRotationX;

            var yRotationAngle = _gun.RotationAngleMultiplierY * deltaRotationInputInAngle.x;
            yRotationAngle = Mathf.Clamp(yRotationAngle, _gun.MinMaxRotationAngleY.x, _gun.MinMaxRotationAngleY.y);
            yRotationAngle *= _gun.InverseRotationY;

            var zRotationAngle = _gun.RotationAngleMultiplierZ * deltaRotationInputInAngle.x;
            zRotationAngle = Mathf.Clamp(zRotationAngle, _gun.MinMaxRotationAngleZ.x, _gun.MinMaxRotationAngleZ.y);
            zRotationAngle *= _gun.InverseRotationZ;

            var resultRotation = Quaternion.AngleAxis(xRotationAngle, Vector3.right) *
                                 Quaternion.AngleAxis(yRotationAngle, Vector3.up) *
                                 Quaternion.AngleAxis(zRotationAngle, Vector3.forward);

            return resultRotation;
        }

        private void MoveGunDueToSurfaceInteraction()
        {
            var gunSurfaceDetectionResult = _gunSurfaceDetector.SurfaceDetection;
            
            if(gunSurfaceDetectionResult == SurfaceDetection.SameSpot)
                return;
                
            var movementDirectionZ = gunSurfaceDetectionResult == SurfaceDetection.Detection ? -_gun.MaximumClippingOffset : 0;
            var nextGunLocalPos = _initialGunLocalPosition + Vector3.forward * movementDirectionZ;
            
            _gunTransform.localPosition = Vector3.Lerp(_gunTransform.localPosition, nextGunLocalPos, GUN_CLIPPING_MOVEMENT_SPEED * Time.fixedDeltaTime);
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
            _gunSurfaceDetector = _gunObserver.GunSurfaceDetector;
            _gun = _gunObserver.GunData;
            _gunTransform = _gunObserver.transform;
            _gunTransform.parent = transform;
            _initialGunLocalRotation = _gunTransform.localRotation;
            _initialGunLocalPosition = _gunTransform.localPosition;
            _hands.parent = _gunTransform;
            
            _playerMover.OnMovementPerformed += SetDeltaMovement;
            _playerMover.OnRotationPerformed += SetDeltaRotation;
        }

        private void ReleaseGunFromHands()
        {
            _gunTransform.parent = null;
            _gunObserver = null;
            _gunSurfaceDetector = null;
            _gun = null;
            _gunTransform = null;
            _hands.parent = transform;
            
            _playerMover.OnMovementPerformed -= SetDeltaMovement;
            _playerMover.OnRotationPerformed -= SetDeltaRotation;
        }
    }
}