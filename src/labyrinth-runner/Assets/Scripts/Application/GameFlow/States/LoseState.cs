using Application.GameFlow.UI;
using Application.GameFlow.UI.Panels;
using sozooo.GameStateMachine.StateInfrastructure;

namespace Application.GameFlow.States
{
    public class LoseState : SimpleState
    {
        private readonly UISwitcher _switcher;

        public LoseState(UISwitcher switcher) =>
            _switcher = switcher;

        public override void Enter() =>
            _switcher.ShowPanel<LosePanel>();

        protected override void Exit() =>
            _switcher.HideAll();
    }
}

