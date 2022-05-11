using UnityEngine;

namespace Ingame.Data.Gunplay
{
    [CreateAssetMenu(menuName = "Ingame/MagazineData", fileName = "NewMagazineData")]
    public class MagazineData : ScriptableObject
    {
        [SerializeField] [Min(0)] private int amountOfAmmoInMagazine = 30;
        [SerializeField] private AmmoType ammoType;
        
        public int AmountOfAmmoInMagazine => amountOfAmmoInMagazine;
        public AmmoType AmmoType => ammoType;
    }

    public enum AmmoType
    {
        Ammo5_56,
        Ammo7_62,
        Ammo9_19
    }
}