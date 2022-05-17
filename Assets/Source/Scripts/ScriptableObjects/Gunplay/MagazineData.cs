using UnityEngine;

namespace Ingame.Data.Gunplay
{
    [CreateAssetMenu(menuName = "Ingame/MagazineData", fileName = "NewMagazineData")]
    public class MagazineData : ScriptableObject
    {
        [SerializeField] [Min(0)] private int maxAmountOfAmmoInMagazine = 30;
        [SerializeField] private AmmoType ammoType;
        
        public int MaxAmountOfAmmoInMagazine => maxAmountOfAmmoInMagazine;
        public AmmoType AmmoType => ammoType;
    }

    public enum AmmoType
    {
        Ammo5_56,
        Ammo7_62,
        Ammo9_19
    }
}