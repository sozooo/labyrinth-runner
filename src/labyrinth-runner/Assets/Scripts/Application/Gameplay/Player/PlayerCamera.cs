using Application.Core.Configs;
using Application.Core.Services;
using UnityEngine;
using Zenject;

namespace Application.Gameplay.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        private PlayerConfig _config;

        private IPlayerInputHandler _inputHandler;
        private Transform _transform;
        private float _xRotation;

        [Inject]
        private void Construct(IPlayerInputHandler inputHandler, PlayerConfig config)
        {
            _inputHandler = inputHandler;
            _config = config;
        }

        private void LateUpdate()
        {
            Vector2 look = _inputHandler.LookDelta * (_config.MouseSensitivity * Time.deltaTime);

            _xRotation = Mathf.Clamp(_xRotation - look.y, _config.VerticalClampRange.x, _config.VerticalClampRange.y);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerTransform.Rotate(Vector3.up * look.x);
        }
    }
}

