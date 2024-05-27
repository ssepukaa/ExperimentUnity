using UnityEngine;

namespace Assets.Game.Scripts.Bases.BaseModels
{
    public interface IModel
    {
        string Id { get; set; }
        string PrefabReference { get; set; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Scale { get; set; }
    }
}