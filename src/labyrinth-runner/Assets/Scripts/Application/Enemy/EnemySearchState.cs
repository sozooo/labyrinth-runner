using UnityEngine;

namespace Application.Enemy
{
    public class EnemySearchState : EnemyState
    {
        private float _waitTimer;

        public EnemySearchState(EnemyBehaviour enemy) : base(enemy)
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
                if (_waitTimer >= EnemyBehaviour.PatrolWaitTime)
                    ChangeState<EnemyPatrolState>();
            }
        }

        public override void Exit()
        {
            _waitTimer = 0;
        }
    }
}
