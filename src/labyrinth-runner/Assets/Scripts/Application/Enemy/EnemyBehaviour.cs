using System;
using System.Collections.Generic;
using Application.Messages;
using Application.Player;
using Configs;
using MessagePipe;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Application.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyConfig _config;
        private EnemyStateMachine _stateMachine;
        private IPublisher<PlayerDiedMessage> _publisher;
        private NavMeshAgent _navAgent;
        private readonly Dictionary<Type, IEnemyState> _states = new();

        [Inject] public IPlayer Player { get; private set; }
        [Inject(Id = "PatrolPoints")] public Vector3[] PatrolPoints { get; private set; }

        public NavMeshAgent NavAgent => _navAgent;
        public Vector3 SearchPosition { get; set; }

        public float DetectionRadius => _config.DetectionRadius;
        public float DetectionAngle => _config.DetectionAngle;
        public float PatrolWaitTime => _config.PatrolWaitTime;
        public float LostTargetTime => _config.LostTargetTime;
        public float PatrolSpeed => _config.PatrolSpeed;
        public float ChaseSpeed => _config.ChaseSpeed;
        public float AttackDistance => _config.AttackDistance;

        public class Factory : PlaceholderFactory<EnemyBehaviour> { }

        [Inject]
        private void Construct(IPublisher<PlayerDiedMessage> publisher, EnemyConfig config)
        {
            _publisher = publisher;
            _config = config;
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
            if (!_states.TryGetValue(typeof(T), out var state))
            {
                state = (T)Activator.CreateInstance(typeof(T), new object[] { this });
                _states[typeof(T)] = state;
            }
            
            _stateMachine.Enter(state);
        }

        public void LoseGame() => 
            _publisher.Publish(default(PlayerDiedMessage));

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayer>(out _))
                LoseGame();
        }
    }
}
