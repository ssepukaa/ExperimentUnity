using Assets.Game.Scripts.Bases.BaseModels;
using UnityEngine;

namespace Assets.Game.Scripts.Bases.BaseViews {
    [System.Serializable]
    public abstract class BaseView : MonoBehaviour, IView {
        [SerializeField] private string _id;

        public string Id {
            get => _id;
            set => _id = value;
        }

        public virtual void Initialize(IModel model) {
            Id = model.Id;
            transform.position = model.Position;
            transform.rotation = model.Rotation;
            transform.localScale = model.Scale;
        }
    }
}