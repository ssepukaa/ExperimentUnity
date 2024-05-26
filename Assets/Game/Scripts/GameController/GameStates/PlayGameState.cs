namespace Assets.Game.Scripts.GameController.GameStates
{
    public class PlayGameState : BaseGameState {
        public PlayGameState(Game game) : base(game) { }

        public override void Enter() {
            base.Enter();
            // Логика для начала игры
        }

        public override void Execute() {
            base.Execute();
            // Логика для выполнения игрового цикла
        }

        public override void Exit() {
            base.Exit();
            // Логика при выходе из состояния игры
        }
    }
}