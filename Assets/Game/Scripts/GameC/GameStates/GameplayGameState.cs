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
            // ������ ��� ������ ����
        }

        public override void Execute()
        {
            base.Execute();
            // ������ ��� ���������� �������� �����
        }

        public override void Exit()
        {
            base.Exit();
            // ������ ��� ������ �� ��������� ����
        }
    }
}