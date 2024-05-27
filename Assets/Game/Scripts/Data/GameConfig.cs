using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Data {
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
    public class GameConfig : ScriptableObject {
        [System.Serializable]
        public class LevelConfig {
            public Vector3 worldSize;
            public int minMicrobeCount;
            public int maxMicrobeCount;
        }

        [SerializeField] public List<LevelConfig> levels;
    }
}