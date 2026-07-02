using Application.Core.Configs;
using Application.Core.Services;
using UnityEngine;
using Zenject;

namespace Application.Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private IPlayerInputHandler _inputHandler;
        private PlayerConfig _config;
        private CharacterController _characterController;
        private float _verticalVelocity;

        [Inject]
        private void Construct(IPlayerInputHandler inputHandler, PlayerConfig config)
        {
            _inputHandler = inputHandler;
            _config = config;
        }

        private void Start() =>
            _characterController = GetComponent<CharacterController>();

        private void Update()
        {
            Vector3 moveDirection =
                transform.right * _inputHandler.MoveDirection.x + transform.forward * _inputHandler.MoveDirection.y;

            float speed = _inputHandler.IsRunning ? _config.WalkSpeed * _config.SprintMultiplier : _config.WalkSpeed;

            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
            Vector3 motion = moveDirection * (Time.deltaTime * speed) + Vector3.up * (_verticalVelocity * Time.deltaTime);
            _characterController.Move(motion);

            if (_characterController.isGrounded)
                _verticalVelocity = 0;
        }
    }
}
