using UnityEngine;

namespace Application.Enemy
{
    public class EnemyChaseState : EnemyState
    {
        private Vector3 _lastKnownPosition;
        private float _lostTimer;

        public EnemyChaseState(EnemyBehaviour enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            EnemyBehaviour.NavAgent.speed = EnemyBehaviour.ChaseSpeed;
            _lastKnownPosition = EnemyBehaviour.Player.transform.position;
            _lostTimer = 0;
        }

        public override void Update()
        {
            EnemyBehaviour.NavAgent.SetDestination(EnemyBehaviour.Player.transform.position);

            float distanceToPlayer = Vector3.Distance(
                EnemyBehaviour.transform.position, EnemyBehaviour.Player.transform.position);

            if (distanceToPlayer <= EnemyBehaviour.AttackDistance)
            {
                EnemyBehaviour.LoseGame();
                return;
            }

            if (CheckPlayerDetection())
            {
                _lostTimer = 0;
                _lastKnownPosition = EnemyBehaviour.Player.transform.position;
            }
            else
            {
                _lostTimer += Time.deltaTime;
                if (_lostTimer >= EnemyBehaviour.LostTargetTime)
                {
                    EnemyBehaviour.SearchPosition = _lastKnownPosition;
                    ChangeState<EnemySearchState>();
                }
            }
        }

        public override void Exit()
        {
            _lostTimer = 0;
        }
    }
}
