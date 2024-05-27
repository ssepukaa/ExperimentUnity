using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.BaseViews;

namespace Assets.Game.Scripts.Bases.BaseControllers
{
    public interface IController
    {
        string Id { get; }
        IModel Model { get; }
        BaseView  View { get; }
    }
}