using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameServices {
    public class RandomService : BaseGameService {
        private System.Random _random = new System.Random();

        public int RandomRange(int min, int max) {
            return _random.Next(min, max);
        }

        public Vector3 RandomPosition(Vector3 worldSize) {
            float x = (float)_random.NextDouble() * worldSize.x - worldSize.x / 2;
            float y = (float)_random.NextDouble() * worldSize.y - worldSize.y / 2;
            float z = (float)_random.NextDouble() * worldSize.z - worldSize.z / 2;
            return new Vector3(x, y, z);
        }
    }
}