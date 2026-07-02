using sozooo.GameStateMachine.StateInfrastructure;

namespace Application.Enemy
{
    public class EnemyStateMachine
    {
        private IEnemyState _activeState;

        public void Enter(IEnemyState state)
        {
            _activeState?.Exit();
            _activeState = state;
            _activeState.Enter();
        }

        public void Tick()
        {
            if (_activeState is IUpdateable updateable)
                updateable.Update();
        }
    }
}
