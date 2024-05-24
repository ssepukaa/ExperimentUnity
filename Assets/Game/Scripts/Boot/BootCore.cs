using UnityEngine;

namespace Assets.Game.Scripts.Boot {
    public class BootCore : MonoBehaviour {
        public static BootCore Instance;

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
