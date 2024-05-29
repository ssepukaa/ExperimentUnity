using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameStates
{
    public class MenuGameState: BaseGameState {
        public override void Enter()
        {
            Debug.Log("Enter MenuGameState");
        }

        public override void Exit()
        {
            Debug.Log("Exit MenuGameState");
        }

        public override void Execute()
        {
            Debug.Log("Execute MenuGameState");
        }

        public MenuGameState(GameController gameController) : base(gameController)
        {
        }
    }
}