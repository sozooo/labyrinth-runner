using Application.UI;
using sozooo.GameStateMachine.StateInfrastructure;

namespace Application.States
{
    public class StartState : SimpleState
    {
        private readonly UISwitcher _switcher;

        public StartState(UISwitcher switcher) =>
            _switcher = switcher;

        public override void Enter() =>
            _switcher.ShowPanel<StartPanel>();

        protected override void Exit() =>
            _switcher.HideAll();
    }
}
