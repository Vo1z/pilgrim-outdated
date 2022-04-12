using Cinemachine;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame.CameraWork
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCameraModelProvider : MonoProvider<VirtualCameraModel>
    {
        [Inject]
        private void Construct()
        {
            value = new VirtualCameraModel
            {
                virtualCamera = GetComponent<CinemachineVirtualCamera>()
            };
        }
    }
}