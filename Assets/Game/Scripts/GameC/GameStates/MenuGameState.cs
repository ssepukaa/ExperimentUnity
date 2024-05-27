using Assets.Game.Scripts.Bases.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameStates
{
    public class MenuGameState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Enter MenuGameState");
        }

        public void Exit()
        {
            Debug.Log("Exit MenuGameState");
        }

        public void Execute()
        {
            Debug.Log("Execute MenuGameState");
        }
    }
}