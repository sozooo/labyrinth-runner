using UnityEngine;

namespace Infrastructure.Input
{
    public interface IPlayerInputHandler
    {
        Vector2 LookDelta { get; }
        Vector2 MoveDirection { get; }
        void EnableControls(bool value);
    }
}