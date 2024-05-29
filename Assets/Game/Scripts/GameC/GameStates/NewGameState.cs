using Assets.Game.Scripts.Bases.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameStates
{
    public class NewGameState : BaseGameState
    {
        public override void Enter()
        {
            Debug.Log("Enter NewGameState");
        }

        public override void Exit()
        {
            Debug.Log("Exit NewGameState");
        }

        public override void Execute()
        {
            Debug.Log("Execute NewGameState");
        }

        public NewGameState(GameController gameController) : base(gameController)
        {
        }
    }
}