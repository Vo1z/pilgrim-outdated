using UnityEngine;
using Voody.UniLeo;

namespace Ingame.CameraWork
{
    [RequireComponent(typeof(Camera))]
    internal sealed class MainCameraTagProvider : MonoProvider<MainCameraTag> { }
}