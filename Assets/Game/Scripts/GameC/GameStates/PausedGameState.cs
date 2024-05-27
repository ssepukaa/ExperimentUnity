using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameStates
{
    public class PausedGameState : BaseGameState
    {
        public PausedGameState(GameController gameController) : base(gameController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Time.timeScale = 0;
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();

            Time.timeScale = 1;
        }
    }
}