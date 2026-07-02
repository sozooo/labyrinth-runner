using Configs;
using Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Application.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IPlayerInputHandler _inputHandler;
        private Transform _transform;
        private PlayerConfig _config;

        [Inject]
        private void Construct(IPlayerInputHandler inputHandler, PlayerConfig config)
        {
            _inputHandler = inputHandler;
            _config = config;
        }
        
        private void Start() => 
            _transform = transform;

        private void Update()
        {
            Vector3 moveDirection = 
                transform.right * _inputHandler.MoveDirection.x + transform.forward * _inputHandler.MoveDirection.y;
            
            float speed = _inputHandler.IsRunning ? _config.WalkSpeed * _config.SprintMultiplier : _config.WalkSpeed;
            _transform.Translate(moveDirection * (Time.deltaTime * speed), Space.World);
        }
    }
}