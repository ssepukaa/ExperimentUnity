using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.Interfaces;

namespace Assets.Game.Scripts.MicrobeC {
    public class MicrobeController : BaseController,IUpdatable {
        private MicrobeModel _model;

        public MicrobeController(MicrobeModel model) : base(model) {
            _model = model;
        }

        public override void Run() {
            // Логика обновления игрока
        }
    }
}
