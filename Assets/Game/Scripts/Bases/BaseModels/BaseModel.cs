using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.Bases.BaseModels {
    [System.Serializable]
    public abstract class BaseModel : IModel {
        [JsonProperty("id")]
        [SerializeField] private string id;
        [JsonProperty("position")]
        [SerializeField] private Vector3 position;
        [JsonProperty("rotation")]
        [SerializeField] private Quaternion rotation;
        [JsonProperty("scale")]
        [SerializeField] private Vector3 scale;
        [JsonProperty("prefabReference")]
        [SerializeField] private string prefabReference;

        public string Id {
            get => id;
            set => id = value;
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

        public string PrefabReference {
            get => prefabReference;
            set => prefabReference = value;
        }
    }
}