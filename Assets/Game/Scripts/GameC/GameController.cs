using System.Collections.Generic;
using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.GameC.GameServices;
using Assets.Game.Scripts.GameC.GameStates;
using UnityEngine;

namespace Assets.Game.Scripts.GameC {

    public class GameController : MonoBehaviour, IGameController {
        public static GameController Instance;
        public string Id => "Game";

        public GameModel Model => _model;
        public GameView View => _view;

        [SerializeReference] private List<BaseModel> _models;
        [SerializeReference] private List<BaseGameService> _services;


        [SerializeField] private GameConfig _gameConfig;

        private BaseGameState _currentGameState;
        private BaseGameState _previousGameState;
        private GameModel _model = new GameModel();
        private GameView _view;
        private List<BaseGameState> _states = new List<BaseGameState>();
        private RandomService _randomService;
        private SaveLoadService _saveLoadService;
        private ControllerFactoryService _controllerService;
        private LevelGenerationService _levelGenerateService;
        private AnimationPoolService _animationSevrvice;

        public RandomService RandomService => GetService<RandomService>();

        public GameConfig Config => _gameConfig;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

            _gameConfig = new GameConfig();
        }

        private void Start() {
            _view = GetComponent<GameView>();
            InitializeGameServices();
            InitializeGameStates();
            Invoke("LoadingGame", 1f);

        }

        private void LoadingGame() {
            ChangeGameState<LoadingGameState>();
        }

        private void InitializeGameServices() {

            _services = new List<BaseGameService>();
            _services.Add(_randomService = new RandomService());
            _services.Add(_saveLoadService = new SaveLoadService());
            _services.Add(_controllerService = new ControllerFactoryService(this));
            _services.Add(_levelGenerateService = new LevelGenerationService(this, Config, 10f)); // Передаем конфигурацию и сервис случайных чисел
            _services.Add(_animationSevrvice = new AnimationPoolService());
        }
        private void InitializeGameStates() {
            _states.Add(new LoadingGameState(this));
            _states.Add(new GameplayGameState(this));
            _states.Add(new PausedGameState(this));
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

        public void ChangeGameState<T>() where T : BaseGameState {
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

        public void CreateNewListControllers(List<BaseModel> models) {
            _models = models;
            var controllerFactory = GetService<ControllerFactoryService>();
            controllerFactory.CreateNewListControllers(models);
        }
        // Новый метод для создания контроллеров на основании конфига

        public void AddController(BaseController controller) {
            var controllerFactory = GetService<ControllerFactoryService>();
            controllerFactory.AddController(controller);
            var view = (controller as BaseController)?.View;

        }

        public void RemoveController(BaseController controller) {
            var controllerFactory = GetService<ControllerFactoryService>();
            controllerFactory.RemoveController(controller);
            var view = (controller as BaseController)?.View;

        }
        public T GetService<T>() where T : BaseGameService {
            return (T)_services.Find(service => service is T);
        }

        public void Save() {
            GetService<SaveLoadService>().Save(GetService<ControllerFactoryService>().Controllers);
        }

        public AnimationPoolService GetAnimationService() {
            return GetService<AnimationPoolService>();
        }
        public UnitConfig GetUnitConfig(MicrobeType typeName) {
            return _gameConfig.Units.Find(unit => unit.TypeName == typeName);
        }
    }
}
