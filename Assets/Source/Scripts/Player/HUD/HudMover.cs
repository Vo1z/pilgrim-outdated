using System.Collections;
using UnityEngine;
using Zenject;

namespace Ingame.PlayerLegacy.HUD
{
    public class HudMover : MonoBehaviour
    {
        [Inject] private PlayerData _playerData;
        [Inject] private PlayerInputReceiver _playerInputReceiver;
        
        private Vector3 _initialHudLocalPosition;
        private Quaternion _initialHudLocalRotation;
        private LeanDirection _currentLeanDirection = LeanDirection.None;

        private void Awake()
        {
            _initialHudLocalPosition = transform.localPosition;
            _initialHudLocalRotation = transform.localRotation;
            
            _playerInputReceiver.OnLeanInputReceived += Lean;
            
            Application.targetFrameRate = 240;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.OnLeanInputReceived -= Lean;
        }

        private IEnumerator LeanRoutine(Vector3 targetLocalPos, Quaternion targetLocalRotation)
        {
            while (Vector3.Distance(transform.localPosition, targetLocalPos) > .001f || 
                   Vector3.Distance(transform.eulerAngles, targetLocalRotation.eulerAngles) > .001f)
            {
                //todo remove hardcode
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, .1f);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetLocalRotation, .1f);
                
                yield return null;
            }

            transform.localPosition = targetLocalPos;
            transform.localRotation = targetLocalRotation;
        }

        private void Lean(LeanDirection inputLeanDirection)
        {
            _currentLeanDirection = _currentLeanDirection == inputLeanDirection ? LeanDirection.None : inputLeanDirection;

            var verticalPositionOffset = Vector3.down * _playerData.LeanDistanceOffset / 2;
            var horizontalPositionOffset = Vector3.right * _playerData.LeanDistanceOffset;
            
            var leanHudPosition = _currentLeanDirection switch
            {
                LeanDirection.Left => _initialHudLocalPosition - horizontalPositionOffset + verticalPositionOffset,
                LeanDirection.Right => _initialHudLocalPosition + horizontalPositionOffset + verticalPositionOffset,
                LeanDirection.None => _initialHudLocalPosition,
                _ => _initialHudLocalPosition
            };
            
            var leanHudRotation = _currentLeanDirection switch
            {
                LeanDirection.Left => _initialHudLocalRotation * Quaternion.AngleAxis(_playerData.LeanAngleOffset, Vector3.forward),
                LeanDirection.Right => _initialHudLocalRotation * Quaternion.AngleAxis(-_playerData.LeanAngleOffset, Vector3.forward),
                LeanDirection.None => _initialHudLocalRotation,
                _ => _initialHudLocalRotation
            };

            StopAllCoroutines();
            StartCoroutine(LeanRoutine(leanHudPosition, leanHudRotation));
        }
    }
}