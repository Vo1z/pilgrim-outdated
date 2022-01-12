using Leopotam.Ecs;

namespace Ingame
{
    public static class LeoEcsExtensions
    {
        public static void SendMessage(this EcsWorld ecsWorld, string message)
        {
            ecsWorld.NewEntity().Get<DebugRequest>().message = message;
        }
    }
}