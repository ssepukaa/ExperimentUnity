using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameServices {
    public class Vector3Converter : JsonConverter {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            Vector3 vector = (Vector3)value;
            JObject jo = new JObject
            {
                { "x", vector.x },
                { "y", vector.y },
                { "z", vector.z }
            };
            jo.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            JObject jo = JObject.Load(reader);
            float x = (float)jo["x"];
            float y = (float)jo["y"];
            float z = (float)jo["z"];
            return new Vector3(x, y, z);
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Vector3);
        }
    }

    public class QuaternionConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            Quaternion quaternion = (Quaternion)value;
            JObject jo = new JObject
            {
                { "x", quaternion.x },
                { "y", quaternion.y },
                { "z", quaternion.z },
                { "w", quaternion.w }
            };
            jo.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            JObject jo = JObject.Load(reader);
            float x = (float)jo["x"];
            float y = (float)jo["y"];
            float z = (float)jo["z"];
            float w = (float)jo["w"];
            return new Quaternion(x, y, z, w);
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Quaternion);
        }
    }
}
