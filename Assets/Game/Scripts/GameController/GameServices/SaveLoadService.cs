using System.Collections.Generic;
using System.IO;
using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.BaseModels;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.GameController.GameServices {
    public class SaveLoadService : IGameService {
        private readonly string saveFilePath;

        public SaveLoadService() {
            saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        }

        public void Save(List<IController> controllers) {
            List<IModel> models = new List<IModel>();
            foreach (var controller in controllers) {
                models.Add(controller.Model);
            }

            string jsonData = JsonConvert.SerializeObject(models, Formatting.Indented, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto
            });
            File.WriteAllText(saveFilePath, jsonData);
        }

        public List<IModel> Load()
        {
            if (!File.Exists(saveFilePath))
            {
                return null;
            }
            string jsonData = File.ReadAllText(saveFilePath);
            return JsonConvert.DeserializeObject<List<IModel>>(jsonData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }
}
