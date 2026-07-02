using Application.Core.Messages;
using Application.Gameplay.Player;
using Application.Gameplay.Enemy.States;
using Application.Core.Configs;
using MessagePipe;
using sozooo.GameStateMachine.Factory;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Application.Gameplay.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyConfig _config;
        private EnemyStateMachine _stateMachine;
        private IPublisher<PlayerDiedMessage> _publisher;
        private IStateFactory _stateFactory;
        private NavMeshAgent _navAgent;

        [Inject] public IPlayer Player { get; private set; }
        [Inject(Id = "PatrolPoints")] public Vector3[] PatrolPoints { get; private set; }

        public NavMeshAgent NavAgent => _navAgent;
        public Vector3 SearchPosition { get; set; }

        public class Factory : PlaceholderFactory<EnemyBehaviour> { }

        [Inject]
        private void Construct(IPublisher<PlayerDiedMessage> publisher, EnemyConfig config, IStateFactory stateFactory)
        {
            _publisher = publisher;
            _config = config;
            _stateFactory = stateFactory;
        }

        private void Start()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new EnemyStateMachine();

            ChangeState<EnemyPatrolState>();
        }

        private void Update() =>
            _stateMachine.Tick();

        public void ChangeState<T>() where T : class, IEnemyState
        {
            var state = _stateFactory.GetState<T>(new object[] { this, _config });
            _stateMachine.Enter(state);
        }

        public void LoseGame() =>
            _publisher.Publish(default);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out _))
                LoseGame();
        }
    }
}

