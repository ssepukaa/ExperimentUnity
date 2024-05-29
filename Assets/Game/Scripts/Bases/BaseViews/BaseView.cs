using Assets.Game.Scripts.Bases.BaseModels;
using UnityEngine;

namespace Assets.Game.Scripts.Bases.BaseViews {
    [System.Serializable]
    public abstract class BaseView : MonoBehaviour {
        [SerializeField] public string _id;
        //[SerializeField] private BaseModel _model;
        [SerializeReference] public BaseModel _model;

        public string Id {
            get => _id;
            set => _id = value;
        }

        public BaseModel Model
        {
            get => _model;
            set => _model = value;
        }

        public virtual void Initialize(BaseModel model) {
            Id = model.Id;
            transform.position = model.Position;
            transform.rotation = model.Rotation;
            transform.localScale = model.Scale;
        }
    }
}