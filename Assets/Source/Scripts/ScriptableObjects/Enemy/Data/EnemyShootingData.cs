using UnityEngine;

[CreateAssetMenu(menuName = "Ingame/Enemy/Data/Shooting", fileName = "EnemyShootingData")]
public class EnemyShootingData : ScriptableObject
{
    [SerializeField][Min(0)]
    private float timer;
    [SerializeField][Min(0)]
    private float accuracy;
    [SerializeField][Min(0)] 
    private float distance;
    [SerializeField][Min(0)] 
    private float damage;
    
    public float Timer =>timer;
    public float Accuracy => accuracy;
    public float Distance => distance;
    public float Damage => damage;
}
