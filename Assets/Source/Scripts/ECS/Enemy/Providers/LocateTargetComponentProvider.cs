using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame.Enemy.Provider
{
    public sealed class LocateTargetComponentProvider: MonoProvider<LocateTargetComponent>
    {
        private readonly float _headPosition = .75f;
        [Inject]
        private void Construct()
        {
            value = new LocateTargetComponent()
            {
                Target = null,
                TargetHeadPositionAccordingToBody = _headPosition
            };
        }
    }
}