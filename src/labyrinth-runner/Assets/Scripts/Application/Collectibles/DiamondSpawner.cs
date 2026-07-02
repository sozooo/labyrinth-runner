using Configs;
using System.Linq;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace Application.Collectibles
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

            _spawnPoints.OrderBy(_ => Random.value)
                .Take(count)
                .ToList()
                .ForEach(point => _diamondFactory.Create().transform.position = point);

            _publisher.Publish(new DiamondsSpawnedMessage(count));
        }
    }
}
