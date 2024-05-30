using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Data {
    [Serializable]
    public enum MicrobeType {
        Chloroplast,
        Amoeba,
        GreenEuglena,
        Infusoria,
        Chloromonad,
        Chlorella,
        Bacteria,
    }

    [Serializable]
    public class GameConfig {
        // Prefab References
        [SerializeField] public  const string PlayerPrefab = "Assets/Game/Prefabs/Units/PlayerPrefab.prefab";
        [SerializeField] public const string MicrobePrefab = "Assets/Game/Prefabs/Units/MicrobePrefab.prefab";
        // [SerializeField] public const string AnotherMicrobePrefab = "Assets/Game/Prefabs/Units/AnotherMicrobePrefab.prefab";
        // [SerializeField] public const string СhloroplastPrefab = "Assets/Game/Prefabs/Units/СhloroplastPrefab.prefab";

        [SerializeField] public readonly List<UnitConfig> Units = new List<UnitConfig> {
            new UnitConfig()
            {
                // Пустой элемент 1
                TypeName = MicrobeType.Amoeba,
                PrefabName = PlayerPrefab,

            },
            new UnitConfig()
            {
                // Пустой элемент 2
                TypeName = MicrobeType.Chloroplast,
                PrefabName = MicrobePrefab,

            },

        };

        [SerializeField] public readonly List<LevelConfig> Levels = new List<LevelConfig>{
            new LevelConfig
            {
                worldSize = new Vector3(20f,20f,0),
                minMicrobeCount = 3,
                maxMicrobeCount = 6
            },

        };

        // Animation References
        public readonly Dictionary<MicrobeType, Dictionary<string, string>> AnimationReferences =
            new Dictionary<MicrobeType, Dictionary<string, string>>
            {
                {
                    MicrobeType.Amoeba, new Dictionary<string, string>
                    {
                        { "Idle", "Assets/Game/Animations/amoeba_idle.anim" },
                        { "Move", "Assets/Game/Animations/amoeba_move.anim" },
                        { "Attack", "Assets/Game/Animations/amoeba_attack.anim" }
                    }
                },
                {
                    MicrobeType.Chloroplast, new Dictionary<string, string>
                    {
                        { "Idle", "Assets/Game/Animations/amoeba_idle.anim" },
                        { "Move", "Assets/Game/Animations/amoeba_move.anim" },
                        { "Attack", "Assets/Game/Animations/amoeba_attack.anim" }
                    }
                }
            };

       

    }
}