using UnityEngine;

namespace Ingame.Gunplay
{
	public struct StabilityComponent
	{
		public Vector3 targetLocalPosition;
		public float currentTransitionTime;
		
		public float currentInstability;
	}
}