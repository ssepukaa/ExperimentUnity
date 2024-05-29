using UnityEngine;

namespace Assets.Game.Scripts.Services.InputServices {
    public interface IInputService {
        Vector3 GetInputPosition();
        bool IsInputActive();
    }
}
