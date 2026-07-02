using System;
using System.Collections.Generic;
using Application.Player;
using Application.States;
using sozooo.GameStateMachine.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Application.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 15f;
        [SerializeField] private float _detectionAngle = 60f;
        [SerializeField] private float _patrolWaitTime = 2f;
        [SerializeField] private float _lostTargetTime = 5f;
        [SerializeField] private float _patrolSpeed = 2f;
        [SerializeField] private float _chaseSpeed = 5f;
        [SerializeField] private float _attackDistance = 1.5f;

        private EnemyStateMachine _stateMachine;
        private IGameStateMachine _gameStateMachine;
        private NavMeshAgent _navAgent;
        private readonly Dictionary<Type, IEnemyState> _states = new();

        [Inject] public PlayerBehaviour Player { get; private set; }
        [Inject(Id = "PatrolPoints")] public Vector3[] PatrolPoints { get; private set; }

        public NavMeshAgent NavAgent => _navAgent;
        public Vector3 SearchPosition { get; set; }

        public float DetectionRadius => _detectionRadius;
        public float DetectionAngle => _detectionAngle;
        public float PatrolWaitTime => _patrolWaitTime;
        public float LostTargetTime => _lostTargetTime;
        public float PatrolSpeed => _patrolSpeed;
        public float ChaseSpeed => _chaseSpeed;
        public float AttackDistance => _attackDistance;

        public class Factory : PlaceholderFactory<EnemyBehaviour> { }

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
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
