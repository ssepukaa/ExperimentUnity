namespace Assets.Game.Scripts.GameController.GameStates
{
    public class PlayGameState : BaseGameState {
        public PlayGameState(Game game) : base(game) { }

        public override void Enter() {
            base.Enter();
            // ������ ��� ������ ����
        }

        public override void Execute() {
            base.Execute();
            // ������ ��� ���������� �������� �����
        }

        public override void Exit() {
            base.Exit();
            // ������ ��� ������ �� ��������� ����
        }
    }
}