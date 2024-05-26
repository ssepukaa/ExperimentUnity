namespace Assets.Game.Scripts.GameController.GameStates {
    public class LoadingGameState : BaseGameState {

        public LoadingGameState(Game game) : base(game) {
        }
        // Переопределение методов, если необходимо
        public override void Enter() {
            base.Enter();
            // Дополнительная логика для состояния Loading
        }

        public override void Execute() {
            base.Execute();
            // Дополнительная логика для состояния Loading

        }

        public override void Exit() {
            base.Exit();
            // Дополнительная логика для состояния Loading
        }
    }
}