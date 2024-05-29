using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Data.Constants;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.PlayerC {
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public class PlayerModel : BaseModel {
        public PlayerModel() {
            PrefabReference = ResourceConstants.PlayerPrefab;
            Speed = 5f;
        }

        [JsonProperty("health")]
        [SerializeField] private float health = 100f;
        [JsonProperty("speed")]
        [SerializeField] private float speed;

        public float Health {
            get => health;
            set => health = value;
        }

        public float Speed {
            get => speed;
            set => speed = value;
        }
    }
}