﻿using System.Collections.Generic;
using Assets.Game.Scripts.Bases.BaseModels;
using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.MicrobeC;
using Assets.Game.Scripts.PlayerC;
using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameServices {
    public class LevelGenerationService : IGameService {
        private GameConfig _gameConfig;
        private RandomService _randomService;
        private float _minDistanceBetweenMicrobes;

        public LevelGenerationService(GameConfig gameConfig, RandomService randomService, float minDistanceBetweenMicrobes) {
            _gameConfig = gameConfig;
            _randomService = randomService;
            _minDistanceBetweenMicrobes = minDistanceBetweenMicrobes;
        }

        public List<IModel> GenerateInitialModels() {
            var models = new List<IModel>();

            // Генерация игрока
            var playerModel = new PlayerModel {
                Id = System.Guid.NewGuid().ToString(),
                Position = new Vector3(0, 0, 0), // Задайте начальную позицию игрока
                Rotation = Quaternion.identity,
                Scale = Vector3.one,
                Health = 100 // Начальное значение здоровья игрока
            };
            models.Add(playerModel);

            // Генерация микробов
            var levelConfig = _gameConfig.levels[0]; // Пример использования первого уровня из конфигурации
            int microbeCount = _randomService.RandomRange(levelConfig.minMicrobeCount, levelConfig.maxMicrobeCount);

            for (int i = 0; i < microbeCount; i++) {
                Vector3 position;
                bool positionIsValid;

                do {
                    position = _randomService.RandomPosition(levelConfig.worldSize);
                    positionIsValid = true;

                    foreach (var model in models) {
                        if (Vector3.Distance(position, model.Position) < _minDistanceBetweenMicrobes) {
                            positionIsValid = false;
                            break;
                        }
                    }
                } while (!positionIsValid);

                var microbeModel = new MicrobeModel {
                    Id = System.Guid.NewGuid().ToString(),
                    Position = position,
                    Rotation = Quaternion.identity,
                    Scale = Vector3.one,
                    Health = 50 // Начальное значение здоровья микроба
                };
                models.Add(microbeModel);
            }

            return models;
        }
    }
}
