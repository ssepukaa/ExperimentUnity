using UnityEngine;

namespace Assets.Game.Scripts.Services.InputServices
{
    public class MobileInputService : IInputService {
        public Vector3 GetInputPosition() {
            if (Input.touchCount > 0) {
                return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            return Vector3.zero;
        }

        public bool IsInputActive()
        {
            return Input.touchCount > 0;
        }
    }
}