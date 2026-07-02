using sozooo.GameStateMachine.StateInfrastructure;
using sozooo.GameStateMachine.StateMachine;

namespace Application.States
{
    public class BootstrapState : SimpleState
    {
        private readonly IGameStateMachine _stateMachine;

        public BootstrapState(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public override void Enter() =>
            _stateMachine.Enter<StartState>();
    }
}
