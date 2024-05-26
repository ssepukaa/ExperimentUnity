using System;
using Newtonsoft.Json;

namespace Assets.Game.Scripts.Bases.BaseModels
{
    [System.Serializable]
    public abstract class BaseModel : IModel {
        [JsonProperty("id")]
        public string Id { get; set; }

        protected BaseModel() {
            Id = Guid.NewGuid().ToString();
        }
    }
}