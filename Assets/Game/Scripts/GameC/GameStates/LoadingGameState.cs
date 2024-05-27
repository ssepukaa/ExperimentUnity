using Assets.Game.Scripts.GameC.GameServices;

namespace Assets.Game.Scripts.GameC.GameStates {
    public class LoadingGameState : BaseGameState {
        private GameController _game;

        public LoadingGameState(GameController game) : base(game) {
            _game = game;
        }

        public override void Enter() {
            base.Enter();
            LoadOrGenerateModels();
        }

        private void LoadOrGenerateModels() {
            var saveLoadService = _game.GetService<SaveLoadService>();
            var models = saveLoadService.Load();

            if (models == null || models.Count == 0) {
                var levelGenerationService = _game.GetService<LevelGenerationService>();
                models = levelGenerationService.GenerateInitialModels();
            }

            _game.CreateNewListControllers(models);
            _game.ChangeGameState<GameplayGameState>();
        }

        public override void Execute() {
            base.Execute();
        }

        public override void Exit() {
            base.Exit();
        }
    }
}