using Ingame.Hud;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Gunplay
{
	public sealed class MoveFirearmDueStabilitySystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly EcsFilter<FirearmComponent, StabilityComponent, TransformModel, HudItemModel> _firearmStabilityFilter;

		private const float FLOAT_COMPARISON_TOLERANCE = .05f;
		
		public void Init()
		{
			foreach (var i in _firearmStabilityFilter)
			{
				ref var firearmComponent = ref _firearmStabilityFilter.Get1(i);
				ref var firearmStabilityComponent = ref _firearmStabilityFilter.Get2(i);
				ref var hudItemModel = ref _firearmStabilityFilter.Get4(i);

				var firearmConfig = firearmComponent.firearmConfig;
				var minMaxTransitionTime = firearmConfig.MinMaxTransitionTime;

				firearmStabilityComponent.currentInstability = firearmConfig.DefaultInstability;
				firearmStabilityComponent.targetLocalPosition = hudItemModel.localPositionInHud;
				firearmStabilityComponent.currentTransitionTime = Random.Range(minMaxTransitionTime.x, minMaxTransitionTime.y);
			}
		}
		
		public void Run()
		{
			foreach (var i in _firearmStabilityFilter)
			{
				ref var firearmComponent = ref _firearmStabilityFilter.Get1(i);
				ref var firearmStabilityComponent = ref _firearmStabilityFilter.Get2(i);
				ref var firearmTransformModel = ref _firearmStabilityFilter.Get3(i);

				var firearmTransform = firearmTransformModel.transform;
				var firearmConfig = firearmComponent.firearmConfig;
				
				float currentInstability = Mathf.Clamp01(firearmStabilityComponent.currentInstability);
				var velocity = Vector3.zero;
				var transitionTime = firearmStabilityComponent.currentTransitionTime * Time.deltaTime;

				firearmTransform.localPosition = Vector3.MoveTowards(firearmTransform.localPosition, firearmStabilityComponent.targetLocalPosition, transitionTime);
				
				if(Vector3.SqrMagnitude(firearmTransform.localPosition - firearmStabilityComponent.targetLocalPosition) > FLOAT_COMPARISON_TOLERANCE || firearmStabilityComponent.currentTransitionTime <= 0)
					return;
				
				var localPositionInHud = _firearmStabilityFilter.Get4(i).localPositionInHud;
				var minMaxTransitionTime = firearmConfig.MinMaxTransitionTime;
				var maxOffset = firearmConfig.MaxWeaponInstabilityOffset;
				var targetDirection = Random.insideUnitSphere;
				targetDirection.z = 0;
				targetDirection = targetDirection.normalized;
				
				firearmStabilityComponent.targetLocalPosition = localPositionInHud + targetDirection * maxOffset;
				firearmStabilityComponent.currentTransitionTime = Random.Range(minMaxTransitionTime.x, minMaxTransitionTime.y);
			}
		}
	}
}