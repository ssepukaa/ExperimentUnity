using Assets.Game.Scripts.Bases.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.GameController.GameStates
{
    public class NewGameState : IGameState {
        public void Enter() {
            Debug.Log("Enter NewGameState");

        }

        public void Exit() {
            Debug.Log("Exit NewGameState");
        }

        public void Execute() {
            Debug.Log("Execute NewGameState");
        }
    }
}