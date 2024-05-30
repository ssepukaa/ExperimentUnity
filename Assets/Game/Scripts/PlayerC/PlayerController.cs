using System.Collections;
using System.Threading.Tasks;
using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.Interfaces;
using Assets.Game.Scripts.GameC;
using Assets.Game.Scripts.Services.InputServices;
using Assets.Game.Scripts.Services.MovementServices;
using UnityEngine;

namespace Assets.Game.Scripts.PlayerC {
    [System.Serializable]
    public class PlayerController : BaseController, IUpdatable {
        [SerializeField] protected PlayerModel _model;
        private IInputService _inputService;
        private MovementService _movementService;

        public PlayerController(PlayerModel model) : base(model) {
            _model = model;
        }

        protected override async void InitServices() {
            base.InitServices();
            _inputService = Application.isMobilePlatform ? new MobileInputService() : new DesktopInputService();
            var animationService = GameController.Instance.GetService<AnimationPoolService>();

            // Получаем словарь с анимациями для данного типа микроба
            var animationAddresses = GameController.Instance.Config.AnimationReferences[_model.Type];

            // Проверяем, чтобы animationAddresses не был null
            if (animationAddresses != null) {
                Debug.Log($"Initializing animations for {_model.Type}");
                await animationService.Initialize(animationAddresses);
            } else {
                Debug.LogWarning($"Animation addresses for {_model.Type} are null.");
            }

            // Передаем AnimationPoolService в MovementService
            _movementService = new MovementService(_view.transform, _model, _view.GetComponent<Animator>(), animationService);
            await _movementService.Initialize();
            _isInitComplete = true;
        }

        public override void Run() {
            if (!_isInitComplete) {
                Debug.LogWarning("InitServices not completed.");
                return;
            }

            base.Run();
            if (_inputService.IsInputActive()) {
                Vector3 inputPosition = _inputService.GetInputPosition();
                inputPosition.z = 0; // Установка Z-координаты в 0 для 2D
                _movementService.SetTarget(inputPosition);
            }

            _movementService.UpdateState();

            // Пример атаки (замените условие на вашу логику)
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (_movementService.TryPlayAttackAnimation()) {
                    _view.StartCoroutine(StopAttackAfterDelay(1.0f)); // Замените 1.0f на длительность анимации атаки
                }
            }
        }

        private IEnumerator StopAttackAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            _movementService.StopAttackAnimation();
        }
    }
}
