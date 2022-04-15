
using UnityEngine;

[CreateAssetMenu(menuName = "Ingame/Enemy/Data/Shooting", fileName = "EnemyShootingData")]
public sealed class EnemyShootingData : ScriptableObject
{
    [SerializeField][Min(0)]
    private float timer;
    [SerializeField][Min(0)]
    private float accuracy;
    [SerializeField][Min(0)] 
    private float distance;
    [SerializeField][Min(0)] 
    private float damage;

    [SerializeField] [Min(0)] 
    private int maxAmountOfAmmunition;

    [SerializeField]
    [Min(0)] private float reloadTime;
    
    [SerializeField] [Min(0)] 
    private float continuousAttackDuration;
    public float Timer =>timer;
    public float Accuracy => accuracy;
    public float Distance => distance;
    public float Damage => damage;
    public int MaxAmountOfAmmunition => maxAmountOfAmmunition;
    public float ReloadTime => reloadTime;
    public float ContinuousAttackDuration=> continuousAttackDuration;
}