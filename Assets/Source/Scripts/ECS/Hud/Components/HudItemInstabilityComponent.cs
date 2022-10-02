using UnityEngine;

namespace Ingame.Hud
{
	public struct HudItemInstabilityComponent
	{
		public Vector3 currentMovementDirection;
		public float currentMovementSpeed;
		public float timeLeftMoving;

		public float currentInstability;
	}
}