using System.Collections.Generic;
using System.IO;
using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.BaseModels;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameServices {
    public class SaveLoadService : BaseGameService {
        private readonly string saveFilePath;

        public SaveLoadService() {
            saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        }

        public void Save(List<BaseController> controllers) {
            List<BaseModel> models = new List<BaseModel>();
            foreach (var controller in controllers) {
                models.Add(controller.Model);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter> {
                    new Vector3Converter(),
                    new QuaternionConverter()
                }
            };

            string jsonData = JsonConvert.SerializeObject(models, settings);
            File.WriteAllText(saveFilePath, jsonData);
        }

        public List<BaseModel> Load() {
            if (!File.Exists(saveFilePath)) {
                return null;
            }

            string jsonData = File.ReadAllText(saveFilePath);
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter> {
                    new Vector3Converter(),
                    new QuaternionConverter()
                }
            };

            return JsonConvert.DeserializeObject<List<BaseModel>>(jsonData, settings);
        }
    }
}