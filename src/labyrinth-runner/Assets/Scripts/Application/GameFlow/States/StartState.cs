using Application.GameFlow.UI;
using Application.GameFlow.UI.Panels;
using sozooo.GameStateMachine.StateInfrastructure;

namespace Application.GameFlow.States
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

