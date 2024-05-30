using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Bases.Interfaces;
using Assets.Game.Scripts.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.PlayerC {
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public class PlayerModel : BaseModel, IMovable {
        public PlayerModel() {
            Speed = 5f;
            Type = MicrobeType.Amoeba;
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