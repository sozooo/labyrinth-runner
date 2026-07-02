using UnityEngine;
using Zenject;

namespace Application.Gameplay.Collectibles
{
    public class Diamond : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 60f;
        [SerializeField] private float _floatFrequency = 1.5f;
        [SerializeField] private float _floatAmplitude = 0.3f;

        private float _startY;

        public class Factory : PlaceholderFactory<Diamond> { }

        private void Start()
        {
            _startY = transform.position.y;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

            Vector3 pos = transform.position;
            pos.y = _startY + Mathf.Sin(Time.time * _floatFrequency) * _floatAmplitude;
            transform.position = pos;
        }
    }
}

