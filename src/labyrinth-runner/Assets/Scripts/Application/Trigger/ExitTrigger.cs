using Application.Player;
using sozooo.GameStateMachine.StateMachine;
using UnityEngine;
using Zenject;

namespace Application.Trigger
{
    public class ExitTrigger : MonoBehaviour
    {
        private IGameStateMachine _stateMachine;

        [Inject]
        private void Construct(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerBehaviour>(out _))
                _stateMachine.Enter<States.WinState>();
        }
    }
}
