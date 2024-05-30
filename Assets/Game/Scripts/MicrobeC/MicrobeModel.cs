using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.MicrobeC {
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public class MicrobeModel : BaseModel {
        public MicrobeModel() {
            Speed = 3;
            Type = MicrobeType.Chloroplast;
        }
        [JsonProperty("health")]
        [SerializeField] private float health;
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