using Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Application.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject] private PlayerInputHandler _inputHandler;
        private Transform _transform;
        
        // [Inject]
        // private void Construct(IPlayerInputHandler inputHandler) => 
        //     _inputHandler = inputHandler;
        
        private void Start() => 
            _transform = transform;

        private void OnEnable() => 
            _inputHandler.EnableControls(true);

        private void Update()
        {
            Vector3 moveDirection = 
                transform.right * _inputHandler.MoveDirection.x + transform.forward * _inputHandler.MoveDirection.y;
            
            _transform.Translate(transform.position + moveDirection);
        }
    }
}