using UnityEngine;

namespace Assets.Game.Scripts.Services.InputServices
{
    public class DesktopInputService : IInputService
    {
        public Vector3 GetInputPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public bool IsInputActive()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}