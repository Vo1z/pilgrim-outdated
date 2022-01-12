using System;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame
{
    public sealed class CharacterControllerModelProvider : MonoProvider<CharacterControllerModel>
    {
        [Inject]
        public void Construct()
        {
            if (!TryGetComponent(out CharacterController characterController))
                throw new NullReferenceException($"There is no CharacterController attached");
            
            value = new CharacterControllerModel
            {
                CharacterController = characterController
            };
        }
    }
}