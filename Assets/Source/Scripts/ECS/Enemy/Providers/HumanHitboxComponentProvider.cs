 
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame.Enemy.Provider
{
    public sealed class HumanHitboxComponentProvider : MonoProvider<HitboxModel>
    {
        [Inject]
        private void Construct()
        {
            if(value.Hitbox == null){
                value = new HitboxModel()
                {
                    Hitbox = GetComponent<CapsuleCollider>(),
                    HitboxData = value.HitboxData
                };
            }
        }
    }
}