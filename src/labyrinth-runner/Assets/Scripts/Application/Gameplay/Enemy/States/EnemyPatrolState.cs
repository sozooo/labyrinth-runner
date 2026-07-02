using Application.Gameplay.Enemy;
using Application.Core.Configs;
using UnityEngine;

namespace Application.Gameplay.Enemy.States
{
    public class EnemyPatrolState : EnemyState
    {
        private int _currentPointIndex;
        private float _waitTimer;

        public EnemyPatrolState(EnemyBehaviour enemy, EnemyConfig config) : base(enemy, config)
        {
        }

        public override void Enter()
        {
            EnemyBehaviour.NavAgent.speed = Config.PatrolSpeed;
            _currentPointIndex = FindNearestPointIndex();
            _waitTimer = 0;
            MoveToNextPoint();
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
                {
                    _currentPointIndex = (_currentPointIndex + 1) % EnemyBehaviour.PatrolPoints.Length;
                    MoveToNextPoint();
                    _waitTimer = 0;
                }
            }
        }

        public override void Exit()
        {
            _waitTimer = 0;
        }

        private void MoveToNextPoint()
        {
            EnemyBehaviour.NavAgent.SetDestination(EnemyBehaviour.PatrolPoints[_currentPointIndex]);
        }

        private int FindNearestPointIndex()
        {
            int nearestIndex = 0;
            float nearestDistance = float.MaxValue;
            Vector3 position = EnemyBehaviour.transform.position;

            for (int i = 0; i < EnemyBehaviour.PatrolPoints.Length; i++)
            {
                float distance = Vector3.Distance(position, EnemyBehaviour.PatrolPoints[i]);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestIndex = i;
                }
            }

            return nearestIndex;
        }
    }
}

