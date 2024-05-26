using Assets.Game.Scripts.Bases.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.GameController.GameStates
{
    public abstract class BaseGameState : IGameState {
        public Game Game;
        public BaseGameState(Game game) {
            Game = game;
        }
        public virtual void Enter() {
            Debug.Log($"Enter {GetType().Name}");

        }

        public virtual void Exit() {
            Debug.Log($"Exit {GetType().Name}");
        }

        public virtual void Execute() {
            Debug.Log($"Execute {GetType().Name}");
        }
    }
}