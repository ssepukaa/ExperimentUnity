using Assets.Game.Scripts.Bases.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameStates
{
    public abstract class BaseGameState
    {
        public GameController GameController;

        public BaseGameState(GameController gameController)
        {
            GameController = gameController;
        }

        public virtual void Enter()
        {
            Debug.Log($"Enter {GetType().Name}");
        }

        public virtual void Exit()
        {
            Debug.Log($"Exit {GetType().Name}");
        }

        public virtual void Execute()
        {
            // Debug.Log($"Execute {GetType().Name}");
        }
    }
}