using UnityEngine;

namespace Application.Core.Services
{
    public interface IPlayerInputHandler
    {
        Vector2 LookDelta { get; }
        Vector2 MoveDirection { get; }
        bool IsRunning { get; }
        void EnableControls(bool value);
    }
}
