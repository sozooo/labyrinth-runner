using Application.Core.Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Input
{
    public class PlayerInputHandler : IPlayerInputHandler, IInitializable, IDisposable
    {
        private readonly PlayerInput _input = new();
        
        public Vector2 LookDelta => _input.Player.Look.ReadValue<Vector2>();
        public Vector2 MoveDirection => _input.Player.Move.ReadValue<Vector2>();
        
        public bool IsRunning { get; private set; }

        public void EnableControls(bool value)
        {
            if (value)
                _input.Enable();
            else
                _input.Disable();
        }

        public void Initialize()
        {
            _input.Player.Sprint.started += EnableSprint;
            _input.Player.Sprint.canceled += DisableSprint;
        }
        
        public void Dispose()
        {
            _input.Player.Sprint.started -= EnableSprint;
            _input.Player.Sprint.canceled -= DisableSprint;
        }

        private void EnableSprint(InputAction.CallbackContext _) => 
            IsRunning = true;
        
        private void DisableSprint(InputAction.CallbackContext _) =>
            IsRunning = false;
    }
}

