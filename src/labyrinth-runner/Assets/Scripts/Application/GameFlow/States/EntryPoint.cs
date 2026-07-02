using sozooo.GameStateMachine.StateMachine;
using Zenject;

namespace Application.GameFlow.States
{
    public class EntryPoint : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;

        public EntryPoint(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void Initialize() =>
            _stateMachine.Enter<BootstrapState>();
    }
}

