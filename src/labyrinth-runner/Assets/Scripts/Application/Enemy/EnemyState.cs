using Application.Player;
using sozooo.GameStateMachine.StateInfrastructure;
using UnityEngine;

namespace Application.Enemy
{
    public abstract class EnemyState : IEnemyState, IUpdateable
    {
        protected readonly EnemyBehaviour EnemyBehaviour;

        protected EnemyState(EnemyBehaviour enemy)
        {
            EnemyBehaviour = enemy;
        }

        public abstract void Enter();
        public abstract void Update();
        public virtual void Exit() { }

        protected void ChangeState<T>() where T : class, IEnemyState
        {
            EnemyBehaviour.ChangeState<T>();
        }

        protected bool CheckPlayerDetection()
        {
            Vector3 directionToPlayer = EnemyBehaviour.Player.Transform.position - EnemyBehaviour.transform.position;
            float distance = directionToPlayer.magnitude;

            if (distance > EnemyBehaviour.DetectionRadius)
                return false;

            float dot = Vector3.Dot(EnemyBehaviour.transform.forward, directionToPlayer.normalized);
            float cosAngle = Mathf.Cos(EnemyBehaviour.DetectionAngle * 0.5f * Mathf.Deg2Rad);
            if (dot < cosAngle)
                return false;

            if (Physics.Raycast(EnemyBehaviour.transform.position, directionToPlayer.normalized, out RaycastHit hit, distance))
                return hit.transform == EnemyBehaviour.Player.Transform;

            return false;
        }

        protected bool HasReachedDestination()
        {
            return !EnemyBehaviour.NavAgent.pathPending
                && EnemyBehaviour.NavAgent.remainingDistance <= EnemyBehaviour.NavAgent.stoppingDistance;
        }
    }
}
