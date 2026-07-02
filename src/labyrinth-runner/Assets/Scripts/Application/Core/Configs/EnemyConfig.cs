using UnityEngine;

namespace Application.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float DetectionRadius { get; private set; } = 15f;
        [field: SerializeField] public float DetectionAngle { get; private set; } = 60f;
        [field: SerializeField] public float PatrolWaitTime { get; private set; } = 2f;
        [field: SerializeField] public float LostTargetTime { get; private set; } = 5f;
        [field: SerializeField] public float PatrolSpeed { get; private set; } = 2f;
        [field: SerializeField] public float ChaseSpeed { get; private set; } = 5f;
        [field: SerializeField] public float AttackDistance { get; private set; } = 1.5f;
    }
}

