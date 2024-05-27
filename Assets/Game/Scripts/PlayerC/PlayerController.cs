using Assets.Game.Scripts.Bases.BaseControllers;
using Assets.Game.Scripts.Bases.Interfaces;

namespace Assets.Game.Scripts.PlayerC {
    public class PlayerController : BaseController, IUpdatable {
        private PlayerModel _model;

        public PlayerController(PlayerModel model) : base(model) {
            _model = model;
        }

        public override void Run() {
            // Логика обновления игрока
        }
    }

}