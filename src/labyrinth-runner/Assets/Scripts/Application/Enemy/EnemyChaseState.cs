using Application.Player;
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
                _lastKnownPosition = EnemyBehaviour.Player.Transform.position;
            _lostTimer = 0;
        }

        public override void Update()
        {
            EnemyBehaviour.NavAgent.SetDestination(EnemyBehaviour.Player.Transform.position);

            float distanceToPlayer = Vector3.Distance(
                EnemyBehaviour.transform.position, EnemyBehaviour.Player.Transform.position);

            if (distanceToPlayer <= EnemyBehaviour.AttackDistance)
            {
                EnemyBehaviour.LoseGame();
                return;
            }

            if (CheckPlayerDetection())
            {
                _lostTimer = 0;
            _lastKnownPosition = EnemyBehaviour.Player.Transform.position;
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
