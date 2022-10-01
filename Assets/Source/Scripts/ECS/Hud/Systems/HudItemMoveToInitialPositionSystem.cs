using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Hud
{
	public sealed class HudItemMoveToInitialPositionSystem : IEcsRunSystem
	{
		private readonly EcsFilter<HudItemModel, TransformModel, InInventoryTag, HudIsInHandsTag> _itemFilter;
		
		public void Run()
		{
			foreach (var i in _itemFilter)
			{
				ref var transformModel = ref _itemFilter.Get2(i); 
				
				var itemData = _itemFilter.Get1(i).itemData;
				var itemTransform = transformModel.transform;

				if (!itemData.IsItemMovedBackToInitialPosition)
					continue;

				var movementOffset = itemData.MoveToInitialPosSpeed * Time.deltaTime;
				itemTransform.localPosition = Vector3.Lerp(itemTransform.localPosition, transformModel.initialLocalPos,
					movementOffset);
			}
		}
	}
}