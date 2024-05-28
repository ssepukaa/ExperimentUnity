using System.Collections.Generic;
using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.BaseViews;
using Assets.Game.Scripts.Bases.Interfaces;
using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.GameC.GameServices;
using Assets.Game.Scripts.GameC.GameStates;
using UnityEngine;

namespace Assets.Game.Scripts.GameC {
    public class GameController : MonoBehaviour, IGame, IController {
        public static GameController Instance;
        public string Id => "Game";

        public IModel Model => _model;
        public BaseView View { get; }

        [SerializeReference] private List<IModel> _models;
        [SerializeReference] private List<IGameService> _services;
       

        [SerializeField] private GameConfig _gameConfig;

        private IGameState _currentGameState;
        private IGameState _previousGameState;
        private IModel _model;
        private List<IGameState> _states = new List<IGameState>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            InitializeGameStates();
            InitializeGameServices();
            ChangeGameState<LoadingGameState>();
        }

        private void InitializeGameStates() {
            _states.Add(new LoadingGameState(this));
            _states.Add(new GameplayGameState(this));
            _states.Add(new PausedGameState(this));
        }

        private void InitializeGameServices() {
            var randomService = new RandomService();
            _services = new List<IGameService>
            {
                new SaveLoadService(),
                new ControllerFactoryService(),
                new LevelGenerationService(_gameConfig, randomService, 2f), // Передаем конфигурацию и сервис случайных чисел
                randomService
            };
        }

        private void Update() {
            _currentGameState?.Execute();
            RunControllers();
        }

        private void RunControllers() {
            var controllerFactory = GetService<ControllerFactoryService>();
            var updatableControllers = controllerFactory.GetUpdatableControllers();
            int count = updatableControllers.Count;
            for (int i = 0; i < count; i++) {
                updatableControllers[i].Run();
            }
        }

        private void FixedUpdate() {
            FixedRunControllers();
        }

        private void FixedRunControllers() {
            var controllerFactory = GetService<ControllerFactoryService>();
            var fixedUpdatableControllers = controllerFactory.GetFixedUpdatableControllers();
            int count = fixedUpdatableControllers.Count;
            for (int i = 0; i < count; i++) {
                fixedUpdatableControllers[i].RunFixed();
            }
        }

        private void LateUpdate() {
            LateRunControllers();
        }

        private void LateRunControllers() {
            var controllerFactory = GetService<ControllerFactoryService>();
            var lateUpdatableControllers = controllerFactory.GetLateUpdatableControllers();
            int count = lateUpdatableControllers.Count;
            for (int i = 0; i < count; i++) {
                lateUpdatableControllers[i].RunLate();
            }
        }

        public void ChangeGameState<T>() where T : IGameState {
            var stateType = typeof(T);
            var newState = _states.Find(state => state.GetType() == stateType);
            if (newState != null) {
                if (_currentGameState != null && _currentGameState.Equals(newState)) {
                    Debug.Log($"This state {newState.ToString()} is already turned on!");
                    return;
                }

                _currentGameState?.Exit();
                _previousGameState = _currentGameState;
                _currentGameState = newState;
                _currentGameState.Enter();
            } else {
                Debug.LogError($"State {stateType} is not initialized.");
            }
        }

        public void CreateNewListControllers(List<IModel> models) {
            var controllerFactory = GetService<ControllerFactoryService>();
            controllerFactory.CreateNewListControllers(models);
        }
        // Новый метод для создания контроллеров на основании конфига

        public void AddController(IController controller) {
            var controllerFactory = GetService<ControllerFactoryService>();
            controllerFactory.AddController(controller);
            var view = (controller as BaseController)?.View;
            
        }

        public void RemoveController(IController controller) {
            var controllerFactory = GetService<ControllerFactoryService>();
            controllerFactory.RemoveController(controller);
            var view = (controller as BaseController)?.View;
            
        }
        public T GetService<T>() where T : IGameService {
            return (T)_services.Find(service => service is T);
        }

        public void Save()
        {
            GetService<SaveLoadService>().Save(GetService<ControllerFactoryService>().Controllers);
        }
    }
}
