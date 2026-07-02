using System.Linq;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace Application.Collectibles
{
    public class DiamondSpawner : IInitializable
    {
        private readonly Diamond.Factory _diamondFactory;
        private readonly Vector3[] _spawnPoints;
        private readonly IBufferedPublisher<DiamondsSpawnedMessage> _publisher;

        public DiamondSpawner(
            Vector3[] spawnPoints,
            Diamond.Factory diamondFactory,
            IBufferedPublisher<DiamondsSpawnedMessage> publisher)
        {
            _spawnPoints = spawnPoints;
            _diamondFactory = diamondFactory;
            _publisher = publisher;
        }

        public void Initialize()
        {
            int count = Random.Range(0, _spawnPoints.Length / 2);

            _spawnPoints.OrderBy(_ => Random.value)
                .Take(count)
                .ToList()
                .ForEach(point => _diamondFactory.Create().transform.position = point);

            _publisher.Publish(new DiamondsSpawnedMessage(count));
        }
    }
}
