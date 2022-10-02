﻿using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Hud
{
	public sealed class HudRecoilSystem : IEcsRunSystem
	{
		private readonly EcsFilter<PlayerModel, CharacterControllerModel> _playerModelFilter;
		private readonly EcsFilter<HudModel> _hudModelFilter;
		private readonly EcsFilter<RecoilRequest> _recoilRequestFilter;

		public void Run()
		{
			if(_hudModelFilter.IsEmpty() || _hudModelFilter.IsEmpty())
				return;

			var playerCharacterController = _playerModelFilter.Get2(0).characterController;
			ref var hudModel = ref _hudModelFilter.Get1(0);

			foreach (var i in _recoilRequestFilter)
			{
				ref var recoilRequest = ref _recoilRequestFilter.Get1(i);

				playerCharacterController.transform.eulerAngles += Vector3.up * recoilRequest.angleStrength.x;
				hudModel.hudLocalRotationX -= recoilRequest.angleStrength.y;
			}
		}
	}
}