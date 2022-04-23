using Support.Extensions;
using UnityEngine;

namespace Ingame.Hud
{
    public class HudAnimationGameObjectManagement : MonoBehaviour
    {
        private void TurnOffChildren()
        {
            gameObject.TurnOffChildren();
        }
        
        private void TurnOnChildren()
        {
            gameObject.TurnOnChildren();
        }
    }
}