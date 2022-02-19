using UnityEngine;

[CreateAssetMenu(menuName = "Ingame/Enemy/Data/Shooting", fileName = "EnemyShootingData")]
public class EnemyShootingData : ScriptableObject
{
    [Min(0)]
    public float Timer;
    [Min(0)]
    public float Accuracy;
    [Min(0)] 
    public float Distance;
    [Min(0)] public float Damage;
}
