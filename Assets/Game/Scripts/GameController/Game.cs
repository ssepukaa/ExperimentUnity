using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.Interfaces;
using Assets.Game.Scripts.GameController.GameServices;
using Assets.Game.Scripts.GameController.GameStates;
using UnityEngine;

namespace Assets.Game.Scripts.GameController {
    public class Game : MonoBehaviour, IGame, IController {
        public static Game Instance;
        public string Id => "Game";

        public IModel Model => _model;

        [SerializeReference] private List<IController> _controllers;
        [SerializeReference] private List<IModel> _models;
        private IGameState _currentGameState;
        private IGameState _previousGameState;
        private IModel _model;
        private Dictionary<System.Type, IGameState> _states = new Dictionary<Type, IGameState>();
        private Dictionary<System.Type, IGameService> _services = new Dictionary<Type, IGameService>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

        }

        private void Start() {
            //_id = Guid.NewGuid().ToString();
            InitilizeGameStates();
            InitilizeGameServices();
            ChangeGameState<MenuGameState>();
        }
        private void InitilizeGameStates()
        {
            _states[typeof(LoadingGameState)] = new LoadingGameState(this);
            _states[typeof(PausedGameState)] = new PausedGameState(this);
        }
        private void InitilizeGameServices()
        {
            _services[typeof(SaveLoadService)] = new SaveLoadService();
        }

        void Update() {
            _currentGameState?.Execute();
            RunControllers();
        }

        private void RunControllers()
        {
            int count = _controllers.Count;
            for (int i = 0; i < count; i++) {
                _controllers[i].Run();
            }
        }

        void FixedUpdate() {

        }

        void LateUpdate() {

        }
        public void Run() {

        }
        public void ChangeGameState<T>() where T: IGameState {
            var stateType = typeof(T);
            if (_states.TryGetValue(stateType, out IGameState newState)) {
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

        public void CreateNewListControllers(List<IController> controllers)
        {
            _controllers = controllers;
        }
        public void AddController(IController controller) {
            if (!_controllers.Contains(controller)) {
                _controllers.Add(controller);
            }
        }

        public void RemoveController(IController controller) {
            if (_controllers.Contains(controller)) {
                _controllers.Remove(controller);
            }
        }

    }
}

