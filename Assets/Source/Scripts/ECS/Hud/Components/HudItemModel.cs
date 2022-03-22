using System;
using Ingame.Data.Hud;
using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Hud
{
    [Serializable]
    public struct HudItemModel
    {
        [Required, Expandable]
        [AllowNesting]
        public HudItemData itemData;
        [Required]
        [AllowNesting]
        public Animator itemAnimator;
    }
}