using UnityEngine;

namespace Assets.Game.Scripts.Cores {
    public class Game : MonoBehaviour {
        public static Game Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

        }

    }
}

