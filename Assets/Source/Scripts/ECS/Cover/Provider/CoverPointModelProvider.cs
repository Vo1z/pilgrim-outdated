using Voody.UniLeo;
using Zenject;

namespace Ingame.Cover
{
    public class CoverPointModelProvider : MonoProvider<CoverPointModel>
    {
        [Inject]
        private void Construct()
        {
            value = new CoverPointModel
            {
                Tag = this.tag,
                Point = this.transform
            };
        }
    }
}