using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.BaseViews;
using Assets.Game.Scripts.GameC;
using Assets.Game.Scripts.GameC.GameServices;
using UnityEngine;

namespace Assets.Game.Scripts.Bases.BaseControllers {
    [System.Serializable]
    public abstract class BaseController {
        protected BaseModel _model;
        //protected BaseModel _baseModel;
        [SerializeField] protected BaseView _view;

        [SerializeField] public string Id => _model.Id;
        public BaseModel Model => _model;
        public BaseView View => _view;
        protected bool _isInitComplete;

        protected BaseController(BaseModel model) {
            _model = model;

        }

        public void SetView(BaseView view) {
            _view = view;
           if (view != null) {
                view.Initialize(_model);
                GetComponentsOnView();
                _view.Model = Model;
                GameController.Instance.GetService<ControllerFactoryService>().AddView(_view);
            }


        }

        protected virtual void GetComponentsOnView() {
            InitServices();
        }
        protected virtual void InitServices() {
        }

        public virtual void Run() {
            if (!_isInitComplete) return;
        }
    }
}