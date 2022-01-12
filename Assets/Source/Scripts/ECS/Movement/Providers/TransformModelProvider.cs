using Voody.UniLeo;
using Zenject;

namespace Ingame
{
    public sealed class TransformModelProvider : MonoProvider<TransformModel>
    {
        [Inject]
        public void Construct()
        {
            value = new TransformModel
            {
                transform = transform
            };
        }
    }
}