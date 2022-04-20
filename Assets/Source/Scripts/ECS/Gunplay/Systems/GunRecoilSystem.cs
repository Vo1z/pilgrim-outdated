using Ingame.Data.Gunplay;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Gunplay
{
    public sealed class GunRecoilSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GunModel, TransformModel, AwaitingShotTag> _gunFilter;
        
        public void Run()
        {
            foreach (var i in _gunFilter)
            {
                ref var gunModel = ref _gunFilter.Get1(i);
                ref var gunTransformModel = ref _gunFilter.Get2(i);
                var gunData = gunModel.gunData;
                var gunTransform = gunTransformModel.transform;

                var targetLocalRotation = GetGunRotationDueToRecoil(gunData, gunTransformModel);
                var targetLocalPos = GetGunLocalPositionDueToRecoil(gunData, gunTransformModel);
                var transitionSpeed = gunData.Instability * Time.deltaTime;

                
                gunTransform.localRotation = Quaternion.Lerp(gunTransform.localRotation, targetLocalRotation, transitionSpeed);
                gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, targetLocalPos, transitionSpeed);
            }
        }

        private Quaternion GetGunRotationDueToRecoil(GunData gunData, TransformModel gunTransformModel)
        {

            var recoilAngleOffset = Quaternion.AngleAxis(-gunData.VerticalRecoilForce, Vector3.left);
            var gunRotationDueRecoil = gunTransformModel.initialLocalRotation * recoilAngleOffset;
            
            return gunRotationDueRecoil;
        }

        private Vector3 GetGunLocalPositionDueToRecoil(GunData gunData, TransformModel gunTransformModel)
        {
            var localPosOffset = Vector3.back * gunData.FrontRecoilForce;
            localPosOffset = Vector3.ClampMagnitude(localPosOffset, gunData.MinMaxRecoilPositionOffset);
            var gunLocalPosDueRecoil = gunTransformModel.initialLocalPos + localPosOffset;

            return gunLocalPosDueRecoil;
        }
    }
}