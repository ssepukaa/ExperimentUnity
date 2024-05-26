using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.Interfaces;

namespace Assets.Game.Scripts.Bases.BaseControllers
{
    public interface IController: IUpdatable {
        string Id { get; }
        IModel Model { get; }
        void Run();
    }
}