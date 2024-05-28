using Assets.Game.Scripts.Bases.BaseModels;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.PlayerC {
    [JsonObject(MemberSerialization.OptIn)]
    [System.Serializable]
    public class PlayerModel : BaseModel {
        public PlayerModel() {
            PrefabReference = "Assets/Game/Prefabs/Units/PlayerPrefab.prefab";
        }

        [JsonProperty("health")]
        [SerializeField] private float health;

        public float Health {
            get => health;
            set => health = value;
        }
    }
}