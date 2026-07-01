using UnityEngine;

namespace Infrastructure.Input
{
    public interface IPlayerLookInputHandler
    {
        Vector2 LookDelta { get; }
        void EnableControls(bool value);
    }
}