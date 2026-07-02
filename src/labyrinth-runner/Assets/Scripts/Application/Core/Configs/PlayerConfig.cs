using UnityEngine;

namespace Application.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MouseSensitivity { get; private set; } = 100f;
        [field: SerializeField] public Vector2 VerticalClampRange { get; private set; } = new(-80f, 80f);
        [field: SerializeField] public float WalkSpeed { get; private set; } = 1f;
        [field: SerializeField] public float SprintMultiplier { get; private set; } = 3f;
    }
}

