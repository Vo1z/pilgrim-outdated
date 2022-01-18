using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class CrouchSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CharacterControllerModel, CrouchRequest> _crouchFilter;

        public void Run()
        {
            foreach (var i in _crouchFilter)
            {
                ref var characterControllerModel = ref _crouchFilter.Get1(i);
                ref var crouchReq = ref _crouchFilter.Get2(i);
                var characterController = characterControllerModel.characterController;

                characterController.height = Mathf.Lerp(characterController.height, crouchReq.height, crouchReq.changeHeightSpeed * Time.deltaTime);
                if (Math.Abs(characterController.height - crouchReq.height) < .001f)
                {
                    characterController.height = crouchReq.height;
                    _crouchFilter.GetEntity(i).Del<CrouchRequest>();
                }
            }
        }
    }
}