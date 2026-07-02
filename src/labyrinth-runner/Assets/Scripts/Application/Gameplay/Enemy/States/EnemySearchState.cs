using Application.Gameplay.Enemy;
using Application.Core.Configs;
using UnityEngine;

namespace Application.Gameplay.Enemy.States
{
    public class EnemySearchState : EnemyState
    {
        private float _waitTimer;

        public EnemySearchState(EnemyBehaviour enemy, EnemyConfig config) : base(enemy, config)
        {
        }

        public override void Enter()
        {
            EnemyBehaviour.NavAgent.SetDestination(EnemyBehaviour.SearchPosition);
            _waitTimer = 0;
        }

        public override void Update()
        {
            if (CheckPlayerDetection())
            {
                ChangeState<EnemyChaseState>();
                return;
            }

            if (HasReachedDestination())
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= Config.PatrolWaitTime)
                    ChangeState<EnemyPatrolState>();
            }
        }

        public override void Exit()
        {
            _waitTimer = 0;
        }
    }
}

