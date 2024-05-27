using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.BaseViews;
using Assets.Game.Scripts.Bases.Interfaces;
using Assets.Game.Scripts.MicrobeC;
using Assets.Game.Scripts.PlayerC;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.GameC.GameServices {
    [Serializable]
    public class ControllerFactoryService : IGameService {
        [SerializeField] private List<IController> _controllers;
        [SerializeField] private List<BaseView> _views; // Добавление списка View
        [SerializeField] private List<IUpdatable> _updatableControllers;
        [SerializeField] private List<IFixedUpdatable> _fixedUpdatableControllers;
        [SerializeField] private List<ILateUpdatable> _lateUpdatableControllers;

        private Transform _parentTransform;

        public ControllerFactoryService() {
            _controllers = new List<IController>();
            _views = new List<BaseView>();
            _updatableControllers = new List<IUpdatable>();
            _fixedUpdatableControllers = new List<IFixedUpdatable>();
            _lateUpdatableControllers = new List<ILateUpdatable>();

            // Создаем родительский объект в точке (0, 0, 0) и задаем ему имя
            GameObject parentGameObject = new GameObject("SpawnedObjects");
            parentGameObject.transform.position = Vector3.zero;
            _parentTransform = parentGameObject.transform;
        }

        public void CreateNewListControllers(List<IModel> models) {
            _controllers.Clear();
            _views.Clear();
            _updatableControllers.Clear();
            _fixedUpdatableControllers.Clear();
            _lateUpdatableControllers.Clear();

            foreach (var model in models) {
                AddController(CreateController(model));
            }
        }

        public void AddController(IController controller) {
            _controllers.Add(controller);
            if (controller is IUpdatable updatableController) {
                _updatableControllers.Add(updatableController);
            }
            if (controller is IFixedUpdatable fixedUpdatableController) {
                _fixedUpdatableControllers.Add(fixedUpdatableController);
            }
            if (controller is ILateUpdatable lateUpdatableController) {
                _lateUpdatableControllers.Add(lateUpdatableController);
            }

          
        }

        public void RemoveController(IController controller) {
            _controllers.Remove(controller);
            if (controller is IUpdatable updatableController) {
                _updatableControllers.Remove(updatableController);
            }
            if (controller is IFixedUpdatable fixedUpdatableController) {
                _fixedUpdatableControllers.Remove(fixedUpdatableController);
            }
            if (controller is ILateUpdatable lateUpdatableController) {
                _lateUpdatableControllers.Remove(lateUpdatableController);
            }

            // Удаление View
            RemoveView(controller.View);
        }

        public List<IUpdatable> GetUpdatableControllers() {
            return _updatableControllers;
        }

        public List<IFixedUpdatable> GetFixedUpdatableControllers() {
            return _fixedUpdatableControllers;
        }

        public List<ILateUpdatable> GetLateUpdatableControllers() {
            return _lateUpdatableControllers;
        }

        public IController CreateController(IModel model) {
            var prefab = Addressables.LoadAssetAsync<GameObject>(model.PrefabReference).WaitForCompletion();
            if (prefab == null) {
                throw new ArgumentException("Prefab not found: " + model.PrefabReference);
            }
            var gameObject = UnityEngine.Object.Instantiate(prefab, _parentTransform);
            var view = gameObject.GetComponent<BaseView>();
            if (model is PlayerModel playerModel) {
                var controller = new PlayerController(playerModel);
                controller.SetView(view);
                return controller;
            }
            if (model is MicrobeModel microbeModel) {
                var controller = new MicrobeController(microbeModel);
                controller.SetView(view);
                return controller;
            }
            // Добавление View
            AddView(view);
            throw new ArgumentException("Unknown model type");
        }

        private void AddView(BaseView gameObject) {
            var view = gameObject.GetComponent<BaseView>();
            if (view != null) {
                _views.Add(view);
            }
        }

        private void RemoveView(BaseView gameObject) {
            var view = gameObject.GetComponent<BaseView>();
            if (view != null) {
                _views.Remove(view);
            }
        }
    }
}
