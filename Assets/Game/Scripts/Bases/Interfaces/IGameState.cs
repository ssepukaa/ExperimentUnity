namespace Assets.Game.Scripts.Bases.Interfaces {
    public interface IGameState {
        void Enter();
        void Exit();
        void Execute();
    }
}
