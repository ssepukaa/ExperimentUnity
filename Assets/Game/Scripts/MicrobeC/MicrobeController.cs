using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.MicrobeC {
    [System.Serializable]
    public class MicrobeController : BaseController,IUpdatable {
        [SerializeField] protected MicrobeModel _model;

        public MicrobeController(MicrobeModel model) : base(model) {
            _model = model;
        }

        public override void Run() {
            // Логика обновления игрока
        }
    }
}
