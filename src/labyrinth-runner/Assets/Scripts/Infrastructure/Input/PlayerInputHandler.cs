using UnityEngine;

namespace Infrastructure.Input
{
    public class PlayerInputHandler : IPlayerInputHandler
    {
        private readonly PlayerInput _input = new();
        
        public Vector2 LookDelta => _input.Player.Look.ReadValue<Vector2>();
        public Vector2 MoveDirection => _input.Player.Move.ReadValue<Vector2>();

        public void EnableControls(bool value)
        {
            if (value)
                _input.Enable();
            else
                _input.Disable();
        }
    }
}