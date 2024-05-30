using Assets.Game.Scripts.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.Bases.BaseModels {
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public abstract class BaseModel {
        [JsonProperty("id")]
        [SerializeField] private string id;

        [JsonProperty("type")]
        [SerializeField] private MicrobeType type;

        [JsonProperty("position")]
        [SerializeField] private Vector3 position;

        [JsonProperty("rotation")]
        [SerializeField] private Quaternion rotation;

        [JsonProperty("scale")]
        [SerializeField] private Vector3 scale;

       

        public string Id {
            get => id;
            set => id = value;
        } 
        public MicrobeType Type {
            get => type;
            set => type = value;
        }

        public Vector3 Position {
            get => position;
            set => position = value;
        }

        public Quaternion Rotation {
            get => rotation;
            set => rotation = value;
        }

        public Vector3 Scale {
            get => scale;
            set => scale = value;
        }

    }
}