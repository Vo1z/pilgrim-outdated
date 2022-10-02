using UnityEngine;

namespace Ingame.Hud
{
	public struct HudItemInstabilityComponent
	{
		public Vector2 currentMovementDirection;
		public float currentMovementSpeed;
		public float timeLeftMoving;
		public float sinTime;
		
		public float currentInstability;
	}
}