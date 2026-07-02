using Application.Gameplay.Enemy.States;
using sozooo.GameStateMachine.StateInfrastructure;

namespace Application.Gameplay.Enemy
{
    public class EnemyStateMachine
    {
        private IEnemyState _activeState;

        public void Enter(IEnemyState state)
        {
            if (_activeState != null)
            {
                _activeState.BeginExit();
                _activeState.EndExit();
            }

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

