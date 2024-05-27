using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.BaseViews;
using UnityEngine;

namespace Assets.Game.Scripts.Bases.BaseControllers {
    [System.Serializable]
    public abstract class BaseController : IController {
        protected IModel _model;
        protected BaseView _view;

        public string Id => _model.Id;
        public IModel Model => _model;
        public BaseView View => _view;

        protected BaseController(IModel model) {
            _model = model;
        }

        public void SetView(BaseView view) {
            _view = view;
            var viewComponent = _view.GetComponent<IView>();
            if (viewComponent != null) {
                viewComponent.Initialize(_model);
            }
        }

        public abstract void Run();
    }
}