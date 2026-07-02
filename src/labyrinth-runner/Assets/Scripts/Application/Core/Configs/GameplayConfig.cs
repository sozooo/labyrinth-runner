using UnityEngine;

namespace Application.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay Config")]
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2Int DiamondCountRange { get; private set; } = new(0, 5);
        [field: SerializeField] public Vector2Int EnemyCountRange { get; private set; } = new(0, 3);
        [field: SerializeField] public float DoorAnimationDuration { get; private set; } = 1f;
    }
}

