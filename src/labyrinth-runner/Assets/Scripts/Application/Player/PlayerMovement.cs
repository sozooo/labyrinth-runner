using Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Application.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IPlayerInputHandler _inputHandler;
        private Transform _transform;
        
        [Inject]
        private void Construct(IPlayerInputHandler inputHandler) => 
            _inputHandler = inputHandler;
        
        private void Start() => 
            _transform = transform;

        private void Update()
        {
            Vector3 moveDirection = 
                transform.right * _inputHandler.MoveDirection.x + transform.forward * _inputHandler.MoveDirection.y;
            
            _transform.Translate(moveDirection * (Time.deltaTime * (_inputHandler.IsRunning ? 3 : 1)), Space.World);
        }
    }
}