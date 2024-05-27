using Assets.Game.Scripts.Bases.BaseModels;

namespace Assets.Game.Scripts.Bases.BaseViews {
    public interface IView {
        string Id { get; set; }
        void Initialize(IModel model);
    }
}