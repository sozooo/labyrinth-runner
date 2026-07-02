using System;
using System.Collections.Generic;
using Application.Player;
using Application.States;
using Configs;
using sozooo.GameStateMachine.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Application.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyConfig _config;
        private EnemyStateMachine _stateMachine;
        private IGameStateMachine _gameStateMachine;
        private NavMeshAgent _navAgent;
        private readonly Dictionary<Type, IEnemyState> _states = new();

        [Inject] public PlayerBehaviour Player { get; private set; }
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
        private void Construct(IGameStateMachine gameStateMachine, EnemyConfig config)
        {
            _gameStateMachine = gameStateMachine;
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
            _gameStateMachine.Enter<LoseState>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerBehaviour>(out _))
                LoseGame();
        }
    }
}
