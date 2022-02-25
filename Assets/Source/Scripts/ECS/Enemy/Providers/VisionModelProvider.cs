using System.Collections.Generic;
using Ingame.Movement;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame.Enemy.Provider
{
    public class VisionModelProvider : MonoProvider<VisionModel>
    {
        [Inject]
        private void Construct()
        {
            value = new VisionModel()
            {
                Vision = value.Vision,
                Opponents = new List<Transform>(),
                Covers = new List<Transform>()
            };
        }
    }
}