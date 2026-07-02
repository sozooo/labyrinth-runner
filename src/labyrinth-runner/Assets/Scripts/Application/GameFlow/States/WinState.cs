using Application.GameFlow.UI;
using Application.GameFlow.UI.Panels;
using sozooo.GameStateMachine.StateInfrastructure;

namespace Application.GameFlow.States
{
    public class WinState : SimpleState
    {
        private readonly UISwitcher _switcher;

        public WinState(UISwitcher switcher) =>
            _switcher = switcher;

        public override void Enter() =>
            _switcher.ShowPanel<WinPanel>();

        protected override void Exit() =>
            _switcher.HideAll();
    }
}

