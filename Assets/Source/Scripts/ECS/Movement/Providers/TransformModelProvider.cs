﻿using Voody.UniLeo;
using Zenject;

namespace Ingame
{
    public sealed class TransformModelProvider : MonoProvider<TransformModel>
    {
        [Inject]
        private void Construct()
        {
            value = new TransformModel
            {
                transform = transform
            };
        }
    }
}