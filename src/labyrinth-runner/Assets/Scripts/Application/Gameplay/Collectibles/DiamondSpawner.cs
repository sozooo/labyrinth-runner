using Application.Core.Configs;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace Application.Gameplay.Collectibles
{
    public class DiamondSpawner
    {
        private readonly Diamond.Factory _diamondFactory;
        private readonly Vector3[] _spawnPoints;
        private readonly IBufferedPublisher<DiamondsSpawnedMessage> _publisher;
        private readonly GameplayConfig _config;

        public DiamondSpawner(
            Vector3[] spawnPoints,
            Diamond.Factory diamondFactory,
            IBufferedPublisher<DiamondsSpawnedMessage> publisher,
            GameplayConfig config)
        {
            _spawnPoints = spawnPoints;
            _diamondFactory = diamondFactory;
            _publisher = publisher;
            _config = config;
        }

        public void Spawn()
        {
            int count = Random.Range(_config.DiamondCountRange.x,
                Mathf.Min(_config.DiamondCountRange.y, _spawnPoints.Length));

            var indices = new int[_spawnPoints.Length];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = i;

            for (int i = indices.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            for (int i = 0; i < count; i++)
                _diamondFactory.Create().transform.position = _spawnPoints[indices[i]];

            _publisher.Publish(new DiamondsSpawnedMessage(count));
        }
    }
}

