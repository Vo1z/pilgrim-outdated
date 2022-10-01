using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Gunplay
{
	[CreateAssetMenu(menuName = "GunPlay/FirearmConfig", fileName = "NewFirearmConfig")]
	public sealed class FirearmConfig : ScriptableObject
	{
		[SerializeField] [Min(0)] private float damage = 30f;
		[SerializeField] [Range(0, 1)] private float defaultInstability;
		[MinMaxSlider(0, 50)]
		[SerializeField] private Vector2 minMaxTransitionTime;
		[SerializeField] [Range(0, 20f)] private float maxWeaponInstabilityOffset = 3f;
		

		public float Damage => damage;
		public float DefaultInstability => defaultInstability;
		public Vector2 MinMaxTransitionTime => minMaxTransitionTime;
		public float MaxWeaponInstabilityOffset => maxWeaponInstabilityOffset;
	}
}