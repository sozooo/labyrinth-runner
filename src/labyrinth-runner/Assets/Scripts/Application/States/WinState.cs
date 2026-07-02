using Application.UI;
using sozooo.GameStateMachine.StateInfrastructure;

namespace Application.States
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
