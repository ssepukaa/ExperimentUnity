using Assets.Game.Scripts.GameC.GameServices;

namespace Assets.Game.Scripts.GameC
{
    public interface IGameController
    {
        RandomService RandomService { get; }
    }
}