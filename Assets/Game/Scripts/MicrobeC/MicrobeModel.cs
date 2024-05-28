using Assets.Game.Scripts.Bases.BaseModels;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.MicrobeC {
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public class MicrobeModel : BaseModel {
        public MicrobeModel() {
            PrefabReference = "Assets/Game/Prefabs/Units/MicrobePrefab.prefab";
        }

        [JsonProperty("health")]
        [SerializeField] private float health;

        public float Health {
            get => health;
            set => health = value;
        }
    }
}