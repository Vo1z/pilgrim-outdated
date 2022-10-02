using System.Runtime.CompilerServices;
using Ingame.Hud;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Gunplay
{
	public sealed class CreateRecoilRequestSystem : IEcsRunSystem
	{
		private readonly EcsWorld _world;
		private readonly EcsFilter<FirearmComponent> _shootingFirearmFilter;
		
		public void Run()
		{
			foreach (var i in _shootingFirearmFilter)
			{
				ref var firearmEntity = ref _shootingFirearmFilter.GetEntity(i);
				ref var firearmComponent = ref _shootingFirearmFilter.Get1(i);

				var firearmConfig = firearmComponent.firearmConfig;

				if (!firearmEntity.Has<AwaitingShotTag>())
				{
					firearmComponent.currentRecoilStrength = Vector2.Lerp(firearmComponent.currentRecoilStrength, Vector2.zero, firearmConfig.RecoilStabilizationSpeed * Time.deltaTime);
					continue;
				}

				var recoilBoost = firearmConfig.RecoilBoost;
				recoilBoost.x *= Random.value * GetRandomSign();

				firearmComponent.currentRecoilStrength += recoilBoost;

				ref var recoilRequest = ref _world.NewEntity().Get<RecoilRequest>();
				recoilRequest.angleStrength = firearmComponent.currentRecoilStrength;
			}
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int GetRandomSign() => Random.value < .5f ? 1 : -1;
	}
}