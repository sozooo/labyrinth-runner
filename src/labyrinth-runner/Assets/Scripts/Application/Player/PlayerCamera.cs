using Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Application.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private float _minVerticalAngle = -80f;
        [SerializeField] private float _maxVerticalAngle = 80f;

        private IPlayerInputHandler _inputHandler;
        private Transform _transform;
        private float _xRotation;

        [Inject]
        private void Construct(IPlayerInputHandler inputHandler) =>
            _inputHandler = inputHandler;

        private void LateUpdate()
        {
            Vector2 look = _inputHandler.LookDelta * (_mouseSensitivity * Time.deltaTime);

            _xRotation = Mathf.Clamp(_xRotation - look.y, _minVerticalAngle, _maxVerticalAngle);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerTransform.Rotate(Vector3.up * look.x);
        }
    }
}
