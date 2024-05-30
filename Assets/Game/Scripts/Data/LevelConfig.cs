using UnityEngine;

namespace Assets.Game.Scripts.Data
{
    [System.Serializable]
    public class LevelConfig {
        public Vector3 worldSize;
        public int minMicrobeCount;
        public int maxMicrobeCount;
    }
}