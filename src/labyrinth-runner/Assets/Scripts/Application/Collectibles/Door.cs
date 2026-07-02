using UnityEngine;

namespace Application.Collectibles
{
    public class Door : MonoBehaviour
    {
        [System.Serializable]
        private struct Shutter
        {
            public Transform transform;
            public Vector3 openRotation;
        }

        [SerializeField] private Shutter[] _shutters;
        [SerializeField] private float _duration = 1f;

        private Quaternion[] _closedRotations;

        private void Awake()
        {
            _closedRotations = new Quaternion[_shutters.Length];

            for (int i = 0; i < _shutters.Length; i++)
                _closedRotations[i] = _shutters[i].transform.localRotation;
        }

        public void Open() => StartCoroutine(OpenRoutine());

        private System.Collections.IEnumerator OpenRoutine()
        {
            float elapsed = 0f;
            Quaternion[] targets = new Quaternion[_shutters.Length];

            for (int i = 0; i < _shutters.Length; i++)
                targets[i] = _closedRotations[i] * Quaternion.Euler(_shutters[i].openRotation);

            while (elapsed < _duration)
            {
                float t = elapsed / _duration;
                for (int i = 0; i < _shutters.Length; i++)
                    _shutters[i].transform.localRotation = Quaternion.Slerp(_closedRotations[i], targets[i], t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < _shutters.Length; i++)
                _shutters[i].transform.localRotation = targets[i];
        }
    }
}
