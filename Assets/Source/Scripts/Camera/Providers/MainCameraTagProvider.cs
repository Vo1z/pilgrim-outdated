using UnityEngine;
using Voody.UniLeo;

namespace Ingame
{
    [RequireComponent(typeof(Camera))]
    internal sealed class MainCameraTagProvider : MonoProvider<MainCameraTag> { }
}