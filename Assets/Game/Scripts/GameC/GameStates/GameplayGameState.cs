namespace Assets.Game.Scripts.GameC.GameStates
{
    public class GameplayGameState : BaseGameState
    {
        public GameplayGameState(GameController gameController) : base(gameController)
        {
        }

        public override void Enter()
        {
            base.Enter();
            // Логика для начала игры
        }

        public override void Execute()
        {
            base.Execute();
            // Логика для выполнения игрового цикла
        }

        public override void Exit()
        {
            base.Exit();
            // Логика при выходе из состояния игры
        }
    }
}